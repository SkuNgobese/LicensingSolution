using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LicensingSolution.Models;

namespace LicensingSolution.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<LicensingSolution.Models.Owner> Owners { get; set; }
        public DbSet<LicensingSolution.Models.Driver> Drivers { get; set; }
        public DbSet<LicensingSolution.Models.DrivingLicence> DrivingLicences { get; set; }
        public DbSet<LicensingSolution.Models.OperatingLicence> OperatingLicences { get; set; }
        public DbSet<LicensingSolution.Models.VehicleLicence> VehicleLicences { get; set; }
        public DbSet<LicensingSolution.Models.Association> Associations { get; set; }
    }
}
