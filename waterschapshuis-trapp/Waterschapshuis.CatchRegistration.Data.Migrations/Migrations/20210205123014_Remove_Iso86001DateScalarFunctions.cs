using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class Remove_Iso86001DateScalarFunctions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20210205123014_Remove_Iso86001DateScalarFunctions.Up.Sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlScript("20210205123014_Remove_Iso86001DateScalarFunctions.Down.Sql");
        }
    }
}
