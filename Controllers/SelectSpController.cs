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
        public ActionResult CreateDataSp()
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
            return View("CreateDataSp", "_LayoutMobile", selectModel);
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
        public ActionResult EditDataSp(string dataCd)
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
            return View("EditDataSp", "_LayoutMobile", SelectModel);
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
    }
}