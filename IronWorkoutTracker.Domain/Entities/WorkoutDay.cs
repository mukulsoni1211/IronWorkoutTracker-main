using System;
namespace IronWorkoutTracker.Domain.Entities;

public class WorkoutDay
{
    public int WorkoutDayId { get; set; }
    public int UserId { get; set; }
    public int ProgramDayId { get; set; }
    public int UserProgramId { get; set; }
    public int WorkoutProgramId { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<WorkoutDayExercise> Exercises { get; set; }
    public UserProgram UserProgram { get; set; }
}
