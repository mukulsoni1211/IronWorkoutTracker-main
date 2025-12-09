using System.Security.Claims;
using IronWorkoutTracker.Application.IRepositories;
using IronWorkoutTracker.Domain.Entities;
using IronWorkoutTracker.Presentation.PresentationConstants;
using Microsoft.AspNetCore.Mvc;
using IronWorkout.Shared.EnvironmentStateModels;
namespace IronWorkoutTracker.Presentation.Controllers
{
    public class WorkoutProgramController : Controller
    {
        private readonly IWorkoutProgramRepository _repo;
        private readonly CurrentUser _currentUser;

        public WorkoutProgramController(IWorkoutProgramRepository repo, CurrentUser currentUser)
        {
            _repo = repo;
            _currentUser = currentUser;
        }

        // GET: /WorkoutProgram
        public async Task<IActionResult> Index()
        {
            var programs = await _repo.GetAllAsync();
            return View(programs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var program = await _repo.GetByIdAsync(id);
            if (program == null)
                return NotFound();

            return View(program);
        }


        // GET: /WorkoutProgram/Create
        public IActionResult Create()
        {
            return View();
        }
    
        // POST: /WorkoutProgram/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkoutProgram model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Get current user id from claims (e.g. JWT or cookie auth)
            if (!string.IsNullOrEmpty(_currentUser?.UserId) && int.TryParse(_currentUser.UserId, out var parsedUserId))
            {
                model.CreatedById = parsedUserId;
            }
            else
            {
                model.CreatedById = null;
            }

            model.CreatedDate = DateTime.UtcNow;
            // Visibility will use whatever is posted from the checkbox; default false if unchecked

            await _repo.AddAsync(model);
            return RedirectToAction(
                        actionName: "Details",
                        controllerName: "WorkoutProgram",
                        routeValues: new { id = model.WorkoutProgramId });
        }

        // GET: /WorkoutProgram/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var program = await _repo.GetByIdAsync(id);
            if (program == null)
                return NotFound();

            return View(program);
        }

        // POST: /WorkoutProgram/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(WorkoutProgram model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _repo.UpdateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        // POST: /WorkoutProgram/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
