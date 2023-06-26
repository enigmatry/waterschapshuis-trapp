using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AlterHistorticalTraps_GeoFixCatchFiltering : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201217120054_AlterFunction_HistoricalTraps_GeoFixCatchFilter.Up.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20201217120054_AlterFunction_HistoricalTraps_GeoFixCatchFilter.Down.sql");
        }
    }
}
