using DoctorReservation.ViewModels;
using DoctorReservation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace DoctorReservation.Controllers
{
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                //FirstName = newUser.FirstName,
                //LastName = newUser.LastName
            };

            IdentityResult result = await _userManager.CreateAsync(user, newUser.Password);

            if (result.Succeeded)
            {
                Console.WriteLine("✅ User Created");
                await _userManager.AddToRoleAsync(user, "User");
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
                    //SignIn
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, loginUser.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Details", "Patient");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Inavalied Password");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalied User Name Or Password");
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
