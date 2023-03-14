using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressLine_1 = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    AddressLine_2 = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: true),
                    AddressLine_3 = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: true),
                    PostalCode = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Musclegroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Musclegroup = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musclegroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reps = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Statustype = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trainingprograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainingprograms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    FirstLogin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exercise_Musclegroups",
                columns: table => new
                {
                    Fk_Exercise_Id = table.Column<int>(type: "int", nullable: false),
                    Fk_Musclegroup_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise_Musclegroups", x => new { x.Fk_Exercise_Id, x.Fk_Musclegroup_Id });
                    table.ForeignKey(
                        name: "FK_Exercise_Musclegroups_Exercises_Fk_Exercise_Id",
                        column: x => x.Fk_Exercise_Id,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exercise_Musclegroups_Musclegroup_Fk_Musclegroup_Id",
                        column: x => x.Fk_Musclegroup_Id,
                        principalTable: "Musclegroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exercise_Sets",
                columns: table => new
                {
                    Fk_Exercise_Id = table.Column<int>(type: "int", nullable: false),
                    Fk_Set_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise_Sets", x => new { x.Fk_Exercise_Id, x.Fk_Set_Id });
                    table.ForeignKey(
                        name: "FK_Exercise_Sets_Exercises_Fk_Exercise_Id",
                        column: x => x.Fk_Exercise_Id,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exercise_Sets_Sets_Fk_Set_Id",
                        column: x => x.Fk_Set_Id,
                        principalTable: "Sets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainingprogram_Categories",
                columns: table => new
                {
                    Fk_Trainingprogram_Id = table.Column<int>(type: "int", nullable: false),
                    Fk_Category_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainingprogram_Categories", x => new { x.Fk_Trainingprogram_Id, x.Fk_Category_Id });
                    table.ForeignKey(
                        name: "FK_Trainingprogram_Categories_Categories_Fk_Category_Id",
                        column: x => x.Fk_Category_Id,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trainingprogram_Categories_Trainingprograms_Fk_Trainingprogram_Id",
                        column: x => x.Fk_Trainingprogram_Id,
                        principalTable: "Trainingprograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Traningprogram_Categories",
                columns: table => new
                {
                    Fk_Traningprogram_Id = table.Column<int>(type: "int", nullable: false),
                    Fk_Category_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traningprogram_Categories", x => new { x.Fk_Traningprogram_Id, x.Fk_Category_Id });
                    table.ForeignKey(
                        name: "FK_Traningprogram_Categories_Categories_Fk_Category_Id",
                        column: x => x.Fk_Category_Id,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Traningprogram_Categories_Trainingprograms_Fk_Traningprogram_Id",
                        column: x => x.Fk_Traningprogram_Id,
                        principalTable: "Trainingprograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fk_user_id = table.Column<int>(type: "int", nullable: false),
                    Fk_address_id = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    MedicalCondition = table.Column<string>(type: "text", nullable: true),
                    Disabilities = table.Column<string>(type: "text", nullable: true),
                    Firstname = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    Lastname = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<string>(type: "nchar(250)", fixedLength: true, maxLength: 250, nullable: true),
                    Email = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Addresses",
                        column: x => x.Fk_address_id,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserProfiles_Users",
                        column: x => x.Fk_user_id,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contributionrequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fk_UserProfile_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contributionrequests_1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contributionrequests_UserProfiles1",
                        column: x => x.Fk_UserProfile_id,
                        principalTable: "UserProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fk_UserProfile_id = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    Fk_Trainingprogram_id = table.Column<int>(type: "int", nullable: true),
                    Fk_status_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goals_Status",
                        column: x => x.Fk_status_id,
                        principalTable: "Status",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Goals_Trainingprograms",
                        column: x => x.Fk_Trainingprogram_id,
                        principalTable: "Trainingprograms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Goals_UserProfiles",
                        column: x => x.Fk_UserProfile_id,
                        principalTable: "UserProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Workout",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    FkUserProfileId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workout", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workout_UserProfiles_FkUserProfileId",
                        column: x => x.FkUserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Goal_Workouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fk_Goal_id = table.Column<int>(type: "int", nullable: false),
                    Fk_Workout_id = table.Column<int>(type: "int", nullable: false),
                    Fk_Status_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goal_Workouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goal_Workouts_Goals_Fk_Goal_id",
                        column: x => x.Fk_Goal_id,
                        principalTable: "Goals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Goal_Workouts_Status_Fk_Status_id",
                        column: x => x.Fk_Status_id,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Goal_Workouts_Workout_Fk_Workout_id",
                        column: x => x.Fk_Workout_id,
                        principalTable: "Workout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainingprogram_Workouts",
                columns: table => new
                {
                    Fk_Trainingprogram_Id = table.Column<int>(type: "int", nullable: false),
                    Fk_Workout_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainingprogram_Workouts", x => new { x.Fk_Trainingprogram_Id, x.Fk_Workout_Id });
                    table.ForeignKey(
                        name: "FK_Trainingprogram_Workouts_Trainingprograms_Fk_Trainingprogram_Id",
                        column: x => x.Fk_Trainingprogram_Id,
                        principalTable: "Trainingprograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trainingprogram_Workouts_Workout_Fk_Workout_Id",
                        column: x => x.Fk_Workout_Id,
                        principalTable: "Workout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workout_Exercises",
                columns: table => new
                {
                    Fk_Workout_Id = table.Column<int>(type: "int", nullable: false),
                    Fk_Exercise_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workout_Exercises", x => new { x.Fk_Workout_Id, x.Fk_Exercise_Id });
                    table.ForeignKey(
                        name: "FK_Workout_Exercises_Exercises_Fk_Exercise_Id",
                        column: x => x.Fk_Exercise_Id,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workout_Exercises_Workout_Fk_Workout_Id",
                        column: x => x.Fk_Workout_Id,
                        principalTable: "Workout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "AddressLine_1", "AddressLine_2", "AddressLine_3", "City", "Country", "PostalCode" },
                values: new object[,]
                {
                    { 1, "Hybenvej 23,st. th.", "", "", "Horsens", "Denmark", 8700 },
                    { 2, "Vestergade 2", "", "", "København", "Denmark", 2000 },
                    { 3, "Nørregade 14", "", "", "Vejle", "Denmark", 7100 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Category" },
                values: new object[,]
                {
                    { 1, "Strength" },
                    { 2, "Endurance" },
                    { 3, "Balance" },
                    { 4, "Flexibility" }
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "any", "Bench press" },
                    { 2, "any", "Squat" },
                    { 3, "any", "Deadlift" },
                    { 4, "any", "Shoulder press" },
                    { 5, "any", "Rows" },
                    { 6, "any", "Arm curl" },
                    { 7, "any", "Arm extention" },
                    { 8, "any", "Walking" },
                    { 9, "any", "Running" },
                    { 10, "any", "Swimming" },
                    { 11, "any", "Push ups" },
                    { 12, "any", "Pull down" },
                    { 13, "any", "Leg curl" },
                    { 14, "any", "Leg extension" },
                    { 15, "any", "Walking" }
                });

            migrationBuilder.InsertData(
                table: "Musclegroup",
                columns: new[] { "Id", "Musclegroup" },
                values: new object[,]
                {
                    { 1, "Fullbody" },
                    { 2, "Chest" },
                    { 3, "Back" },
                    { 4, "Leg" },
                    { 5, "Arms" },
                    { 6, "Shoulders" },
                    { 7, "Heart" }
                });

            migrationBuilder.InsertData(
                table: "Sets",
                columns: new[] { "Id", "Reps", "Total" },
                values: new object[,]
                {
                    { 1, 6, 4 },
                    { 2, 8, 4 },
                    { 3, 10, 3 },
                    { 4, 12, 3 }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Statustype" },
                values: new object[,]
                {
                    { 1, "Completed" },
                    { 2, "Pending" }
                });

            migrationBuilder.InsertData(
                table: "Trainingprograms",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Weightlifting" },
                    { 2, "Powerlifting" },
                    { 3, "Bodybuilding" },
                    { 4, "Swimming" },
                    { 5, "Cyckling" },
                    { 6, "Long-distance running" },
                    { 7, "Yoga" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstLogin", "Username" },
                values: new object[,]
                {
                    { 1, false, "administrator@gmail.com" },
                    { 2, false, "contributor@gmail.com" },
                    { 3, false, "regularuser@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Workout",
                columns: new[] { "Id", "FkUserProfileId", "Name", "Type" },
                values: new object[,]
                {
                    { 1, null, "Upper", "Strength" },
                    { 2, null, "Lower", "Strength" },
                    { 3, null, "Push", "Strength" },
                    { 4, null, "Pull", "Strength" },
                    { 5, null, "Chest", "Strength" },
                    { 6, null, "Back", "Strength" },
                    { 7, null, "Leg", "Strength" },
                    { 8, null, "Arms", "Strength" },
                    { 9, null, "Shoulder", "Strength" },
                    { 10, null, "Cardio", "Endurance" },
                    { 11, null, "Fullbody", "Strength" }
                });

            migrationBuilder.InsertData(
                table: "Exercise_Musclegroups",
                columns: new[] { "Fk_Exercise_Id", "Fk_Musclegroup_Id" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 4 },
                    { 3, 3 },
                    { 4, 6 },
                    { 5, 3 }
                });

            migrationBuilder.InsertData(
                table: "Exercise_Sets",
                columns: new[] { "Fk_Exercise_Id", "Fk_Set_Id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "Trainingprogram_Workouts",
                columns: new[] { "Fk_Trainingprogram_Id", "Fk_Workout_Id" },
                values: new object[,]
                {
                    { 3, 1 },
                    { 3, 2 },
                    { 3, 7 },
                    { 6, 10 }
                });

            migrationBuilder.InsertData(
                table: "Traningprogram_Categories",
                columns: new[] { "Fk_Category_Id", "Fk_Traningprogram_Id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 3, 6 }
                });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "Disabilities", "Email", "Firstname", "Fk_address_id", "Fk_user_id", "Height", "Lastname", "MedicalCondition", "Phone", "Picture", "Weight" },
                values: new object[,]
                {
                    { 1, "", "administrator@gmail.com", "Admin", 1, 1, 180.0, "Admin", "", 12345, "", 80.0 },
                    { 2, "", "administrator@gmail.com", "Admin", 1, 1, 170.0, "Admin", "", 12345, "", 70.0 },
                    { 3, "", "administrator@gmail.com", "Admin", 1, 1, 165.0, "Admin", "", 12345, "", 65.0 }
                });

            migrationBuilder.InsertData(
                table: "Workout_Exercises",
                columns: new[] { "Fk_Exercise_Id", "Fk_Workout_Id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 5, 1 },
                    { 12, 1 },
                    { 2, 2 },
                    { 13, 2 },
                    { 9, 10 },
                    { 10, 10 }
                });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "Id", "EndDate", "Fk_status_id", "Fk_Trainingprogram_id", "Fk_UserProfile_id", "StartDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 3, 14, 12, 52, 49, 1, DateTimeKind.Local).AddTicks(5378), 2, null, 1, new DateTime(2023, 2, 18, 12, 52, 49, 1, DateTimeKind.Local).AddTicks(5305) },
                    { 2, new DateTime(2023, 2, 28, 12, 52, 49, 1, DateTimeKind.Local).AddTicks(5395), 1, 3, 2, new DateTime(2023, 2, 18, 12, 52, 49, 1, DateTimeKind.Local).AddTicks(5393) },
                    { 3, new DateTime(2023, 2, 28, 12, 52, 49, 1, DateTimeKind.Local).AddTicks(5405), 1, 3, 3, new DateTime(2023, 2, 18, 12, 52, 49, 1, DateTimeKind.Local).AddTicks(5404) },
                    { 4, new DateTime(2023, 3, 14, 12, 52, 49, 1, DateTimeKind.Local).AddTicks(5416), 2, null, 3, new DateTime(2023, 2, 18, 12, 52, 49, 1, DateTimeKind.Local).AddTicks(5414) }
                });

            migrationBuilder.InsertData(
                table: "Workout",
                columns: new[] { "Id", "FkUserProfileId", "Name", "Type" },
                values: new object[] { 12, 3, "MyWorkout", "Custom" });

            migrationBuilder.InsertData(
                table: "Goal_Workouts",
                columns: new[] { "Id", "Fk_Goal_id", "Fk_Status_id", "Fk_Workout_id" },
                values: new object[,]
                {
                    { 1, 2, 1, 1 },
                    { 2, 2, 1, 2 },
                    { 3, 2, 1, 7 },
                    { 4, 3, 1, 1 },
                    { 5, 3, 1, 2 },
                    { 6, 3, 1, 7 },
                    { 7, 3, 2, 12 }
                });

            migrationBuilder.InsertData(
                table: "Workout_Exercises",
                columns: new[] { "Fk_Exercise_Id", "Fk_Workout_Id" },
                values: new object[,]
                {
                    { 9, 12 },
                    { 10, 12 },
                    { 15, 12 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contributionrequests_Fk_UserProfile_id",
                table: "Contributionrequests",
                column: "Fk_UserProfile_id");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_Musclegroups_Fk_Musclegroup_Id",
                table: "Exercise_Musclegroups",
                column: "Fk_Musclegroup_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_Sets_Fk_Set_Id",
                table: "Exercise_Sets",
                column: "Fk_Set_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Goal_Workouts_Fk_Goal_id",
                table: "Goal_Workouts",
                column: "Fk_Goal_id");

            migrationBuilder.CreateIndex(
                name: "IX_Goal_Workouts_Fk_Status_id",
                table: "Goal_Workouts",
                column: "Fk_Status_id");

            migrationBuilder.CreateIndex(
                name: "IX_Goal_Workouts_Fk_Workout_id",
                table: "Goal_Workouts",
                column: "Fk_Workout_id");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_Fk_status_id",
                table: "Goals",
                column: "Fk_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_Fk_Trainingprogram_id",
                table: "Goals",
                column: "Fk_Trainingprogram_id");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_Fk_UserProfile_id",
                table: "Goals",
                column: "Fk_UserProfile_id");

            migrationBuilder.CreateIndex(
                name: "IX_Trainingprogram_Categories_Fk_Category_Id",
                table: "Trainingprogram_Categories",
                column: "Fk_Category_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Trainingprogram_Workouts_Fk_Workout_Id",
                table: "Trainingprogram_Workouts",
                column: "Fk_Workout_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Traningprogram_Categories_Fk_Category_Id",
                table: "Traningprogram_Categories",
                column: "Fk_Category_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_Fk_address_id",
                table: "UserProfiles",
                column: "Fk_address_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_Fk_user_id",
                table: "UserProfiles",
                column: "Fk_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Workout_FkUserProfileId",
                table: "Workout",
                column: "FkUserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Workout_Exercises_Fk_Exercise_Id",
                table: "Workout_Exercises",
                column: "Fk_Exercise_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contributionrequests");

            migrationBuilder.DropTable(
                name: "Exercise_Musclegroups");

            migrationBuilder.DropTable(
                name: "Exercise_Sets");

            migrationBuilder.DropTable(
                name: "Goal_Workouts");

            migrationBuilder.DropTable(
                name: "Trainingprogram_Categories");

            migrationBuilder.DropTable(
                name: "Trainingprogram_Workouts");

            migrationBuilder.DropTable(
                name: "Traningprogram_Categories");

            migrationBuilder.DropTable(
                name: "Workout_Exercises");

            migrationBuilder.DropTable(
                name: "Musclegroup");

            migrationBuilder.DropTable(
                name: "Sets");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Workout");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Trainingprograms");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
