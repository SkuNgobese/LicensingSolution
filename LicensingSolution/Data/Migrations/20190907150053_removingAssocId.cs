using Microsoft.EntityFrameworkCore.Migrations;

namespace LicensingSolution.Data.Migrations
{
    public partial class removingAssocId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperatingLicences_Associations_AssociationId",
                table: "OperatingLicences");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleLicences_Associations_AssociationId",
                table: "VehicleLicences");

            migrationBuilder.DropIndex(
                name: "IX_VehicleLicences_AssociationId",
                table: "VehicleLicences");

            migrationBuilder.DropIndex(
                name: "IX_OperatingLicences_AssociationId",
                table: "OperatingLicences");

            migrationBuilder.DropColumn(
                name: "AssociationId",
                table: "VehicleLicences");

            migrationBuilder.DropColumn(
                name: "AssociationId",
                table: "OperatingLicences");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssociationId",
                table: "VehicleLicences",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssociationId",
                table: "OperatingLicences",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleLicences_AssociationId",
                table: "VehicleLicences",
                column: "AssociationId");

            migrationBuilder.CreateIndex(
                name: "IX_OperatingLicences_AssociationId",
                table: "OperatingLicences",
                column: "AssociationId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperatingLicences_Associations_AssociationId",
                table: "OperatingLicences",
                column: "AssociationId",
                principalTable: "Associations",
                principalColumn: "AssociationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleLicences_Associations_AssociationId",
                table: "VehicleLicences",
                column: "AssociationId",
                principalTable: "Associations",
                principalColumn: "AssociationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
