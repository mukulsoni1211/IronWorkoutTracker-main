using IronWorkoutTracker.Application.IRepositories;
using IronWorkoutTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IronWorkoutTracker.Presentation.Controllers
{
    public class WorkoutProgramController : Controller
    {
        private readonly IWorkoutProgramRepository _repo;

        public WorkoutProgramController(IWorkoutProgramRepository repo)
        {
            _repo = repo;
        }

        // GET: /WorkoutProgram
        public async Task<IActionResult> Index()
        {
            var programs = await _repo.GetAllAsync();
            return View(programs);
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
            {
                // Inspect all errors
                foreach (var kvp in ModelState)
                {
                    var key = kvp.Key;
                    var errors = kvp.Value.Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
                    }
                }

                return View(model);
            }

            // TODO: set CreatedById from logged-in user
            model.CreatedDate = DateTime.UtcNow;

            await _repo.AddAsync(model);
            return RedirectToAction("Index", "Home");
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
