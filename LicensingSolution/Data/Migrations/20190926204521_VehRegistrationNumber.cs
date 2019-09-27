using Microsoft.EntityFrameworkCore.Migrations;

namespace LicensingSolution.Data.Migrations
{
    public partial class VehRegistrationNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OperatingLicences",
                table: "OperatingLicences");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Config",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OperatingLicences",
                table: "OperatingLicences",
                column: "VehRegistrationNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OperatingLicences",
                table: "OperatingLicences");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Config",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OperatingLicences",
                table: "OperatingLicences",
                column: "OperatingLicenceNumber");
        }
    }
}
