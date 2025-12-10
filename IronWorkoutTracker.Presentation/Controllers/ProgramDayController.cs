using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IronWorkoutTracker.Application.IRepositories;
using IronWorkoutTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IronWorkoutTracker.Presentation.Controllers
{
    public class ProgramDayController : Controller
    {
        private readonly IProgramDayRepository _programDayRepo;
        private readonly IWorkoutProgramRepository _workoutProgramRepo;

        public ProgramDayController(
            IProgramDayRepository programDayRepo,
            IWorkoutProgramRepository workoutProgramRepo)
        {
            _programDayRepo = programDayRepo;
            _workoutProgramRepo = workoutProgramRepo;
        }

        // GET: /ProgramDay/Create?workoutProgramId=5
        public async Task<IActionResult> Create(int workoutProgramId)
        {
            var program = await _workoutProgramRepo.GetByIdAsync(workoutProgramId);
            if (program == null) return NotFound();

            var model = new ProgramDay { WorkoutProgramId = workoutProgramId };
            return PartialView("_FormModal", model);
        }

        // GET: Load Edit form in modal
        public async Task<IActionResult> EditModal(int id)
        {
            var day = await _programDayRepo.GetByIdAsync(id);
            if (day == null) return NotFound();

            return PartialView("_FormModal", day);
        }

        // POST: Save (Create or Update via modal)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveDay(ProgramDay model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                }
                return PartialView("_FormModal", model);
            }

            if (model.ProgramDayId == 0)
            {
                await _programDayRepo.AddAsync(model);
            }
            else
            {
                var existing = await _programDayRepo.GetByIdAsync(model.ProgramDayId);
                if (existing != null)
                {
                    existing.Title = model.Title;
                    existing.Note = model.Note;
                    await _programDayRepo.UpdateAsync(existing);
                }
            }

            return Json(new { success = true });
        }

        // POST: /ProgramDay/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var day = await _programDayRepo.GetByIdAsync(id);
            if (day == null)
                return NotFound();

            var programId = day.WorkoutProgramId;
            await _programDayRepo.DeleteAsync(id);

            return RedirectToAction(
                actionName: "Details",
                controllerName: "WorkoutProgram",
                routeValues: new { id = programId });
        }
    }
}
