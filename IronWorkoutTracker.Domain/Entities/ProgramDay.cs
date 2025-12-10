using System;

namespace IronWorkoutTracker.Domain.Entities;

public class ProgramDay
{
    public int ProgramDayId { get; set; }

    public int WorkoutProgramId { get; set; }

    public string Title { get; set; }
    public string? Note { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? Order { get; set; }

    public WorkoutProgram? WorkoutProgram { get; set; }
}