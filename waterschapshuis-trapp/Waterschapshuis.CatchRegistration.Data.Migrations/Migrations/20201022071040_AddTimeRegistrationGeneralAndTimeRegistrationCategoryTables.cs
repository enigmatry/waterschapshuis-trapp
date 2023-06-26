using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class AddTimeRegistrationGeneralAndTimeRegistrationCategoryTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeRegistrationCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeRegistrationCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeRegistrationGeneral",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false, defaultValue: new Guid("8207db25-94d1-4f3d-bf18-90da283221f7")),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false, defaultValue: new Guid("8207db25-94d1-4f3d-bf18-90da283221f7")),
                    UserId = table.Column<Guid>(nullable: false),
                    TimeRegistrationCategoryId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Hours = table.Column<double>(nullable: false),
                    Year = table.Column<int>(nullable: true, defaultValueSql: "2020"),
                    Period = table.Column<int>(nullable: true, defaultValueSql: "1"),
                    Week = table.Column<int>(nullable: true, defaultValueSql: "1"),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeRegistrationGeneral", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeRegistrationGeneral_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimeRegistrationGeneral_TimeRegistrationCategory_TimeRegistrationCategoryId",
                        column: x => x.TimeRegistrationCategoryId,
                        principalTable: "TimeRegistrationCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeRegistrationGeneral_User_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimeRegistrationGeneral_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TimeRegistrationCategory",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("935340b4-929e-46ce-f0f7-08d8765994e4"), "Administratie / ICT" },
                    { new Guid("4d773155-70bc-4c67-f6e3-08d8765994e4"), "Voorlichting" },
                    { new Guid("e6f9e5e6-47df-4c1b-f6ce-08d8765994e4"), "Seniorenverlof" },
                    { new Guid("64cbcfee-a513-4d62-f6b8-08d8765994e4"), "Reistijd Woon-Werk (CAP)" },
                    { new Guid("72b53fd0-c68f-4207-f6a1-08d8765994e4"), "Reistijd Werk-Werk" },
                    { new Guid("f3e776df-df8b-4b95-f688-08d8765994e4"), "Plusuren" },
                    { new Guid("cab4ded2-255f-4186-f673-08d8765994e4"), "Planning / Voortgangsrapportage" },
                    { new Guid("4a956e92-3d0c-484b-f65d-08d8765994e4"), "Personeelbijeenkomst" },
                    { new Guid("fac9ee8d-c03b-40b3-f647-08d8765994e4"), "Overleg" },
                    { new Guid("a20a5f9c-8b41-406c-f632-08d8765994e4"), "Overig" },
                    { new Guid("479a4375-6820-4d46-f61c-08d8765994e4"), "Ouderschapsverlof" },
                    { new Guid("4b983ec7-c03d-4930-f606-08d8765994e4"), "OR-uren" },
                    { new Guid("220d84d1-a3b0-4e90-f5ee-08d8765994e4"), "Materiaal / Onderhoud" },
                    { new Guid("dad170ee-8ac4-4867-f5cf-08d8765994e4"), "Kort verzuim" },
                    { new Guid("0ac78057-29d4-40db-f5ba-08d8765994e4"), "Dijkleger" },
                    { new Guid("cd4647ef-3f8f-4236-f593-08d8765994e4"), "Cursus" },
                    { new Guid("16dd1391-39c9-4e7a-f54d-08d8765994e4"), "Bijzonderverlof" },
                    { new Guid("82217e2b-2268-461c-f538-08d8765994e4"), "Basisverlof" },
                    { new Guid("ab2e247d-0d85-493d-f51f-08d8765994e4"), "Arbo" },
                    { new Guid("5972d9db-1c6a-4d4e-f4dc-08d8765994e4"), "Adv" },
                    { new Guid("8ac90922-ed2d-4de1-f6f9-08d8765994e4"), "Waterschap" },
                    { new Guid("71106f2a-e599-4030-f70e-08d8765994e4"), "Ziek" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegistrationGeneral_CreatedById",
                table: "TimeRegistrationGeneral",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Date",
                table: "TimeRegistrationGeneral",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegistrationGeneral_TimeRegistrationCategoryId",
                table: "TimeRegistrationGeneral",
                column: "TimeRegistrationCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegistrationGeneral_UpdatedById",
                table: "TimeRegistrationGeneral",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegistrationGeneral_UserId",
                table: "TimeRegistrationGeneral",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeRegistrationGeneral");

            migrationBuilder.DropTable(
                name: "TimeRegistrationCategory");
        }
    }
}
