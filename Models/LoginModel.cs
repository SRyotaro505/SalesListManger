using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DBConTemplate.Models
{
    public class LoginModel : BaseModel
    {
        /// <summary>
        /// CD
        /// </summary>
        public string cd { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// 名前
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 更新日
        /// </summary>
        public string date { get; set; }

        /// <summary>
        /// データ総件数
        /// </summary>
        public int dataCount { get; set; }

        /// <summary>
        /// データリスト
        /// </summary>
        public List<rowData> listData { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LoginModel()
        {
            this.listData = new List<rowData>();
        }

        public class rowData
        {
            public string cd { get; set; }
            public string id { get; set; }
            public string password { get; set; }
            public string name { get; set; }
            public string date { get; set; }
            public int dataCount { get; set; }
        }

        //全件取得
        public LoginModel AuthUser(string id, string password)
        {
            //DB接続
            var conn = base.OpenDbConnect();
            try
            {
                SHA256 sha256 = SHA256.Create();
                byte[] encoded = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(encoded);
                string hashed = string.Concat(hash.Select(b => $"{b:x2}"));

                //Query生成
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT * FROM USER_AUTH");
                query.AppendLine("WHERE");
                query.AppendLine("id='" + id + "'");
                query.AppendLine("AND password='" + hashed + "'");

                //クエリ実行
                DataTable dt = this.SqlSelect(conn, query.ToString());

                //総件数取得
                dt = this.SqlSelect(conn, query.ToString());
                this.dataCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                throw ex;
            }
            finally
            {
                //DB切断
                base.CloseDbConnect(conn);
            }
            return this;
        }
    }
}