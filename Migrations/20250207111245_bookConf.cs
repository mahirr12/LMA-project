using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project___ConsoleApp__Library_Management_Application_.Migrations
{
    /// <inheritdoc />
    public partial class bookConf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LoanItems_BookId",
                table: "LoanItems");

            migrationBuilder.CreateIndex(
                name: "IX_LoanItems_BookId",
                table: "LoanItems",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LoanItems_BookId",
                table: "LoanItems");

            migrationBuilder.CreateIndex(
                name: "IX_LoanItems_BookId",
                table: "LoanItems",
                column: "BookId",
                unique: true);
        }
    }
}
