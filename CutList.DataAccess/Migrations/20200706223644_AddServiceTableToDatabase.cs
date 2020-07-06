using Microsoft.EntityFrameworkCore.Migrations;

namespace CutList.DataAccess.Migrations
{
    public partial class AddServiceTableToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    price = table.Column<double>(nullable: false),
                    longDescription = table.Column<string>(nullable: true),
                    imageUrl = table.Column<string>(nullable: true),
                    JobId = table.Column<int>(nullable: false),
                    FrequencyId = table.Column<int>(nullable: false),
                    FrequncyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Service_Frequency_FrequncyId",
                        column: x => x.FrequncyId,
                        principalTable: "Frequency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Service_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Service_FrequncyId",
                table: "Service",
                column: "FrequncyId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_JobId",
                table: "Service",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Service");
        }
    }
}
