using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RocketLink.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                schema: "dbo",
                table: "links",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                schema: "dbo",
                table: "links");
        }
    }
}
