using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrmsCoreMvc.Migrations
{
    /// <inheritdoc />
    public partial class updateeventmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_taskboards_tasks_tasksTaskId",
                table: "taskboards");

            migrationBuilder.DropIndex(
                name: "IX_taskboards_tasksTaskId",
                table: "taskboards");

            migrationBuilder.DropColumn(
                name: "tasksTaskId",
                table: "taskboards");

            migrationBuilder.CreateIndex(
                name: "IX_taskboards_TaskId",
                table: "taskboards",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_taskboards_tasks_TaskId",
                table: "taskboards",
                column: "TaskId",
                principalTable: "tasks",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_taskboards_tasks_TaskId",
                table: "taskboards");

            migrationBuilder.DropIndex(
                name: "IX_taskboards_TaskId",
                table: "taskboards");

            migrationBuilder.AddColumn<int>(
                name: "tasksTaskId",
                table: "taskboards",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_taskboards_tasksTaskId",
                table: "taskboards",
                column: "tasksTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_taskboards_tasks_tasksTaskId",
                table: "taskboards",
                column: "tasksTaskId",
                principalTable: "tasks",
                principalColumn: "TaskId");
        }
    }
}
