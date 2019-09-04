using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LicensingSolution.Data.Migrations
{
    public partial class driver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrivingLicences_Owners_IDNumber",
                table: "DrivingLicences");

            migrationBuilder.DropForeignKey(
                name: "FK_OperatingLicences_Owners_IDNumber",
                table: "OperatingLicences");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleLicences_Owners_IDNumber",
                table: "VehicleLicences");

            migrationBuilder.RenameColumn(
                name: "IDNumber",
                table: "VehicleLicences",
                newName: "OwnerIDNumber");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleLicences_IDNumber",
                table: "VehicleLicences",
                newName: "IX_VehicleLicences_OwnerIDNumber");

            migrationBuilder.RenameColumn(
                name: "IDNumber",
                table: "OperatingLicences",
                newName: "OwnerIDNumber");

            migrationBuilder.RenameIndex(
                name: "IX_OperatingLicences_IDNumber",
                table: "OperatingLicences",
                newName: "IX_OperatingLicences_OwnerIDNumber");

            migrationBuilder.AddColumn<string>(
                name: "DrivingLicenceLicenceNumber",
                table: "Owners",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Driver",
                columns: table => new
                {
                    IDNumber = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Img = table.Column<byte[]>(nullable: true),
                    OwnerIDNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Driver", x => x.IDNumber);
                    table.ForeignKey(
                        name: "FK_Driver_Owners_OwnerIDNumber",
                        column: x => x.OwnerIDNumber,
                        principalTable: "Owners",
                        principalColumn: "IDNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Owners_DrivingLicenceLicenceNumber",
                table: "Owners",
                column: "DrivingLicenceLicenceNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Driver_OwnerIDNumber",
                table: "Driver",
                column: "OwnerIDNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_DrivingLicences_Driver_IDNumber",
                table: "DrivingLicences",
                column: "IDNumber",
                principalTable: "Driver",
                principalColumn: "IDNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OperatingLicences_Owners_OwnerIDNumber",
                table: "OperatingLicences",
                column: "OwnerIDNumber",
                principalTable: "Owners",
                principalColumn: "IDNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_DrivingLicences_DrivingLicenceLicenceNumber",
                table: "Owners",
                column: "DrivingLicenceLicenceNumber",
                principalTable: "DrivingLicences",
                principalColumn: "LicenceNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleLicences_Owners_OwnerIDNumber",
                table: "VehicleLicences",
                column: "OwnerIDNumber",
                principalTable: "Owners",
                principalColumn: "IDNumber",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrivingLicences_Driver_IDNumber",
                table: "DrivingLicences");

            migrationBuilder.DropForeignKey(
                name: "FK_OperatingLicences_Owners_OwnerIDNumber",
                table: "OperatingLicences");

            migrationBuilder.DropForeignKey(
                name: "FK_Owners_DrivingLicences_DrivingLicenceLicenceNumber",
                table: "Owners");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleLicences_Owners_OwnerIDNumber",
                table: "VehicleLicences");

            migrationBuilder.DropTable(
                name: "Driver");

            migrationBuilder.DropIndex(
                name: "IX_Owners_DrivingLicenceLicenceNumber",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "DrivingLicenceLicenceNumber",
                table: "Owners");

            migrationBuilder.RenameColumn(
                name: "OwnerIDNumber",
                table: "VehicleLicences",
                newName: "IDNumber");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleLicences_OwnerIDNumber",
                table: "VehicleLicences",
                newName: "IX_VehicleLicences_IDNumber");

            migrationBuilder.RenameColumn(
                name: "OwnerIDNumber",
                table: "OperatingLicences",
                newName: "IDNumber");

            migrationBuilder.RenameIndex(
                name: "IX_OperatingLicences_OwnerIDNumber",
                table: "OperatingLicences",
                newName: "IX_OperatingLicences_IDNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_DrivingLicences_Owners_IDNumber",
                table: "DrivingLicences",
                column: "IDNumber",
                principalTable: "Owners",
                principalColumn: "IDNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OperatingLicences_Owners_IDNumber",
                table: "OperatingLicences",
                column: "IDNumber",
                principalTable: "Owners",
                principalColumn: "IDNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleLicences_Owners_IDNumber",
                table: "VehicleLicences",
                column: "IDNumber",
                principalTable: "Owners",
                principalColumn: "IDNumber",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
