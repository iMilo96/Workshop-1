using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workshop.Backend.Migrations
{
    /// <inheritdoc />
    public partial class FixLastNameIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_FirstName",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_LastName",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FirstName",
                table: "Employees",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_LastName",
                table: "Employees",
                column: "LastName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_FirstName",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_LastName",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FirstName",
                table: "Employees",
                column: "FirstName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_LastName",
                table: "Employees",
                column: "LastName",
                unique: true);
        }
    }
}
