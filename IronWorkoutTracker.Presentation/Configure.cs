using System;
using IronWorkoutTracker.Presentation.PresentationConstants;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace IronWorkoutTracker.Presentation;

public static class Configure
{
    public static void AddPresntation(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Auth/Login";
            options.LogoutPath = "/Auth/Logout";
            options.AccessDeniedPath="/Auth/AccessDenied";
            options.ExpireTimeSpan = TimeSpan.FromHours(8);
            options.SlidingExpiration = true;
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(PolicyNameConstants.AdminOnly, policy =>
            {
                policy.RequireClaim(ClaimConstants.Role, "ADMIN");
            });
        });
    }


}
