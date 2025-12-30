using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IronWorkoutTracker.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using IronWorkoutTracker.Application.IRepositories;
using IronWorkout.Shared.EnvironmentStateModels;
using IronWorkoutTracker.Domain.Entities;

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
            // My programs: created OR adopted
            "myprograms" => allPrograms.Where(p => 
                p.CreatedById == currentUserId || 
                (p.UserPrograms != null && p.UserPrograms.Any(up => up.UserId == currentUserId))),

            // All programs: didn't create AND didn't adopt
            "all" => allPrograms.Where(p => 
                p.CreatedById != currentUserId && 
                (p.UserPrograms == null || !p.UserPrograms.Any(up => up.UserId == currentUserId))),

            // History: programs where status is Finished
            "history" => allPrograms.Where(p => 
                p.UserPrograms != null && 
                p.UserPrograms.Any(up => up.UserId == currentUserId && up.Status == ProgramStatus.Finished)),

            // Workout: last started program (status InProgress)
            "workout" => allPrograms.Where(p => 
                p.UserPrograms != null && 
                p.UserPrograms.Any(up => up.UserId == currentUserId && up.Status == ProgramStatus.InProgress))
                .OrderByDescending(p => p.UserPrograms.FirstOrDefault(up => up.UserId == currentUserId).StartDate)
                .Take(1),

            _ => allPrograms // default
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
