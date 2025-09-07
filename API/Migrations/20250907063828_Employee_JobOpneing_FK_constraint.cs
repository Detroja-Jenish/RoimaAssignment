using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Employee_JobOpneing_FK_constraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOpenings_Employees_CreatedByEmployeeEmployeeID",
                table: "JobOpenings");

            migrationBuilder.DropIndex(
                name: "IX_JobOpenings_CreatedByEmployeeEmployeeID",
                table: "JobOpenings");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeEmployeeID",
                table: "JobOpenings");

            migrationBuilder.CreateIndex(
                name: "IX_JobOpenings_CreatedBy",
                table: "JobOpenings",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOpenings_Employees_CreatedBy",
                table: "JobOpenings",
                column: "CreatedBy",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOpenings_Employees_CreatedBy",
                table: "JobOpenings");

            migrationBuilder.DropIndex(
                name: "IX_JobOpenings_CreatedBy",
                table: "JobOpenings");

            migrationBuilder.AddColumn<int>(
                name: "CreatedByEmployeeEmployeeID",
                table: "JobOpenings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JobOpenings_CreatedByEmployeeEmployeeID",
                table: "JobOpenings",
                column: "CreatedByEmployeeEmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOpenings_Employees_CreatedByEmployeeEmployeeID",
                table: "JobOpenings",
                column: "CreatedByEmployeeEmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
