// File: IronWorkoutTracker.Infrastructure/Data/Context/IronDbContext.cs
using System;
using Microsoft.EntityFrameworkCore;
using IronWorkoutTracker.Domain.Entities;

namespace IronWorkoutTracker.Infrastructure.Data.Context
{
    public class IronDbContext : DbContext
    {	
        public IronDbContext(DbContextOptions<IronDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<WorkoutProgram> WorkoutPrograms { get; set; }
        public DbSet<UserProgram> UserPrograms { get; set; }
        public DbSet<ProgramDay> ProgramDays { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ProgramDayExercise> ProgramDayExercises { get; set; }
        public DbSet<ProgramDayExerciseSet> ProgramDayExerciseSets { get; set; }

        // Workout
        public DbSet<WorkoutDay> WorkoutDays { get; set; }
        public DbSet<WorkoutDayExercise> WorkoutDayExercises { get; set; }
        public DbSet<WorkoutDayExerciseSet> WorkoutDayExerciseSets { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WorkoutProgram>()
                .HasOne(p => p.CreatedBy)
                .WithMany(u => u.WorkoutPrograms)
                .HasForeignKey(p => p.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserProgram>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserPrograms)
                .HasForeignKey(up => up.UserId);

            modelBuilder.Entity<UserProgram>()
                .HasOne(up => up.WorkoutProgram)
                .WithMany(p => p.UserPrograms)
                .HasForeignKey(up => up.WorkoutProgramId);

            modelBuilder.Entity<UserProgram>()
                .HasMany(up => up.WorkoutDays)
                .WithOne(wd => wd.UserProgram)
                .HasForeignKey(wd => wd.UserProgramId);


            modelBuilder.Entity<ProgramDay>()
                .HasMany(pd => pd.Exercises)
                .WithOne(wpe => wpe.ProgramDay)
                .HasForeignKey(wpe => wpe.ProgramDayId);

            modelBuilder.Entity<WorkoutDay>()
                .HasMany(wd => wd.Exercises)
                .WithOne(wde => wde.WorkoutDay)
                .HasForeignKey(wde => wde.WorkoutDayId);

            modelBuilder.Entity<WorkoutDayExercise>()
                .HasMany(wde => wde.Sets)
                .WithOne(wdes => wdes.WorkoutDayExercise)
                .HasForeignKey(wdes => wdes.WorkoutDayExerciseId);
        }
    }
}