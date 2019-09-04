using Microsoft.EntityFrameworkCore.Migrations;

namespace LicensingSolution.Data.Migrations
{
    public partial class driverowner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrivingLicences_Driver_IDNumber",
                table: "DrivingLicences");

            migrationBuilder.DropIndex(
                name: "IX_DrivingLicences_IDNumber",
                table: "DrivingLicences");

            migrationBuilder.DropColumn(
                name: "IDNumber",
                table: "DrivingLicences");

            migrationBuilder.AddColumn<string>(
                name: "LicenceNumber",
                table: "Driver",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Driver_DrivingLicences_LicenceNumber",
                table: "Driver");

            migrationBuilder.DropIndex(
                name: "IX_Driver_LicenceNumber",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "LicenceNumber",
                table: "Driver");

            migrationBuilder.AddColumn<string>(
                name: "IDNumber",
                table: "DrivingLicences",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DrivingLicences_IDNumber",
                table: "DrivingLicences",
                column: "IDNumber",
                unique: true,
                filter: "[IDNumber] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_DrivingLicences_Driver_IDNumber",
                table: "DrivingLicences",
                column: "IDNumber",
                principalTable: "Driver",
                principalColumn: "IDNumber",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
