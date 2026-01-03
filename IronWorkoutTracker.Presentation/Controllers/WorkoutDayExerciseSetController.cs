using IronWorkoutTracker.Application.IRepositories;
using IronWorkoutTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using IronWorkout.Shared.EnvironmentStateModels;

namespace IronWorkoutTracker.Presentation.Controllers
{
    public class WorkoutDayExerciseSetController : Controller
    {
        private readonly IWorkoutDayExerciseSetRepository _setRepo;
        private readonly IWorkoutDayExerciseRepository _workoutDayExerciseRepository;
        private readonly CurrentUser _currentUser;

        public WorkoutDayExerciseSetController(
            IWorkoutDayExerciseSetRepository setRepo,
            IWorkoutDayExerciseRepository workoutDayExerciseRepository,
            CurrentUser currentUser)
        {
            _setRepo = setRepo;
            _workoutDayExerciseRepository = workoutDayExerciseRepository;
            _currentUser = currentUser;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int workoutDayExerciseId)
        {
            var model = new WorkoutDayExerciseSet { WorkoutDayExerciseId = workoutDayExerciseId };
            return PartialView("_FormModal", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveSet(WorkoutDayExerciseSet model)
        {
            if (!ModelState.IsValid)
                return PartialView("_FormModal", model);

            if (model.WorkoutDayExerciseSetId == 0)
            {
                await _setRepo.AddAsync(model);
            }
            else
            {
                var existing = await _setRepo.GetByIdAsync(model.WorkoutDayExerciseSetId);
                if (existing != null)
                {
                    existing.Reps = model.Reps;
                    existing.Weight = model.Weight;
                    existing.RestSeconds = model.RestSeconds;
                    existing.RPE = model.RPE;
                    existing.Note = model.Note;

                    await _setRepo.UpdateAsync(existing);
                }
            }

            return Redirect("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _setRepo.DeleteAsync(id);
            return RedirectToAction("Index", "Home", new { filter = "workout" });
        }
    }
}