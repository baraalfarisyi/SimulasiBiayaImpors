using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimulasiBiayaImpors.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BiayaImpors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KodeBarang = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    UraianBarang = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Bm = table.Column<int>(type: "int", nullable: false),
                    NilaiKomoditas = table.Column<float>(type: "real", nullable: false),
                    NilaiBm = table.Column<float>(type: "real", nullable: false),
                    WaktuInsert = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiayaImpors", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BiayaImpors");
        }
    }
}
