using Microsoft.EntityFrameworkCore.Migrations;

namespace LicensingSolution.Data.Migrations
{
    public partial class association1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Association_AssociationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_OperatingLicences_Association_AssociationId",
                table: "OperatingLicences");

            migrationBuilder.DropForeignKey(
                name: "FK_Owners_Association_AssociationId",
                table: "Owners");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleLicences_Association_AssociationId",
                table: "VehicleLicences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Association",
                table: "Association");

            migrationBuilder.RenameTable(
                name: "Association",
                newName: "Associations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Associations",
                table: "Associations",
                column: "AssociationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Associations_AssociationId",
                table: "AspNetUsers",
                column: "AssociationId",
                principalTable: "Associations",
                principalColumn: "AssociationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperatingLicences_Associations_AssociationId",
                table: "OperatingLicences",
                column: "AssociationId",
                principalTable: "Associations",
                principalColumn: "AssociationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_Associations_AssociationId",
                table: "Owners",
                column: "AssociationId",
                principalTable: "Associations",
                principalColumn: "AssociationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleLicences_Associations_AssociationId",
                table: "VehicleLicences",
                column: "AssociationId",
                principalTable: "Associations",
                principalColumn: "AssociationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Associations_AssociationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_OperatingLicences_Associations_AssociationId",
                table: "OperatingLicences");

            migrationBuilder.DropForeignKey(
                name: "FK_Owners_Associations_AssociationId",
                table: "Owners");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleLicences_Associations_AssociationId",
                table: "VehicleLicences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Associations",
                table: "Associations");

            migrationBuilder.RenameTable(
                name: "Associations",
                newName: "Association");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Association",
                table: "Association",
                column: "AssociationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Association_AssociationId",
                table: "AspNetUsers",
                column: "AssociationId",
                principalTable: "Association",
                principalColumn: "AssociationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperatingLicences_Association_AssociationId",
                table: "OperatingLicences",
                column: "AssociationId",
                principalTable: "Association",
                principalColumn: "AssociationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_Association_AssociationId",
                table: "Owners",
                column: "AssociationId",
                principalTable: "Association",
                principalColumn: "AssociationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleLicences_Association_AssociationId",
                table: "VehicleLicences",
                column: "AssociationId",
                principalTable: "Association",
                principalColumn: "AssociationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
