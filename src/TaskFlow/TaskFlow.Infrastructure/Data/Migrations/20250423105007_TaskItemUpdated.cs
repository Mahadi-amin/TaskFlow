using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskFlow.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class TaskItemUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskDependencies_TaskItem_PrerequisiteTaskId",
                table: "TaskDependencies");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskDependencies_TaskItem_TaskItemId",
                table: "TaskDependencies");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_Statuses_StatusId",
                table: "TaskItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskItem",
                table: "TaskItem");

            migrationBuilder.RenameTable(
                name: "TaskItem",
                newName: "TaskItems");

            migrationBuilder.RenameIndex(
                name: "IX_TaskItem_StatusId",
                table: "TaskItems",
                newName: "IX_TaskItems_StatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskItems",
                table: "TaskItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDependencies_TaskItems_PrerequisiteTaskId",
                table: "TaskDependencies",
                column: "PrerequisiteTaskId",
                principalTable: "TaskItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDependencies_TaskItems_TaskItemId",
                table: "TaskDependencies",
                column: "TaskItemId",
                principalTable: "TaskItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_Statuses_StatusId",
                table: "TaskItems",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskDependencies_TaskItems_PrerequisiteTaskId",
                table: "TaskDependencies");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskDependencies_TaskItems_TaskItemId",
                table: "TaskDependencies");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_Statuses_StatusId",
                table: "TaskItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskItems",
                table: "TaskItems");

            migrationBuilder.RenameTable(
                name: "TaskItems",
                newName: "TaskItem");

            migrationBuilder.RenameIndex(
                name: "IX_TaskItems_StatusId",
                table: "TaskItem",
                newName: "IX_TaskItem_StatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskItem",
                table: "TaskItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDependencies_TaskItem_PrerequisiteTaskId",
                table: "TaskDependencies",
                column: "PrerequisiteTaskId",
                principalTable: "TaskItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDependencies_TaskItem_TaskItemId",
                table: "TaskDependencies",
                column: "TaskItemId",
                principalTable: "TaskItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_Statuses_StatusId",
                table: "TaskItem",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
