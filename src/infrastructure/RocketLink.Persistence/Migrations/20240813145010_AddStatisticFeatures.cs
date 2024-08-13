using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RocketLink.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddStatisticFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViewsCount",
                schema: "dbo",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClickCount",
                schema: "dbo",
                table: "links",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewsCount",
                schema: "dbo",
                table: "users");

            migrationBuilder.DropColumn(
                name: "ClickCount",
                schema: "dbo",
                table: "links");
        }
    }
}
