using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace DBConTemplate.Models
{
    public class BaseModel
    {
        //ログ
        protected static NLog.Logger log = NLog.LogManager.GetLogger("log");

        //MySql関連
        protected MySqlConnection conn { get; set; }
        protected MySqlTransaction transaction { get; set; }

        //コンストラクタ
        public BaseModel()
        {
            //総件数
            this.totalRecordCount = 0;
            //ページ番号
            this.dispCount = int.Parse(MvcApplication.SEARCH_LIST_COUNT);
            //ページャー表示件数
            this.pageListCount = int.Parse(MvcApplication.PAGE_LIST_COUNT);
        }

        //ページ番号
        public int pageIndex { get; set; }

        //総件数
        public int totalRecordCount { get; set; }

        //検索結果表示件数
        public int dispCount { get; set; }

        //ページャー表示件数
        public int pageListCount { get; set; }

        //DB接続
        public MySqlConnection OpenDbConnect()
        {
            try
            {
                this.conn = new MySqlConnection(MvcApplication.DB_CONNECT_INFO);
                this.conn.Open();
                return this.conn;
            }
            catch(Exception ex)
            {
                log.Fatal(ex);
                throw ex;
            }
        }

        //DB切断
        public void CloseDbConnect(MySqlConnection conn)
        {
            try
            {
                if(conn != null)
                {
                    conn.Close();
                }
            }
            catch(Exception ex)
            {
                log.Fatal(ex);
                throw ex;
            }
        }

        //トランザクション開始
        public MySqlTransaction TranStart(MySqlConnection conn)
        {
            try
            {
                this.transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                return this.transaction;
            }
            catch(Exception ex)
            {
                log.Fatal(ex);
                throw ex;
            }
        }

        //トランザクションをコミット
        public void TranCommit(MySqlTransaction transaction)
        {
            try
            {
                transaction.Commit();
            }
            catch(Exception ex)
            {
                log.Fatal(ex);
                throw ex;
            }
        }

        //トランザクションをロールバック
        public void TranRollBack(MySqlTransaction transaction)
        {
            try
            {
                transaction.Rollback();
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                throw ex;
            }
        }

        //SQL実行
        public void SqlExecution(MySqlConnection conn, MySqlTransaction transaction, string sql)
        {
            try
            {
                MySqlCommand command = new MySqlCommand();
                command.CommandText = sql;
                command.Connection = conn;
                command.Transaction = transaction;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                throw ex;
            }
        }

        //SQLを実行し、データテーブルを返す
        public DataTable SqlSelect(MySqlConnection conn, string sql)
        {
            try
            {
                //データを格納するテーブルを作成
                DataTable dt = new DataTable();
                //SQL文と接続情報を指定し、データアダプタを作成
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //データ取得
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                throw ex;
            }
        }

        //リミットSQL文を取得する
        public string GetLimit()
        {
            string limit = String.Empty;
            long start = 0;

            if(this.pageIndex >= 1)
            {
                start = (this.pageIndex) * this.dispCount; 
            }

            limit = "limit " + start.ToString() + "," + this.dispCount.ToString();

            return limit;
        }
    }
}