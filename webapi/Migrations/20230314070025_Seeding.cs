using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AddressLine_3",
                table: "Addresses",
                type: "nchar(50)",
                fixedLength: true,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10,
                oldNullable: true);

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
                columns: new[] { "Id", "Token", "Username" },
                values: new object[,]
                {
                    { 1, "78acbd80-93b7-4821-9fb0-a5ee776318da", "administrator@gmail.com" },
                    { 2, "4d24f01a-5261-40f6-a7e0-f051e8e6f599", "contributor@gmail.com" },
                    { 3, "78acbd80-93b7-4821-9fb0-a5ee776318da", "regularuser@gmail.com" }
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
                columns: new[] { "Id", "Achived", "EndDate", "Fk_status_id", "Fk_Trainingprogram_id", "Fk_UserProfile_id" },
                values: new object[,]
                {
                    { 1, false, new DateTime(2023, 3, 14, 8, 0, 25, 490, DateTimeKind.Local).AddTicks(2588), 2, null, 1 },
                    { 2, true, new DateTime(2023, 2, 28, 8, 0, 25, 490, DateTimeKind.Local).AddTicks(2676), 1, 3, 2 },
                    { 3, true, new DateTime(2023, 2, 28, 8, 0, 25, 490, DateTimeKind.Local).AddTicks(2692), 1, 3, 3 },
                    { 4, false, new DateTime(2023, 3, 14, 8, 0, 25, 490, DateTimeKind.Local).AddTicks(2716), 2, null, 3 }
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
                name: "IX_Traningprogram_Categories_Fk_Category_Id",
                table: "Traningprogram_Categories",
                column: "Fk_Category_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Traningprogram_Categories");

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Exercise_Musclegroups",
                keyColumns: new[] { "Fk_Exercise_Id", "Fk_Musclegroup_Id" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Exercise_Musclegroups",
                keyColumns: new[] { "Fk_Exercise_Id", "Fk_Musclegroup_Id" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "Exercise_Musclegroups",
                keyColumns: new[] { "Fk_Exercise_Id", "Fk_Musclegroup_Id" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Exercise_Musclegroups",
                keyColumns: new[] { "Fk_Exercise_Id", "Fk_Musclegroup_Id" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.DeleteData(
                table: "Exercise_Musclegroups",
                keyColumns: new[] { "Fk_Exercise_Id", "Fk_Musclegroup_Id" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "Exercise_Sets",
                keyColumns: new[] { "Fk_Exercise_Id", "Fk_Set_Id" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Exercise_Sets",
                keyColumns: new[] { "Fk_Exercise_Id", "Fk_Set_Id" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Exercise_Sets",
                keyColumns: new[] { "Fk_Exercise_Id", "Fk_Set_Id" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "Exercise_Sets",
                keyColumns: new[] { "Fk_Exercise_Id", "Fk_Set_Id" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "Exercise_Sets",
                keyColumns: new[] { "Fk_Exercise_Id", "Fk_Set_Id" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Goal_Workouts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Goal_Workouts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Goal_Workouts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Goal_Workouts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Goal_Workouts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Goal_Workouts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Goal_Workouts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Musclegroup",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Musclegroup",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Musclegroup",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Trainingprogram_Workouts",
                keyColumns: new[] { "Fk_Trainingprogram_Id", "Fk_Workout_Id" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "Trainingprogram_Workouts",
                keyColumns: new[] { "Fk_Trainingprogram_Id", "Fk_Workout_Id" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "Trainingprogram_Workouts",
                keyColumns: new[] { "Fk_Trainingprogram_Id", "Fk_Workout_Id" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                table: "Trainingprogram_Workouts",
                keyColumns: new[] { "Fk_Trainingprogram_Id", "Fk_Workout_Id" },
                keyValues: new object[] { 6, 10 });

            migrationBuilder.DeleteData(
                table: "Trainingprograms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Trainingprograms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Trainingprograms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Trainingprograms",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Trainingprograms",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Workout",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Workout",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Workout",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Workout",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Workout",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Workout",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Workout",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Workout_Exercises",
                keyColumns: new[] { "Fk_Exercise_Id", "Fk_Workout_Id" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Workout_Exercises",
                keyColumns: new[] { "Fk_Exercise_Id", "Fk_Workout_Id" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "Workout_Exercises",
                keyColumns: new[] { "Fk_Exercise_Id", "Fk_Workout_Id" },
                keyValues: new object[] { 12, 1 });

            migrationBuilder.DeleteData(
                table: "Workout_Exercises",
                keyColumns: new[] { "Fk_Exercise_Id", "Fk_Workout_Id" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Workout_Exercises",
                keyColumns: new[] { "Fk_Exercise_Id", "Fk_Workout_Id" },
                keyValues: new object[] { 13, 2 });

            migrationBuilder.DeleteData(
                table: "Workout_Exercises",
                keyColumns: new[] { "Fk_Exercise_Id", "Fk_Workout_Id" },
                keyValues: new object[] { 9, 10 });

            migrationBuilder.DeleteData(
                table: "Workout_Exercises",
                keyColumns: new[] { "Fk_Exercise_Id", "Fk_Workout_Id" },
                keyValues: new object[] { 10, 10 });

            migrationBuilder.DeleteData(
                table: "Workout_Exercises",
                keyColumns: new[] { "Fk_Exercise_Id", "Fk_Workout_Id" },
                keyValues: new object[] { 9, 12 });

            migrationBuilder.DeleteData(
                table: "Workout_Exercises",
                keyColumns: new[] { "Fk_Exercise_Id", "Fk_Workout_Id" },
                keyValues: new object[] { 10, 12 });

            migrationBuilder.DeleteData(
                table: "Workout_Exercises",
                keyColumns: new[] { "Fk_Exercise_Id", "Fk_Workout_Id" },
                keyValues: new object[] { 15, 12 });

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Goals",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Musclegroup",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Musclegroup",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Musclegroup",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Musclegroup",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Trainingprograms",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Workout",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Workout",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Workout",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Workout",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Workout",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Trainingprograms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "AddressLine_3",
                table: "Addresses",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(50)",
                oldFixedLength: true,
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
