using System;

namespace IronWorkoutTracker.Domain.Entities;

public class ProgramDayExerciseSet
{	
	public int ProgramDayExerciseSetId { get; set; }
    public int ProgramDayExerciseId { get; set; }


    public int? Reps { get; set; }
    public decimal? Weight { get; set; }
    public int? RestSeconds { get; set; }
    public int? RPE { get; set; }

    public int? Order { get; set; }
    public string? Note { get; set; }
    public DateTime? CreatedAt { get; set; }

    public ProgramDayExercise? ProgramDayExercise { get; set; }
}
