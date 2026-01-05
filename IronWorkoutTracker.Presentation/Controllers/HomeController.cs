using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IronWorkoutTracker.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using IronWorkoutTracker.Application.IRepositories;
using IronWorkout.Shared.EnvironmentStateModels;
using IronWorkoutTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IronWorkoutTracker.Presentation.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWorkoutProgramRepository _workoutProgramRepository;
    private readonly IUserProgramRepository _userProgramRepo; 
    private readonly CurrentUser _currentUser;

    public HomeController(ILogger<HomeController> logger, IWorkoutProgramRepository workoutProgramRepository, CurrentUser currentUser, IUserProgramRepository userProgramRepo)
    {
        _logger = logger;
        _workoutProgramRepository = workoutProgramRepository;
        _userProgramRepo = userProgramRepo;
        _currentUser = currentUser;
    }

    public async Task<IActionResult> Index(string filter = "workout")
    {
        var allPrograms = await _workoutProgramRepository.GetAllAsync();
        var currentUserId = int.Parse(_currentUser.UserId);

        IEnumerable<Domain.Entities.WorkoutProgram> filteredPrograms = filter.ToLower() switch
        {
            // My programs: created OR adopted
            "myprograms" => allPrograms.Where(p => 
                p.CreatedById == currentUserId || 
                (p.UserPrograms != null && p.UserPrograms.Any(up => up.UserId == currentUserId && up.Status != ProgramStatus.Finished))),

            // All programs: didn't create AND didn't adopt
            "all" => allPrograms.Where(p => 
                p.CreatedById != currentUserId && 
                (p.UserPrograms == null || !p.UserPrograms.Any(up => up.UserId == currentUserId))),

            // History: programs where status is Finished
            "history" => allPrograms.Where(p => 
                p.UserPrograms != null && 
                p.UserPrograms.Any(up => up.UserId == currentUserId && up.Status == ProgramStatus.Finished)),

            // Workout: last started program (status InProgress)
            "workout" => await _workoutProgramRepository.GetQuery()
                .Include(p => p.UserPrograms)
                    .ThenInclude(up => up.WorkoutDays)
                        .ThenInclude(wd => wd.Exercises)
                            .ThenInclude(wde => wde.Exercise)
                .Include(p => p.UserPrograms)
                    .ThenInclude(up => up.WorkoutDays)
                        .ThenInclude(wd => wd.Exercises)
                            .ThenInclude(wde => wde.Sets)
                .Where(p => 
                    p.UserPrograms != null && 
                    p.UserPrograms.Any(up => up.UserId == currentUserId && up.Status == ProgramStatus.InProgress))
                    .OrderByDescending(p => p.UserPrograms.FirstOrDefault(up => up.UserId == currentUserId).StartDate)
                    .Take(1)
                    .ToListAsync(),

            _ => allPrograms // default
        };

        ViewBag.Filter = filter;
        ViewBag.CurrentUserId = currentUserId;
        return View(filteredPrograms);
    }


    public async Task<IActionResult> History(int Id) // from asp-route-id
    {
        var currentUserId = int.Parse(_currentUser.UserId);

        // Load the specific UserProgram with related graph
        var userProgram = await _userProgramRepo.GetQuery()
            .Include(up => up.WorkoutProgram)
            .Include(up => up.WorkoutDays)
                .ThenInclude(wd => wd.Exercises)
                    .ThenInclude(wde => wde.Sets)
            .Include(up => up.WorkoutDays)
                .ThenInclude(wd => wd.Exercises)
                    .ThenInclude(wde => wde.Exercise)
            .FirstOrDefaultAsync(up => up.UserProgramId == Id 
                                       && up.UserId == currentUserId);

        if (userProgram == null)
            return NotFound();

        //IEnumerable<UserProgram>
        var model = new List<UserProgram> { userProgram };

        ViewBag.CurrentUserId = currentUserId;
        ViewBag.UserProgramId = Id;
        return View("History", model); // History.cshtml
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
