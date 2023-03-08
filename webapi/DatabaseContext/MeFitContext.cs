using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using webapi.Models;


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
                je =>
                {
                    je.HasKey("Fk_Exercise_Id", "Fk_Set_Id");

                });

        modelBuilder.Entity<Goal>(entity =>
        {
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.FkUserProfileId).HasColumnName("Fk_UserProfile_id");
            entity.Property(e => e.FkTrainingprogramId).HasColumnName("Fk_Trainingprogram_id");
            entity.Property(e => e.FkStatusId).HasColumnName("Fk_status_id");

            entity.HasOne(d => d.FkUserProfile).WithMany(p => p.Goals)
                .HasForeignKey(d => d.FkUserProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Goals_UserProfiles");

            entity.HasOne(d => d.FkProgram).WithMany(p => p.Goals)
                .HasForeignKey(d => d.FkTrainingprogramId)
                .HasConstraintName("FK_Goals_Trainingprograms");

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

        modelBuilder.Entity<UserUserProfile>(entity =>
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

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsFixedLength();

        });

        // Workout Goal linking table
        modelBuilder.Entity<Workout>()
            .HasMany(m => m.Goals)
            .WithMany(c => c.Workouts)
            .UsingEntity<Dictionary<string, object>>(
                "Workout_Goals",
                r => r.HasOne<Goal>().WithMany().HasForeignKey("Fk_Goal_Id"),
                l => l.HasOne<Workout>().WithMany().HasForeignKey("Fk_Workout_Id"),
                je =>
                {
                    je.HasKey("Fk_Workout_Id", "Fk_Goal_Id");
                    je.HasOne<Status>().WithMany().HasForeignKey("Fk_Status_Id");
                    je.ToTable("Workout_Goals_Status");

                });




        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
