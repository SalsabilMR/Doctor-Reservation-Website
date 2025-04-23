using DoctorReservation.Helpers;
using DoctorReservation.Models;
using DoctorReservation.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DoctorReservation.Helpers;

namespace DoctorReservation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //builder.Services.AddSession(s=>s.IdleTimeout = TimeSpan.FromMinutes(2));
            builder.Services.AddSession();

            //Services
            builder.Services.AddDbContext<DoctorReservationDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Local")));
            builder.Services.AddScoped<DoctorServices>();
            builder.Services.AddScoped<PatientServices>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole> ().AddEntityFrameworkStores<DoctorReservationDBContext>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await RoleInitializer.SeedRolesAsync(services);
            }


        }
    }
}
