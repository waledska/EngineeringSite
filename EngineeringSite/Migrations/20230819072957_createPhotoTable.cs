using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EngineeringSite.Migrations
{
    public partial class createPhotoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photo",
                table: "jobsFinisheds");

            migrationBuilder.CreateTable(
                name: "jobFinishedPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jobsFinishedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jobFinishedPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_jobFinishedPhotos_jobsFinisheds_jobsFinishedId",
                        column: x => x.jobsFinishedId,
                        principalTable: "jobsFinisheds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_jobFinishedPhotos_jobsFinishedId",
                table: "jobFinishedPhotos",
                column: "jobsFinishedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "jobFinishedPhotos");

            migrationBuilder.AddColumn<string>(
                name: "photo",
                table: "jobsFinisheds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
