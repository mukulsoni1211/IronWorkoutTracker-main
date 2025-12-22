using System;
namespace IronWorkoutTracker.Domain.Entities;

public class WorkoutDayExercise
{
    public int WorkoutDayExerciseId { get; set; }
    public int WorkoutDayId { get; set; }
    public int ProgramDayExerciseId { get; set; }
    public int ExerciseId { get; set; }
    public string ExerciseName { get; set; }
    
    public List<WorkoutDayExerciseSet> Sets { get; set; }
    public WorkoutDay WorkoutDay { get; set; }
}
