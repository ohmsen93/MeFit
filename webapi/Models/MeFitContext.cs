using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace webapi.Models;

public partial class MeFitContext : DbContext
{
    public MeFitContext()
    {
    }

    public MeFitContext(DbContextOptions<MeFitContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Contributionrequest> Contributionrequests { get; set; }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<ExerciseMusclegroup> ExerciseMusclegroups { get; set; }

    public virtual DbSet<Goal> Goals { get; set; }

    public virtual DbSet<Musclegroup> Musclegroups { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<Program> Programs { get; set; }

    public virtual DbSet<ProgramCategory> ProgramCategories { get; set; }

    public virtual DbSet<ProgramWorkout> ProgramWorkouts { get; set; }

    public virtual DbSet<Set> Sets { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Workout> Workouts { get; set; }

    public virtual DbSet<WorkoutGoal> WorkoutGoals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=MeFit;Encrypt=False;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.Property(e => e.AddressLine1)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("AddressLine_1");
            entity.Property(e => e.AddressLine2)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("AddressLine_2");
            entity.Property(e => e.AddressLine3)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("AddressLine_3");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Category1)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("Category");
        });

        modelBuilder.Entity<Contributionrequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Contributionrequests_1");

            entity.Property(e => e.FkProfileId).HasColumnName("Fk_profile_id");

            entity.HasOne(d => d.FkProfile).WithMany(p => p.Contributionrequests)
                .HasForeignKey(d => d.FkProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contributionrequests_Profiles1");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<ExerciseMusclegroup>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Exercise_Musclegroups");

            entity.Property(e => e.FkExerciseId).HasColumnName("Fk_exercise_id");
            entity.Property(e => e.FkMusclegroupId).HasColumnName("Fk_musclegroup_id");

            entity.HasOne(d => d.FkExercise).WithMany()
                .HasForeignKey(d => d.FkExerciseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Exercise_Musclegroups_Exercises");

            entity.HasOne(d => d.FkMusclegroup).WithMany()
                .HasForeignKey(d => d.FkMusclegroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Exercise_Musclegroups_Musclegroup");
        });


        modelBuilder.Entity<Goal>(entity =>
        {
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.FkProfileId).HasColumnName("Fk_profile_id");
            entity.Property(e => e.FkProgramId).HasColumnName("Fk_program_id");
            entity.Property(e => e.FkStatusId).HasColumnName("Fk_status_id");

            entity.HasOne(d => d.FkProfile).WithMany(p => p.Goals)
                .HasForeignKey(d => d.FkProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Goals_Profiles");

            entity.HasOne(d => d.FkProgram).WithMany(p => p.Goals)
                .HasForeignKey(d => d.FkProgramId)
                .HasConstraintName("FK_Goals_Programs");

            entity.HasOne(d => d.FkStatus).WithMany(p => p.Goals)
                .HasForeignKey(d => d.FkStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Goals_Status");
        });

        modelBuilder.Entity<Musclegroup>(entity =>
        {
            entity.ToTable("Musclegroup");

            entity.Property(e => e.Musclegroup1)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("Musclegroup");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.Property(e => e.Disabilities).HasColumnType("text");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.FkAddressId).HasColumnName("Fk_address_id");
            entity.Property(e => e.FkUserId).HasColumnName("Fk_user_id");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.MedicalCondition).HasColumnType("text");
            entity.Property(e => e.Picture)
                .HasMaxLength(250)
                .IsFixedLength();

            entity.HasOne(d => d.FkAddress).WithMany(p => p.Profiles)
                .HasForeignKey(d => d.FkAddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profiles_Addresses");

            entity.HasOne(d => d.FkUser).WithMany(p => p.Profiles)
                .HasForeignKey(d => d.FkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profiles_Users");
        });

        modelBuilder.Entity<Program>(entity =>
        {
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<ProgramCategory>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Program_Categories");

            entity.Property(e => e.FkCategoryId).HasColumnName("Fk_category_id");
            entity.Property(e => e.FkProgramId).HasColumnName("Fk_program_id");

            entity.HasOne(d => d.FkCategory).WithMany()
                .HasForeignKey(d => d.FkCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Program_Categories_Categories");

            entity.HasOne(d => d.FkProgram).WithMany()
                .HasForeignKey(d => d.FkProgramId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Program_Categories_Programs");
        });

        modelBuilder.Entity<ProgramWorkout>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Program_Workouts");

            entity.Property(e => e.FkProgramId).HasColumnName("Fk_program_id");
            entity.Property(e => e.FkWorkoutId).HasColumnName("Fk_workout_id");

            entity.HasOne(d => d.FkProgram).WithMany()
                .HasForeignKey(d => d.FkProgramId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Program_Workouts_Programs");

            entity.HasOne(d => d.FkWorkout).WithMany()
                .HasForeignKey(d => d.FkWorkoutId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Program_Workouts_Workout1");
        });

        modelBuilder.Entity<Set>(entity =>
        {
            entity.Property(e => e.FkExerciseId).HasColumnName("Fk_exercise_id");

            entity.HasOne(d => d.FkExercise).WithMany(p => p.Sets)
                .HasForeignKey(d => d.FkExerciseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sets_Exercises");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.Statustype)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Token)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<Workout>(entity =>
        {
            entity.ToTable("Workout");

            entity.Property(e => e.FkSetId).HasColumnName("Fk_set_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsFixedLength();

            entity.HasOne(d => d.FkSet).WithMany(p => p.Workouts)
                .HasForeignKey(d => d.FkSetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Workout_Sets");
        });

        modelBuilder.Entity<WorkoutGoal>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Workout_Goals");

            entity.Property(e => e.FkGoalId).HasColumnName("Fk_goal_id");
            entity.Property(e => e.FkStatusId).HasColumnName("Fk_status_id");
            entity.Property(e => e.FkWorkoutId).HasColumnName("Fk_workout_id");

            entity.HasOne(d => d.FkGoal).WithMany()
                .HasForeignKey(d => d.FkGoalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Workout_Goals_Goals");

            entity.HasOne(d => d.FkStatus).WithMany()
                .HasForeignKey(d => d.FkStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Workout_Goals_Status");

            entity.HasOne(d => d.FkWorkout).WithMany()
                .HasForeignKey(d => d.FkWorkoutId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Workout_Goals_Workout");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
