using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DBConTemplate
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //DB�ڑ����
        public static readonly string DB_CONNECT_INFO = ConfigurationManager.AppSettings["DB_CONNECT_INFO"];
        //�������ʕ\������
        public static readonly string SEARCH_LIST_COUNT = ConfigurationManager.AppSettings["SEARCH_LIST_COUNT"];
        //�y�[�W���[�\������
        public static readonly string PAGE_LIST_COUNT = ConfigurationManager.AppSettings["PAGE_LIST_COUNT"];
    }
}
