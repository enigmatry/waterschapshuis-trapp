using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class Add_Table_ScheduledJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduledJob",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    State = table.Column<int>(nullable: false),
                    StartedAt = table.Column<DateTimeOffset>(nullable: true),
                    FinishedAt = table.Column<DateTimeOffset>(nullable: true),
                    Params = table.Column<string>(nullable: false),
                    Output = table.Column<string>(maxLength: 30000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledJob", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledJob");
        }
    }
}
