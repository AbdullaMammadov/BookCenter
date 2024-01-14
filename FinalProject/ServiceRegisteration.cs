using BackProject.Helpers;
using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.Services;
//using FinalProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;




namespace FinalProject
{
    public static class ServiceRegistiration
    {
        public static void Register(this IServiceCollection services, IConfiguration config)
        {
           services.AddControllersWithViews();
           services.AddDbContext<AppDbContext>(options =>
           {
               options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
           });
            services.AddHttpContextAccessor();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<ILayoutService, LayoutService>();
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 3;


            }).
            AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders().AddErrorDescriber<CustomIdentityErrorDescriber>();
            services.AddSession(option =>
            {
               option.IdleTimeout = TimeSpan.FromMinutes(20);
            });
            services.AddHttpContextAccessor();
            ;



        }
    }
}