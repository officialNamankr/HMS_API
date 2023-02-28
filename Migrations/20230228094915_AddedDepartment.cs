using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS_API.Migrations
{
    public partial class AddedDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentDoctor",
                columns: table => new
                {
                    DepartmentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorsDoctorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentDoctor", x => new { x.DepartmentsId, x.DoctorsDoctorId });
                    table.ForeignKey(
                        name: "FK_DepartmentDoctor_Departments_DepartmentsId",
                        column: x => x.DepartmentsId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentDoctor_Doctors_DoctorsDoctorId",
                        column: x => x.DoctorsDoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentDoctor_DoctorsDoctorId",
                table: "DepartmentDoctor",
                column: "DoctorsDoctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentDoctor");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
