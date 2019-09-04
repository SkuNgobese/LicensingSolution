using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LicensingSolution.Data.Migrations
{
    public partial class association : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "IDNumber",
                table: "Owners",
                newName: "OwnerId");

            migrationBuilder.RenameColumn(
                name: "DriverIDNumber",
                table: "DrivingLicences",
                newName: "DriverId");

            migrationBuilder.RenameColumn(
                name: "OwnerIDNumber",
                table: "Drivers",
                newName: "OwnerId");

            migrationBuilder.RenameColumn(
                name: "IDNumber",
                table: "Drivers",
                newName: "DriverId");

            migrationBuilder.RenameIndex(
                name: "IX_Drivers_OwnerIDNumber",
                table: "Drivers",
                newName: "IX_Drivers_OwnerId");

            migrationBuilder.AddColumn<int>(
                name: "AssociationId",
                table: "VehicleLicences",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssociationId",
                table: "Owners",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AssociationId",
                table: "OperatingLicences",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Association",
                columns: table => new
                {
                    AssociationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Association", x => x.AssociationId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    AssociationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Association_AssociationId",
                        column: x => x.AssociationId,
                        principalTable: "Association",
                        principalColumn: "AssociationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleLicences_AssociationId",
                table: "VehicleLicences",
                column: "AssociationId");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_AssociationId",
                table: "Owners",
                column: "AssociationId");

            migrationBuilder.CreateIndex(
                name: "IX_OperatingLicences_AssociationId",
                table: "OperatingLicences",
                column: "AssociationId");

            migrationBuilder.CreateIndex(
                name: "IX_DrivingLicences_DriverId",
                table: "DrivingLicences",
                column: "DriverId",
                unique: true,
                filter: "[DriverId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_AssociationId",
                table: "User",
                column: "AssociationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Owners_OwnerId",
                table: "Drivers",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "OwnerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DrivingLicences_Drivers_DriverId",
                table: "DrivingLicences",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "DriverId",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Owners_OwnerId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_DrivingLicences_Drivers_DriverId",
                table: "DrivingLicences");

            migrationBuilder.DropForeignKey(
                name: "FK_OperatingLicences_Association_AssociationId",
                table: "OperatingLicences");

            migrationBuilder.DropForeignKey(
                name: "FK_Owners_Association_AssociationId",
                table: "Owners");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleLicences_Association_AssociationId",
                table: "VehicleLicences");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Association");

            migrationBuilder.DropIndex(
                name: "IX_VehicleLicences_AssociationId",
                table: "VehicleLicences");

            migrationBuilder.DropIndex(
                name: "IX_Owners_AssociationId",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_OperatingLicences_AssociationId",
                table: "OperatingLicences");

            migrationBuilder.DropIndex(
                name: "IX_DrivingLicences_DriverId",
                table: "DrivingLicences");

            migrationBuilder.DropColumn(
                name: "AssociationId",
                table: "VehicleLicences");

            migrationBuilder.DropColumn(
                name: "AssociationId",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "AssociationId",
                table: "OperatingLicences");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Owners",
                newName: "IDNumber");

            migrationBuilder.RenameColumn(
                name: "DriverId",
                table: "DrivingLicences",
                newName: "DriverIDNumber");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Drivers",
                newName: "OwnerIDNumber");

            migrationBuilder.RenameColumn(
                name: "DriverId",
                table: "Drivers",
                newName: "IDNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Drivers_OwnerId",
                table: "Drivers",
                newName: "IX_Drivers_OwnerIDNumber");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

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
    }
}
