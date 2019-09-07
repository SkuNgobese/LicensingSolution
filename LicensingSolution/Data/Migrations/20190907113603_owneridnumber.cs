using Microsoft.EntityFrameworkCore.Migrations;

namespace LicensingSolution.Data.Migrations
{
    public partial class owneridnumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperatingLicences_Owners_OwnerIDNumber",
                table: "OperatingLicences");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleLicences_Owners_OwnerIDNumber",
                table: "VehicleLicences");

            migrationBuilder.RenameColumn(
                name: "OwnerIDNumber",
                table: "VehicleLicences",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleLicences_OwnerIDNumber",
                table: "VehicleLicences",
                newName: "IX_VehicleLicences_OwnerId");

            migrationBuilder.RenameColumn(
                name: "OwnerIDNumber",
                table: "OperatingLicences",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_OperatingLicences_OwnerIDNumber",
                table: "OperatingLicences",
                newName: "IX_OperatingLicences_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperatingLicences_Owners_OwnerId",
                table: "OperatingLicences",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "OwnerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleLicences_Owners_OwnerId",
                table: "VehicleLicences",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "OwnerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperatingLicences_Owners_OwnerId",
                table: "OperatingLicences");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleLicences_Owners_OwnerId",
                table: "VehicleLicences");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "VehicleLicences",
                newName: "OwnerIDNumber");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleLicences_OwnerId",
                table: "VehicleLicences",
                newName: "IX_VehicleLicences_OwnerIDNumber");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "OperatingLicences",
                newName: "OwnerIDNumber");

            migrationBuilder.RenameIndex(
                name: "IX_OperatingLicences_OwnerId",
                table: "OperatingLicences",
                newName: "IX_OperatingLicences_OwnerIDNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_OperatingLicences_Owners_OwnerIDNumber",
                table: "OperatingLicences",
                column: "OwnerIDNumber",
                principalTable: "Owners",
                principalColumn: "OwnerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleLicences_Owners_OwnerIDNumber",
                table: "VehicleLicences",
                column: "OwnerIDNumber",
                principalTable: "Owners",
                principalColumn: "OwnerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
