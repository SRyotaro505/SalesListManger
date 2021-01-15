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
    public class SelectModel : BaseModel
    {
        /// <summary>
        /// CD
        /// </summary>
        public string cd { get; set; }

        /// <summary>
        /// 企業URL
        /// </summary>
        [Display(Name = "企業URL")]
        [Required(ErrorMessage = "{0}を入力してください。")]
        public string companyUrl { get; set; }

        /// <summary>
        /// 企業名
        /// </summary>
        [Display(Name = "企業名")]
        [Required(ErrorMessage = "{0}を入力してください。")]
        public string companyName { get; set; }

        /// <summary>
        /// ステータス
        /// </summary>
        [Display(Name = "状態")]
        [Required(ErrorMessage = "{0}を選択してください。")]
        public string status { get; set; }

        /// <summary>
        /// 担当者
        /// </summary>
        [Display(Name = "担当者")]
        [Required(ErrorMessage = "{0}を入力してください。")]
        public string charge { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        public string note { get; set; }

        /// <summary>
        /// 更新日
        /// </summary>
        public string date { get; set; }

        /// <summary>
        /// データ総件数
        /// </summary>
        public int dataCount { get; set; }

        /// <summary>
        /// 棒グラフカウント
        /// </summary>
        public int barCount1 { get; set; }

        /// <summary>
        /// 棒グラフカウント
        /// </summary>
        public int barCount2 { get; set; }

        /// <summary>
        /// 棒グラフカウント
        /// </summary>
        public int barCount3 { get; set; }

        /// <summary>
        /// 棒グラフカウント
        /// </summary>
        public int barCount4 { get; set; }

        /// <summary>
        /// 棒グラフカウント
        /// </summary>
        public int barCount5 { get; set; }

        /// <summary>
        /// 円グラフカウント
        /// </summary>
        public int pieCount1 { get; set; }

        /// <summary>
        /// 円グラフカウント
        /// </summary>
        public int pieCount2 { get; set; }

        /// <summary>
        /// 円グラフカウント
        /// </summary>
        public int pieCount3 { get; set; }

        /// <summary>
        /// 円グラフカウント
        /// </summary>
        public int pieCount4 { get; set; }

        /// <summary>
        /// 円グラフカウント
        /// </summary>
        public int pieCount5 { get; set; }

        /// <summary>
        /// エラーフラグ
        /// </summary>
        public int errorFlg { get; set; }

        /// <summary>
        /// 名前
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// メールアドレス
        /// </summary>
        public string mail { get; set; }

        /// <summary>
        /// データリスト
        /// </summary>
        public List<rowData> listData { get; set; }

        /// <summary>
        /// データリスト
        /// </summary>
        public List<statusData> statusList { get; set; }

        /// <summary>
        /// データリスト
        /// </summary>
        public List<userData> userList { get; set; }

        /// <summary>
        /// 棒グラフデータリスト
        /// </summary>
        public List<barChartData> barChartList { get; set; }

        /// <summary>
        /// 円グラフデータリスト
        /// </summary>
        public List<pieChartData> pieChartList { get; set; }

        public Dictionary<string, int> barChartDic = new Dictionary<string, int>();

        public Dictionary<string, int> pieChartDic = new Dictionary<string, int>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SelectModel()
        {
            this.listData = new List<rowData>();
            this.statusList = new List<statusData>();
            this.userList = new List<userData>();
            this.barChartList = new List<barChartData>();
            this.pieChartList = new List<pieChartData>();
            this.barChartDic = new Dictionary<string, int>();
            this.pieChartDic = new Dictionary<string, int>();
        }

        public class rowData
        {
            public string cd { get; set; }
            public string companyUrl { get; set; }
            public string companyName { get; set; }
            public string status { get; set; }
            public string charge { get; set; }
            public string note { get; set; }
            public string date { get; set; }
            public int dataCount { get; set; }
        }

        public class statusData
        {
            public string cd { get; set; }
            public string status { get; set; }
        }

        public class userData
        {
            public string cd { get; set; }
            public string userName { get; set; }
            public string mail { get; set; }
            public string password { get; set; }
        }

        public class barChartData
        {
            public string statusId { get; set; }
        }

        public class pieChartData
        {
            public string userCd { get; set; }
        }

        //ステータス取得
        public SelectModel GetStatus()
        {
            //DB接続
            var conn = base.OpenDbConnect();
            try
            {
                //Query生成
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT * FROM MST_STATUS");

                //クエリ実行
                DataTable dt = this.SqlSelect(conn, query.ToString());

                //実行結果を格納
                foreach (DataRow row in dt.Rows)
                {
                    statusData data = new statusData();
                    data.cd = row["cd"].ToString();
                    data.status = row["status"].ToString();
                    this.statusList.Add(data);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                this.errorFlg = 1;
                throw ex;
            }
            finally
            {
                //DB切断
                base.CloseDbConnect(conn);
            }
            return this;
        }

        //ステータス取得
        public SelectModel GetStatusDetail(string statusCd)
        {
            //DB接続
            var conn = base.OpenDbConnect();
            try
            {
                //Query生成
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT * FROM MST_STATUS");
                query.AppendLine("WHERE");
                query.AppendLine("cd='" + statusCd + "'");

                //クエリ実行
                DataTable dt = this.SqlSelect(conn, query.ToString());

                //実行結果を格納
                foreach (DataRow row in dt.Rows)
                {
                    statusData data = new statusData();
                    this.cd = row["cd"].ToString();
                    this.status = row["status"].ToString();
                    this.statusList.Add(data);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                this.errorFlg = 1;
                throw ex;
            }
            finally
            {
                //DB切断
                base.CloseDbConnect(conn);
            }
            return this;
        }

        //ユーザ取得
        public SelectModel GetUser()
        {
            //DB接続
            var conn = base.OpenDbConnect();
            try
            {
                //Query生成
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT * FROM TABLE_USER");

                //クエリ実行
                DataTable dt = this.SqlSelect(conn, query.ToString());

                //実行結果を格納
                foreach (DataRow row in dt.Rows)
                {
                    userData data = new userData();
                    data.cd = row["cd"].ToString();
                    data.userName = row["user_name"].ToString();
                    data.mail = row["mail"].ToString();
                    this.userList.Add(data);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                this.errorFlg = 1;
                throw ex;
            }
            finally
            {
                //DB切断
                base.CloseDbConnect(conn);
            }
            return this;
        }

        //ユーザー情報取得
        public SelectModel GetUserDetail(string userCd)
        {
            //DB接続
            var conn = base.OpenDbConnect();
            try
            {
                //Query生成
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT");
                query.AppendLine("table_user.cd as userCd,");
                query.AppendLine("table_user.USER_NAME,");
                query.AppendLine("table_user.mail,");
                query.AppendLine("user_auth.password");
                query.AppendLine("from table_user");
                query.AppendLine("inner join user_auth");
                query.AppendLine("on table_user.cd = user_auth.id");
                query.AppendLine("WHERE");
                query.AppendLine("table_user.cd='" + userCd + "'");


                //クエリ実行
                DataTable dt = this.SqlSelect(conn, query.ToString());

                //実行結果を格納
                foreach (DataRow row in dt.Rows)
                {
                    userData data = new userData();
                    this.cd = row["userCd"].ToString();
                    this.userName = row["user_name"].ToString();
                    this.mail = row["mail"].ToString();
                    data.password = row["password"].ToString();
                    this.userList.Add(data);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                this.errorFlg = 1;
                throw ex;
            }
            finally
            {
                //DB切断
                base.CloseDbConnect(conn);
            }
            return this;
        }

        //全件取得
        public SelectModel GetAllData()
        {
            //DB接続
            var conn = base.OpenDbConnect();
            try
            {
                //Query生成
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT * FROM COMPANYLIST");
                query.AppendLine("INNER JOIN TABLE_USER");
                query.AppendLine("ON TABLE_USER.cd = COMPANYLIST.charge");
                query.AppendLine("INNER JOIN MST_STATUS");
                query.AppendLine("ON MST_STATUS.cd = COMPANYLIST.status_id");

                //クエリ実行
                DataTable dt = this.SqlSelect(conn, query.ToString());

                //実行結果を格納
                foreach (DataRow row in dt.Rows)
                {
                    rowData data = new rowData();
                    data.cd = row["cd"].ToString();
                    data.companyName = row["company_name"].ToString();
                    data.companyUrl = row["company_url"].ToString();
                    data.status = row["status"].ToString();
                    data.charge = row["user_name"].ToString();
                    data.note = row["note"].ToString();
                    var regdate = row["created_date"].ToString();
                    data.date = DateTime.Parse(regdate).ToString("yyyy/MM/dd");
                    this.listData.Add(data);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                this.errorFlg = 1;
                throw ex;
            }
            finally
            {
                //DB切断
                base.CloseDbConnect(conn);
            }
            return this;
        }

        //一覧取得
        public SelectModel GetData(int cnt)
        {
            //DB接続
            var conn = base.OpenDbConnect();
            this.pageIndex = cnt;
            try
            {
                //Query生成
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT * FROM COMPANYLIST");
                query.AppendLine("INNER JOIN TABLE_USER");
                query.AppendLine("ON TABLE_USER.cd = COMPANYLIST.charge");
                query.AppendLine("INNER JOIN MST_STATUS");
                query.AppendLine("ON MST_STATUS.cd = COMPANYLIST.status_id");
                query.AppendLine("ORDER BY COMPANYLIST.cd ASC");

                //総件数取得用
                String totalCountQuery = query.ToString();
                query.AppendLine(base.GetLimit());

                //クエリ実行
                DataTable dt = this.SqlSelect(conn, query.ToString());

                //実行結果を格納
                foreach (DataRow row in dt.Rows)
                {
                    rowData data = new rowData();
                    data.cd = row["cd"].ToString();
                    data.companyName = row["company_name"].ToString();
                    data.companyUrl = row["company_url"].ToString();
                    data.status = row["status"].ToString();
                    data.charge = row["user_name"].ToString();
                    data.note = row["note"].ToString();
                    var regdate = row["created_date"].ToString();
                    data.date = DateTime.Parse(regdate).ToString("yyyy/MM/dd");
                    this.listData.Add(data);
                }

                //総件数取得
                dt = this.SqlSelect(conn, totalCountQuery.ToString());
                this.dataCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                this.errorFlg = 1;
                throw ex;
            }
            finally
            {
                //DB切断
                base.CloseDbConnect(conn);
            }
            return this;
        }

        //検索
        public SelectModel GetSearchData(string statusSearch, string chargeSearch, int count)
        {
            //DB接続
            var conn = base.OpenDbConnect();
            this.pageIndex = count;
            try
            {
                var charge = string.Empty;
                if (chargeSearch != "0")
                {
                    charge = chargeSearch;
                }
                else
                {
                    charge = "";
                }
                //Query生成
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT * FROM COMPANYLIST");
                query.AppendLine("INNER JOIN TABLE_USER");
                query.AppendLine("ON TABLE_USER.cd = COMPANYLIST.charge");
                query.AppendLine("INNER JOIN MST_STATUS");
                query.AppendLine("ON MST_STATUS.cd = COMPANYLIST.status_id");
                if (statusSearch != "" && chargeSearch != "")
                {
                    query.AppendLine("WHERE");
                    query.AppendLine("status_id='" + statusSearch + "'");
                    query.AppendLine("AND charge='" + charge + "'");
                }
                else if (statusSearch == "" && chargeSearch != "")
                {
                    query.AppendLine("WHERE");
                    query.AppendLine("charge='" + charge + "'");
                }
                else if (statusSearch != "" && chargeSearch == "")
                {
                    query.AppendLine("WHERE");
                    query.AppendLine("status_id='" + statusSearch + "'");
                }
                query.AppendLine("ORDER BY COMPANYLIST.cd ASC");

                //総件数取得用
                String totalCountQuery = query.ToString();
                query.AppendLine(base.GetLimit());

                //クエリ実行
                DataTable dt = this.SqlSelect(conn, query.ToString());

                //実行結果を格納
                foreach (DataRow row in dt.Rows)
                {
                    rowData data = new rowData();
                    data.cd = row["cd"].ToString();
                    data.companyName = row["company_name"].ToString();
                    data.companyUrl = row["company_url"].ToString();
                    data.status = row["status"].ToString();
                    data.charge = row["user_name"].ToString();
                    data.note = row["note"].ToString();
                    var regdate = row["created_date"].ToString();
                    data.date = DateTime.Parse(regdate).ToString("yyyy/MM/dd");
                    this.listData.Add(data);
                }

                //総件数取得
                dt = this.SqlSelect(conn, totalCountQuery.ToString());
                this.dataCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                this.errorFlg = 1;
                throw ex;
            }
            finally
            {
                //DB切断
                base.CloseDbConnect(conn);
            }
            return this;
        }

        //降順切り替え
        public SelectModel GetDataDesc(int count)
        {
            //DB接続
            var conn = base.OpenDbConnect();
            this.pageIndex = count;
            try
            {
                //Query生成
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT * FROM COMPANYLIST");
                query.AppendLine("INNER JOIN TABLE_USER");
                query.AppendLine("ON TABLE_USER.cd = COMPANYLIST.charge");
                query.AppendLine("INNER JOIN MST_STATUS");
                query.AppendLine("ON MST_STATUS.cd = COMPANYLIST.status_id");
                query.AppendLine("ORDER BY COMPANYLIST.cd DESC");

                //総件数取得用
                String totalCountQuery = query.ToString();
                query.AppendLine(base.GetLimit());

                //クエリ実行
                DataTable dt = this.SqlSelect(conn, query.ToString());

                //実行結果を格納
                foreach (DataRow row in dt.Rows)
                {
                    rowData data = new rowData();
                    data.cd = row["cd"].ToString();
                    data.companyName = row["company_name"].ToString();
                    data.companyUrl = row["company_url"].ToString();
                    data.status = row["status"].ToString();
                    data.charge = row["user_name"].ToString();
                    data.note = row["note"].ToString();
                    var regdate = row["created_date"].ToString();
                    data.date = DateTime.Parse(regdate).ToString("yyyy/MM/dd");
                    this.listData.Add(data);
                }

                //総件数取得
                dt = this.SqlSelect(conn, totalCountQuery.ToString());
                this.dataCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                this.errorFlg = 1;
                throw ex;
            }
            finally
            {
                //DB切断
                base.CloseDbConnect(conn);
            }
            return this;
        }

        //昇順切り替え
        public SelectModel GetDataAsc(int count)
        {
            //DB接続
            var conn = base.OpenDbConnect();
            this.pageIndex = count;
            try
            {
                //Query生成
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT * FROM COMPANYLIST");
                query.AppendLine("INNER JOIN TABLE_USER");
                query.AppendLine("ON TABLE_USER.cd = COMPANYLIST.charge");
                query.AppendLine("INNER JOIN MST_STATUS");
                query.AppendLine("ON MST_STATUS.cd = COMPANYLIST.status_id");
                query.AppendLine("ORDER BY COMPANYLIST.cd ASC");

                //総件数取得用
                String totalCountQuery = query.ToString();
                query.AppendLine(base.GetLimit());

                //クエリ実行
                DataTable dt = this.SqlSelect(conn, query.ToString());

                //実行結果を格納
                foreach (DataRow row in dt.Rows)
                {
                    rowData data = new rowData();
                    data.cd = row["cd"].ToString();
                    data.companyName = row["company_name"].ToString();
                    data.companyUrl = row["company_url"].ToString();
                    data.status = row["status"].ToString();
                    data.charge = row["user_name"].ToString();
                    data.note = row["note"].ToString();
                    var regdate = row["created_date"].ToString();
                    data.date = DateTime.Parse(regdate).ToString("yyyy/MM/dd");
                    this.listData.Add(data);
                }

                //総件数取得
                dt = this.SqlSelect(conn, totalCountQuery.ToString());
                this.dataCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                this.errorFlg = 1;
                throw ex;
            }
            finally
            {
                //DB切断
                base.CloseDbConnect(conn);
            }
            return this;
        }

        //CDで絞り込み
        public SelectModel GetOneData(string cd)
        {
            //DB接続
            var conn = base.OpenDbConnect();
            try
            {
                //Query生成
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT * FROM COMPANYLIST");
                query.AppendLine("WHERE cd='" + cd + "'");

                //クエリ実行
                DataTable dt = this.SqlSelect(conn, query.ToString());

                //実行結果を格納
                foreach (DataRow row in dt.Rows)
                {
                    rowData data = new rowData();
                    this.cd = row["cd"].ToString();
                    this.companyName = row["company_name"].ToString();
                    this.companyUrl = row["company_url"].ToString();
                    this.status = row["status_id"].ToString();
                    this.charge = row["charge"].ToString();
                    this.note = row["note"].ToString();
                    this.date = row["created_date"].ToString();
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                this.errorFlg = 1;
                throw ex;
            }
            finally
            {
                //DB切断
                base.CloseDbConnect(conn);
            }
            return this;
        }

        //データ削除
        public bool DelData(string cd)
        {
            //DB接続
            base.OpenDbConnect();
            base.TranStart(conn);
            try
            {
                //Query生成
                StringBuilder query = new StringBuilder();
                query.AppendLine("DELETE FROM COMPANYLIST");
                query.AppendLine("WHERE cd='" + cd + "'");

                //クエリ実行
                this.SqlExecution(conn, transaction, query.ToString());

                //コミット
                base.TranCommit(transaction);
                return true;
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                base.TranRollBack(transaction);
                this.errorFlg = 1;
                return false;
            }
            finally
            {
                //DB切断
                base.CloseDbConnect(conn);
            }
        }

        //新規データ作成
        public bool CreateNewData(string companyName, string companyUrl, string status, string charge, string note)
        {
            this.errorFlg = 0;
            //DB接続
            base.OpenDbConnect();
            base.TranStart(conn);
            try
            {
                var statInt = int.Parse(status);
                var chgInt = int.Parse(charge);

                //Query生成
                StringBuilder query = new StringBuilder();
                query.AppendLine("INSERT INTO COMPANYLIST");
                query.AppendLine("(company_name, company_url, status_id, charge, note, created_date)");
                query.AppendLine("Values(");
                query.Append("'").Append(companyName).AppendLine("',");
                query.Append("'").Append(companyUrl).AppendLine("',");
                query.Append("'").Append(statInt).AppendLine("',");
                query.Append("'").Append(charge).AppendLine("',");
                query.Append("'").Append(note).AppendLine("',");
                query.AppendLine("NOW()");
                query.AppendLine(")");

                //クエリ実行
                this.SqlExecution(conn, transaction, query.ToString());

                //コミット
                base.TranCommit(transaction);
                return true;
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                base.TranRollBack(transaction);
                this.errorFlg = 1;
                return false;
            }
            finally
            {
                //DB切断
                base.CloseDbConnect(conn);
            }
        }

        public bool EditData(string cd, string companyName, string companyUrl, string status, string charge, string note)
        {
            //DB接続
            base.OpenDbConnect();
            base.TranStart(conn);
            try
            {
                //Query生成
                StringBuilder query = new StringBuilder();
                query.AppendLine("UPDATE COMPANYLIST");
                query.AppendLine("SET");
                query.AppendLine("company_name='" + companyName + "'" + ",");
                query.AppendLine("company_url='" + companyUrl + "'" + ",");
                query.AppendLine("status_id='" + status + "'" + ",");
                query.AppendLine("charge='" + charge + "'" + ",");
                query.AppendLine("note='" + note + "'" + ",");
                query.AppendLine("created_date=NOW()");
                query.AppendLine("WHERE cd='" + cd + "'");

                //クエリ実行
                this.SqlExecution(conn, transaction, query.ToString());

                //コミット
                base.TranCommit(transaction);
                return true;
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                base.TranRollBack(transaction);
                this.errorFlg = 1;
                return false;
            }
            finally
            {
                //DB切断
                base.CloseDbConnect(conn);
            }
        }

        //棒グラフ生成データ取得
        public SelectModel GetBarChartData()
        {
            List<int> list = new List<int>();
            var statusName = string.Empty;
            //DB接続
            var conn = base.OpenDbConnect();
            try
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT");
                query.AppendLine("COMPANYLIST.cd,");
                query.AppendLine("COMPANYLIST.status_id,");
                query.AppendLine("COMPANYLIST.charge,");
                query.AppendLine("COMPANYLIST.created_date as companyCreatedDate,");
                query.AppendLine("TABLE_USER.cd as userCd,");
                query.AppendLine("TABLE_USER.USER_NAME,");
                query.AppendLine("MST_STATUS.status");
                query.AppendLine("FROM COMPANYLIST");
                query.AppendLine("INNER JOIN TABLE_USER");
                query.AppendLine("ON TABLE_USER.cd = COMPANYLIST.charge");
                query.AppendLine("INNER JOIN MST_STATUS");
                query.AppendLine("ON MST_STATUS.cd = COMPANYLIST.status_id");

                //クエリ実行
                DataTable dt = this.SqlSelect(conn, query.ToString());

                //実行結果を格納
                int statusIdForList;
                foreach (DataRow row in dt.Rows)
                {
                    var sid = row["status_id"].ToString();
                    statusIdForList = int.Parse(sid);
                    list.Add(statusIdForList);
                }

                //重複削除
                IEnumerable<int> result = list.Distinct();
                var barChartKey = result.ToList();

                for (int i = 0; i < barChartKey.Count; i++)
                {
                    StringBuilder queryBc = new StringBuilder();
                    queryBc.AppendLine("SELECT");
                    queryBc.AppendLine("COMPANYLIST.cd,");
                    queryBc.AppendLine("COMPANYLIST.status_id,");
                    queryBc.AppendLine("COMPANYLIST.charge,");
                    queryBc.AppendLine("COMPANYLIST.created_date as companyCreatedDate,");
                    queryBc.AppendLine("TABLE_USER.cd as userCd,");
                    queryBc.AppendLine("TABLE_USER.USER_NAME,");
                    queryBc.AppendLine("MST_STATUS.status");
                    queryBc.AppendLine("FROM COMPANYLIST");
                    queryBc.AppendLine("INNER JOIN TABLE_USER");
                    queryBc.AppendLine("ON TABLE_USER.cd = COMPANYLIST.charge");
                    queryBc.AppendLine("INNER JOIN MST_STATUS");
                    queryBc.AppendLine("ON MST_STATUS.cd = COMPANYLIST.status_id");
                    queryBc.AppendLine("WHERE COMPANYLIST.status_id ='" + barChartKey[i] + "'");

                    //クエリ実行
                    DataTable barChart = this.SqlSelect(conn, queryBc.ToString());

                    //実行結果を格納
                    int cnt = 0;
                    foreach (DataRow row in barChart.Rows)
                    {
                        statusName = row["status"].ToString();
                        if (cnt == 0)
                        {
                            int barCnt = barChart.Rows.Count;
                            this.barChartDic.Add(statusName, barCnt);
                        }
                        else if (!barChartDic.ContainsKey(statusName))
                        {
                            int barCnt = barChart.Rows.Count;
                            this.barChartDic.Add(statusName, barCnt);
                        }
                        cnt++;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                this.errorFlg = 1;
                throw ex;
            }
            finally
            {
                //DB切断
                base.CloseDbConnect(conn);
            }
            return this;
        }

        //円グラフ生成データ取得
        public SelectModel GetPieChartData()
        {
            List<int> list = new List<int>();
            var userName = string.Empty;
            //DB接続
            var conn = base.OpenDbConnect();
            try
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT");
                query.AppendLine("COMPANYLIST.cd,");
                query.AppendLine("COMPANYLIST.status_id,");
                query.AppendLine("COMPANYLIST.charge,");
                query.AppendLine("COMPANYLIST.created_date as companyCreatedDate,");
                query.AppendLine("TABLE_USER.cd as userCd,");
                query.AppendLine("TABLE_USER.USER_NAME,");
                query.AppendLine("MST_STATUS.status");
                query.AppendLine("FROM COMPANYLIST");
                query.AppendLine("INNER JOIN TABLE_USER");
                query.AppendLine("ON TABLE_USER.cd = COMPANYLIST.charge");
                query.AppendLine("INNER JOIN MST_STATUS");
                query.AppendLine("ON MST_STATUS.cd = COMPANYLIST.status_id");

                //クエリ実行
                DataTable dt = this.SqlSelect(conn, query.ToString());

                //実行結果を格納
                int userIdForList;
                foreach (DataRow row in dt.Rows)
                {
                    var uid = row["userCd"].ToString();
                    userIdForList = int.Parse(uid);
                    list.Add(userIdForList);
                }

                //重複削除
                IEnumerable<int> result = list.Distinct();
                var pieChartKey = result.ToList();

                for (int i = 0; i < pieChartKey.Count; i++)
                {
                    StringBuilder queryBc = new StringBuilder();
                    queryBc.AppendLine("SELECT");
                    queryBc.AppendLine("COMPANYLIST.cd,");
                    queryBc.AppendLine("COMPANYLIST.status_id,");
                    queryBc.AppendLine("COMPANYLIST.charge,");
                    queryBc.AppendLine("COMPANYLIST.created_date as companyCreatedDate,");
                    queryBc.AppendLine("TABLE_USER.cd as userCd,");
                    queryBc.AppendLine("TABLE_USER.USER_NAME,");
                    queryBc.AppendLine("MST_STATUS.status");
                    queryBc.AppendLine("FROM COMPANYLIST");
                    queryBc.AppendLine("INNER JOIN TABLE_USER");
                    queryBc.AppendLine("ON TABLE_USER.cd = COMPANYLIST.charge");
                    queryBc.AppendLine("INNER JOIN MST_STATUS");
                    queryBc.AppendLine("ON MST_STATUS.cd = COMPANYLIST.status_id");
                    queryBc.AppendLine("WHERE COMPANYLIST.charge ='" + pieChartKey[i] + "'");

                    //クエリ実行
                    DataTable pieChart = this.SqlSelect(conn, queryBc.ToString());

                    //実行結果を格納
                    int cnt = 0;
                    foreach (DataRow row in pieChart.Rows)
                    {
                        userName = row["USER_NAME"].ToString();
                        if (cnt == 0)
                        {
                            int pieCnt = pieChart.Rows.Count;
                            this.pieChartDic.Add(userName, pieCnt);
                        }
                        else if (!pieChartDic.ContainsKey(userName))
                        {
                            int pieCnt = pieChart.Rows.Count;
                            this.pieChartDic.Add(userName, pieCnt);
                        }
                        cnt++;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                this.errorFlg = 1;
                throw ex;
            }
            finally
            {
                //DB切断
                base.CloseDbConnect(conn);
            }
            return this;
        }

        //ユーザーデータ更新
        public bool EditUserData(string cd, string userName, string mail, string password)
        {
            //DB接続
            base.OpenDbConnect();
            base.TranStart(conn);
            try
            {
                //Query生成
                StringBuilder query = new StringBuilder();
                query.AppendLine("UPDATE table_user");
                query.AppendLine("SET");
                query.AppendLine("user_name ='" + userName + "'" + ",");
                query.AppendLine("mail='" + mail + "'");
                query.AppendLine("WHERE cd='" + cd + "'");

                //クエリ実行
                this.SqlExecution(conn, transaction, query.ToString());

                //コミット
                base.TranCommit(transaction);
                if (password != "")
                {
                    UpdatePassword(cd, password);
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                base.TranRollBack(transaction);
                this.errorFlg = 1;
                return false;
            }
            finally
            {
                //DB切断
                base.CloseDbConnect(conn);
            }
        }

        //ユーザーデータ更新
        public bool UpdatePassword(string cd, string password)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] encoded = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha256.ComputeHash(encoded);
            string hashed = string.Concat(hash.Select(b => $"{b:x2}"));

            //DB接続
            base.OpenDbConnect();
            base.TranStart(conn);
            try
            {
                //Query生成
                StringBuilder query = new StringBuilder();
                query.AppendLine("UPDATE user_auth");
                query.AppendLine("SET");
                query.AppendLine("password ='" + hashed + "'");
                query.AppendLine("WHERE id='" + cd + "'");

                //クエリ実行
                this.SqlExecution(conn, transaction, query.ToString());

                //コミット
                base.TranCommit(transaction);
                return true;
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                base.TranRollBack(transaction);
                this.errorFlg = 1;
                return false;
            }
            finally
            {
                //DB切断
                base.CloseDbConnect(conn);
            }
        }

        //新規データ作成
        public bool CreateNewUser(string userName, string mail, string password)
        {
            this.errorFlg = 0;
            //DB接続
            base.OpenDbConnect();
            base.TranStart(conn);
            try
            {
                //Query生成
                StringBuilder query = new StringBuilder();
                query.AppendLine("INSERT INTO TABLE_USER");
                query.AppendLine("(user_name, mail, created_date)");
                query.AppendLine("Values(");
                query.Append("'").Append(userName).AppendLine("',");
                query.Append("'").Append(mail).AppendLine("',");
                query.AppendLine("NOW()");
                query.AppendLine(")");

                //クエリ実行
                this.SqlExecution(conn, transaction, query.ToString());

                //自動採番されたCDを取得
                DataTable dt = this.SqlSelect(conn, "SELECT LAST_INSERT_ID() lastid");
                var lastCd = dt.Rows[0]["lastid"].ToString();
                //CDが取得できない場合
                if (String.IsNullOrEmpty(lastCd))
                {
                    throw new SystemException("コードの取得に失敗しました。");
                }
                //コミット
                base.TranCommit(transaction);
                //パスワード作成
                CreateNewPassWord(lastCd, password);
                return true;
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                base.TranRollBack(transaction);
                this.errorFlg = 1;
                return false;
            }
            finally
            {
                //DB切断
                base.CloseDbConnect(conn);
            }
        }

        //新規パスワード作成
        public bool CreateNewPassWord(string lastCd, string password)
        {
            this.errorFlg = 0;
            SHA256 sha256 = SHA256.Create();
            byte[] encoded = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha256.ComputeHash(encoded);
            string hashed = string.Concat(hash.Select(b => $"{b:x2}"));
            int lastcd = int.Parse(lastCd);
            //DB接続
            base.OpenDbConnect();
            base.TranStart(conn);
            try
            {
                //Query生成
                StringBuilder query = new StringBuilder();
                query.AppendLine("INSERT INTO USER_AUTH");
                query.AppendLine("(id, password, created_date)");
                query.AppendLine("Values(");
                query.Append("'").Append(lastcd).AppendLine("',");
                query.Append("'").Append(hashed).AppendLine("',");
                query.AppendLine("NOW()");
                query.AppendLine(")");

                //クエリ実行
                this.SqlExecution(conn, transaction, query.ToString());
                //コミット
                base.TranCommit(transaction);
                return true;
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                base.TranRollBack(transaction);
                this.errorFlg = 1;
                return false;
            }
        }

        //データ削除
        public bool DeleteUser(string cd)
        {
            //DB接続
            base.OpenDbConnect();
            base.TranStart(conn);
            try
            {
                //Query生成
                StringBuilder query = new StringBuilder();
                query.AppendLine("DELETE FROM TABLE_USER");
                query.AppendLine("WHERE cd='" + cd + "'");

                //クエリ実行
                this.SqlExecution(conn, transaction, query.ToString());

                //Query生成
                StringBuilder queryAu = new StringBuilder();
                queryAu.AppendLine("DELETE FROM USER_AUTH");
                queryAu.AppendLine("WHERE id='" + cd + "'");

                //クエリ実行
                this.SqlExecution(conn, transaction, queryAu.ToString());

                //Query生成
                StringBuilder queryCk = new StringBuilder();
                queryCk.AppendLine("SELECT * FROM COMPANYLIST");
                queryCk.AppendLine("WHERE charge='" + cd + "'");

                //総件数取得用
                String totalCountQuery = queryCk.ToString();
                //クエリ実行
                DataTable dt = this.SqlSelect(conn, query.ToString());
                dt = this.SqlSelect(conn, totalCountQuery.ToString());
                this.dataCount = dt.Rows.Count;

                if (this.dataCount > 0)
                {
                    //Query生成
                    StringBuilder queryCl = new StringBuilder();
                    queryCl.AppendLine("UPDATE COMPANYLIST");
                    queryCl.AppendLine("SET");
                    queryCl.AppendLine("charge = '0'");
                    queryCl.AppendLine("WHERE charge='" + cd + "'");

                    //クエリ実行
                    this.SqlExecution(conn, transaction, queryCl.ToString());
                }

                //コミット
                base.TranCommit(transaction);
                return true;
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                base.TranRollBack(transaction);
                this.errorFlg = 1;
                return false;
            }
            finally
            {
                //DB切断
                base.CloseDbConnect(conn);
            }
        }

        //新規データ作成
        public bool CreateNewStatus(string statusName)
        {
            this.errorFlg = 0;
            //DB接続
            base.OpenDbConnect();
            base.TranStart(conn);
            try
            {
                //Query生成
                StringBuilder query = new StringBuilder();
                query.AppendLine("INSERT INTO MST_STATUS");
                query.AppendLine("(status, created_date)");
                query.AppendLine("Values(");
                query.Append("'").Append(statusName).AppendLine("',");
                query.AppendLine("NOW()");
                query.AppendLine(")");

                //クエリ実行
                this.SqlExecution(conn, transaction, query.ToString());
                //コミット
                base.TranCommit(transaction);
                return true;
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                base.TranRollBack(transaction);
                this.errorFlg = 1;
                return false;
            }
            finally
            {
                //DB切断
                base.CloseDbConnect(conn);
            }
        }

        //ユーザーデータ更新
        public bool EditStatus(string cd, string statusName)
        {
            //DB接続
            base.OpenDbConnect();
            base.TranStart(conn);
            try
            {
                //Query生成
                StringBuilder query = new StringBuilder();
                query.AppendLine("UPDATE mst_status");
                query.AppendLine("SET");
                query.AppendLine("status ='" + statusName + "'");
                query.AppendLine("WHERE cd='" + cd + "'");

                //クエリ実行
                this.SqlExecution(conn, transaction, query.ToString());

                //コミット
                base.TranCommit(transaction);

                return true;
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                base.TranRollBack(transaction);
                this.errorFlg = 1;
                return false;
            }
            finally
            {
                //DB切断
                base.CloseDbConnect(conn);
            }
        }

        public bool DeleteStatus(string cd)
        {
            //DB接続
            base.OpenDbConnect();
            base.TranStart(conn);
            try
            {
                //Query生成
                StringBuilder query = new StringBuilder();
                query.AppendLine("DELETE FROM MST_STATUS");
                query.AppendLine("WHERE cd='" + cd + "'");

                //クエリ実行
                this.SqlExecution(conn, transaction, query.ToString());

                //Query生成
                StringBuilder queryCk = new StringBuilder();
                queryCk.AppendLine("SELECT * FROM COMPANYLIST");
                queryCk.AppendLine("WHERE status_id='" + cd + "'");

                //総件数取得用
                String totalCountQuery = queryCk.ToString();
                //クエリ実行
                DataTable dt = this.SqlSelect(conn, totalCountQuery.ToString());
                this.dataCount = dt.Rows.Count;

                if (this.dataCount > 0)
                {
                    //Query生成
                    StringBuilder queryCl = new StringBuilder();
                    queryCl.AppendLine("UPDATE COMPANYLIST");
                    queryCl.AppendLine("SET");
                    queryCl.AppendLine("status_id = '0'");
                    queryCl.AppendLine("WHERE status_id='" + cd + "'");

                    //クエリ実行
                    this.SqlExecution(conn, transaction, queryCl.ToString());
                }

                //コミット
                base.TranCommit(transaction);
                return true;
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                base.TranRollBack(transaction);
                this.errorFlg = 1;
                return false;
            }
            finally
            {
                //DB切断
                base.CloseDbConnect(conn);
            }
        }
    }
}