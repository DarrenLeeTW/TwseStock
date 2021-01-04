using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Tool.GetStockData.Model
{
    class Config
    {
        public static string ConnectionString { get => ConfigurationManager.AppSettings["ConnectionString"]; }
    }
}
