using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrmsCoreMvc.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectsUserDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsUser_AllProjects_ProjectsProjectId",
                table: "ProjectsUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsUser_user_UsersUserId",
                table: "ProjectsUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectsUser",
                table: "ProjectsUser");

            migrationBuilder.RenameTable(
                name: "ProjectsUser",
                newName: "projectsUsers");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectsUser_UsersUserId",
                table: "projectsUsers",
                newName: "IX_projectsUsers_UsersUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_projectsUsers",
                table: "projectsUsers",
                columns: new[] { "ProjectsProjectId", "UsersUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_projectsUsers_AllProjects_ProjectsProjectId",
                table: "projectsUsers",
                column: "ProjectsProjectId",
                principalTable: "AllProjects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_projectsUsers_user_UsersUserId",
                table: "projectsUsers",
                column: "UsersUserId",
                principalTable: "user",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projectsUsers_AllProjects_ProjectsProjectId",
                table: "projectsUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_projectsUsers_user_UsersUserId",
                table: "projectsUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_projectsUsers",
                table: "projectsUsers");

            migrationBuilder.RenameTable(
                name: "projectsUsers",
                newName: "ProjectsUser");

            migrationBuilder.RenameIndex(
                name: "IX_projectsUsers_UsersUserId",
                table: "ProjectsUser",
                newName: "IX_ProjectsUser_UsersUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectsUser",
                table: "ProjectsUser",
                columns: new[] { "ProjectsProjectId", "UsersUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsUser_AllProjects_ProjectsProjectId",
                table: "ProjectsUser",
                column: "ProjectsProjectId",
                principalTable: "AllProjects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsUser_user_UsersUserId",
                table: "ProjectsUser",
                column: "UsersUserId",
                principalTable: "user",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
