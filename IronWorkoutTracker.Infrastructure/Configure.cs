using System;
using IronWorkoutTracker.Application.IRepositories;
using IronWorkoutTracker.Application.IServices;
using IronWorkoutTracker.Infrastructure.Repositories;
using IronWorkoutTracker.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IronWorkoutTracker.Infrastructure;

public static class Configure
{
    public static void AddInfrastructure(this IServiceCollection service)
    {
        AddBusinessServices(service);
        AddRepositories(service);
    }

    private static void AddRepositories(IServiceCollection service)
    {
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<IWorkoutProgramRepository, WorkoutProgramRepository>();
        service.AddScoped<IUserProgramRepository, UserProgramRepository>();
        service.AddScoped<IProgramDayRepository, ProgramDayRepository>();

     
    }

    private static void AddBusinessServices(IServiceCollection service)
    {
        service.AddScoped<IUserService, UserService>();
    }
}
