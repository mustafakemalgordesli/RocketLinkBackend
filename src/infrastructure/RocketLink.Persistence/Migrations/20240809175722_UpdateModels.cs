using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RocketLink.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Firstname",
                schema: "dbo",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                schema: "dbo",
                table: "users",
                newName: "Fullname");

            migrationBuilder.RenameColumn(
                name: "IconUrl",
                schema: "dbo",
                table: "links",
                newName: "IconCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fullname",
                schema: "dbo",
                table: "users",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "IconCode",
                schema: "dbo",
                table: "links",
                newName: "IconUrl");

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                schema: "dbo",
                table: "users",
                type: "text",
                nullable: true);
        }
    }
}
