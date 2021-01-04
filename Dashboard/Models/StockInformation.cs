using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.Models
{
    public class StockInformation
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string PeRatio { get; set; }
        public string PbRatio { get; set; }
        public string Yield { get; set; }
        public string DividendYear { get; set; }
        public string FinancialQuarter { get; set; }
        public string DataDate { get; set; }
    }
}