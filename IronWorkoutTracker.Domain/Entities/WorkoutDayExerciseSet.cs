using System;
namespace IronWorkoutTracker.Domain.Entities;

public class WorkoutDayExerciseSet
{
    public int WorkoutDayExerciseSetId { get; set; }
    public int WorkoutDayExerciseId { get; set; }
    public int? Reps { get; set; }
    public int? Weight { get; set; }
    public int? RestSeconds { get; set; }
    public int? RPE { get; set; }
    public string? Note { get; set; }

    public WorkoutDayExercise WorkoutDayExercise { get; set; }
}
