using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LicensingSolution.Data.Migrations
{
    public partial class driverphoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "Driver");

            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Driver",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Driver");

            migrationBuilder.AddColumn<byte[]>(
                name: "Img",
                table: "Driver",
                nullable: true);
        }
    }
}
