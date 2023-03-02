using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS_API.Migrations
{
    public partial class UpdatedTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Recommended_Tests_RecommendedTestRTId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_RecommendedTestRTId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "RecommendedTestRTId",
                table: "Tests");

            migrationBuilder.CreateTable(
                name: "RecommendedTestTest",
                columns: table => new
                {
                    RecommendedTestsRTId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestsTestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendedTestTest", x => new { x.RecommendedTestsRTId, x.TestsTestId });
                    table.ForeignKey(
                        name: "FK_RecommendedTestTest_Recommended_Tests_RecommendedTestsRTId",
                        column: x => x.RecommendedTestsRTId,
                        principalTable: "Recommended_Tests",
                        principalColumn: "RTId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecommendedTestTest_Tests_TestsTestId",
                        column: x => x.TestsTestId,
                        principalTable: "Tests",
                        principalColumn: "TestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecommendedTestTest_TestsTestId",
                table: "RecommendedTestTest",
                column: "TestsTestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecommendedTestTest");

            migrationBuilder.AddColumn<Guid>(
                name: "RecommendedTestRTId",
                table: "Tests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_RecommendedTestRTId",
                table: "Tests",
                column: "RecommendedTestRTId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Recommended_Tests_RecommendedTestRTId",
                table: "Tests",
                column: "RecommendedTestRTId",
                principalTable: "Recommended_Tests",
                principalColumn: "RTId");
        }
    }
}
