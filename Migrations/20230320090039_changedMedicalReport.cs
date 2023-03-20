using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS_API.Migrations
{
    public partial class changedMedicalReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medical_Reports_Doctors_DoctorId",
                table: "Medical_Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Medical_Reports_Patients_PatientId",
                table: "Medical_Reports");

            migrationBuilder.DropIndex(
                name: "IX_Medical_Reports_DoctorId",
                table: "Medical_Reports");

            migrationBuilder.DropIndex(
                name: "IX_Medical_Reports_PatientId",
                table: "Medical_Reports");

            migrationBuilder.DropColumn(
                name: "DateTimeOfExamination",
                table: "Medical_Reports");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Medical_Reports");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Medical_Reports");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeOfExamination",
                table: "Medical_Reports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                table: "Medical_Reports",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PatientId",
                table: "Medical_Reports",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Medical_Reports_DoctorId",
                table: "Medical_Reports",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Medical_Reports_PatientId",
                table: "Medical_Reports",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medical_Reports_Doctors_DoctorId",
                table: "Medical_Reports",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medical_Reports_Patients_PatientId",
                table: "Medical_Reports",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
