using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class LabelOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Label_Notes_NoteId",
                table: "Label");

            migrationBuilder.DropForeignKey(
                name: "FK_Label_User_UserId",
                table: "Label");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Label",
                table: "Label");

            migrationBuilder.RenameTable(
                name: "Label",
                newName: "Labels");

            migrationBuilder.RenameIndex(
                name: "IX_Label_UserId",
                table: "Labels",
                newName: "IX_Labels_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Label_NoteId",
                table: "Labels",
                newName: "IX_Labels_NoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Labels",
                table: "Labels",
                column: "LabelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_Notes_NoteId",
                table: "Labels",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_User_UserId",
                table: "Labels",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Labels_Notes_NoteId",
                table: "Labels");

            migrationBuilder.DropForeignKey(
                name: "FK_Labels_User_UserId",
                table: "Labels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Labels",
                table: "Labels");

            migrationBuilder.RenameTable(
                name: "Labels",
                newName: "Label");

            migrationBuilder.RenameIndex(
                name: "IX_Labels_UserId",
                table: "Label",
                newName: "IX_Label_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Labels_NoteId",
                table: "Label",
                newName: "IX_Label_NoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Label",
                table: "Label",
                column: "LabelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Label_Notes_NoteId",
                table: "Label",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Label_User_UserId",
                table: "Label",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
