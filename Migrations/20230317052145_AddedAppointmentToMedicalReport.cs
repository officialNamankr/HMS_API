using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS_API.Migrations
{
    public partial class AddedAppointmentToMedicalReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppointmentId",
                table: "Medical_Reports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Medical_Reports_AppointmentId",
                table: "Medical_Reports",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medical_Reports_Appointments_AppointmentId",
                table: "Medical_Reports",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "AppointmentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medical_Reports_Appointments_AppointmentId",
                table: "Medical_Reports");

            migrationBuilder.DropIndex(
                name: "IX_Medical_Reports_AppointmentId",
                table: "Medical_Reports");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "Medical_Reports");
        }
    }
}
