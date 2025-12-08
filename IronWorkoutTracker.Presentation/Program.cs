using Microsoft.EntityFrameworkCore;


using IronWorkoutTracker.Application;
using IronWorkoutTracker.Domain;
using IronWorkoutTracker.Presentation;
using IronWorkoutTracker.Infrastructure;
using IronWorkoutTracker.Infrastructure.Data.Context;
using IronWorkout.Shared;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddPresntation();
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddDomain();
builder.Services.AddShared();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
builder.Services.AddDbContext<IronDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
