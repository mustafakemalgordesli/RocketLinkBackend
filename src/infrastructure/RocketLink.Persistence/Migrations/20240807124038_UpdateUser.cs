using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RocketLink.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_Email",
                schema: "dbo",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_Username",
                schema: "dbo",
                table: "users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                schema: "dbo",
                table: "users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_Username",
                schema: "dbo",
                table: "users",
                column: "Username",
                unique: true);
        }
    }
}
