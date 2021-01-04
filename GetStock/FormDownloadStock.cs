using Dapper;
using Tool.GetStockData.Models;
using System;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Transactions;
using System.Windows.Forms;

namespace GetStock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDownloadStock_Click(object sender, EventArgs e)
        {
            string OrginalDataDate = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");
            string ConvertDataDate = dateTimePicker1.Value.Date.ToString("yyyyMMdd");
            string DownloadData;
            string Uri = $"https://www.twse.com.tw/exchangeReport/BWIBBU_d?response=csv&date={ConvertDataDate}&selectType=ALL";

            #region HttpWebRequest to get stcok data
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            try
            {
              using (HttpWebResponse aHttpWebResponse =
              (HttpWebResponse)request.GetResponse())
                {
                    using Stream aStream = aHttpWebResponse.GetResponseStream();
                    using StreamReader aReader = new StreamReader(aStream, Encoding.GetEncoding(950));
                    DownloadData = aReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion

            if (DownloadData.Trim().Length == 0)
            {
                MessageBox.Show("No stock data to download!");
                return;
            }

            #region Sql scripts
            StringBuilder UpdateSql = new StringBuilder();
            string InitialSQL = @"INSERT INTO [dbo].[StockInformation]
                                       ([Code],          [Name],          [Yield], 
                                        [DividendYear],  [PeRatio],       [PbRatio],
                                        [FinancialQuarter], [DataDate] )	
                                VALUES (
                                ";
            string DeleteDuplicateDataSql = $"DELETE FROM StockInformation WHERE DataDate = '{OrginalDataDate}' ; ";
            #endregion

            #region Paser the stock data
            CsvReader Csv = new CsvReader();
            string[] LineStrs = DownloadData.Split('\n');
            string Code ;                   // 證券代號
            string Name ;                   // 證券名稱
            string Yield ;                  // 殖利率
            string DividendYear;            // 股利年度
            string PeRatio;                 // 本益比
            string PbRatio;                 // 股價淨值比
            string FinancialQuarter;        // 財報年/季

            for (int i = 0; i < LineStrs.Length; i++)
            {
                string strline = LineStrs[i];

                // First row or no data
                if (i == 0 || i == 1 || strline.Trim().Length == 0)
                    continue;

                // Exclude the sheet description wording
                if (strline.IndexOf("個股日本益比、殖利率及股價淨值比") > -1
                    || strline.IndexOf("說明") > -1
                    || strline.IndexOf("本網頁資料僅供研究參考用途") > -1
                    || strline.IndexOf("本網頁資料推計基礎如下") > -1)
                    continue;

                ArrayList Result = new ArrayList();
                Csv.ParseCSVData(Result, strline);
                string[] StockDatas = (string[])Result.ToArray(typeof(string));

                if (StockDatas.Count() < 7)
                    continue;

                Code = StockDatas[0].Trim();                // 證券代號
                Name = StockDatas[1].Trim();                // 證券名稱
                Yield = StockDatas[2].Trim();               // 殖利率
                DividendYear = StockDatas[3].Trim();        // 股利年度
                PeRatio = StockDatas[4].Trim();             // 本益比
                PbRatio = StockDatas[5].Trim();             // 股價淨值比
                FinancialQuarter = StockDatas[6].Trim();    // 財報年/季

                UpdateSql.Append(InitialSQL);
                UpdateSql.Append($"'{Code}','{Name}','{Yield}','{DividendYear}','{PeRatio}'," +
                                 $"'{PbRatio}','{FinancialQuarter}','{OrginalDataDate}');");
            }

            #endregion

            #region Insert to db
            try
            {
                using (var Conn = new SqlConnection(CommonSetting.DbSetting.DbConnectionString))
                {
                    using (TransactionScope aTransactionScope = new TransactionScope())
                    {
                        Conn.Execute(DeleteDuplicateDataSql);
                        Conn.Execute(UpdateSql.ToString());
                        aTransactionScope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion

            MessageBox.Show($"Download the stock data of {OrginalDataDate} successfully!");
        }
    }
}
