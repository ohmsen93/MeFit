using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class AddedGaolWorkout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Workout_Goals_Status");

            migrationBuilder.CreateTable(
                name: "Goal_Workouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fk_Goal_id = table.Column<int>(type: "int", nullable: false),
                    Fk_Workout_id = table.Column<int>(type: "int", nullable: false),
                    Fk_status_id = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_Goal_Workouts_Status_Fk_status_id",
                        column: x => x.Fk_status_id,
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

            migrationBuilder.CreateIndex(
                name: "IX_Goal_Workouts_Fk_Goal_id",
                table: "Goal_Workouts",
                column: "Fk_Goal_id");

            migrationBuilder.CreateIndex(
                name: "IX_Goal_Workouts_Fk_status_id",
                table: "Goal_Workouts",
                column: "Fk_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_Goal_Workouts_Fk_Workout_id",
                table: "Goal_Workouts",
                column: "Fk_Workout_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Goal_Workouts");

            migrationBuilder.CreateTable(
                name: "Workout_Goals_Status",
                columns: table => new
                {
                    Fk_Workout_Id = table.Column<int>(type: "int", nullable: false),
                    Fk_Goal_Id = table.Column<int>(type: "int", nullable: false),
                    Fk_Status_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workout_Goals_Status", x => new { x.Fk_Workout_Id, x.Fk_Goal_Id });
                    table.ForeignKey(
                        name: "FK_Workout_Goals_Status_Goals_Fk_Goal_Id",
                        column: x => x.Fk_Goal_Id,
                        principalTable: "Goals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workout_Goals_Status_Status_Fk_Status_Id",
                        column: x => x.Fk_Status_Id,
                        principalTable: "Status",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Workout_Goals_Status_Workout_Fk_Workout_Id",
                        column: x => x.Fk_Workout_Id,
                        principalTable: "Workout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workout_Goals_Status_Fk_Goal_Id",
                table: "Workout_Goals_Status",
                column: "Fk_Goal_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Workout_Goals_Status_Fk_Status_Id",
                table: "Workout_Goals_Status",
                column: "Fk_Status_Id");
        }
    }
}
