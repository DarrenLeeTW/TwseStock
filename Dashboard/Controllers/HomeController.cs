using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Dapper;
using Dashboard.Models;

namespace Dashboard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetStocks(string StockCodeOrName, string StartDataDate, string EndDataDate)
        {
            #region Sql scripts
            string InitialSQL = @"
 SELECT Code
       ,Name
       ,Yield
       ,DividendYear
       ,PeRatio
       ,PbRatio
       ,FinancialQuarter
       ,DataDate
  FROM  StockInformation WHERE 1 = 1
";
            StringBuilder ExecuteSql = new StringBuilder();
            ExecuteSql.Append(InitialSQL);
            if (StockCodeOrName != String.Empty)
                ExecuteSql.Append(" AND (Code = @StockCodeOrName OR Name = @StockCodeOrName)");

            if (StartDataDate != String.Empty && EndDataDate != String.Empty)
                ExecuteSql.Append(" AND DataDate BETWEEN @StartDataDate AND @EndDataDate ");

            ExecuteSql.Append(" ORDER BY DataDate DESC, Name DESC");
            #endregion

            List<StockInformation> StockInformations;

            #region Get stocks from db
            using (var Conn = new SqlConnection(CommonSetting.DbSetting.DbConnectionString))
            {
                StockInformations = Conn.Query<StockInformation>(ExecuteSql.ToString(),
                     new { StockCodeOrName, StartDataDate, EndDataDate }
                    ).ToList();
            }
            #endregion

            var jsonData = new {
                rows = StockInformations
            };

            return Json(jsonData);
        }
    }
}