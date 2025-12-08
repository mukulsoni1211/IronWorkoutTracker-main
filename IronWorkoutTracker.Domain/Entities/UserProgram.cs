using System;

namespace IronWorkoutTracker.Domain.Entities;

public class UserProgram
{
    public int UserProgramId { get; set; }

    public int UserId { get; set; }
    public int ProgramId { get; set; }

    public DateTime StartDate { get; set; }
    public bool IsActive { get; set; }

    public User User { get; set; }
    public WorkoutProgram WorkoutProgram { get; set; }
}