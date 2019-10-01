using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicensingSolution.Models.ViewModels
{
    public class ReportViewModel
    {
        public string DimensionOne { get; set; }
        public int Quantity { get; set; }
        public int TotalQuantity { get; set; }
    }
}
