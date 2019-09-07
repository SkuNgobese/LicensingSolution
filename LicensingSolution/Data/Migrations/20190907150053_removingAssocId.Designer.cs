﻿// <auto-generated />
using System;
using LicensingSolution.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LicensingSolution.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190907150053_removingAssocId")]
    partial class removingAssocId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LicensingSolution.Models.Association", b =>
                {
                    b.Property<int>("AssociationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("AssociationId");

                    b.ToTable("Associations");
                });

            modelBuilder.Entity("LicensingSolution.Models.Driver", b =>
                {
                    b.Property<string>("DriverId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("ImgPath");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("MiddleName")
                        .HasMaxLength(50);

                    b.Property<string>("OwnerId")
                        .IsRequired();

                    b.HasKey("DriverId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("LicensingSolution.Models.DrivingLicence", b =>
                {
                    b.Property<string>("LicenceNumber")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DriverId");

                    b.Property<DateTime>("LicenceExpiryDate");

                    b.Property<DateTime>("PDPExpiryDate");

                    b.HasKey("LicenceNumber");

                    b.HasIndex("DriverId")
                        .IsUnique()
                        .HasFilter("[DriverId] IS NOT NULL");

                    b.ToTable("DrivingLicences");
                });

            modelBuilder.Entity("LicensingSolution.Models.OperatingLicence", b =>
                {
                    b.Property<string>("OperatingLicenceNumber")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20);

                    b.Property<string>("ApplicationNumber")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("AssociationName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("EngineNumber")
                        .IsRequired();

                    b.Property<string>("Manufacturer")
                        .IsRequired();

                    b.Property<string>("OwnerId");

                    b.Property<int>("Passengers");

                    b.Property<DateTime>("ValidFrom");

                    b.Property<DateTime>("ValidUntil");

                    b.Property<string>("VehDescription")
                        .IsRequired();

                    b.Property<int>("VehMass");

                    b.Property<string>("VehRegistrationNumber")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<int>("YearOfReg");

                    b.HasKey("OperatingLicenceNumber");

                    b.HasIndex("OwnerId");

                    b.ToTable("OperatingLicences");
                });

            modelBuilder.Entity("LicensingSolution.Models.Owner", b =>
                {
                    b.Property<string>("OwnerId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AssociationId");

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("EmailAddress");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("MiddleName")
                        .HasMaxLength(50);

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.Property<string>("PrimaryContactNumber");

                    b.Property<string>("SecondaryContactNumber");

                    b.Property<string>("Street")
                        .IsRequired();

                    b.Property<string>("Suburb")
                        .IsRequired();

                    b.HasKey("OwnerId");

                    b.HasIndex("AssociationId");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("LicensingSolution.Models.VehicleLicence", b =>
                {
                    b.Property<string>("VehRegisterNumber")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateOfExpiry");

                    b.Property<string>("OwnerId");

                    b.Property<string>("RegisteringAuthority")
                        .IsRequired();

                    b.Property<string>("VehIdentificationNumber")
                        .IsRequired();

                    b.Property<string>("VehLicenceNumber")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("VehMake")
                        .IsRequired();

                    b.HasKey("VehRegisterNumber");

                    b.HasIndex("OwnerId");

                    b.ToTable("VehicleLicences");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("LicensingSolution.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<int>("AssociationId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleName");

                    b.Property<string>("Title");

                    b.HasIndex("AssociationId");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("LicensingSolution.Models.Driver", b =>
                {
                    b.HasOne("LicensingSolution.Models.Owner", "Owner")
                        .WithMany("Drivers")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LicensingSolution.Models.DrivingLicence", b =>
                {
                    b.HasOne("LicensingSolution.Models.Driver", "Driver")
                        .WithOne("DrivingLicence")
                        .HasForeignKey("LicensingSolution.Models.DrivingLicence", "DriverId");
                });

            modelBuilder.Entity("LicensingSolution.Models.OperatingLicence", b =>
                {
                    b.HasOne("LicensingSolution.Models.Owner", "Owner")
                        .WithMany("OperatingLicences")
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("LicensingSolution.Models.Owner", b =>
                {
                    b.HasOne("LicensingSolution.Models.Association", "Association")
                        .WithMany("Owners")
                        .HasForeignKey("AssociationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LicensingSolution.Models.VehicleLicence", b =>
                {
                    b.HasOne("LicensingSolution.Models.Owner", "Owner")
                        .WithMany("VehicleLicences")
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LicensingSolution.Models.ApplicationUser", b =>
                {
                    b.HasOne("LicensingSolution.Models.Association", "Association")
                        .WithMany("ApplicationUsers")
                        .HasForeignKey("AssociationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
