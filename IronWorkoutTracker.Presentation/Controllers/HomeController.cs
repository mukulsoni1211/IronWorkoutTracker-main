using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IronWorkoutTracker.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using IronWorkoutTracker.Application.IRepositories;

namespace IronWorkoutTracker.Presentation.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWorkoutProgramRepository _workoutProgramRepository;

    public HomeController(ILogger<HomeController> logger, IWorkoutProgramRepository workoutProgramRepository)
    {
        _logger = logger;
        _workoutProgramRepository = workoutProgramRepository;
    }

    public async Task<IActionResult> Index()
    {
        var programs = await _workoutProgramRepository.GetAllAsync();
        return View(programs); // pass programs to home
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
