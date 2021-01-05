# TwseStock

爬蟲程式(Tool.GetStockData)從證交所下載證券資料，並呈現在網頁(StockDashboard)上，網頁可依照時間區間查詢證券個股日本益比、殖利率及股價淨值比
使用技術:HttpRequest取證券資料(csv檔案)，ORM Dapper存取資料庫，NET MVC網頁


**使用說明:**

先在local端建立MS SQL DB:

1.建立DB Category:StockDashboard

2.執行sql script,建立table:StockInformation

[Download sql script](https://drive.google.com/file/d/1XaOS3nI8umbYRxB_K0VEBztp1uxTCeKL/view?usp=sharing)

3.在CommonSetting\DbSetting.cs加入table:StockInformation帳號密碼

4.執行Tool.GetStockData: 從證券交易所下載證券資料的工具，先使用此工具將指定日期的證券資料匯入

5.執行網站Dashboard:可依照時間區間查詢證券個股日本益比、殖利率及股價淨值比
