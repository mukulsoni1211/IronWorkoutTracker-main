using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IronWorkoutTracker.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using IronWorkoutTracker.Application.IRepositories;
using IronWorkout.Shared.EnvironmentStateModels;
using IronWorkoutTracker.Domain.Entities;

public class WorkoutDayExerciseSetController : Controller
{
    private readonly IWorkoutDayExerciseSetRepository _setRepo;
    private readonly CurrentUser _currentUser;

    public WorkoutDayExerciseSetController(
        IWorkoutDayExerciseSetRepository setRepo,
        CurrentUser currentUser)
    {
        _setRepo = setRepo;
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

        return Json(new { success = true });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _setRepo.DeleteAsync(id);
        return RedirectToAction("Index", "Home", new { filter = "workout" });
    }
}
