using System;

namespace IronWorkoutTracker.Domain.Entities;

public class Exercise
{
	public int ExerciseId { get; set; }
	public string Name { get; set; }
	public string? Category { get; set; }
	public string? Equipment { get; set; }
	public string? Description { get; set; }
	public bool? IsActive { get; set; }
	public DateTime? CreatedDate { get; set; }
}