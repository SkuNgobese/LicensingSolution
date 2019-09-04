using Microsoft.EntityFrameworkCore.Migrations;

namespace LicensingSolution.Data.Migrations
{
    public partial class final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Driver_DrivingLicences_LicenceNumber",
                table: "Driver");

            migrationBuilder.DropForeignKey(
                name: "FK_Driver_Owners_OwnerIDNumber",
                table: "Driver");

            migrationBuilder.DropForeignKey(
                name: "FK_Owners_DrivingLicences_DrivingLicenceLicenceNumber",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Owners_DrivingLicenceLicenceNumber",
                table: "Owners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Driver",
                table: "Driver");

            migrationBuilder.DropIndex(
                name: "IX_Driver_LicenceNumber",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "DrivingLicenceLicenceNumber",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "LicenceNumber",
                table: "Driver");

            migrationBuilder.RenameTable(
                name: "Driver",
                newName: "Drivers");

            migrationBuilder.RenameIndex(
                name: "IX_Driver_OwnerIDNumber",
                table: "Drivers",
                newName: "IX_Drivers_OwnerIDNumber");

            migrationBuilder.AlterColumn<int>(
                name: "VehMass",
                table: "OperatingLicences",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "LicenceNumber",
                table: "DrivingLicences",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 12);

            migrationBuilder.AddColumn<string>(
                name: "DriverIDNumber",
                table: "DrivingLicences",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drivers",
                table: "Drivers",
                column: "IDNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DrivingLicences_DriverIDNumber",
                table: "DrivingLicences",
                column: "DriverIDNumber",
                unique: true,
                filter: "[DriverIDNumber] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Owners_OwnerIDNumber",
                table: "Drivers",
                column: "OwnerIDNumber",
                principalTable: "Owners",
                principalColumn: "IDNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DrivingLicences_Drivers_DriverIDNumber",
                table: "DrivingLicences",
                column: "DriverIDNumber",
                principalTable: "Drivers",
                principalColumn: "IDNumber",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Owners_OwnerIDNumber",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_DrivingLicences_Drivers_DriverIDNumber",
                table: "DrivingLicences");

            migrationBuilder.DropIndex(
                name: "IX_DrivingLicences_DriverIDNumber",
                table: "DrivingLicences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drivers",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "DriverIDNumber",
                table: "DrivingLicences");

            migrationBuilder.RenameTable(
                name: "Drivers",
                newName: "Driver");

            migrationBuilder.RenameIndex(
                name: "IX_Drivers_OwnerIDNumber",
                table: "Driver",
                newName: "IX_Driver_OwnerIDNumber");

            migrationBuilder.AddColumn<string>(
                name: "DrivingLicenceLicenceNumber",
                table: "Owners",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "VehMass",
                table: "OperatingLicences",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "LicenceNumber",
                table: "DrivingLicences",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "LicenceNumber",
                table: "Driver",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Driver",
                table: "Driver",
                column: "IDNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_DrivingLicenceLicenceNumber",
                table: "Owners",
                column: "DrivingLicenceLicenceNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Driver_LicenceNumber",
                table: "Driver",
                column: "LicenceNumber",
                unique: true,
                filter: "[LicenceNumber] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Driver_DrivingLicences_LicenceNumber",
                table: "Driver",
                column: "LicenceNumber",
                principalTable: "DrivingLicences",
                principalColumn: "LicenceNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Driver_Owners_OwnerIDNumber",
                table: "Driver",
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
        }
    }
}
