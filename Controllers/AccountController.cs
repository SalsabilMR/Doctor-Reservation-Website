using DoctorReservation.ViewModels;
using DoctorReservation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DoctorReservation.Services;


namespace DoctorReservation.Controllers
{
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        PatientServices _patientServices;
        DoctorServices _doctorServices;


        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager , PatientServices patientServices , DoctorServices doctorServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _patientServices = patientServices;
            _doctorServices = doctorServices ; 
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel newUser)
        {
            Console.WriteLine("🔹 Register POST triggered");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("❌ ModelState is NOT valid");
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"🔸 Error in '{state.Key}': {error.ErrorMessage}");
                    }
                }
                return View(newUser);
            }

            ApplicationUser user = new ApplicationUser
            {
                UserName = newUser.UserName,
                Email = newUser.EmailAddress,
                // FirstName = newUser.FirstName,
                // LastName = newUser.LastName
            };

            IdentityResult result = await _userManager.CreateAsync(user, newUser.Password);

            if (result.Succeeded)
            {
                Console.WriteLine("✅ User Created");

                var selectedRole = Request.Form["Role"].ToString();
                var redirectTarget = Request.Form["Redirect"].ToString();

                if (selectedRole != "Doctor" && selectedRole != "User")
                    selectedRole = "User"; // fallback

                await _userManager.AddToRoleAsync(user, selectedRole);

                if (redirectTarget == "Doctor")
                {
                    return RedirectToAction("Create", "Doctor", new { userId = user.Id });
                }
                else if (redirectTarget == "Patient")
                {
                    return RedirectToAction("Create", "Patient", new { userId = user.Id });
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Console.WriteLine("❌ Failed to create user");
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"🔸 Identity Error: {error.Description}");
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(newUser);
        }

        [HttpGet]
        public async Task<IActionResult> CreateRoles()
        {
            string[] roles = { "User", "Doctor", "Admin" };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                    Console.WriteLine($"✅ Created Role: {role}");
                }
            }

            return Content("✅ Roles created successfully!");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginUser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(loginUser.UserName);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginUser.Password, false, false);

                    if (result.Succeeded)
                    {
                        if (await _userManager.IsInRoleAsync(user, "Doctor"))
                        {
                            var doctor = _doctorServices.GetAll()?.FirstOrDefault(d => d.ApplicationUserId == user.Id);

                            if (doctor != null)
                            {
                                return RedirectToAction("Profile", "Doctor", new { userId = user.Id });
                            }
                            else
                            {
                                return RedirectToAction("Create", "Doctor", new { userId = user.Id });
                            }
                        }
                        else if (await _userManager.IsInRoleAsync(user, "User"))
                        {
                            var patient = _patientServices.GetAll()?.FirstOrDefault(p => p.ApplicationUserId == user.Id);

                            if (patient != null)
                            {
                                return RedirectToAction("Profile", "Patient", new { userId = user.Id });
                            }
                            else
                            {
                                return RedirectToAction("Create", "Patient", new { userId = user.Id });
                            }
                        }
                        else
                        {
                            // لو مش Doctor ولا User
                            return RedirectToAction("Index", "Home");
                        }
                    }

                    else
                    {
                        ModelState.AddModelError("", "❌ Invalid Password");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "❌ Invalid Username or Password");
                }
            }

            return View(loginUser);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult AdminRegister()
        {
            return View("Register");
        }
        [HttpPost]
        public async Task<IActionResult> AdminRegister(RegisterViewModel newUser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = newUser.UserName;
                user.PasswordHash = newUser.Password;
                user.Email = newUser.EmailAddress;

                IdentityResult result = await _userManager.CreateAsync(user, newUser.Password);
                if (result.Succeeded)
                {
                    // Assign role Admin to User
                    await _userManager.AddToRoleAsync(user, "Admin");
                    //Login
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View("Register", newUser);
        }


    }
} 
