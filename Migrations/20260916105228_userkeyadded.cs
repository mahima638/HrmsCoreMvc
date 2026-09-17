using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HrmsCoreMvc.Migrations
{
    /// <inheritdoc />
    public partial class userkeyadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_promotion_user_UserId",
                table: "promotion");

            migrationBuilder.DropForeignKey(
                name: "FK_resignations_department_DepartmentId",
                table: "resignations");

            migrationBuilder.DropForeignKey(
                name: "FK_resignations_user_UserId",
                table: "resignations");

            migrationBuilder.DropForeignKey(
                name: "FK_terminations_user_UserId",
                table: "terminations");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Training",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Training",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "role",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "role",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "designation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Training_UserId",
                table: "Training",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_promotion_user_UserId",
                table: "promotion",
                column: "UserId",
                principalTable: "user",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_resignations_department_DepartmentId",
                table: "resignations",
                column: "DepartmentId",
                principalTable: "department",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_resignations_user_UserId",
                table: "resignations",
                column: "UserId",
                principalTable: "user",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_terminations_user_UserId",
                table: "terminations",
                column: "UserId",
                principalTable: "user",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Training_user_UserId",
                table: "Training",
                column: "UserId",
                principalTable: "user",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_promotion_user_UserId",
                table: "promotion");

            migrationBuilder.DropForeignKey(
                name: "FK_resignations_department_DepartmentId",
                table: "resignations");

            migrationBuilder.DropForeignKey(
                name: "FK_resignations_user_UserId",
                table: "resignations");

            migrationBuilder.DropForeignKey(
                name: "FK_terminations_user_UserId",
                table: "terminations");

            migrationBuilder.DropForeignKey(
                name: "FK_Training_user_UserId",
                table: "Training");

            migrationBuilder.DropIndex(
                name: "IX_Training_UserId",
                table: "Training");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Training",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Training",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "role",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "role",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "designation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_promotion_user_UserId",
                table: "promotion",
                column: "UserId",
                principalTable: "user",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_resignations_department_DepartmentId",
                table: "resignations",
                column: "DepartmentId",
                principalTable: "department",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_resignations_user_UserId",
                table: "resignations",
                column: "UserId",
                principalTable: "user",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_terminations_user_UserId",
                table: "terminations",
                column: "UserId",
                principalTable: "user",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
