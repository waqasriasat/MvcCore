using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvLab_WebApp.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase432024201 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessRights",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SOR = table.Column<int>(type: "int", nullable: true),
                    EmpID = table.Column<int>(type: "int", nullable: true),
                    Assigning = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRights", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AddClients",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CID = table.Column<int>(type: "int", nullable: false),
                    CName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientActive = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pwd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceChangabletrue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CCont = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inc_Routine = table.Column<double>(type: "float", nullable: false),
                    Inc_Special = table.Column<double>(type: "float", nullable: false),
                    Inc_NoDisc = table.Column<double>(type: "float", nullable: false),
                    Inc_Xray = table.Column<double>(type: "float", nullable: false),
                    Inc_Ultra = table.Column<double>(type: "float", nullable: false),
                    Inc_Ctscan = table.Column<double>(type: "float", nullable: false),
                    Inc_Mri = table.Column<double>(type: "float", nullable: false),
                    Inc_Echo = table.Column<double>(type: "float", nullable: false),
                    Inc_Ecg = table.Column<double>(type: "float", nullable: false),
                    Inc_Cdopler = table.Column<double>(type: "float", nullable: false),
                    UId = table.Column<int>(type: "int", nullable: false),
                    BDO = table.Column<int>(type: "int", nullable: false),
                    BusinessType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientInstraction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PAWC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dsc_Routine = table.Column<double>(type: "float", nullable: false),
                    Dsc_Special = table.Column<double>(type: "float", nullable: false),
                    Dsc_NoDisc = table.Column<double>(type: "float", nullable: false),
                    Dsc_Xray = table.Column<double>(type: "float", nullable: false),
                    Dsc_Ultra = table.Column<double>(type: "float", nullable: false),
                    Dsc_Ctscan = table.Column<double>(type: "float", nullable: false),
                    Dsc_Mri = table.Column<double>(type: "float", nullable: false),
                    Dsc_Echo = table.Column<double>(type: "float", nullable: false),
                    Dsc_Ecg = table.Column<double>(type: "float", nullable: false),
                    Dsc_Cdopler = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddClients", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AddConnLabs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LocActive = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocID = table.Column<int>(type: "int", nullable: true),
                    LocCate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UId = table.Column<int>(type: "int", nullable: true),
                    Idloc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompMac = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddConnLabs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AddDeparts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SID = table.Column<int>(type: "int", nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Placement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Depart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UId = table.Column<int>(type: "int", nullable: true),
                    Idloc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompMac = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddDeparts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AddDescriptions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescID = table.Column<int>(type: "int", nullable: false),
                    TestCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lock = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Depart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubDepart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerformLab = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SampleSended = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Charges = table.Column<int>(type: "int", nullable: false),
                    TRDTypr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TProcedure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddDescriptions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AddDesignations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SID = table.Column<int>(type: "int", nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UId = table.Column<int>(type: "int", nullable: true),
                    Idloc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompMac = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddDesignations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AddLocations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LocActive = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocID = table.Column<int>(type: "int", nullable: true),
                    LocCate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reconsile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocSno = table.Column<int>(type: "int", nullable: true),
                    UId = table.Column<int>(type: "int", nullable: true),
                    Idloc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompMac = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddLocations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AddPlacements",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SID = table.Column<int>(type: "int", nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlaceCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UId = table.Column<int>(type: "int", nullable: true),
                    Idloc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompMac = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddPlacements", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AddSubDeparts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SID = table.Column<int>(type: "int", nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Depart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubDepart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UId = table.Column<int>(type: "int", nullable: true),
                    Idloc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompMac = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddSubDeparts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ChangePasswords",
                columns: table => new
                {
                    EmpId = table.Column<int>(type: "int", nullable: false),
                    CurrentPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmNewPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "CrystalReportPaths",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CrystalReport = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    db = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    server = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Loc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CNL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultClient = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrystalReportPaths", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DescCashiers",
                columns: table => new
                {
                    LabNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MRNO = table.Column<int>(type: "int", nullable: false),
                    CpName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CpNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientID = table.Column<int>(type: "int", nullable: false),
                    ConsName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrossA = table.Column<double>(type: "float", nullable: true),
                    DiscPer = table.Column<double>(type: "float", nullable: true),
                    Discount = table.Column<double>(type: "float", nullable: true),
                    TotalA = table.Column<double>(type: "float", nullable: false),
                    PaidA = table.Column<double>(type: "float", nullable: true),
                    BlanceA = table.Column<double>(type: "float", nullable: true),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditCardNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxN = table.Column<double>(type: "float", nullable: true),
                    UId = table.Column<int>(type: "int", nullable: false),
                    Idloc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RbalanceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RbalanceA = table.Column<double>(type: "float", nullable: true),
                    RUId1 = table.Column<int>(type: "int", nullable: false),
                    RIdloc1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentB = table.Column<double>(type: "float", nullable: true),
                    BillNo = table.Column<int>(type: "int", nullable: false),
                    F_VNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RV1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pwd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceSMS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DortocSMS = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescCashiers", x => x.LabNo);
                });

            migrationBuilder.CreateTable(
                name: "DescQs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LocationNo = table.Column<int>(type: "int", nullable: true),
                    CpNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CpName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UId = table.Column<int>(type: "int", nullable: true),
                    Uname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Idloc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescQs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DuesRecQs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabNo = table.Column<int>(type: "int", nullable: true),
                    AmountRec = table.Column<double>(type: "float", nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UId = table.Column<int>(type: "int", nullable: true),
                    Uname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Idloc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DuesRecQs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "LoginLogs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    logDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MacAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoginStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginLogs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PatReg_Shortkeys",
                columns: table => new
                {
                    Sno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Initial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Relation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Years = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatReg_Shortkeys", x => x.Sno);
                });

            migrationBuilder.CreateTable(
                name: "PatRegs",
                columns: table => new
                {
                    MRNO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Initial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Relation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    AgeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DBO = table.Column<DateOnly>(type: "date", nullable: true),
                    ContNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UId = table.Column<int>(type: "int", nullable: false),
                    Idloc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatRegs", x => x.MRNO);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoginStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoginStatusIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoginStatusComp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NSend = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmpID = table.Column<int>(type: "int", nullable: false),
                    UName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CNL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Depart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubDepart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Placement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UId = table.Column<int>(type: "int", nullable: true),
                    Idloc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompMac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayGenerate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleID = table.Column<int>(type: "int", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DescLabs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabNo = table.Column<int>(type: "int", nullable: false),
                    BarcodeNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescID = table.Column<int>(type: "int", nullable: false),
                    RepDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Charges = table.Column<int>(type: "int", nullable: false),
                    DStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescLabs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DescLabs_DescCashiers_LabNo",
                        column: x => x.LabNo,
                        principalTable: "DescCashiers",
                        principalColumn: "LabNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DescLabs_LabNo",
                table: "DescLabs",
                column: "LabNo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessRights");

            migrationBuilder.DropTable(
                name: "AddClients");

            migrationBuilder.DropTable(
                name: "AddConnLabs");

            migrationBuilder.DropTable(
                name: "AddDeparts");

            migrationBuilder.DropTable(
                name: "AddDescriptions");

            migrationBuilder.DropTable(
                name: "AddDesignations");

            migrationBuilder.DropTable(
                name: "AddLocations");

            migrationBuilder.DropTable(
                name: "AddPlacements");

            migrationBuilder.DropTable(
                name: "AddSubDeparts");

            migrationBuilder.DropTable(
                name: "ChangePasswords");

            migrationBuilder.DropTable(
                name: "CrystalReportPaths");

            migrationBuilder.DropTable(
                name: "DescLabs");

            migrationBuilder.DropTable(
                name: "DescQs");

            migrationBuilder.DropTable(
                name: "DuesRecQs");

            migrationBuilder.DropTable(
                name: "LoginLogs");

            migrationBuilder.DropTable(
                name: "PatReg_Shortkeys");

            migrationBuilder.DropTable(
                name: "PatRegs");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "DescCashiers");
        }
    }
}
