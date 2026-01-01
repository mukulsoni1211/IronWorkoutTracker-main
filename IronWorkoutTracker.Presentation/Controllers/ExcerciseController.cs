using System.Threading.Tasks;
using IronWorkoutTracker.Application.IRepositories;
using IronWorkoutTracker.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IronWorkoutTracker.Presentation.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly IExerciseRepository _repo;

        public ExerciseController(IExerciseRepository repo)
        {
            _repo = repo;
        }

        // GET: /Exercise
        // Authorize with Admin role
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var exercises = await _repo.GetAllAsync();
            return View(exercises);
        }

        // GET: /Exercise/Create
        public IActionResult Create()
        {
            return PartialView("_FormModal");
        }

        // GET: /Exercise/EditModal/5
        public async Task<IActionResult> EditModal(int id)
        {
            var exercise = await _repo.GetByIdAsync(id);
            if (exercise == null)
                return NotFound();

            return PartialView("_FormModal", exercise);
        }

        // POST: /Exercise/SaveExercise
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveExercise(Exercise model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_FormModal", model);
            }

            if (model.ExerciseId == 0)
            {
                await _repo.AddAsync(model);
            }
            else
            {
                var existing = await _repo.GetByIdAsync(model.ExerciseId);
                if (existing != null)
                {
                    existing.Name = model.Name;
                    existing.Category = model.Category;
                    existing.Equipment = model.Equipment;
                    existing.Description = model.Description;
                    existing.IsActive = model.IsActive;

                    await _repo.UpdateAsync(existing);
                }
            }

            return RedirectToAction(actionName: "Index");
        }

        // POST: /Exercise/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
