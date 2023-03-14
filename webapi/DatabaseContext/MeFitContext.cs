using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Profiles;


namespace webapi.DatabaseContext;

public partial class MeFitContext : DbContext
{
    private readonly IConfiguration _config;


    public MeFitContext(IConfiguration config)
    {
        _config = config;
    }

    //Connection string
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Contributionrequest> Contributionrequests { get; set; }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<Goal> Goals { get; set; }

    public virtual DbSet<Musclegroup> Musclegroups { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    public virtual DbSet<Trainingprogram> Trainingprograms { get; set; }

    public virtual DbSet<Set> Sets { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Workout> Workouts { get; set; }

    public virtual DbSet<GoalWorkouts> GoalWorkouts { get; set; }

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
                .HasMaxLength(50)
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

            entity.Property(e => e.FkUserProfileId).HasColumnName("Fk_UserProfile_id");

            entity.HasOne(d => d.FkUserProfile).WithMany(p => p.Contributionrequests)
                .HasForeignKey(d => d.FkUserProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contributionrequests_UserProfiles1");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        // Excercise-Musclegroup Linking table

        modelBuilder.Entity<Exercise>()
            .HasMany(m => m.Musclegroups)
            .WithMany(c => c.Exercises)
            .UsingEntity<Dictionary<string, object>>(
                "Exercise_Musclegroups",
                r => r.HasOne<Musclegroup>().WithMany().HasForeignKey("Fk_Musclegroup_Id"),
                l => l.HasOne<Exercise>().WithMany().HasForeignKey("Fk_Exercise_Id"),
                je =>
                {
                    je.HasKey("Fk_Exercise_Id", "Fk_Musclegroup_Id");
                    je.Property<int>("Fk_Exercise_Id").ValueGeneratedNever();
                    je.Property<int>("Fk_Musclegroup_Id").ValueGeneratedNever();

                });

        // Exercise-Sets Linking table

        modelBuilder.Entity<Exercise>()
            .HasMany(m => m.Sets)
            .WithMany(c => c.Exercises)
            .UsingEntity<Dictionary<string, object>>(
                "Exercise_Sets",
                r => r.HasOne<Set>().WithMany().HasForeignKey("Fk_Set_Id"),
                l => l.HasOne<Exercise>().WithMany().HasForeignKey("Fk_Exercise_Id"),
                je => { je.HasKey("Fk_Exercise_Id", "Fk_Set_Id"); });

        modelBuilder.Entity<Goal>(entity =>
        {
            entity.Property(e => e.StartDate).HasColumnType("date");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.FkUserProfileId).HasColumnName("Fk_UserProfile_id");
            entity.Property(e => e.FkTrainingprogramId).HasColumnName("Fk_Trainingprogram_id");
            entity.Property(e => e.FkStatusId).HasColumnName("Fk_status_id");

            entity.HasOne(d => d.FkUserProfile).WithMany(p => p.Goals)
                .HasForeignKey(d => d.FkUserProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Goals_UserProfiles");

            entity.HasOne(d => d.FkTrainingprogram).WithMany(p => p.Goals)
                .HasForeignKey(d => d.FkTrainingprogramId)
                .HasConstraintName("FK_Goals_Trainingprograms");

            entity.HasOne(d => d.FkStatus).WithMany(p => p.Goals)
                .HasForeignKey(d => d.FkStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Goals_Status");
        });


        // Added 
        modelBuilder.Entity<GoalWorkouts>(entity =>
        {
            entity.Property(e => e.FkGoalId).HasColumnName("Fk_Goal_id");
            entity.Property(e => e.FkWorkoutId).HasColumnName("Fk_Workout_id");
            entity.Property(e => e.FkStatusId).HasColumnName("Fk_Status_id");
        });
        modelBuilder.Entity<GoalWorkouts>().ToTable("Goal_Workouts");


        modelBuilder.Entity<Musclegroup>(entity =>
        {
            entity.ToTable("Musclegroup");

            entity.Property(e => e.Musclegroup1)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("Musclegroup");
        });

        modelBuilder.Entity<UserProfile>(entity =>
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

            entity.HasOne(d => d.FkAddress).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.FkAddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserProfiles_Addresses");

            entity.HasOne(d => d.FkUser).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.FkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserProfiles_Users");
        });

        modelBuilder.Entity<Trainingprogram>(entity =>
        {
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        // Program Category linking table


        modelBuilder.Entity<Trainingprogram>()
            .HasMany(m => m.Categories)
            .WithMany(c => c.Trainingprograms)
            .UsingEntity<Dictionary<string, object>>(
                "Trainingprogram_Categories",
                r => r.HasOne<Category>().WithMany().HasForeignKey("Fk_Category_Id"),
                l => l.HasOne<Trainingprogram>().WithMany().HasForeignKey("Fk_Trainingprogram_Id"),
                je =>
                {
                    je.HasKey("Fk_Trainingprogram_Id", "Fk_Category_Id");
                    je.Property<int>("Fk_Trainingprogram_Id").ValueGeneratedNever();
                    je.Property<int>("Fk_Category_Id").ValueGeneratedNever();

                });

        // Trainingprogram Workout linking table


        modelBuilder.Entity<Trainingprogram>()
            .HasMany(m => m.Workouts)
            .WithMany(c => c.Trainingprograms)
            .UsingEntity<Dictionary<string, object>>(
                "Trainingprogram_Workouts",
                r => r.HasOne<Workout>().WithMany().HasForeignKey("Fk_Workout_Id"),
                l => l.HasOne<Trainingprogram>().WithMany().HasForeignKey("Fk_Trainingprogram_Id"),
                je =>
                {
                    je.HasKey("Fk_Trainingprogram_Id", "Fk_Workout_Id");
                    je.Property<int>("Fk_Trainingprogram_Id").ValueGeneratedNever();
                    je.Property<int>("Fk_Workout_Id").ValueGeneratedNever();

                });


        //  Workout Exercises linking table

        modelBuilder.Entity<Workout>()
            .HasMany(m => m.Exercises)
            .WithMany(c => c.Workouts)
            .UsingEntity<Dictionary<string, object>>(
                "Workout_Exercises",
                r => r.HasOne<Exercise>().WithMany().HasForeignKey("Fk_Exercise_Id"),
                l => l.HasOne<Workout>().WithMany().HasForeignKey("Fk_Workout_Id"),
                je =>
                {
                    je.HasKey("Fk_Workout_Id", "Fk_Exercise_Id");
                    je.Property<int>("Fk_Workout_Id").ValueGeneratedNever();
                    je.Property<int>("Fk_Exercise_Id").ValueGeneratedNever();

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
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<Workout>(entity =>
        {
            entity.ToTable("Workout");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.FkUserProfileId) // Make it nullable
                .HasColumnName("FkUserProfileId")
                .HasColumnType("int")
                .IsRequired(false); // Add this line to make it nullable

        });

        //// Workout Goal linking table
        //modelBuilder.Entity<Workout>()
        //    .HasMany(m => m.Goals)
        //    .WithMany(c => c.Workouts)
        //    .UsingEntity<Dictionary<string, object>>(
        //        "Workout_Goals",
        //        r => r.HasOne<Goal>().WithMany().HasForeignKey("Fk_Goal_Id"),
        //        l => l.HasOne<Workout>().WithMany().HasForeignKey("Fk_Workout_Id"),
        //        je =>
        //        {
        //            je.HasKey("Fk_Workout_Id", "Fk_Goal_Id");
        //            je.HasOne<Status>().WithMany().HasForeignKey("Fk_Status_Id");
        //            je.ToTable("Workout_Goals_Status");

        //        });

        Seeding(modelBuilder);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    private void Seeding(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>().HasData(new User { Id = 1, Username = "administrator@gmail.com" });
        modelBuilder.Entity<User>().HasData(new User { Id = 2, Username = "contributor@gmail.com" });
        modelBuilder.Entity<User>().HasData(new User { Id = 3, Username = "regularuser@gmail.com" });

        modelBuilder.Entity<Address>().HasData(new Address
        {
            Id = 1, AddressLine1 = "Hybenvej 23,st. th.", AddressLine2 = "", AddressLine3 = "", PostalCode = 8700,
            City = "Horsens", Country = "Denmark"
        });
        modelBuilder.Entity<Address>().HasData(new Address
        {
            Id = 2, AddressLine1 = "Vestergade 2", AddressLine2 = "", AddressLine3 = "", PostalCode = 2000,
            City = "København", Country = "Denmark"
        });
        modelBuilder.Entity<Address>().HasData(new Address
        {
            Id = 3, AddressLine1 = "Nørregade 14", AddressLine2 = "", AddressLine3 = "", PostalCode = 7100,
            City = "Vejle", Country = "Denmark"
        });

        modelBuilder.Entity<UserProfile>().HasData(new UserProfile
        {
            Id = 1, FkUserId = 1, FkAddressId = 1, Weight = 80, Height = 180, MedicalCondition = "", Disabilities = "",
            Firstname = "Admin", Lastname = "Admin", Email = "administrator@gmail.com", Phone = 12345, Picture = ""
        });
        modelBuilder.Entity<UserProfile>().HasData(new UserProfile
        {
            Id = 2, FkUserId = 1, FkAddressId = 1, Weight = 70, Height = 170, MedicalCondition = "", Disabilities = "",
            Firstname = "Admin", Lastname = "Admin", Email = "administrator@gmail.com", Phone = 12345, Picture = ""
        });
        modelBuilder.Entity<UserProfile>().HasData(new UserProfile
        {
            Id = 3, FkUserId = 1, FkAddressId = 1, Weight = 65, Height = 165, MedicalCondition = "", Disabilities = "",
            Firstname = "Admin", Lastname = "Admin", Email = "administrator@gmail.com", Phone = 12345, Picture = ""
        });

        modelBuilder.Entity<Goal>().HasData(new Goal
        {
            Id = 1, FkUserProfileId = 1, StartDate = DateTime.Now.AddDays(-24), EndDate = DateTime.Now,
            FkTrainingprogramId = null, FkStatusId = 2
        });
        modelBuilder.Entity<Goal>().HasData(new Goal
        {
            Id = 2, FkUserProfileId = 2, StartDate = DateTime.Now.AddDays(-24), EndDate = DateTime.Now.AddDays(-14),
            FkTrainingprogramId = 3, FkStatusId = 1
        });
        modelBuilder.Entity<Goal>().HasData(new Goal
        {
            Id = 3, FkUserProfileId = 3, StartDate = DateTime.Now.AddDays(-24), EndDate = DateTime.Now.AddDays(-14),
            FkTrainingprogramId = 3, FkStatusId = 1
        });
        modelBuilder.Entity<Goal>().HasData(new Goal
        {
            Id = 4, FkUserProfileId = 3, StartDate = DateTime.Now.AddDays(-24), EndDate = DateTime.Now,
            FkTrainingprogramId = null, FkStatusId = 2
        });


        modelBuilder.Entity<GoalWorkouts>().HasData(new GoalWorkouts
            { Id = 1, FkGoalId = 2, FkWorkoutId = 1, FkStatusId = 1 });
        modelBuilder.Entity<GoalWorkouts>().HasData(new GoalWorkouts
            { Id = 2, FkGoalId = 2, FkWorkoutId = 2, FkStatusId = 1 });
        modelBuilder.Entity<GoalWorkouts>().HasData(new GoalWorkouts
            { Id = 3, FkGoalId = 2, FkWorkoutId = 7, FkStatusId = 1 });
        modelBuilder.Entity<GoalWorkouts>().HasData(new GoalWorkouts
            { Id = 4, FkGoalId = 3, FkWorkoutId = 1, FkStatusId = 1 });
        modelBuilder.Entity<GoalWorkouts>().HasData(new GoalWorkouts
            { Id = 5, FkGoalId = 3, FkWorkoutId = 2, FkStatusId = 1 });
        modelBuilder.Entity<GoalWorkouts>().HasData(new GoalWorkouts
            { Id = 6, FkGoalId = 3, FkWorkoutId = 7, FkStatusId = 1 });
        modelBuilder.Entity<GoalWorkouts>().HasData(new GoalWorkouts
            { Id = 7, FkGoalId = 3, FkWorkoutId = 12, FkStatusId = 2 });
        //modelBuilder.Entity<GoalWorkout>().HasData(new GoalWorkout {Id=7, FkGoalId=4, FkWorkoutId=7,FkStatusId=2});

        modelBuilder.Entity<Status>().HasData(new Status { Id = 1, Statustype = "Completed" });
        modelBuilder.Entity<Status>().HasData(new Status { Id = 2, Statustype = "Pending" });

        modelBuilder.Entity<Category>().HasData(new Category { Id = 1, Category1 = "Strength" });
        modelBuilder.Entity<Category>().HasData(new Category { Id = 2, Category1 = "Endurance" });
        modelBuilder.Entity<Category>().HasData(new Category { Id = 3, Category1 = "Balance" });
        modelBuilder.Entity<Category>().HasData(new Category { Id = 4, Category1 = "Flexibility" });

        modelBuilder.Entity<Trainingprogram>().HasData(new Trainingprogram { Id = 1, Name = "Weightlifting" });
        modelBuilder.Entity<Trainingprogram>().HasData(new Trainingprogram { Id = 2, Name = "Powerlifting" });
        modelBuilder.Entity<Trainingprogram>().HasData(new Trainingprogram { Id = 3, Name = "Bodybuilding" });
        modelBuilder.Entity<Trainingprogram>().HasData(new Trainingprogram { Id = 4, Name = "Swimming" });
        modelBuilder.Entity<Trainingprogram>().HasData(new Trainingprogram { Id = 5, Name = "Cyckling" });
        modelBuilder.Entity<Trainingprogram>().HasData(new Trainingprogram { Id = 6, Name = "Long-distance running" });
        modelBuilder.Entity<Trainingprogram>().HasData(new Trainingprogram { Id = 7, Name = "Yoga" });

        #region M2M seeding Trainingprogram_Categories

        //Seed m2m Trainingprogram_Categories.Need to define m2m and access linking table
        modelBuilder.Entity<Trainingprogram>()
            .HasMany(t => t.Categories)
            .WithMany(c => c.Trainingprograms)
            .UsingEntity<Dictionary<string, object>>(
                "Traningprogram_Categories",
                r => r.HasOne<Category>().WithMany().HasForeignKey("Fk_Category_Id"),
                l => l.HasOne<Trainingprogram>().WithMany().HasForeignKey("Fk_Traningprogram_Id"),
                je =>
                {
                    je.HasKey("Fk_Traningprogram_Id", "Fk_Category_Id");
                    je.HasData(
                        new { Fk_Traningprogram_Id = 1, Fk_Category_Id = 1 },
                        new { Fk_Traningprogram_Id = 2, Fk_Category_Id = 1 },
                        new { Fk_Traningprogram_Id = 3, Fk_Category_Id = 1 },
                        new { Fk_Traningprogram_Id = 4, Fk_Category_Id = 2 },
                        new { Fk_Traningprogram_Id = 5, Fk_Category_Id = 2 },
                        new { Fk_Traningprogram_Id = 6, Fk_Category_Id = 3 }
                    );
                });

        #endregion

        modelBuilder.Entity<Set>().HasData(new Set { Id = 1, Reps = 6, Total = 4 });
        modelBuilder.Entity<Set>().HasData(new Set { Id = 2, Reps = 8, Total = 4 });
        modelBuilder.Entity<Set>().HasData(new Set { Id = 3, Reps = 10, Total = 3 });
        modelBuilder.Entity<Set>().HasData(new Set { Id = 4, Reps = 12, Total = 3 });

        modelBuilder.Entity<Exercise>().HasData(new Exercise { Id = 1, Name = "Bench press", Description = "any" });
        modelBuilder.Entity<Exercise>().HasData(new Exercise { Id = 2, Name = "Squat", Description = "any" });
        modelBuilder.Entity<Exercise>().HasData(new Exercise { Id = 3, Name = "Deadlift", Description = "any" });
        modelBuilder.Entity<Exercise>().HasData(new Exercise { Id = 4, Name = "Shoulder press", Description = "any" });
        modelBuilder.Entity<Exercise>().HasData(new Exercise { Id = 5, Name = "Rows", Description = "any" });
        modelBuilder.Entity<Exercise>().HasData(new Exercise { Id = 6, Name = "Arm curl", Description = "any" });
        modelBuilder.Entity<Exercise>().HasData(new Exercise { Id = 7, Name = "Arm extention", Description = "any" });
        modelBuilder.Entity<Exercise>().HasData(new Exercise { Id = 8, Name = "Walking", Description = "any" });
        modelBuilder.Entity<Exercise>().HasData(new Exercise { Id = 9, Name = "Running", Description = "any" });
        modelBuilder.Entity<Exercise>().HasData(new Exercise { Id = 10, Name = "Swimming", Description = "any" });
        modelBuilder.Entity<Exercise>().HasData(new Exercise { Id = 11, Name = "Push ups", Description = "any" });
        modelBuilder.Entity<Exercise>().HasData(new Exercise { Id = 12, Name = "Pull down", Description = "any" });
        modelBuilder.Entity<Exercise>().HasData(new Exercise { Id = 13, Name = "Leg curl", Description = "any" });
        modelBuilder.Entity<Exercise>().HasData(new Exercise { Id = 14, Name = "Leg extension", Description = "any" });
        modelBuilder.Entity<Exercise>().HasData(new Exercise { Id = 15, Name = "Walking", Description = "any" });


        modelBuilder.Entity<Musclegroup>().HasData(new Musclegroup { Id = 1, Musclegroup1 = "Fullbody" });
        modelBuilder.Entity<Musclegroup>().HasData(new Musclegroup { Id = 2, Musclegroup1 = "Chest" });
        modelBuilder.Entity<Musclegroup>().HasData(new Musclegroup { Id = 3, Musclegroup1 = "Back" });
        modelBuilder.Entity<Musclegroup>().HasData(new Musclegroup { Id = 4, Musclegroup1 = "Leg" });
        modelBuilder.Entity<Musclegroup>().HasData(new Musclegroup { Id = 5, Musclegroup1 = "Arms" });
        modelBuilder.Entity<Musclegroup>().HasData(new Musclegroup { Id = 6, Musclegroup1 = "Shoulders" });
        modelBuilder.Entity<Musclegroup>().HasData(new Musclegroup { Id = 7, Musclegroup1 = "Heart" });

        #region M2M seeding Excersise_Sets

        // Seed m2m Exercise_Sets. Need to define m2m and access linking table
        modelBuilder.Entity<Exercise>()
            .HasMany(e => e.Sets)
            .WithMany(s => s.Exercises)
            .UsingEntity<Dictionary<string, object>>(
                "Exercise_Sets",
                r => r.HasOne<Set>().WithMany().HasForeignKey("Fk_Set_Id"),
                l => l.HasOne<Exercise>().WithMany().HasForeignKey("Fk_Exercise_Id"),
                je =>
                {
                    je.HasKey("Fk_Exercise_Id", "Fk_Set_Id");
                    je.HasData(
                        new { Fk_Exercise_Id = 1, Fk_Set_Id = 1 },
                        new { Fk_Exercise_Id = 2, Fk_Set_Id = 1 },
                        new { Fk_Exercise_Id = 3, Fk_Set_Id = 1 },
                        new { Fk_Exercise_Id = 4, Fk_Set_Id = 1 },
                        new { Fk_Exercise_Id = 5, Fk_Set_Id = 1 }
                    );
                });

        #endregion

        #region M2M seeding Excercise_Musclegroups

        // Seed m2m Exercise_Musclegroup. Need to define m2m and access linking table
        modelBuilder.Entity<Exercise>()
            .HasMany(m => m.Musclegroups)
            .WithMany(s => s.Exercises)
            .UsingEntity<Dictionary<string, object>>(
                "Exercise_Musclegroups",
                r => r.HasOne<Musclegroup>().WithMany().HasForeignKey("Fk_Musclegroup_Id"),
                l => l.HasOne<Exercise>().WithMany().HasForeignKey("Fk_Exercise_Id"),
                je =>
                {
                    je.HasKey("Fk_Exercise_Id", "Fk_Musclegroup_Id");
                    je.HasData(
                        new { Fk_Exercise_Id = 1, Fk_Musclegroup_Id = 2 },
                        new { Fk_Exercise_Id = 2, Fk_Musclegroup_Id = 4 },
                        new { Fk_Exercise_Id = 3, Fk_Musclegroup_Id = 3 },
                        new { Fk_Exercise_Id = 4, Fk_Musclegroup_Id = 6 },
                        new { Fk_Exercise_Id = 5, Fk_Musclegroup_Id = 3 }
                    );
                });

        #endregion

        modelBuilder.Entity<Workout>().HasData(new Workout { Id = 1, Name = "Upper", Type = "Strength" });
        modelBuilder.Entity<Workout>().HasData(new Workout { Id = 2, Name = "Lower", Type = "Strength" });
        modelBuilder.Entity<Workout>().HasData(new Workout { Id = 3, Name = "Push", Type = "Strength" });
        modelBuilder.Entity<Workout>().HasData(new Workout { Id = 4, Name = "Pull", Type = "Strength" });
        modelBuilder.Entity<Workout>().HasData(new Workout { Id = 5, Name = "Chest", Type = "Strength" });
        modelBuilder.Entity<Workout>().HasData(new Workout { Id = 6, Name = "Back", Type = "Strength" });
        modelBuilder.Entity<Workout>().HasData(new Workout { Id = 7, Name = "Leg", Type = "Strength" });
        modelBuilder.Entity<Workout>().HasData(new Workout { Id = 8, Name = "Arms", Type = "Strength" });
        modelBuilder.Entity<Workout>().HasData(new Workout { Id = 9, Name = "Shoulder", Type = "Strength" });
        modelBuilder.Entity<Workout>().HasData(new Workout { Id = 10, Name = "Cardio", Type = "Endurance" });
        modelBuilder.Entity<Workout>().HasData(new Workout { Id = 11, Name = "Fullbody", Type = "Strength" });
        modelBuilder.Entity<Workout>().HasData(new Workout
            { Id = 12, Name = "MyWorkout", Type = "Custom", FkUserProfileId = 3 });

        #region M2M seeding Workout-Exercises

        // Seed m2m Workout-Exercises. Need to define m2m and access linking table
        modelBuilder.Entity<Workout>()
            .HasMany(w => w.Exercises)
            .WithMany(e => e.Workouts)
            .UsingEntity<Dictionary<string, object>>(
                "Workout_Exercises",
                r => r.HasOne<Exercise>().WithMany().HasForeignKey("Fk_Exercise_Id"),
                l => l.HasOne<Workout>().WithMany().HasForeignKey("Fk_Workout_Id"),
                je =>
                {
                    je.HasKey("Fk_Workout_Id", "Fk_Exercise_Id");
                    je.HasData(
                        new { Fk_Workout_Id = 1, Fk_Exercise_Id = 1 },
                        new { Fk_Workout_Id = 1, Fk_Exercise_Id = 5 },
                        new { Fk_Workout_Id = 1, Fk_Exercise_Id = 12 },
                        new { Fk_Workout_Id = 2, Fk_Exercise_Id = 2 },
                        new { Fk_Workout_Id = 2, Fk_Exercise_Id = 13 },
                        new { Fk_Workout_Id = 10, Fk_Exercise_Id = 9 },
                        new { Fk_Workout_Id = 10, Fk_Exercise_Id = 10 },
                        new { Fk_Workout_Id = 12, Fk_Exercise_Id = 9 },
                        new { Fk_Workout_Id = 12, Fk_Exercise_Id = 10 },
                        new { Fk_Workout_Id = 12, Fk_Exercise_Id = 15 }
                    );
                });

        #endregion

        #region M2M seeding Traningsprogram-Workouts

        // Seed m2m Traningsprogram-Workouts. Need to define m2m and access linking table
        modelBuilder.Entity<Trainingprogram>()
            .HasMany(t => t.Workouts)
            .WithMany(w => w.Trainingprograms)
            .UsingEntity<Dictionary<string, object>>(
                "Trainingprogram_Workouts",
                r => r.HasOne<Workout>().WithMany().HasForeignKey("Fk_Workout_Id"),
                l => l.HasOne<Trainingprogram>().WithMany().HasForeignKey("Fk_Trainingprogram_Id"),
                je =>
                {
                    je.HasKey("Fk_Trainingprogram_Id", "Fk_Workout_Id");
                    je.HasData(
                        new { Fk_Trainingprogram_Id = 3, Fk_Workout_Id = 1 },
                        new { Fk_Trainingprogram_Id = 3, Fk_Workout_Id = 2 },
                        new { Fk_Trainingprogram_Id = 3, Fk_Workout_Id = 7 },
                        new { Fk_Trainingprogram_Id = 6, Fk_Workout_Id = 10 }

                    );
                });

        #endregion
    }
}
