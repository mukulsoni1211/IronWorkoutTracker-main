using System.Threading.Tasks;
using IronWorkoutTracker.Application.IRepositories;
using IronWorkoutTracker.Domain.Entities;
using IronWorkoutTracker.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IronWorkoutTracker.Presentation.Controllers
{
    public class ProgramDayExerciseSetController : Controller
    {
        private readonly IProgramDayExerciseSetRepository _setRepo;
        private readonly IProgramDayExerciseRepository _exerciseRepo;

        public ProgramDayExerciseSetController(
            IProgramDayExerciseSetRepository setRepo,
            IProgramDayExerciseRepository exerciseRepo)
        {
            _setRepo = setRepo;
            _exerciseRepo = exerciseRepo;
        }

        // GET: /ProgramDayExerciseSet/Create?programDayExerciseId=5
        public async Task<IActionResult> Create(int programDayExerciseId)
        {
            var exercise = await _exerciseRepo.GetByIdAsync(programDayExerciseId);
            if (exercise == null)
                return NotFound();

            var model = new ProgramDayExerciseSet { ProgramDayExerciseId = programDayExerciseId };
            return PartialView("_FormModal", model);
        }

        // GET: /ProgramDayExerciseSet/EditModal/5
        public async Task<IActionResult> EditModal(int id)
        {
            var set = await _setRepo.GetByIdAsync(id);
            if (set == null)
                return NotFound();

            return PartialView("_FormModal", set);
        }

        // POST: /ProgramDayExerciseSet/SaveSet
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveSet(ProgramDayExerciseSet model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_FormModal", model);
            }
            var exercise = await _exerciseRepo.GetByIdAsync(model.ProgramDayExerciseId);
            if (model.ProgramDayExerciseSetId == 0)
            {
                await _setRepo.AddAsync(model);
            }
            else
            {
                var existing = await _setRepo.GetByIdAsync(model.ProgramDayExerciseSetId);
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

            return RedirectToAction(
                actionName: "Index",
                controllerName: "ProgramDayExercise",
                routeValues: new { programDayId = exercise?.ProgramDayId ?? 0 });
        }

        // POST: /ProgramDayExerciseSet/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var set = await _setRepo.GetByIdAsync(id);
            if (set == null)
                return NotFound();

            var exercise = await _exerciseRepo.GetByIdAsync(set.ProgramDayExerciseId);
            var exerciseId = set.ProgramDayExerciseId;
            await _setRepo.DeleteAsync(id);

            return RedirectToAction(
                actionName: "Index",
                controllerName: "ProgramDayExercise",
                routeValues: new { programDayId = set.ProgramDayExercise?.ProgramDayId });
        }
    }
}
