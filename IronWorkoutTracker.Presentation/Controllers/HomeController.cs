using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IronWorkoutTracker.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using IronWorkoutTracker.Application.IRepositories;
using IronWorkout.Shared.EnvironmentStateModels;

namespace IronWorkoutTracker.Presentation.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWorkoutProgramRepository _workoutProgramRepository;
    private readonly CurrentUser _currentUser;

    public HomeController(ILogger<HomeController> logger, IWorkoutProgramRepository workoutProgramRepository, CurrentUser currentUser)
    {
        _logger = logger;
        _workoutProgramRepository = workoutProgramRepository;
        _currentUser = currentUser;
    }

    public async Task<IActionResult> Index(string filter = "all")
    {
        var allPrograms = await _workoutProgramRepository.GetAllAsync();
        var currentUserId = int.Parse(_currentUser.UserId);

        IEnumerable<Domain.Entities.WorkoutProgram> filteredPrograms = filter.ToLower() switch
        {
            "adopted" => allPrograms.Where(p => p.UserPrograms != null && p.UserPrograms.Any(up => up.UserId == currentUserId) && p.CreatedById != currentUserId),
            "myprograms" => allPrograms.Where(p => p.CreatedById == currentUserId),
            _ => allPrograms // "all" or default
        };

        ViewBag.Filter = filter;
        return View(filteredPrograms);
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
