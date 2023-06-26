using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatchType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    IsByCatch = table.Column<bool>(nullable: false),
                    AnimalType = table.Column<byte>(type: "tinyint", nullable: false),
                    Order = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatchType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FieldTest",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    StartYear = table.Column<int>(nullable: true, defaultValueSql: "2020"),
                    StartPeriod = table.Column<int>(nullable: true, defaultValueSql: "1"),
                    EndYear = table.Column<int>(nullable: true, defaultValueSql: "2020"),
                    EndPeriod = table.Column<int>(nullable: true, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldTest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HourSquare",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 5, nullable: false),
                    Geometry = table.Column<Geometry>(type: "geometry", nullable: false),
                    CpeAB = table.Column<double>(nullable: true),
                    CpeRecent = table.Column<double>(nullable: true),
                    PopulationPresent = table.Column<bool>(nullable: true),
                    R2 = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HourSquare", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationEventLog",
                columns: table => new
                {
                    EventId = table.Column<Guid>(nullable: false),
                    EventTypeName = table.Column<string>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    TimesSent = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Content = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationEventLog", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ShortName = table.Column<string>(maxLength: 10, nullable: false),
                    Geometry = table.Column<Geometry>(type: "geometry", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Geometry = table.Column<Geometry>(type: "geometry", nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportTemplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Group = table.Column<string>(maxLength: 50, nullable: false),
                    RouteUri = table.Column<string>(maxLength: 250, nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    Key = table.Column<string>(maxLength: 50, nullable: true),
                    ExportFileName = table.Column<string>(maxLength: 250, nullable: true),
                    Content = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Exported = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    DisplayOrderIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrackingLine",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Polyline = table.Column<MultiLineString>(type: "geometry", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingLine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrappingType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrappingType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FieldTestHourSquare",
                columns: table => new
                {
                    FieldTestId = table.Column<Guid>(nullable: false),
                    HourSquareId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldTestHourSquare", x => new { x.FieldTestId, x.HourSquareId });
                    table.ForeignKey(
                        name: "FK_FieldTestHourSquare_FieldTest_FieldTestId",
                        column: x => x.FieldTestId,
                        principalTable: "FieldTest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FieldTestHourSquare_HourSquare_HourSquareId",
                        column: x => x.HourSquareId,
                        principalTable: "HourSquare",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rayon",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Geometry = table.Column<Geometry>(type: "geometry", nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rayon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rayon_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    ExternalId = table.Column<long>(nullable: true),
                    Surname = table.Column<string>(maxLength: 255, nullable: true),
                    GivenName = table.Column<string>(maxLength: 255, nullable: true),
                    Authorized = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    ObjectId = table.Column<Guid>(nullable: true),
                    ApplicationId = table.Column<Guid>(nullable: true),
                    Version = table.Column<string>(maxLength: 5, nullable: true),
                    OrganizationId = table.Column<Guid>(nullable: true),
                    ConfidentialityConfirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_User_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WaterAuthority",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    CodeUvw = table.Column<string>(maxLength: 10, nullable: false),
                    Geometry = table.Column<Geometry>(type: "geometry", nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterAuthority", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WaterAuthority_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => new { x.PermissionId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrapType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    TrappingTypeId = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Order = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrapType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrapType_TrappingType_TrappingTypeId",
                        column: x => x.TrappingTypeId,
                        principalTable: "TrappingType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatchArea",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Geometry = table.Column<Geometry>(type: "geometry", nullable: false),
                    RayonId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatchArea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatchArea_Rayon_RayonId",
                        column: x => x.RayonId,
                        principalTable: "Rayon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tracking",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false),
                    RecordedOn = table.Column<DateTimeOffset>(nullable: false),
                    Location = table.Column<Point>(type: "geometry", nullable: false),
                    TrappingTypeId = table.Column<Guid>(nullable: false),
                    SessionId = table.Column<Guid>(nullable: false),
                    IsTimewriting = table.Column<bool>(nullable: false),
                    IsTrackingMap = table.Column<bool>(nullable: false),
                    isTrackingPrivate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tracking_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tracking_TrappingType_TrappingTypeId",
                        column: x => x.TrappingTypeId,
                        principalTable: "TrappingType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tracking_User_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrapTypeTrapStatusStyle",
                columns: table => new
                {
                    TrapStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    TrapTypeId = table.Column<Guid>(nullable: false),
                    IconName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrapTypeTrapStatusStyle", x => new { x.TrapTypeId, x.TrapStatus });
                    table.ForeignKey(
                        name: "FK_TrapTypeTrapStatusStyle_TrapType_TrapTypeId",
                        column: x => x.TrapTypeId,
                        principalTable: "TrapType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubArea",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 10, nullable: false),
                    Geometry = table.Column<Geometry>(type: "geometry", nullable: false),
                    CatchAreaId = table.Column<Guid>(nullable: false),
                    WaterAuthorityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubArea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubArea_CatchArea_CatchAreaId",
                        column: x => x.CatchAreaId,
                        principalTable: "CatchArea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubArea_WaterAuthority_WaterAuthorityId",
                        column: x => x.WaterAuthorityId,
                        principalTable: "WaterAuthority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubAreaHourSquare",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SubAreaId = table.Column<Guid>(nullable: false),
                    HourSquareId = table.Column<Guid>(nullable: false),
                    KmWaterway = table.Column<int>(nullable: false),
                    PercentageDitch = table.Column<byte>(type: "tinyint", nullable: false),
                    Ditch = table.Column<double>(nullable: false),
                    WetDitch = table.Column<double>(nullable: false),
                    Geometry = table.Column<Geometry>(type: "geometry", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubAreaHourSquare", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubAreaHourSquare_HourSquare_HourSquareId",
                        column: x => x.HourSquareId,
                        principalTable: "HourSquare",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubAreaHourSquare_SubArea_SubAreaId",
                        column: x => x.SubAreaId,
                        principalTable: "SubArea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Observation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false),
                    RecordedOn = table.Column<DateTimeOffset>(nullable: false),
                    SubAreaHourSquareId = table.Column<Guid>(nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    PhotoUrl = table.Column<string>(maxLength: 1024, nullable: true),
                    Remarks = table.Column<string>(maxLength: 255, nullable: false),
                    Location = table.Column<Point>(type: "geometry", nullable: false),
                    Archived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Observation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Observation_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Observation_SubAreaHourSquare_SubAreaHourSquareId",
                        column: x => x.SubAreaHourSquareId,
                        principalTable: "SubAreaHourSquare",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Observation_User_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TimeRegistration",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false, defaultValue: new Guid("8207db25-94d1-4f3d-bf18-90da283221f7")),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false, defaultValue: new Guid("8207db25-94d1-4f3d-bf18-90da283221f7")),
                    UserId = table.Column<Guid>(nullable: false),
                    SubAreaHourSquareId = table.Column<Guid>(nullable: false),
                    TrappingTypeId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Hours = table.Column<double>(nullable: false),
                    Year = table.Column<int>(nullable: true, defaultValueSql: "2020"),
                    Period = table.Column<int>(nullable: true, defaultValueSql: "1"),
                    Week = table.Column<int>(nullable: true, defaultValueSql: "1"),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeRegistration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeRegistration_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimeRegistration_SubAreaHourSquare_SubAreaHourSquareId",
                        column: x => x.SubAreaHourSquareId,
                        principalTable: "SubAreaHourSquare",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeRegistration_TrappingType_TrappingTypeId",
                        column: x => x.TrappingTypeId,
                        principalTable: "TrappingType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeRegistration_User_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimeRegistration_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trap",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false),
                    RecordedOn = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "getutcdate()"),
                    ExternalId = table.Column<string>(maxLength: 50, nullable: true),
                    Location = table.Column<Point>(type: "geometry", nullable: false),
                    NumberOfTraps = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 250, nullable: true),
                    RemovedOn = table.Column<DateTimeOffset>(nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    ProvinceId = table.Column<Guid>(nullable: true),
                    SubAreaHourSquareId = table.Column<Guid>(nullable: false),
                    TrapTypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trap_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trap_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trap_SubAreaHourSquare_SubAreaHourSquareId",
                        column: x => x.SubAreaHourSquareId,
                        principalTable: "SubAreaHourSquare",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trap_TrapType_TrapTypeId",
                        column: x => x.TrapTypeId,
                        principalTable: "TrapType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trap_User_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Catch",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false),
                    RecordedOn = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "getutcdate()"),
                    TrapId = table.Column<Guid>(nullable: false),
                    CatchTypeId = table.Column<Guid>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: true, defaultValueSql: "2020"),
                    Period = table.Column<int>(nullable: true, defaultValueSql: "1"),
                    Week = table.Column<int>(nullable: true, defaultValueSql: "1"),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catch_CatchType_CatchTypeId",
                        column: x => x.CatchTypeId,
                        principalTable: "CatchType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Catch_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Catch_Trap_TrapId",
                        column: x => x.TrapId,
                        principalTable: "Trap",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Catch_User_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrapHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false),
                    RecordedOn = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "getutcdate()"),
                    Message = table.Column<string>(maxLength: 250, nullable: false),
                    TrapId = table.Column<Guid>(nullable: false),
                    CatchId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrapHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrapHistory_Catch_CatchId",
                        column: x => x.CatchId,
                        principalTable: "Catch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrapHistory_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrapHistory_Trap_TrapId",
                        column: x => x.TrapId,
                        principalTable: "Trap",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrapHistory_User_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "CatchType",
                columns: new[] { "Id", "AnimalType", "IsByCatch", "Name", "Order" },
                values: new object[,]
                {
                    { new Guid("a4a5a1f2-a14d-9590-1d81-e29d858a1475"), (byte)1, true, "woelrat", (byte)1 },
                    { new Guid("37ceec1c-7a34-7927-5932-b03448247b44"), (byte)3, true, "spiegelkarper", (byte)92 },
                    { new Guid("8b124eb4-8c67-91e4-55cd-95cf3e406937"), (byte)3, true, "kopvoorn", (byte)91 },
                    { new Guid("1370ee41-7fca-a1d9-6b68-eca6cc076bc3"), (byte)3, true, "bittervoorn", (byte)90 },
                    { new Guid("57faaf49-108b-78e9-6373-97a021c3062f"), (byte)3, true, "amer. hondsvis", (byte)89 },
                    { new Guid("3fbc8687-644c-09b5-1f38-32b03f0c39e3"), (byte)3, true, "bruine amerikaanse", (byte)88 },
                    { new Guid("06d350b0-378c-8d23-3c7b-31ab92d65d39"), (byte)3, true, "pos", (byte)87 },
                    { new Guid("fc5d5c74-34f1-1263-571f-170a1e6f483e"), (byte)3, true, "kwabaal", (byte)86 },
                    { new Guid("fc3a6849-43ff-1750-1e6f-291eab227230"), (byte)3, true, "bot", (byte)85 },
                    { new Guid("4b29436b-92ec-3a3f-105d-4f9c53b092d8"), (byte)3, true, "schol", (byte)84 },
                    { new Guid("3005e270-97ae-68ff-919a-99347b9d949f"), (byte)3, true, "karper", (byte)83 },
                    { new Guid("cb478141-a50f-0a25-2eee-de00eae81410"), (byte)3, true, "snoekbaars", (byte)82 },
                    { new Guid("2094367b-41cf-0357-3985-37be1759918b"), (byte)3, true, "inheemse meerval", (byte)81 },
                    { new Guid("aa139422-996c-7970-3f81-cc52421f42db"), (byte)3, true, "grote modderkruiper", (byte)80 },
                    { new Guid("7c16c5cd-93b9-315d-2096-d214301b5d30"), (byte)3, true, "baars", (byte)78 },
                    { new Guid("6aa19e7f-74fe-42a8-3256-1ef358538c6f"), (byte)3, true, "brasem", (byte)77 },
                    { new Guid("fd1a6eea-28b5-8718-4c9f-df5822301e64"), (byte)3, true, "aal", (byte)76 },
                    { new Guid("b3142f85-881e-58d1-9550-1f8bc58a1d3e"), (byte)3, true, "ruisvoorn", (byte)75 },
                    { new Guid("67dd1253-a69f-8755-50ec-4a8e431089bc"), (byte)3, true, "voorn", (byte)74 },
                    { new Guid("7f11f101-04d3-4ec5-3924-a379686b5542"), (byte)3, true, "blankvoorn", (byte)73 },
                    { new Guid("045164e9-6606-7c9a-8976-05c6f62a4c95"), (byte)3, true, "zeelt", (byte)72 },
                    { new Guid("f863a895-35c3-6432-7f93-235ad27456f5"), (byte)3, true, "snoek", (byte)71 },
                    { new Guid("f296f546-13f4-2172-51dd-6394577831bc"), (byte)2, true, "canadese gans", (byte)70 },
                    { new Guid("fe25db3d-2bf6-a7a6-6272-7b50468f1aa7"), (byte)2, true, "geoorde fuut", (byte)69 },
                    { new Guid("6bc3a885-66a0-8f35-1142-4d06703a4f46"), (byte)2, true, "smient", (byte)68 },
                    { new Guid("8b563a94-516a-19db-4d96-3dfda633042b"), (byte)2, true, "zwarte zwaan", (byte)67 },
                    { new Guid("59fb893b-9f4d-5f83-59e2-1748451739dc"), (byte)2, true, "tafeleend", (byte)66 },
                    { new Guid("f6d9670b-4fcc-a0bd-00f3-43dafb6c5f3b"), (byte)2, true, "bruine kiekendief", (byte)65 },
                    { new Guid("ae71b03c-0f45-010b-2966-e860104c698e"), (byte)3, true, "graskarper", (byte)93 },
                    { new Guid("bdd0ccf0-4ef4-4e18-6f0e-3db98690a6dd"), (byte)3, true, "zilverkarper", (byte)94 },
                    { new Guid("bdbf0caa-64b5-64cb-87b1-a489c6598af3"), (byte)3, true, "kroeskarper", (byte)95 },
                    { new Guid("5c71eaae-3a9e-3276-7e35-1dec90b19ab4"), (byte)3, true, "kleine modderkruiper", (byte)96 },
                    { new Guid("c8783519-41c6-5654-1977-f6956aba2ef4"), (byte)1, false, "Muskusrat", (byte)254 },
                    { new Guid("44711e96-25b8-0af6-669b-ccdc8aba9017"), (byte)1, false, "Muskusrat moer jong (<1jr)", (byte)253 },
                    { new Guid("645f7089-7f21-50c5-30c4-5fe30cc693f1"), (byte)1, false, "Muskusrat moer oud (>1jr)", (byte)252 },
                    { new Guid("e72ccb01-65bb-a1aa-a5e8-eb909fe77374"), (byte)1, false, "Muskusrat ram jong (<1jr)", (byte)251 },
                    { new Guid("3d1358f4-61d4-21d8-9438-90096eeea47e"), (byte)1, false, "Muskusrat ram oud (>1jr)", (byte)250 },
                    { new Guid("49b51935-918b-5a38-2493-a4141fef8c52"), (byte)1, false, "Beverrat", (byte)249 },
                    { new Guid("7a8199e8-21df-7556-1f0a-549e94645b6f"), (byte)1, false, "Beverrat moer jong (<1jr)", (byte)248 },
                    { new Guid("85803328-15e7-92ef-528f-00e91b6d4815"), (byte)1, false, "Beverrat moer oud (>1jr)", (byte)247 },
                    { new Guid("2539b02a-9298-7b9f-4273-3e8ac99d7c63"), (byte)1, false, "Beverrat ram jong (<1jr)", (byte)246 },
                    { new Guid("8957cb9d-936c-29cb-8511-a9c9a7ec6a7e"), (byte)1, false, "Beverrat ram oud (>1jr)", (byte)245 },
                    { new Guid("7dc99bf1-251c-4bd0-a359-9a168f6a6af4"), (byte)4, true, "Marmerkreeft", (byte)114 },
                    { new Guid("76fa1418-960d-4f33-adfe-d2603d047348"), (byte)4, true, "Geknobbelde Amerikaanse rivierkreeft", (byte)113 },
                    { new Guid("67800503-61ce-48b1-bb9d-37d1943a545b"), (byte)4, true, "Gevlekte Amerikaanse rivierkreeft", (byte)112 },
                    { new Guid("3f4f77f6-19f1-90c0-534c-223c58d164b5"), (byte)2, true, "zwaan", (byte)64 },
                    { new Guid("f6b62b18-7dc6-4548-8a0d-5151f2e41ec4"), (byte)4, true, "Californische rivierkreeft", (byte)111 },
                    { new Guid("c6e6943d-89aa-42e3-a096-f18911968095"), (byte)4, true, "Gestreepte Amerikaanse rivierkreeft", (byte)109 },
                    { new Guid("0042cfb2-1624-4af2-90dd-149d46f7c683"), (byte)4, true, "Europese rivierkreeft", (byte)108 },
                    { new Guid("2db0b44e-8ec5-41f2-befc-89fe1c05c17c"), (byte)4, true, "Turkse rivierkreeft", (byte)107 },
                    { new Guid("d0994e03-6d7f-9309-574d-687c36eb1499"), (byte)4, true, "inheemse rivierkreeft", (byte)106 },
                    { new Guid("7d2e51c9-36cb-4f2b-6859-d65cc3ee0124"), (byte)4, true, "zoetwatermossel", (byte)105 },
                    { new Guid("78bc4319-368c-1324-8e8e-711e40210508"), (byte)4, true, "bruine kikker", (byte)104 },
                    { new Guid("7fc8c7c6-8db1-8b20-a566-ed0c83ad94ba"), (byte)4, true, "gewone pad", (byte)103 },
                    { new Guid("d913aacb-1e93-37b5-6e36-926d319e84b2"), (byte)4, true, "groene kikker", (byte)102 },
                    { new Guid("81a64eb4-335a-0d5f-a378-96c3cb5847d8"), (byte)4, true, "brulkikker", (byte)101 },
                    { new Guid("7bbef5b0-0a32-74f1-44ee-a2395dba74d8"), (byte)4, true, "roodwangschildpad", (byte)100 },
                    { new Guid("856e5b58-14f3-6131-3eaa-976715f8a642"), (byte)4, true, "wolhandkrab", (byte)99 },
                    { new Guid("e31e1410-8cbf-0bbd-29b7-e2290f811c04"), (byte)4, true, "Amerikaanese rivierkreeft", (byte)98 },
                    { new Guid("b4302d39-5657-5c64-63ac-91f7f1ba9d51"), (byte)3, true, "riviergrondel", (byte)97 },
                    { new Guid("2497dea4-f0c8-40ba-94d6-13cc3dd60682"), (byte)4, true, "Rode Amerikaanse rivierkreeft", (byte)110 },
                    { new Guid("cd343cbf-9f02-4955-340b-daf38c3c95fd"), (byte)2, true, "grutto", (byte)63 },
                    { new Guid("79ee016d-92c3-6f74-5e4d-0405346f92fa"), (byte)3, true, "kolblei", (byte)79 },
                    { new Guid("448b7701-491f-9d9e-1d8f-399e9fc74ec2"), (byte)2, true, "roerdomp", (byte)61 },
                    { new Guid("49f3fa88-8c51-a558-667f-e79da80d7495"), (byte)2, true, "tamme eend", (byte)29 },
                    { new Guid("1fa69307-35cd-24e0-62cb-83d21e6f706d"), (byte)2, true, "blauwe reiger", (byte)28 },
                    { new Guid("dfcf25f4-37b7-85af-0486-8cc7f76475a1"), (byte)2, true, "waterral", (byte)27 },
                    { new Guid("b7691621-97fa-26f9-86bc-d2d537e92d59"), (byte)2, true, "dodaars", (byte)26 },
                    { new Guid("eb6d4c1d-333a-538f-4228-92151f6c11aa"), (byte)2, true, "fuut", (byte)25 },
                    { new Guid("386c68bb-76e0-5847-8717-e876399e0d61"), (byte)2, true, "grauwe gans", (byte)24 },
                    { new Guid("52529727-0e5c-07c0-2f00-173f081834d2"), (byte)2, true, "meerkoet", (byte)23 },
                    { new Guid("e2406e40-63b0-8926-5f49-185513c03e90"), (byte)2, true, "waterhoen", (byte)22 },
                    { new Guid("843d3051-8f82-1df3-6e73-bfba9cba15cf"), (byte)2, true, "aalscholver", (byte)21 },
                    { new Guid("053210c7-1491-0255-a36a-223fab8780bd"), (byte)2, true, "wilde eend", (byte)20 },
                    { new Guid("50cc209c-9168-905c-391c-8641ec5a887a"), (byte)1, true, "otter", (byte)19 },
                    { new Guid("c1a5586a-5416-442e-a6d4-e8d001302c14"), (byte)1, true, "boommarter", (byte)18 },
                    { new Guid("b2f24fb5-359d-2116-9520-5bd38a0522fe"), (byte)1, true, "vos", (byte)17 },
                    { new Guid("c0d3288c-24b4-57b4-30e7-9d37de42909e"), (byte)2, true, "zwarte kraai", (byte)30 },
                    { new Guid("5b2131fe-743d-2467-477a-b7afbf181e81"), (byte)1, true, "das", (byte)16 },
                    { new Guid("ca585bdf-495b-6ba2-62dc-a68d66202a6f"), (byte)1, true, "eekhoorn", (byte)14 },
                    { new Guid("a7efd4df-703e-8abf-6767-f24760cb2c1d"), (byte)1, true, "haas", (byte)13 },
                    { new Guid("7aba1245-13d7-0317-8307-4009eb3b51bf"), (byte)1, true, "konijn", (byte)12 },
                    { new Guid("2f61eb42-26b2-1e00-a70b-d0b4098d1d05"), (byte)1, true, "egel", (byte)11 },
                    { new Guid("24873fd8-7b41-8a8b-7c81-621fde45520a"), (byte)1, true, "mol", (byte)10 },
                    { new Guid("d4b917f0-50a6-1992-2c9a-db686c578fb3"), (byte)1, true, "fret", (byte)9 },
                    { new Guid("82bb23be-6bab-45a5-83fc-ac1c81fa963f"), (byte)1, true, "bever", (byte)8 },
                    { new Guid("9d04c71c-251f-9ba3-98a6-871977f68055"), (byte)1, true, "wezel", (byte)7 },
                    { new Guid("eb989ecc-74d3-4642-48d7-00a512fe2304"), (byte)1, true, "zwarte rat", (byte)6 },
                    { new Guid("2a241941-61b6-2d0a-2d3f-6c09760ea2bd"), (byte)1, true, "hermelijn", (byte)5 },
                    { new Guid("a2b00e4d-1360-4f79-0844-a19d41528b90"), (byte)1, true, "amerikaanse nerts", (byte)4 },
                    { new Guid("a800c291-6104-a0f6-6bce-50f180d18cbe"), (byte)1, true, "bunzing", (byte)3 },
                    { new Guid("30bd14ac-9fba-8858-55d6-5afa921916ed"), (byte)1, true, "bruine rat", (byte)2 },
                    { new Guid("4047a8c6-35a2-30ba-3f1a-51b479aa2bdd"), (byte)2, true, "kauw", (byte)62 },
                    { new Guid("79b23520-2380-49e2-1e23-b226971a579b"), (byte)2, true, "ekster", (byte)31 },
                    { new Guid("e9b0b7e7-3d23-034f-9999-a7f9c4ad1cd5"), (byte)1, true, "steenmarter", (byte)15 },
                    { new Guid("7b4e5dd0-696b-24e2-1f68-685500cf477c"), (byte)2, true, "mandarijneend", (byte)33 },
                    { new Guid("d1f172ce-15c8-88e8-92ec-3e8997f97374"), (byte)2, true, "vlaamse gaai", (byte)32 },
                    { new Guid("08304be0-03d9-0419-9d95-7dda70bf117f"), (byte)2, true, "tamme (boeren) gans", (byte)60 },
                    { new Guid("7de9739c-3b0b-80a4-25a2-25ced3895635"), (byte)2, true, "brandgans", (byte)59 },
                    { new Guid("75283ee9-5706-9a47-2c13-3aba63af2aae"), (byte)2, true, "rotgans", (byte)58 },
                    { new Guid("615bb03e-5756-0e85-2bdc-41952fba4bab"), (byte)2, true, "nijlgans", (byte)57 },
                    { new Guid("d36f55c5-05a9-68ba-18ba-bea7fc3fa3b8"), (byte)2, true, "knobbelzwaan", (byte)56 },
                    { new Guid("5cd8f246-327f-4a4f-067c-e1573b2b7f94"), (byte)2, true, "tortelduif", (byte)55 },
                    { new Guid("570abd85-a4f2-2216-0218-bf2547d3a47c"), (byte)2, true, "holenduif", (byte)53 },
                    { new Guid("8c3de473-2e36-5f98-2bd8-7de7cc9424ed"), (byte)2, true, "porseleinhoen", (byte)52 },
                    { new Guid("63223bed-466f-75b8-4569-6d6b2a306204"), (byte)2, true, "slobeend", (byte)51 },
                    { new Guid("64a42c1a-7d1b-121d-9a04-927941d6457d"), (byte)2, true, "middelste zaagbek", (byte)50 },
                    { new Guid("41051a0f-3060-1036-50a8-797f0d246223"), (byte)2, true, "grote zaagbek", (byte)49 },
                    { new Guid("c815bc1e-5fdf-3b4c-23bf-1a5ded45717a"), (byte)2, true, "zanglijster", (byte)48 },
                    { new Guid("f251fde0-5a5c-7426-7a24-15c5976d0c9f"), (byte)2, true, "watersnip", (byte)47 },
                    { new Guid("7f446720-5862-2800-89fe-f0c3b28d573d"), (byte)2, true, "kleine zilverreiger", (byte)54 },
                    { new Guid("3a737d68-0316-8de4-a349-90aeeeb44186"), (byte)2, true, "roodborst", (byte)46 },
                    { new Guid("0e3644b2-04f1-0bdd-2fb2-d673416d944c"), (byte)2, true, "nonnetje", (byte)34 },
                    { new Guid("f7bacd7d-63dc-5efb-71da-556c77e54768"), (byte)2, true, "roek", (byte)35 },
                    { new Guid("0eff39db-3937-6a4b-4526-8472b88ca537"), (byte)2, true, "spreeuw", (byte)36 },
                    { new Guid("680c7557-4ad8-66b9-9c2d-1310b7d107cb"), (byte)2, true, "vink", (byte)37 },
                    { new Guid("4f6e48e1-8213-24ae-80ff-658183378727"), (byte)2, true, "zomertaling", (byte)39 },
                    { new Guid("0f44982f-5f41-517a-8eac-38ea36541345"), (byte)2, true, "blauwborst", (byte)40 },
                    { new Guid("50a63929-570a-614b-7635-aa5adfc39c80"), (byte)2, true, "wintertaling", (byte)38 },
                    { new Guid("2e8a45e5-90ef-3da9-1657-9485a719762a"), (byte)2, true, "merel", (byte)41 },
                    { new Guid("0691a0c0-11ed-0cd0-81da-0a962bc13352"), (byte)2, true, "meeuw", (byte)42 },
                    { new Guid("5dfac5da-54ea-3b6d-8390-745baaa97c1f"), (byte)2, true, "fazant", (byte)43 },
                    { new Guid("399eaf18-256a-0ae9-4f27-ead861d68f3a"), (byte)2, true, "kuifeend", (byte)44 },
                    { new Guid("651014a7-422d-5f82-75f6-12f816ac40ad"), (byte)2, true, "koperwiek", (byte)45 }
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 3, "Wijzigen gegevens" },
                    { 7, "Urenrapportage" },
                    { 6, "Meldingen wijzigen" },
                    { 5, "Meldingen bekijken" },
                    { 4, "Vangstrapportage" },
                    { 16, "Urenrapportage beheer" },
                    { 2, "Bekijken gegevens" },
                    { 14, "Externe data interface" },
                    { 10, "Gebruikers" },
                    { 11, "Beheer" },
                    { 12, "Alleen lezen" },
                    { 13, "Externe data interface (no auth)" },
                    { 15, "Beheerder Rechten Toekennen" },
                    { 100, "Mobiel" },
                    { 8, "Speurkaart bekijken" },
                    { 1, "Kaartoverzicht" },
                    { 9, "Speurkaart wijzigen" }
                });

            migrationBuilder.InsertData(
                table: "ReportTemplate",
                columns: new[] { "Id", "Active", "Content", "ExportFileName", "Exported", "Group", "Key", "RouteUri", "Title", "Type" },
                values: new object[,]
                {
                    { new Guid("40a95cad-f942-4f99-a836-9c63cb47e4f8"), true, @"{
                                        ""layer"": ""catch-registration-v3:TrackingLines""
                                    }", null!, false, "Speurkaart", null!, "tracking-lines", "Alles", (byte)2 },
                    { new Guid("8d2dfefb-166c-48c2-a3de-477cb5cd5ec4"), true, @"[
                	                    {
                		                    ""caption"": ""Organisatie"",
                		                    ""dataField"": ""organizationName"",
                		                    ""sortOrder"": ""asc"",
                		                    ""area"": ""row"",
                		                    ""width"": 120
                	                    },
                	                    {
                		                    ""caption"": ""Datum"",
                		                    ""dataField"": ""catchRecordedOn"",
                		                    ""dataType"": ""date"",
                		                    ""sortOrder"": ""asc"",
                		                    ""groupInterval"": ""year"",
                		                    ""area"": ""column""
                	                    },
                	                    {
                		                    ""caption"": ""Totaal"",
                		                    ""dataField"": ""numberOfCatches"",
                		                    ""dataType"": ""number"",
                		                    ""summaryType"": ""sum"",
                		                    ""area"": ""data""
                	                    },
                	                    {
                		                    ""dataField"": ""catchId"",
                		                    ""visible"": false
                	                    },
                	                    {
                		                    ""dataField"": ""catchRecordedOn"",
                		                    ""visible"": false
                	                    }
                                    ]", "vangstrapportage", false, "Vangstrapportage", "CatchId", "catches-per-organization", "Aantal vangsten per organisatie", (byte)1 },
                    { new Guid("a4337297-00d2-41ad-9da7-d58d6f073801"), true, @"{
                                        ""startDate"": """",
                                        ""endDate"": """",
                                        ""catchType"": """",
                                        ""organization"": """"
                                    }", null!, false, "Heatmap", null!, "heat-map", "Heatmap", (byte)2 },
                    { new Guid("33b0648d-724b-4be9-8ed9-5ecea181a5de"), true, @"{
                                        ""layer"": ""catch-registration-v3:OrganizationCatches"",
                                        ""measurement"": 0,
                                        ""yearFrom"": """",
                                        ""periodFrom"": """",
                                        ""yearTo"": """",
                                        ""periodTo"": """",
                                        ""trappingType"": """"
                                    }", null!, false, "Vangsten en uren op kaart", null!, "catches-by-geo-region", "Vangsten en uren op kaart", (byte)2 },
                    { new Guid("77f0ac51-3e70-4fec-9727-e68246525c22"), true, @"[
                	                    {
                		                    ""caption"": ""Organisatie"",
                		                    ""dataField"": ""organizationName"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120
                	                    },
                	                    {
                		                    ""caption"": ""Waterschap"",
                		                    ""dataField"": ""waterAuthorityName"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120
                	                    },
                	                    {
                		                    ""caption"": ""Rayon"",
                		                    ""dataField"": ""rayonName"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120
                	                    },
                	                    {
                		                    ""caption"": ""Vanggebied"",
                		                    ""dataField"": ""catchAreaName"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120
                	                    },
                	                    {
                		                    ""caption"": ""Deelgebied"",
                		                    ""dataField"": ""subAreaName"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120
                	                    },
                	                    {
                		                    ""caption"": ""Provincie"",
                		                    ""dataField"": ""provinceName"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120
                	                    },
                	                    {
                		                    ""caption"": ""Bestrijdingssoort"",
                		                    ""dataField"": ""trappingType"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120
                	                    },
                	                    {
                		                    ""caption"": ""Vangmiddeltype"",
                		                    ""dataField"": ""trapType"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120
                	                    },
                	                    {
                		                    ""caption"": ""Diersoort"",
                		                    ""dataField"": ""catchTypeName"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120
                	                    },
                	                    {
                		                    ""caption"": ""Vangstsoort"",
                		                    ""dataField"": ""catchType"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120
                	                    },
                	                    {
                		                    ""caption"": ""Datum"",
                		                    ""dataField"": ""catchRecordedOn"",
                		                    ""dataType"": ""date"",
                		                    ""sortOrder"": ""asc"",
                		                    ""groupInterval"": ""year""
                	                    },
                	                    {
                		                    ""caption"": ""Periode"",
                		                    ""dataField"": ""period"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120
                	                    },
                	                    {
                		                    ""caption"": ""Persoon"",
                		                    ""dataField"": ""userFulName"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120
                	                    },
                	                    {
                		                    ""caption"": ""Week"",
                		                    ""dataField"": ""week"",
                		                    ""sortOrder"": ""asc"",
                		                    ""width"": 120
                	                    },
                	                    {
                		                    ""caption"": ""Vangsten"",
                		                    ""dataField"": ""numberOfCatches"",
                		                    ""dataType"": ""number"",
                		                    ""summaryType"": ""sum"",
                		                    ""area"": ""data""
                	                    },
                	                    {
                		                    ""caption"": ""Bijvangsten"",
                		                    ""dataField"": ""numberOfByCatches"",
                		                    ""dataType"": ""number"",
                		                    ""summaryType"": ""sum""
                	                    },
                                        {
                                            ""caption"": ""Vangsten / Bijvangsten"",
                                            ""dxFunctionPlaceholder"": ""Divide:Vangsten,Bijvangsten"",
                                            ""dataType"": ""number"",
                                            ""format"": {
                                                ""type"": ""largeNumber"",
                                                ""precision"": 3
                                            }
                                        },
                	                    {
                		                    ""caption"": ""Km waterweg"",
                		                    ""dataField"": ""kmOfWaterways"",
                		                    ""dataType"": ""number"",
                		                    ""summaryType"": ""sum""
                	                    },
                	                    {
                		                    ""caption"": ""Aantal vallen"",
                		                    ""dataField"": ""numberOfTraps"",
                		                    ""dataType"": ""number"",
                		                    ""summaryType"": ""sum""
                	                    },
                	                    {
                		                    ""dataField"": ""catchId"",
                		                    ""visible"": false
                	                    },
                	                    {
                		                    ""dataField"": ""catchRecordedOn"",
                		                    ""visible"": false
                	                    }
                                    ]", "vangstrapportage", false, "Vangstrapportage", "CatchId", "own-report", "Eigen rapportage", (byte)1 }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "DisplayOrderIndex", "Name" },
                values: new object[,]
                {
                    { new Guid("c5add94f-982e-443d-bfa3-fb7efa3a7ec4"), "Trapper", 0, "Bestrijder" },
                    { new Guid("7c0eb52a-fc78-4bee-a8a1-f62f3036c0ba"), "Senior user", 1, "Senior gebruiker" },
                    { new Guid("da236317-eff5-44c4-b668-006ee2914309"), "Maintainer", 2, "Beheerder" },
                    { new Guid("65f2e6f6-a12c-4a10-833d-909c184f8965"), "National reporter", 3, "Landelijke rapporteur" },
                    { new Guid("fcf68091-04f0-4714-b889-b9230435feff"), "Regions application manager", 4, "Regionale functioneel beheerder" },
                    { new Guid("7cedd2da-1429-4577-a9e9-e7ffa592e27e"), "National application manager", 5, "Landelijke functioneel beheerder" },
                    { new Guid("8a54405f-1fd8-4ec4-88ae-840415b1af04"), "External public interface", 6, "Externe partij publiek" },
                    { new Guid("c8e15955-47ce-430e-bdc0-733f43911ec1"), "External private interface", 7, "Externe partij private" }
                });

            migrationBuilder.InsertData(
                table: "TrappingType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("a2ba2913-77d6-47d9-b893-f9d0cc0432bb"), "Muskusrat" },
                    { new Guid("76b4e3e7-ce9d-4eed-ab39-0ba2c6395b2b"), "Beverrat" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "ApplicationId", "Authorized", "ConfidentialityConfirmed", "CreatedById", "CreatedOn", "Email", "ExternalId", "GivenName", "Name", "ObjectId", "OrganizationId", "Surname", "TenantId", "UpdatedById", "UpdatedOn", "Version" },
                values: new object[,]
                {
                    { new Guid("16b691d5-da79-49ef-8f67-514b15754071"), null!, true, false, new Guid("16b691d5-da79-49ef-8f67-514b15754071"), new DateTimeOffset(new DateTime(2020, 3, 21, 17, 11, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "developer@enigmatry.com", null!, null!, "Developer", null!, null!, null!, null!, new Guid("16b691d5-da79-49ef-8f67-514b15754071"), new DateTimeOffset(new DateTime(2020, 3, 21, 17, 11, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),  null!},
                    { new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"), null!, false, false, new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"), new DateTimeOffset(new DateTime(2019, 5, 6, 14, 31, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "trap.system@enigmatry.com", null!, null!, "System", null!, null!, null!, null!, new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"), new DateTimeOffset(new DateTime(2019, 5, 6, 14, 31, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),  null!}
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1, new Guid("c5add94f-982e-443d-bfa3-fb7efa3a7ec4") },
                    { 100, new Guid("da236317-eff5-44c4-b668-006ee2914309") },
                    { 15, new Guid("da236317-eff5-44c4-b668-006ee2914309") },
                    { 1, new Guid("65f2e6f6-a12c-4a10-833d-909c184f8965") },
                    { 2, new Guid("65f2e6f6-a12c-4a10-833d-909c184f8965") },
                    { 4, new Guid("65f2e6f6-a12c-4a10-833d-909c184f8965") },
                    { 5, new Guid("65f2e6f6-a12c-4a10-833d-909c184f8965") },
                    { 12, new Guid("65f2e6f6-a12c-4a10-833d-909c184f8965") },
                    { 1, new Guid("fcf68091-04f0-4714-b889-b9230435feff") },
                    { 2, new Guid("fcf68091-04f0-4714-b889-b9230435feff") },
                    { 3, new Guid("fcf68091-04f0-4714-b889-b9230435feff") },
                    { 4, new Guid("fcf68091-04f0-4714-b889-b9230435feff") },
                    { 6, new Guid("fcf68091-04f0-4714-b889-b9230435feff") },
                    { 11, new Guid("da236317-eff5-44c4-b668-006ee2914309") },
                    { 10, new Guid("fcf68091-04f0-4714-b889-b9230435feff") },
                    { 2, new Guid("7cedd2da-1429-4577-a9e9-e7ffa592e27e") },
                    { 4, new Guid("7cedd2da-1429-4577-a9e9-e7ffa592e27e") },
                    { 5, new Guid("7cedd2da-1429-4577-a9e9-e7ffa592e27e") },
                    { 10, new Guid("7cedd2da-1429-4577-a9e9-e7ffa592e27e") },
                    { 11, new Guid("7cedd2da-1429-4577-a9e9-e7ffa592e27e") },
                    { 2, new Guid("8a54405f-1fd8-4ec4-88ae-840415b1af04") },
                    { 5, new Guid("8a54405f-1fd8-4ec4-88ae-840415b1af04") },
                    { 13, new Guid("8a54405f-1fd8-4ec4-88ae-840415b1af04") },
                    { 14, new Guid("8a54405f-1fd8-4ec4-88ae-840415b1af04") },
                    { 2, new Guid("c8e15955-47ce-430e-bdc0-733f43911ec1") },
                    { 5, new Guid("c8e15955-47ce-430e-bdc0-733f43911ec1") },
                    { 13, new Guid("c8e15955-47ce-430e-bdc0-733f43911ec1") },
                    { 1, new Guid("7cedd2da-1429-4577-a9e9-e7ffa592e27e") },
                    { 10, new Guid("da236317-eff5-44c4-b668-006ee2914309") },
                    { 5, new Guid("fcf68091-04f0-4714-b889-b9230435feff") },
                    { 8, new Guid("da236317-eff5-44c4-b668-006ee2914309") },
                    { 9, new Guid("da236317-eff5-44c4-b668-006ee2914309") },
                    { 3, new Guid("c5add94f-982e-443d-bfa3-fb7efa3a7ec4") },
                    { 4, new Guid("c5add94f-982e-443d-bfa3-fb7efa3a7ec4") },
                    { 5, new Guid("c5add94f-982e-443d-bfa3-fb7efa3a7ec4") },
                    { 6, new Guid("c5add94f-982e-443d-bfa3-fb7efa3a7ec4") },
                    { 8, new Guid("c5add94f-982e-443d-bfa3-fb7efa3a7ec4") },
                    { 9, new Guid("c5add94f-982e-443d-bfa3-fb7efa3a7ec4") },
                    { 100, new Guid("c5add94f-982e-443d-bfa3-fb7efa3a7ec4") },
                    { 1, new Guid("7c0eb52a-fc78-4bee-a8a1-f62f3036c0ba") },
                    { 2, new Guid("7c0eb52a-fc78-4bee-a8a1-f62f3036c0ba") },
                    { 3, new Guid("7c0eb52a-fc78-4bee-a8a1-f62f3036c0ba") },
                    { 4, new Guid("7c0eb52a-fc78-4bee-a8a1-f62f3036c0ba") },
                    { 5, new Guid("7c0eb52a-fc78-4bee-a8a1-f62f3036c0ba") },
                    { 6, new Guid("7c0eb52a-fc78-4bee-a8a1-f62f3036c0ba") },
                    { 2, new Guid("c5add94f-982e-443d-bfa3-fb7efa3a7ec4") },
                    { 7, new Guid("da236317-eff5-44c4-b668-006ee2914309") },
                    { 7, new Guid("7c0eb52a-fc78-4bee-a8a1-f62f3036c0ba") },
                    { 6, new Guid("da236317-eff5-44c4-b668-006ee2914309") },
                    { 5, new Guid("da236317-eff5-44c4-b668-006ee2914309") },
                    { 3, new Guid("da236317-eff5-44c4-b668-006ee2914309") },
                    { 2, new Guid("da236317-eff5-44c4-b668-006ee2914309") },
                    { 4, new Guid("da236317-eff5-44c4-b668-006ee2914309") },
                    { 100, new Guid("7c0eb52a-fc78-4bee-a8a1-f62f3036c0ba") },
                    { 9, new Guid("7c0eb52a-fc78-4bee-a8a1-f62f3036c0ba") },
                    { 8, new Guid("7c0eb52a-fc78-4bee-a8a1-f62f3036c0ba") },
                    { 16, new Guid("7c0eb52a-fc78-4bee-a8a1-f62f3036c0ba") },
                    { 1, new Guid("da236317-eff5-44c4-b668-006ee2914309") }
                });

            migrationBuilder.InsertData(
                table: "TrapType",
                columns: new[] { "Id", "Active", "Name", "Order", "TrappingTypeId" },
                values: new object[,]
                {
                    { new Guid("0d39267f-2ef1-5978-4fb5-68b7ec956f0f"), false, "Levend vangende kooi op vlot", (byte)20, new Guid("76b4e3e7-ce9d-4eed-ab39-0ba2c6395b2b") },
                    { new Guid("e2ba4c87-65fd-2f70-a1ea-68a6a4549db6"), true, "Schijnduiker", (byte)16, new Guid("a2ba2913-77d6-47d9-b893-f9d0cc0432bb") },
                    { new Guid("2fed9c2e-7151-316f-5e5d-644bc5620172"), true, "Dood aangetroffen", (byte)17, new Guid("a2ba2913-77d6-47d9-b893-f9d0cc0432bb") },
                    { new Guid("198be8a5-569f-233c-93ef-3879aa97120c"), false, "Duikerkooi (groot/klein)", (byte)18, new Guid("a2ba2913-77d6-47d9-b893-f9d0cc0432bb") },
                    { new Guid("ff7c880c-9ac6-433e-1b92-3563869a48e2"), false, "Otter", (byte)19, new Guid("a2ba2913-77d6-47d9-b893-f9d0cc0432bb") },
                    { new Guid("dc56c661-710f-63ab-870b-36ede6f9204e"), false, "Levend vangende kooi op oever", (byte)21, new Guid("76b4e3e7-ce9d-4eed-ab39-0ba2c6395b2b") },
                    { new Guid("54af411e-25f6-2a11-4bbf-e7547e212e76"), false, "Grondklem", (byte)28, new Guid("76b4e3e7-ce9d-4eed-ab39-0ba2c6395b2b") },
                    { new Guid("222b0a31-092a-4978-71df-1f5c55e41000"), true, "Levend vangende kooi", (byte)23, new Guid("76b4e3e7-ce9d-4eed-ab39-0ba2c6395b2b") },
                    { new Guid("13eb51ac-6984-95e9-04ee-ddae1927a499"), true, "Geweer", (byte)24, new Guid("76b4e3e7-ce9d-4eed-ab39-0ba2c6395b2b") },
                    { new Guid("5fa2dc7f-8c1a-1255-a41e-6bf28b183def"), true, "Slaan en delven", (byte)25, new Guid("76b4e3e7-ce9d-4eed-ab39-0ba2c6395b2b") },
                    { new Guid("c3c795b9-49d2-0722-7f4b-e28bf43da5c4"), true, "Geweer", (byte)15, new Guid("a2ba2913-77d6-47d9-b893-f9d0cc0432bb") },
                    { new Guid("ca6c4838-2b63-71c5-3bec-6dbefe7678a2"), true, "Dood aangetroffen", (byte)26, new Guid("76b4e3e7-ce9d-4eed-ab39-0ba2c6395b2b") },
                    { new Guid("586729d8-980e-2a76-81f2-dbb5c57c9d6f"), false, "Conibear", (byte)27, new Guid("76b4e3e7-ce9d-4eed-ab39-0ba2c6395b2b") },
                    { new Guid("b73b56e0-9691-6eff-43c5-0f0f36ff69c2"), false, "Levend vangende kooi op wissel", (byte)22, new Guid("76b4e3e7-ce9d-4eed-ab39-0ba2c6395b2b") },
                    { new Guid("eb9f0577-a55e-0ed7-2102-f04cefe54680"), false, "Lokaasklem (oever)", (byte)14, new Guid("a2ba2913-77d6-47d9-b893-f9d0cc0432bb") },
                    { new Guid("47890978-910d-969a-2499-0b848fa80f8a"), false, "Levend vangende kooi op oever", (byte)8, new Guid("a2ba2913-77d6-47d9-b893-f9d0cc0432bb") },
                    { new Guid("2ff5402a-96b5-6b49-3eed-e5a4372666fb"), true, "Klemmenrekje", (byte)12, new Guid("a2ba2913-77d6-47d9-b893-f9d0cc0432bb") },
                    { new Guid("935a02f4-69b0-8142-29f8-885124db34bc"), true, "Slaan en delven", (byte)11, new Guid("a2ba2913-77d6-47d9-b893-f9d0cc0432bb") },
                    { new Guid("1620509f-4bb2-90ea-637c-af77b636964a"), true, "Grondklem", (byte)10, new Guid("a2ba2913-77d6-47d9-b893-f9d0cc0432bb") },
                    { new Guid("10d026ed-0d1e-7b3b-786c-0246c0367222"), false, "Levend vangende kooi op wissel", (byte)9, new Guid("a2ba2913-77d6-47d9-b893-f9d0cc0432bb") },
                    { new Guid("e887e560-8959-8994-960b-a85fc5c22e4d"), false, "Levend vangende kooi op vlot", (byte)7, new Guid("a2ba2913-77d6-47d9-b893-f9d0cc0432bb") },
                    { new Guid("dc90faa1-1ad8-a4f2-22c2-582dcc5d4a84"), true, "Levend vangende kooi", (byte)6, new Guid("a2ba2913-77d6-47d9-b893-f9d0cc0432bb") },
                    { new Guid("3c881890-4d00-6b96-25be-67dec7314b2f"), true, "Slootafzetting met kooi", (byte)5, new Guid("a2ba2913-77d6-47d9-b893-f9d0cc0432bb") },
                    { new Guid("6539abe5-081d-a060-31b9-1c5c43f74abb"), true, "Lokaaskooi", (byte)4, new Guid("a2ba2913-77d6-47d9-b893-f9d0cc0432bb") },
                    { new Guid("9f91a9d1-77d9-06d9-03a9-18f2efcc0bcc"), true, "Duikerafzetting", (byte)3, new Guid("a2ba2913-77d6-47d9-b893-f9d0cc0432bb") },
                    { new Guid("264f0093-6056-110b-1de8-2aefd1d73c4a"), true, "Postklem", (byte)2, new Guid("a2ba2913-77d6-47d9-b893-f9d0cc0432bb") },
                    { new Guid("a0a0503e-0cd7-0642-73ab-464e7ca0a28e"), true, "Conibear", (byte)1, new Guid("a2ba2913-77d6-47d9-b893-f9d0cc0432bb") },
                    { new Guid("eb992687-4000-6956-a688-2fc9242d2e20"), true, "Lokaasklem", (byte)13, new Guid("a2ba2913-77d6-47d9-b893-f9d0cc0432bb") },
                    { new Guid("3b1215f3-05a2-660e-85f4-d27dd5ae115d"), false, "Overig", (byte)29, new Guid("76b4e3e7-ce9d-4eed-ab39-0ba2c6395b2b") }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("16b691d5-da79-49ef-8f67-514b15754071"), new Guid("da236317-eff5-44c4-b668-006ee2914309") },
                    { new Guid("8207db25-94d1-4f3d-bf18-90da283221f7"), new Guid("da236317-eff5-44c4-b668-006ee2914309") }
                });

            migrationBuilder.InsertData(
                table: "TrapTypeTrapStatusStyle",
                columns: new[] { "TrapTypeId", "TrapStatus", "IconName" },
                values: new object[,]
                {
                    { new Guid("a0a0503e-0cd7-0642-73ab-464e7ca0a28e"), (byte)1, "trap-conibear.svg" },
                    { new Guid("eb992687-4000-6956-a688-2fc9242d2e20"), (byte)1, "trap-lokaasklem.svg" },
                    { new Guid("eb992687-4000-6956-a688-2fc9242d2e20"), (byte)3, "trap-removed-lokaasklem.svg" },
                    { new Guid("c3c795b9-49d2-0722-7f4b-e28bf43da5c4"), (byte)1, "trap-musk-other.svg" },
                    { new Guid("c3c795b9-49d2-0722-7f4b-e28bf43da5c4"), (byte)3, "trap-removed-musk-other.svg" },
                    { new Guid("e2ba4c87-65fd-2f70-a1ea-68a6a4549db6"), (byte)1, "trap-schijnduiker.svg" },
                    { new Guid("e2ba4c87-65fd-2f70-a1ea-68a6a4549db6"), (byte)3, "trap-removed-schijnduiker.svg" },
                    { new Guid("e2ba4c87-65fd-2f70-a1ea-68a6a4549db6"), (byte)2, "trap-unplaced-schijnduiker.svg" },
                    { new Guid("2fed9c2e-7151-316f-5e5d-644bc5620172"), (byte)1, "trap-musk-other.svg" },
                    { new Guid("2fed9c2e-7151-316f-5e5d-644bc5620172"), (byte)3, "trap-removed-musk-other.svg" },
                    { new Guid("222b0a31-092a-4978-71df-1f5c55e41000"), (byte)1, "trap-beaver-living.svg" },
                    { new Guid("222b0a31-092a-4978-71df-1f5c55e41000"), (byte)3, "trap-removed-beaver-living.svg" },
                    { new Guid("13eb51ac-6984-95e9-04ee-ddae1927a499"), (byte)1, "trap-beaver-other.svg" },
                    { new Guid("13eb51ac-6984-95e9-04ee-ddae1927a499"), (byte)3, "trap-removed-beaver-other.svg" },
                    { new Guid("5fa2dc7f-8c1a-1255-a41e-6bf28b183def"), (byte)1, "trap-beaver-other.svg" },
                    { new Guid("5fa2dc7f-8c1a-1255-a41e-6bf28b183def"), (byte)3, "trap-removed-beaver-other.svg" },
                    { new Guid("2ff5402a-96b5-6b49-3eed-e5a4372666fb"), (byte)2, "trap-unplaced-klemmenrek.svg" },
                    { new Guid("2ff5402a-96b5-6b49-3eed-e5a4372666fb"), (byte)3, "trap-removed-klemmenrek.svg" },
                    { new Guid("2ff5402a-96b5-6b49-3eed-e5a4372666fb"), (byte)1, "trap-klemmenrek.svg" },
                    { new Guid("935a02f4-69b0-8142-29f8-885124db34bc"), (byte)3, "trap-removed-musk-other.svg" },
                    { new Guid("a0a0503e-0cd7-0642-73ab-464e7ca0a28e"), (byte)3, "trap-removed-conibear.svg" },
                    { new Guid("264f0093-6056-110b-1de8-2aefd1d73c4a"), (byte)1, "trap-postklem.svg" },
                    { new Guid("264f0093-6056-110b-1de8-2aefd1d73c4a"), (byte)3, "trap-removed-postklem.svg" },
                    { new Guid("9f91a9d1-77d9-06d9-03a9-18f2efcc0bcc"), (byte)1, "trap-duikerafzetting.svg" },
                    { new Guid("9f91a9d1-77d9-06d9-03a9-18f2efcc0bcc"), (byte)3, "trap-removed-duikerafzetting.svg" },
                    { new Guid("9f91a9d1-77d9-06d9-03a9-18f2efcc0bcc"), (byte)2, "trap-unplaced-duikerafzetting.svg" },
                    { new Guid("6539abe5-081d-a060-31b9-1c5c43f74abb"), (byte)1, "trap-lokaaskooi.svg" },
                    { new Guid("ca6c4838-2b63-71c5-3bec-6dbefe7678a2"), (byte)1, "trap-beaver-other.svg" },
                    { new Guid("6539abe5-081d-a060-31b9-1c5c43f74abb"), (byte)3, "trap-removed-lokaaskooi.svg" },
                    { new Guid("3c881890-4d00-6b96-25be-67dec7314b2f"), (byte)3, "trap-removed-slootafzetting.svg" },
                    { new Guid("3c881890-4d00-6b96-25be-67dec7314b2f"), (byte)2, "trap-unplaced-slootafzetting.svg" },
                    { new Guid("dc90faa1-1ad8-a4f2-22c2-582dcc5d4a84"), (byte)1, "trap-musk-living.svg" },
                    { new Guid("dc90faa1-1ad8-a4f2-22c2-582dcc5d4a84"), (byte)3, "trap-removed-musk-living.svg" },
                    { new Guid("1620509f-4bb2-90ea-637c-af77b636964a"), (byte)1, "trap-grondklem.svg" },
                    { new Guid("1620509f-4bb2-90ea-637c-af77b636964a"), (byte)3, "trap-removed-grondklem.svg" },
                    { new Guid("935a02f4-69b0-8142-29f8-885124db34bc"), (byte)1, "trap-musk-other.svg" },
                    { new Guid("3c881890-4d00-6b96-25be-67dec7314b2f"), (byte)1, "trap-slootafzetting.svg" },
                    { new Guid("ca6c4838-2b63-71c5-3bec-6dbefe7678a2"), (byte)3, "trap-removed-beaver-other.svg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Catch_CatchTypeId",
                table: "Catch",
                column: "CatchTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Catch_CreatedById",
                table: "Catch",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RecordedOn",
                table: "Catch",
                column: "RecordedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Catch_TrapId",
                table: "Catch",
                column: "TrapId");

            migrationBuilder.CreateIndex(
                name: "IX_Catch_UpdatedById",
                table: "Catch",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CatchArea_RayonId",
                table: "CatchArea",
                column: "RayonId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldTestHourSquare_HourSquareId",
                table: "FieldTestHourSquare",
                column: "HourSquareId");

            migrationBuilder.CreateIndex(
                name: "IX_Observation_CreatedById",
                table: "Observation",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Observation_SubAreaHourSquareId",
                table: "Observation",
                column: "SubAreaHourSquareId");

            migrationBuilder.CreateIndex(
                name: "IX_Observation_UpdatedById",
                table: "Observation",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Rayon_OrganizationId",
                table: "Rayon",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_RoleId",
                table: "RolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SubArea_CatchAreaId",
                table: "SubArea",
                column: "CatchAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubArea_WaterAuthorityId",
                table: "SubArea",
                column: "WaterAuthorityId");

            migrationBuilder.CreateIndex(
                name: "IX_SubAreaHourSquare_HourSquareId",
                table: "SubAreaHourSquare",
                column: "HourSquareId");

            migrationBuilder.CreateIndex(
                name: "IX_SubAreaHourSquare_SubAreaId_HourSquareId",
                table: "SubAreaHourSquare",
                columns: new[] { "SubAreaId", "HourSquareId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegistration_CreatedById",
                table: "TimeRegistration",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Date",
                table: "TimeRegistration",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegistration_SubAreaHourSquareId",
                table: "TimeRegistration",
                column: "SubAreaHourSquareId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegistration_TrappingTypeId",
                table: "TimeRegistration",
                column: "TrappingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegistration_UpdatedById",
                table: "TimeRegistration",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegistration_UserId",
                table: "TimeRegistration",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tracking_CreatedById",
                table: "Tracking",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tracking_TrappingTypeId",
                table: "Tracking",
                column: "TrappingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tracking_UpdatedById",
                table: "Tracking",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Trap_CreatedById",
                table: "Trap",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Trap_ProvinceId",
                table: "Trap",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Trap_SubAreaHourSquareId",
                table: "Trap",
                column: "SubAreaHourSquareId");

            migrationBuilder.CreateIndex(
                name: "IX_Trap_TrapTypeId",
                table: "Trap",
                column: "TrapTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trap_UpdatedById",
                table: "Trap",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TrapHistory_CatchId",
                table: "TrapHistory",
                column: "CatchId");

            migrationBuilder.CreateIndex(
                name: "IX_TrapHistory_CreatedById",
                table: "TrapHistory",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TrapHistory_TrapId",
                table: "TrapHistory",
                column: "TrapId");

            migrationBuilder.CreateIndex(
                name: "IX_TrapHistory_UpdatedById",
                table: "TrapHistory",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TrapType_TrappingTypeId",
                table: "TrapType",
                column: "TrappingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatedById",
                table: "User",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_OrganizationId",
                table: "User",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UpdatedById",
                table: "User",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WaterAuthority_OrganizationId",
                table: "WaterAuthority",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FieldTestHourSquare");

            migrationBuilder.DropTable(
                name: "IntegrationEventLog");

            migrationBuilder.DropTable(
                name: "Observation");

            migrationBuilder.DropTable(
                name: "ReportTemplate");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "TimeRegistration");

            migrationBuilder.DropTable(
                name: "Tracking");

            migrationBuilder.DropTable(
                name: "TrackingLine");

            migrationBuilder.DropTable(
                name: "TrapHistory");

            migrationBuilder.DropTable(
                name: "TrapTypeTrapStatusStyle");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "FieldTest");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Catch");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "CatchType");

            migrationBuilder.DropTable(
                name: "Trap");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Province");

            migrationBuilder.DropTable(
                name: "SubAreaHourSquare");

            migrationBuilder.DropTable(
                name: "TrapType");

            migrationBuilder.DropTable(
                name: "HourSquare");

            migrationBuilder.DropTable(
                name: "SubArea");

            migrationBuilder.DropTable(
                name: "TrappingType");

            migrationBuilder.DropTable(
                name: "CatchArea");

            migrationBuilder.DropTable(
                name: "WaterAuthority");

            migrationBuilder.DropTable(
                name: "Rayon");

            migrationBuilder.DropTable(
                name: "Organization");
        }
    }
}
