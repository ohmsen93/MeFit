using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
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
                    AddressLine_3 = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
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
                    Token = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exercise_Musclegroups",
                columns: table => new
                {
                    FK_Exercise_Id = table.Column<int>(type: "int", nullable: false),
                    FK_Musclegroup_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise_Musclegroups", x => new { x.FK_Exercise_Id, x.FK_Musclegroup_Id });
                    table.ForeignKey(
                        name: "FK_Exercise_Musclegroups_Exercises_FK_Exercise_Id",
                        column: x => x.FK_Exercise_Id,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exercise_Musclegroups_Musclegroup_FK_Musclegroup_Id",
                        column: x => x.FK_Musclegroup_Id,
                        principalTable: "Musclegroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exercise_Sets",
                columns: table => new
                {
                    FK_Exercise_Id = table.Column<int>(type: "int", nullable: false),
                    FK_Set_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise_Sets", x => new { x.FK_Exercise_Id, x.FK_Set_Id });
                    table.ForeignKey(
                        name: "FK_Exercise_Sets_Exercises_FK_Exercise_Id",
                        column: x => x.FK_Exercise_Id,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exercise_Sets_Sets_FK_Set_Id",
                        column: x => x.FK_Set_Id,
                        principalTable: "Sets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainingprogram_Categories",
                columns: table => new
                {
                    FK_Trainingprogram_Id = table.Column<int>(type: "int", nullable: false),
                    FK_Category_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainingprogram_Categories", x => new { x.FK_Trainingprogram_Id, x.FK_Category_Id });
                    table.ForeignKey(
                        name: "FK_Trainingprogram_Categories_Categories_FK_Category_Id",
                        column: x => x.FK_Category_Id,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trainingprogram_Categories_Trainingprograms_FK_Trainingprogram_Id",
                        column: x => x.FK_Trainingprogram_Id,
                        principalTable: "Trainingprograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_User_id = table.Column<int>(type: "int", nullable: false),
                    FK_Address_id = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Addresses",
                        column: x => x.FK_Address_id,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Profiles_Users",
                        column: x => x.FK_User_id,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contributionrequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_Profile_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contributionrequests_1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contributionrequests_Profiles1",
                        column: x => x.FK_Profile_id,
                        principalTable: "Profiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_Profile_id = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    FK_Trainingprogram_id = table.Column<int>(type: "int", nullable: true),
                    FK_Status_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goals_Profiles",
                        column: x => x.FK_Profile_id,
                        principalTable: "Profiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Goals_Status",
                        column: x => x.FK_Status_id,
                        principalTable: "Status",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Goals_Trainingprograms",
                        column: x => x.FK_Trainingprogram_id,
                        principalTable: "Trainingprograms",
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
                    FK_Profile_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workout", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workout_Profiles_FK_Profile_id",
                        column: x => x.FK_Profile_id,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trainingprogram_Workouts",
                columns: table => new
                {
                    FK_Trainingprogram_Id = table.Column<int>(type: "int", nullable: false),
                    FK_Workout_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainingprogram_Workouts", x => new { x.FK_Trainingprogram_Id, x.FK_Workout_Id });
                    table.ForeignKey(
                        name: "FK_Trainingprogram_Workouts_Trainingprograms_FK_Trainingprogram_Id",
                        column: x => x.FK_Trainingprogram_Id,
                        principalTable: "Trainingprograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trainingprogram_Workouts_Workout_FK_Workout_Id",
                        column: x => x.FK_Workout_Id,
                        principalTable: "Workout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workout_Exercises",
                columns: table => new
                {
                    FK_Workout_Id = table.Column<int>(type: "int", nullable: false),
                    FK_Exercise_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workout_Exercises", x => new { x.FK_Workout_Id, x.FK_Exercise_Id });
                    table.ForeignKey(
                        name: "FK_Workout_Exercises_Exercises_FK_Exercise_Id",
                        column: x => x.FK_Exercise_Id,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workout_Exercises_Workout_FK_Workout_Id",
                        column: x => x.FK_Workout_Id,
                        principalTable: "Workout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workout_Goals_Status",
                columns: table => new
                {
                    FK_Workout_Id = table.Column<int>(type: "int", nullable: false),
                    FK_Goal_Id = table.Column<int>(type: "int", nullable: false),
                    FK_Status_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workout_Goals_Status", x => new { x.FK_Workout_Id, x.FK_Goal_Id });
                    table.ForeignKey(
                        name: "FK_Workout_Goals_Status_Goals_FK_Goal_Id",
                        column: x => x.FK_Goal_Id,
                        principalTable: "Goals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workout_Goals_Status_Status_FK_Status_Id",
                        column: x => x.FK_Status_Id,
                        principalTable: "Status",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Workout_Goals_Status_Workout_FK_Workout_Id",
                        column: x => x.FK_Workout_Id,
                        principalTable: "Workout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contributionrequests_FK_Profile_id",
                table: "Contributionrequests",
                column: "FK_Profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_Musclegroups_FK_Musclegroup_Id",
                table: "Exercise_Musclegroups",
                column: "FK_Musclegroup_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_Sets_FK_Set_Id",
                table: "Exercise_Sets",
                column: "FK_Set_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_FK_Profile_id",
                table: "Goals",
                column: "FK_Profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_FK_Status_id",
                table: "Goals",
                column: "FK_Status_id");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_FK_Trainingprogram_id",
                table: "Goals",
                column: "FK_Trainingprogram_id");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_FK_Address_id",
                table: "Profiles",
                column: "FK_Address_id");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_FK_User_id",
                table: "Profiles",
                column: "FK_User_id");

            migrationBuilder.CreateIndex(
                name: "IX_Trainingprogram_Categories_FK_Category_Id",
                table: "Trainingprogram_Categories",
                column: "FK_Category_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Trainingprogram_Workouts_FK_Workout_Id",
                table: "Trainingprogram_Workouts",
                column: "FK_Workout_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Workout_FK_Profile_id",
                table: "Workout",
                column: "FK_Profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_Workout_Exercises_FK_Exercise_Id",
                table: "Workout_Exercises",
                column: "FK_Exercise_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Workout_Goals_Status_FK_Goal_Id",
                table: "Workout_Goals_Status",
                column: "FK_Goal_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Workout_Goals_Status_FK_Status_Id",
                table: "Workout_Goals_Status",
                column: "FK_Status_Id");
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
                name: "Trainingprogram_Categories");

            migrationBuilder.DropTable(
                name: "Trainingprogram_Workouts");

            migrationBuilder.DropTable(
                name: "Workout_Exercises");

            migrationBuilder.DropTable(
                name: "Workout_Goals_Status");

            migrationBuilder.DropTable(
                name: "Musclegroup");

            migrationBuilder.DropTable(
                name: "Sets");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "Workout");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Trainingprograms");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
