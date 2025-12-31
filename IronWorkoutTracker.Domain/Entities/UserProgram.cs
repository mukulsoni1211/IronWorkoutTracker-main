using System;

namespace IronWorkoutTracker.Domain.Entities;

public class UserProgram
{
    public int UserProgramId { get; set; }

    public int UserId { get; set; }
    public int WorkoutProgramId { get; set; }

    public DateTime StartDate { get; set; }
    public bool IsActive { get; set; }
    public ProgramStatus Status { get; set; } = ProgramStatus.NotStarted;

    public User User { get; set; }
    public WorkoutProgram WorkoutProgram { get; set; }
    public ICollection<WorkoutDay> WorkoutDays { get; set; } = new List<WorkoutDay>();
}

public enum ProgramStatus
{
    NotStarted = 0,
    InProgress = 1,
    Finished = 2
}
