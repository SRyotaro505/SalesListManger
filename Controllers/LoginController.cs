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
    public class LoginController : Controller
    {
        //ログ
        protected static NLog.Logger log = NLog.LogManager.GetLogger("log");

        //ログインページ
        public ActionResult Login()
        {
            return View("Login","_LoginLayout");
        }

        //認証
        public ActionResult Auth(string id, string password)
        {
            LoginModel LoginModel = new LoginModel();
            String errorMessage = String.Empty;
            try
            {
                LoginModel.AuthUser(id, password);
                if (LoginModel.dataCount == 0)
                {
                    errorMessage = "IDまたはパスワードが違います。";
                }
            }
            catch (Exception e)
            {
                log.Fatal(e);
                errorMessage = "IDまたはパスワードが違います。";
            }
            return Json(new { DATA = LoginModel, ErrorMessage = errorMessage });
        }
    }
}