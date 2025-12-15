using System.Collections.Generic;
using System.Threading.Tasks;
using IronWorkoutTracker.Application.IRepositories;
using IronWorkoutTracker.Domain.Entities;
using IronWorkoutTracker.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IronWorkoutTracker.Presentation.Controllers
{
    public class ProgramDayExerciseController : Controller
    {
        private readonly IProgramDayExerciseRepository _programDayExerciseRepo;
        private readonly IProgramDayRepository _programDayRepo;
        private readonly IExerciseRepository _exerciseRepo;

        public ProgramDayExerciseController(
            IProgramDayExerciseRepository programDayExerciseRepo,
            IProgramDayRepository programDayRepo,
            IExerciseRepository exerciseRepo)
        {
            _programDayExerciseRepo = programDayExerciseRepo;
            _programDayRepo = programDayRepo;
            _exerciseRepo = exerciseRepo;
        }

        // GET: /ProgramDayExercise/Index?programDayId=5
        public async Task<IActionResult> Index(int programDayId)
        {
            var programDay = await _programDayRepo.GetByIdAsync(programDayId);
            if (programDay == null)
                return NotFound();

            var exercises = await _programDayExerciseRepo.GetByProgramDayIdAsync(programDayId);

            var vm = new ProgramDayExerciseIndexViewModel
            {
                ProgramDay = programDay,
                Exercises = exercises
            };

            return View(vm);
        }

        // GET: /ProgramDayExercise/Create?programDayId=5
        public async Task<IActionResult> Create(int programDayId)
        {
            var programDay = await _programDayRepo.GetByIdAsync(programDayId);
            if (programDay == null)
                return NotFound();

            var allExercises = await _exerciseRepo.GetAllAsync();

            var model = new ProgramDayExercise { ProgramDayId = programDayId };

            var vm = new ProgramDayExerciseFormViewModel
            {
                ProgramDayExercise = model,
                Exercises = allExercises
            };

            return PartialView("_FormModal", vm);
        }

        // GET: /ProgramDayExercise/EditModal/5
        public async Task<IActionResult> EditModal(int id)
        {
            var exerciseAssignment = await _programDayExerciseRepo.GetByIdAsync(id);
            if (exerciseAssignment == null)
                return NotFound();

            var allExercises = await _exerciseRepo.GetAllAsync();

            var vm = new ProgramDayExerciseFormViewModel
            {
                ProgramDayExercise = exerciseAssignment,
                Exercises = allExercises
            };

            return PartialView("_FormModal", vm);
        }

        // POST: /ProgramDayExercise/SaveExercise
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveExercise(ProgramDayExercise model)
        {
            if (!ModelState.IsValid)
            {
                var allExercises = await _exerciseRepo.GetAllAsync();
                var vm = new ProgramDayExerciseFormViewModel
                {
                    ProgramDayExercise = model,
                    Exercises = allExercises
                };
                return PartialView("_FormModal", vm);
            }

            if (model.ProgramDayExerciseId == 0)
            {
                await _programDayExerciseRepo.AddAsync(model);
            }
            else
            {
                var existing = await _programDayExerciseRepo.GetByIdAsync(model.ProgramDayExerciseId);
                if (existing != null)
                {
                    existing.ExerciseId = model.ExerciseId;
                    existing.TrainingStyle = model.TrainingStyle;
                    existing.Note = model.Note;

                    await _programDayExerciseRepo.UpdateAsync(existing);
                }
            }

            return RedirectToAction(
                actionName: "Index",
                controllerName: "ProgramDayExercise",
                routeValues: new { programDayId = model.ProgramDayId });
        }

        // POST: /ProgramDayExercise/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var exercise = await _programDayExerciseRepo.GetByIdAsync(id);
            if (exercise == null)
                return NotFound();

            var programDayId = exercise.ProgramDayId;
            await _programDayExerciseRepo.DeleteAsync(id);

            return RedirectToAction(nameof(Index), new { programDayId });
        }
    }
}
