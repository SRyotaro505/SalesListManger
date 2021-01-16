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
    public class SelectSpController : Controller
    {
        //ログ
        protected static NLog.Logger log = NLog.LogManager.GetLogger("log");

        //リストページ
        public ActionResult AjaxSelectSp()
        {
            SelectModel selectModel = new SelectModel();
            String errorMessage = String.Empty;
            try
            {
                selectModel.GetStatus();
                selectModel.GetUser();
            }
            catch (Exception e)
            {
                log.Fatal(e);
                errorMessage = "データの取得に失敗しました。";
            }
            return View("AjaxSelectSp", "_LayoutMobile", selectModel);
        }

        //全件取得SP
        public ActionResult GetDataFromAjaxSp(int count, int usCd)
        {
            SelectModel selectModel = new SelectModel();
            String errorMessage = String.Empty;
            try
            {
                //データ取得
                selectModel.GetDataSp(count, usCd);
            }
            catch (Exception e)
            {
                log.Fatal(e);
                errorMessage = "データの取得に失敗しました。";
            }
            return Json(new { DATA = selectModel, ErrorMessage = errorMessage });
        }

        //降順切り替え
        public ActionResult DataSortDesc(int count)
        {
            SelectModel selectModel = new SelectModel();
            String errorMessage = String.Empty;
            try
            {
                //データ取得
                selectModel.GetDataDesc(count);
            }
            catch (Exception e)
            {
                log.Fatal(e);
                errorMessage = "データの取得に失敗しました。";
            }
            return Json(new { DATA = selectModel, ErrorMessage = errorMessage });
        }

        //昇順切り替え
        public ActionResult DataSortAsc(int count)
        {
            SelectModel selectModel = new SelectModel();
            String errorMessage = String.Empty;
            try
            {
                //データ取得
                selectModel.GetDataAsc(count);
            }
            catch (Exception e)
            {
                log.Fatal(e);
                errorMessage = "データの取得に失敗しました。";
            }
            return Json(new { DATA = selectModel, ErrorMessage = errorMessage });
        }

        //検索
        public ActionResult SearchData(string statusSearch, string chargeSearch, int count)
        {
            SelectModel selectModel = new SelectModel();
            String errorMessage = String.Empty;
            try
            {
                //データ取得
                selectModel.GetSearchData(statusSearch, chargeSearch, count);
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
            SelectModel selectModel = new SelectModel();
            String errorMessage = String.Empty;
            try
            {
                selectModel.GetStatus();
                selectModel.GetUser();
            }
            catch (Exception e)
            {
                log.Fatal(e);
            }
            return View(selectModel);
        }

        //新規作成
        public ActionResult SubmitNewData(string companyName, string companyUrl, string status, string charge, string note, string mail)
        {
            SelectModel selectModel = new SelectModel();
            String errorMessage = String.Empty;
            try
            {
                if (charge == "")
                {
                    charge = "0";
                }
                selectModel.CreateNewData(companyName, companyUrl, status, charge, note, mail);
                if (selectModel.errorFlg != 0)
                {
                    errorMessage = "データの作成に失敗しました。";
                }
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
                SelectModel = SelectModel.GetStatus();
                SelectModel = SelectModel.GetUser();
            }
            catch (Exception e)
            {
                log.Fatal(e);
            }
            return View("EditData", SelectModel);
        }

        //データ更新
        public ActionResult EditDataSubmit(string cd, string companyName, string companyUrl, string status, string charge, string note, string mail)
        {
            SelectModel selectModel = new SelectModel();
            String errorMessage = String.Empty;
            try
            {
                //データ取得
                selectModel.EditData(cd, companyName, companyUrl, status, charge, note, mail);
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
                var worksheet = workbook.Worksheets.Add("営業先リスト");
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
                for (int i = 0; i < selectModel.listData.Count; i++)
                {
                    var color = XLColor.Yellow;
                    var number = i + 2;
                    worksheet.Cell("A" + number).SetValue(selectModel.listData[i].companyName).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell("B" + number).SetValue(selectModel.listData[i].companyUrl).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell("C" + number).SetValue(selectModel.listData[i].charge).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell("D" + number).SetValue(selectModel.listData[i].status).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell("E" + number).SetValue(selectModel.listData[i].note).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell("F" + number).SetValue(selectModel.listData[i].date).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                    if (selectModel.listData[i].charge == "未定")
                    {
                        worksheet.Cell("A" + number).Style.Fill.SetBackgroundColor(color);
                        worksheet.Cell("B" + number).Style.Fill.SetBackgroundColor(color);
                        worksheet.Cell("C" + number).Style.Fill.SetBackgroundColor(color);
                        worksheet.Cell("D" + number).Style.Fill.SetBackgroundColor(color);
                        worksheet.Cell("E" + number).Style.Fill.SetBackgroundColor(color);
                        worksheet.Cell("F" + number).Style.Fill.SetBackgroundColor(color);
                    }
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
                var date = DateTime.Now.ToString("MMdd");
                var fileName = "SalesList" + date;
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
                //棒グラフ生成データ取得
                SelectModel = SelectModel.GetBarChartData();
                //円グラフ生成データ取得
                SelectModel = SelectModel.GetPieChartData();
            }
            catch (Exception e)
            {
                log.Fatal(e);
            }
            return View("AggrGate", SelectModel);
        }

        //マスタ管理画面
        public ActionResult MasterAdmin()
        {
            return View();
        }

        //ユーザ編集画面
        public ActionResult EditUserList()
        {
            var SelectModel = new SelectModel();
            try
            {
                SelectModel = SelectModel.GetUser();
            }
            catch (Exception e)
            {
                log.Fatal(e);
            }
            return View("EditUserList", SelectModel);
        }

        public ActionResult EditUser(string userCd)
        {
            var SelectModel = new SelectModel();
            try
            {
                SelectModel = SelectModel.GetUserDetail(userCd);
            }
            catch (Exception e)
            {
                log.Fatal(e);
            }
            return View("EditUser", SelectModel);
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        //新規作成
        public ActionResult SubmitNewUser(string userName, string mail, string password)
        {
            SelectModel selectModel = new SelectModel();
            String errorMessage = String.Empty;
            try
            {
                selectModel.CreateNewUser(userName, mail, password);
                if (selectModel.errorFlg != 0)
                {
                    errorMessage = "データの作成に失敗しました。";
                }
            }
            catch (Exception e)
            {
                log.Fatal(e);
                errorMessage = "データの作成に失敗しました。";
            }
            return Json(new { DATA = selectModel, ErrorMessage = errorMessage });
        }

        //ユーザーデータ更新
        public ActionResult EditUserDataSubmit(string cd, string userName, string mail, string password)
        {
            SelectModel selectModel = new SelectModel();
            String errorMessage = String.Empty;
            try
            {
                //データ取得
                selectModel.EditUserData(cd, userName, mail, password);
            }
            catch (Exception e)
            {
                log.Fatal(e);
                errorMessage = "データの削除に失敗しました。";
            }
            return Json(new { DATA = selectModel, ErrorMessage = errorMessage });
        }

        //ユーザーデータ削除
        public ActionResult DeleteUserDataSubmit(string cd)
        {
            SelectModel selectModel = new SelectModel();
            String errorMessage = String.Empty;
            try
            {
                //データ取得
                selectModel.DeleteUser(cd);
            }
            catch (Exception e)
            {
                log.Fatal(e);
                errorMessage = "データの削除に失敗しました。";
            }
            return Json(new { DATA = selectModel, ErrorMessage = errorMessage });
        }

        //ユーザ編集画面
        public ActionResult EditStatusList()
        {
            var SelectModel = new SelectModel();
            try
            {
                SelectModel = SelectModel.GetStatus();
            }
            catch (Exception e)
            {
                log.Fatal(e);
            }
            return View("EditStatusList", SelectModel);
        }

        public ActionResult CreateStatus()
        {
            return View();
        }

        //新規作成
        public ActionResult SubmitNewStatus(string statusName)
        {
            SelectModel selectModel = new SelectModel();
            String errorMessage = String.Empty;
            try
            {
                selectModel.CreateNewStatus(statusName);
                if (selectModel.errorFlg != 0)
                {
                    errorMessage = "データの作成に失敗しました。";
                }
            }
            catch (Exception e)
            {
                log.Fatal(e);
                errorMessage = "データの作成に失敗しました。";
            }
            return Json(new { DATA = selectModel, ErrorMessage = errorMessage });
        }

        //状態編集
        public ActionResult EditStatus(string statusCd)
        {
            var SelectModel = new SelectModel();
            try
            {
                SelectModel = SelectModel.GetStatusDetail(statusCd);
            }
            catch (Exception e)
            {
                log.Fatal(e);
            }
            return View("EditStatus", SelectModel);
        }

        //ユーザーデータ更新
        public ActionResult EditStatusSubmit(string cd, string statusName)
        {
            SelectModel selectModel = new SelectModel();
            String errorMessage = String.Empty;
            try
            {
                selectModel.EditStatus(cd, statusName);
            }
            catch (Exception e)
            {
                log.Fatal(e);
                errorMessage = "データの削除に失敗しました。";
            }
            return Json(new { DATA = selectModel, ErrorMessage = errorMessage });
        }

        //ユーザーデータ削除
        public ActionResult DeleteStatusSubmit(string cd)
        {
            SelectModel selectModel = new SelectModel();
            String errorMessage = String.Empty;
            try
            {
                //データ取得
                selectModel.DeleteStatus(cd);
            }
            catch (Exception e)
            {
                log.Fatal(e);
                errorMessage = "データの削除に失敗しました。";
            }
            return Json(new { DATA = selectModel, ErrorMessage = errorMessage });
        }
    }
}