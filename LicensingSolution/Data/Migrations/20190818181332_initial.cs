using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LicensingSolution.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    IDNumber = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    EmailAddress = table.Column<string>(nullable: true),
                    PrimaryContactNumber = table.Column<string>(nullable: true),
                    SecondaryContactNumber = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: false),
                    Suburb = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    PostalCode = table.Column<string>(maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.IDNumber);
                });

            migrationBuilder.CreateTable(
                name: "DrivingLicences",
                columns: table => new
                {
                    LicenceNumber = table.Column<string>(maxLength: 12, nullable: false),
                    LicenceExpiryDate = table.Column<DateTime>(nullable: false),
                    PDPExpiryDate = table.Column<DateTime>(nullable: false),
                    IDNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrivingLicences", x => x.LicenceNumber);
                    table.ForeignKey(
                        name: "FK_DrivingLicences_Owners_IDNumber",
                        column: x => x.IDNumber,
                        principalTable: "Owners",
                        principalColumn: "IDNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OperatingLicences",
                columns: table => new
                {
                    OperatingLicenceNumber = table.Column<string>(maxLength: 20, nullable: false),
                    ApplicationNumber = table.Column<string>(maxLength: 10, nullable: false),
                    AssociationName = table.Column<string>(maxLength: 150, nullable: false),
                    VehRegistrationNumber = table.Column<string>(maxLength: 10, nullable: false),
                    EngineNumber = table.Column<string>(nullable: false),
                    VehMass = table.Column<double>(nullable: false),
                    Manufacturer = table.Column<string>(nullable: false),
                    VehDescription = table.Column<double>(nullable: false),
                    Passengers = table.Column<int>(nullable: false),
                    YearOfReg = table.Column<int>(nullable: false),
                    ValidFrom = table.Column<DateTime>(nullable: false),
                    ValidUntil = table.Column<DateTime>(nullable: false),
                    IDNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatingLicences", x => x.OperatingLicenceNumber);
                    table.ForeignKey(
                        name: "FK_OperatingLicences_Owners_IDNumber",
                        column: x => x.IDNumber,
                        principalTable: "Owners",
                        principalColumn: "IDNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleLicences",
                columns: table => new
                {
                    VehRegisterNumber = table.Column<string>(nullable: false),
                    VehLicenceNumber = table.Column<string>(maxLength: 10, nullable: false),
                    VehIdentificationNumber = table.Column<string>(nullable: false),
                    VehMake = table.Column<string>(nullable: false),
                    RegisteringAuthority = table.Column<string>(nullable: false),
                    DateOfExpiry = table.Column<DateTime>(nullable: false),
                    IDNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleLicences", x => x.VehRegisterNumber);
                    table.ForeignKey(
                        name: "FK_VehicleLicences_Owners_IDNumber",
                        column: x => x.IDNumber,
                        principalTable: "Owners",
                        principalColumn: "IDNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrivingLicences_IDNumber",
                table: "DrivingLicences",
                column: "IDNumber",
                unique: true,
                filter: "[IDNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OperatingLicences_IDNumber",
                table: "OperatingLicences",
                column: "IDNumber");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleLicences_IDNumber",
                table: "VehicleLicences",
                column: "IDNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrivingLicences");

            migrationBuilder.DropTable(
                name: "OperatingLicences");

            migrationBuilder.DropTable(
                name: "VehicleLicences");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}
