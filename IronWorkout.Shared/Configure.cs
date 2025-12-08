using System;
using System.Runtime.CompilerServices;
using IronWorkout.Shared.EnvironmentStateModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace IronWorkout.Shared;

public static  class Configure
{
    public static void AddShared(this IServiceCollection services)
    {
        services.AddScoped<CurrentUser>(sp =>
        {
            var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
            var httpContext = httpContextAccessor.HttpContext;

            if(httpContext?.User?.Identity?.IsAuthenticated == true)
            {
                var user = httpContext.User;

                var name = user.FindFirst("Name")?.Value;
                var email = user.FindFirst("Email")?.Value;
                var Role = user.FindFirst("Role")?.Value;
                var Id = user.FindFirst("Id")?.Value;
                
                return new CurrentUser
                {
                    UserName = name,
                    Email = email,
                    Role = Role,
                    UserId = Id,
                };
            }

            return null;

             
        });
    }
}
