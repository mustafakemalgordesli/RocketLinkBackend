using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RocketLink.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_Users_UserId",
                table: "Links");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Links",
                table: "Links");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Links",
                newName: "links",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_Links_UserId",
                schema: "dbo",
                table: "links",
                newName: "IX_links_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                schema: "dbo",
                table: "users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "dbo",
                table: "users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "dbo",
                table: "links",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                schema: "dbo",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_links",
                schema: "dbo",
                table: "links",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_links_users_UserId",
                schema: "dbo",
                table: "links",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_links_users_UserId",
                schema: "dbo",
                table: "links");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                schema: "dbo",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_links",
                schema: "dbo",
                table: "links");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "dbo",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "links",
                schema: "dbo",
                newName: "Links");

            migrationBuilder.RenameIndex(
                name: "IX_links_UserId",
                table: "Links",
                newName: "IX_Links_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Links",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Links",
                table: "Links",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Users_UserId",
                table: "Links",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
