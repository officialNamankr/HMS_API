using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS_API.Migrations
{
    public partial class AddedAllTheTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date_Of_Appointment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time_Of_Appointment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Recommended_Tests",
                columns: table => new
                {
                    RTId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommended_Tests", x => x.RTId);
                });

            migrationBuilder.CreateTable(
                name: "Medical_Reports",
                columns: table => new
                {
                    MedicalReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTimeOfExamination = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecommendedTestRTId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medical_Reports", x => x.MedicalReportId);
                    table.ForeignKey(
                        name: "FK_Medical_Reports_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Medical_Reports_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Medical_Reports_Recommended_Tests_RecommendedTestRTId",
                        column: x => x.RecommendedTestRTId,
                        principalTable: "Recommended_Tests",
                        principalColumn: "RTId");
                });

            migrationBuilder.CreateTable(
                name: "Test_Reports",
                columns: table => new
                {
                    TestReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecommendedTestRTId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test_Reports", x => x.TestReportId);
                    table.ForeignKey(
                        name: "FK_Test_Reports_Recommended_Tests_RecommendedTestRTId",
                        column: x => x.RecommendedTestRTId,
                        principalTable: "Recommended_Tests",
                        principalColumn: "RTId");
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    TestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecommendedTestRTId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.TestId);
                    table.ForeignKey(
                        name: "FK_Tests_Recommended_Tests_RecommendedTestRTId",
                        column: x => x.RecommendedTestRTId,
                        principalTable: "Recommended_Tests",
                        principalColumn: "RTId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Medical_Reports_DoctorId",
                table: "Medical_Reports",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Medical_Reports_PatientId",
                table: "Medical_Reports",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Medical_Reports_RecommendedTestRTId",
                table: "Medical_Reports",
                column: "RecommendedTestRTId");

            migrationBuilder.CreateIndex(
                name: "IX_Test_Reports_RecommendedTestRTId",
                table: "Test_Reports",
                column: "RecommendedTestRTId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_RecommendedTestRTId",
                table: "Tests",
                column: "RecommendedTestRTId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Medical_Reports");

            migrationBuilder.DropTable(
                name: "Test_Reports");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "Recommended_Tests");
        }
    }
}
