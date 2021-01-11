using ClosedXML.Excel;
using DBConTemplate.Models;
using MySql.Data.MySqlClient;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBConTemplate.Controllers
{
    public class SelectController : Controller
    {
        //ログ
        protected static NLog.Logger log = NLog.LogManager.GetLogger("log");

        //リストページ
        public ActionResult AjaxSelect()
        {
            return View();
        }

        //全件取得
        public ActionResult GetDataFromAjax(int count)
        {
            SelectModel selectModel = new SelectModel();
            String errorMessage = String.Empty;
            try
            {
                //データ取得
                selectModel.GetData(count);
            }
            catch (Exception e)
            {
                log.Fatal(e);
                errorMessage = "データの取得に失敗しました。";
            }
            return Json(new { DATA = selectModel, ErrorMessage = errorMessage });
        }


        //検索
        public ActionResult SearchData(string statusSearch, string chargeSearch)
        {
            SelectModel selectModel = new SelectModel();
            String errorMessage = String.Empty;
            try
            {
                //データ取得
                selectModel.GetSearchData(statusSearch, chargeSearch);
            }
            catch (Exception e)
            {
                log.Fatal(e);
                errorMessage = "データの取得に失敗しました。";
            }
            return Json(new { DATA = selectModel, ErrorMessage = errorMessage });
        }

        //データ削除
        public ActionResult DeleteData(string cd)
        {
            SelectModel selectModel = new SelectModel();
            String errorMessage = String.Empty;
            try
            {
                selectModel.DelData(cd);
            }
            catch (Exception e)
            {
                log.Fatal(e);
                errorMessage = "データの削除に失敗しました。";
            }
            return Json(new { DATA = selectModel, ErrorMessage = errorMessage });
        }

        //新規作成画面
        public ActionResult CreateData()
        {
            return View();
        }

        //新規作成
        public ActionResult SubmitNewData(string companyName, string companyUrl, string status, string charge, string note)
        {
            SelectModel selectModel = new SelectModel();
            String errorMessage = String.Empty;
            try
            {
                selectModel.CreateNewData(companyName, companyUrl, status, charge, note);
            }
            catch (Exception e)
            {
                log.Fatal(e);
                errorMessage = "データの削除に失敗しました。";
            }
            return Json(new { DATA = selectModel, ErrorMessage = errorMessage });
        }

        //データ編集画面
        public ActionResult EditData(string dataCd)
        {
            var SelectModel = new SelectModel();
            try
            {
                SelectModel = SelectModel.GetOneData(dataCd);
            }
            catch (Exception e)
            {
                log.Fatal(e);
            }
            return View("EditData", SelectModel);
        }

        //データ更新
        public ActionResult EditDataSubmit(string cd, string companyName, string companyUrl, string status, string charge, string note)
        {
            SelectModel selectModel = new SelectModel();
            String errorMessage = String.Empty;
            try
            {
                //データ取得
                selectModel.EditData(cd, companyName, companyUrl, status, charge, note);
            }
            catch (Exception e)
            {
                log.Fatal(e);
                errorMessage = "データの削除に失敗しました。";
            }
            return Json(new { DATA = selectModel, ErrorMessage = errorMessage });
        }

        //EXCEL出力
        public void DownloadExcel()
        {
            SelectModel selectModel = new SelectModel();
            selectModel.GetAllData();
            using (var stream = new MemoryStream())
            {
                // Excel ファイル作成
                var workbook = new XLWorkbook();
                //シート作成
                var worksheet =workbook.Worksheets.Add("営業先リスト");
                //ヘッダー書き込み
                worksheet.Cell("A1").Value = "企業名";
                worksheet.Cell("B1").Value = "URL";
                worksheet.Cell("C1").Value = "担当者";
                worksheet.Cell("D1").Value = "状態";
                worksheet.Cell("E1").Value = "備考";
                worksheet.Cell("F1").Value = "更新日";
                //スタイル設定
                worksheet.Range("A1:F1").Style.Border.BottomBorder = XLBorderStyleValues.Double;
                worksheet.Range("A1:F1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                
                //データ書き込み
                for (int i=0; i<selectModel.listData.Count; i++)
                {
                    var number = i + 2;
                    worksheet.Cell("A"+ number).Value = selectModel.listData[i].companyName;
                    worksheet.Cell("B"+ number).Value = selectModel.listData[i].companyUrl;
                    worksheet.Cell("C" + number).Value = selectModel.listData[i].charge;
                    worksheet.Cell("D" + number).Value = selectModel.listData[i].status;
                    worksheet.Cell("E" + number).Value = selectModel.listData[i].note;
                    worksheet.Cell("F" + number).Value = selectModel.listData[i].date;
                }

                // 列の幅を自動調整する
                worksheet.Column("A").AdjustToContents();
                worksheet.Column("B").AdjustToContents();
                worksheet.Column("C").AdjustToContents();
                worksheet.Column("D").AdjustToContents();
                worksheet.Column("E").AdjustToContents();
                worksheet.Column("F").AdjustToContents();

                //ブック保存
                workbook.SaveAs(stream);

                // ダウンロード
                var date = DateTime.Now.ToString("MM.dd");
                var fileName = "営業先リスト" + date;
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Length", stream.Length.ToString());
                Response.AddHeader("Content-Disposition", $"attachment; filename={fileName}.xlsx");
                Response.BinaryWrite(stream.ToArray());
                Response.Flush();
                Response.End();
            }
        }

        //データ編集画面
        public ActionResult AggrGate()
        {
            var SelectModel = new SelectModel();
            try
            {
                SelectModel = SelectModel.GetChartData();
            }
            catch (Exception e)
            {
                log.Fatal(e);
            }
            return View("AggrGate", SelectModel);
        }
    }
}