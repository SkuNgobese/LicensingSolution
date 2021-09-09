using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicensingSolution.Models.ViewModels
{
    public class ExpiringListViewModel
    {
        public IEnumerable<Driver> Drivers { get; set; }
        public IEnumerable<OperatingLicence> OperatingLicences { get; set; }
        public IEnumerable<VehicleLicence> VehicleLicences { get; set; }
    }
}
