using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace College.Migrations
{
    /// <inheritdoc />
    public partial class Intialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CandidateExams",
                columns: table => new
                {
                    CRollNo = table.Column<string>(type: "text", nullable: false),
                    CName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateExams", x => x.CRollNo);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubCode = table.Column<string>(type: "text", nullable: false),
                    SubTitle = table.Column<string>(type: "text", nullable: false),
                    FullMarks = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubCode);
                });

            migrationBuilder.CreateTable(
                name: "Marks",
                columns: table => new
                {
                    MarksId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CRollNo = table.Column<string>(type: "text", nullable: false),
                    SubCode = table.Column<string>(type: "text", nullable: false),
                    MarksObtained = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marks", x => x.MarksId);
                    table.ForeignKey(
                        name: "FK_Marks_CandidateExams_CRollNo",
                        column: x => x.CRollNo,
                        principalTable: "CandidateExams",
                        principalColumn: "CRollNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Marks_Subjects_SubCode",
                        column: x => x.SubCode,
                        principalTable: "Subjects",
                        principalColumn: "SubCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Marks_CRollNo",
                table: "Marks",
                column: "CRollNo");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_SubCode",
                table: "Marks",
                column: "SubCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Marks");

            migrationBuilder.DropTable(
                name: "CandidateExams");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
