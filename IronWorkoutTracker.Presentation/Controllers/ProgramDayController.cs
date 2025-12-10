using System;
using System.Collections.Generic;
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

        // POST: /ProgramDay/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProgramDay model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _programDayRepo.AddAsync(model);

            return RedirectToAction(
                actionName: "Details",
                controllerName: "WorkoutProgram",
                routeValues: new { id = model.WorkoutProgramId });
        }

        // GET: Load Edit form in modal
		public async Task<IActionResult> EditModal(int id)
		{
		    var day = await _programDayRepo.GetByIdAsync(id);
		    if (day == null) return NotFound();

		    return PartialView("_FormModal", day);
		}

		[HttpPost]
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

		    return RedirectToAction(
                actionName: "Details",
                controllerName: "WorkoutProgram",
                routeValues: new { id = model.WorkoutProgramId });
		}

        // GET: /ProgramDay/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var day = await _programDayRepo.GetByIdAsync(id);
            if (day == null)
                return NotFound();

            return View(day);
        }

        // POST: /ProgramDay/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProgramDay model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existingDay = await _programDayRepo.GetByIdAsync(model.ProgramDayId);
            if (existingDay == null)
                return NotFound();

            existingDay.Title = model.Title;
            existingDay.Note = model.Note;

            await _programDayRepo.UpdateAsync(existingDay);

            return RedirectToAction(
                actionName: "Details",
                controllerName: "WorkoutProgram",
                routeValues: new { id = existingDay.WorkoutProgramId });
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
