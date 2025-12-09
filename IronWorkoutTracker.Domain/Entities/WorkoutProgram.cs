using System;
namespace IronWorkoutTracker.Domain.Entities;


public class WorkoutProgram
{
    public int WorkoutProgramId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? CreatedById { get; set; }

    public bool Visibility { get; set; }
    public DateTime CreatedDate { get; set; }

    public User? CreatedBy { get; set; }
    public ICollection<UserProgram>? UserPrograms { get; set; }
}
