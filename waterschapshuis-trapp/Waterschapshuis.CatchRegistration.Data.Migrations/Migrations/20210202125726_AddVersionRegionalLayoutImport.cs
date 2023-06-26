using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AddVersionRegionalLayoutImport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VersionRegionalLayoutImport",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    StartedBy = table.Column<string>(maxLength: 255, nullable: false),
                    StartedAt = table.Column<DateTimeOffset>(nullable: false),
                    FinishedAt = table.Column<DateTimeOffset>(nullable: true),
                    NextVersionRegionalLayoutName = table.Column<string>(maxLength: 30, nullable: false),
                    Output = table.Column<string>(maxLength: 30000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VersionRegionalLayoutImport", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VersionRegionalLayoutImport");
        }
    }
}
