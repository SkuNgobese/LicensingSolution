using Microsoft.EntityFrameworkCore.Migrations;

namespace LicensingSolution.Data.Migrations
{
    public partial class owner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperatingLicences_Owners_IDNumber",
                table: "OperatingLicences");

            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                table: "Owners",
                newName: "Email");

            migrationBuilder.AlterColumn<string>(
                name: "IDNumber",
                table: "OperatingLicences",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OperatingLicences_Owners_IDNumber",
                table: "OperatingLicences",
                column: "IDNumber",
                principalTable: "Owners",
                principalColumn: "IDNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperatingLicences_Owners_IDNumber",
                table: "OperatingLicences");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Owners",
                newName: "EmailAddress");

            migrationBuilder.AlterColumn<string>(
                name: "IDNumber",
                table: "OperatingLicences",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_OperatingLicences_Owners_IDNumber",
                table: "OperatingLicences",
                column: "IDNumber",
                principalTable: "Owners",
                principalColumn: "IDNumber",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
