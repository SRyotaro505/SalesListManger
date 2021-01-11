using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
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
        [Display(Name="企業URL")]
        [Required(ErrorMessage ="{0}を入力してください。")]
        public string companyUrl { get; set; }

        /// <summary>
        /// 企業名
        /// </summary>
        [Display(Name="企業名")]
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
        /// データリスト
        /// </summary>
        public List<rowData> listData { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SelectModel()
        {
            this.listData = new List<rowData>();
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
                    data.charge = row["charge"].ToString();
                    data.note = row["note"].ToString();
                    var regdate = row["created_date"].ToString();
                    data.date = DateTime.Parse(regdate).ToString("yyyy/MM/dd");
                    this.listData.Add(data);
                }
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
                    data.charge = row["charge"].ToString();
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
        public SelectModel GetSearchData(string statusSearch, string chargeSearch)
        {
            //DB接続
            var conn = base.OpenDbConnect();
            try
            {
                var charge = string.Empty;
                if(chargeSearch != "0")
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
                if (statusSearch != "" && chargeSearch != "")
                {
                    query.AppendLine("WHERE");
                    query.AppendLine("status='" + statusSearch + "'");
                    query.AppendLine("AND charge='" + charge + "'");
                }
                else if(statusSearch == "" && chargeSearch != "")
                {
                    query.AppendLine("WHERE");
                    query.AppendLine("charge='" + charge + "'");
                }
                else if(statusSearch != "" && chargeSearch == "")
                {
                    query.AppendLine("WHERE");
                    query.AppendLine("status='" + statusSearch + "'");
                }

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
                    data.charge = row["charge"].ToString();
                    data.note = row["note"].ToString();
                    var regdate = row["created_date"].ToString();
                    data.date = DateTime.Parse(regdate).ToString("yyyy/MM/dd");
                    this.listData.Add(data);
                }
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
                    this.status = row["status"].ToString();
                    this.charge = row["charge"].ToString();
                    this.note = row["note"].ToString();
                    this.date = row["created_date"].ToString();
                }
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
            //DB接続
            base.OpenDbConnect();
            base.TranStart(conn);
            try
            {
                //Query生成
                StringBuilder query = new StringBuilder();
                query.AppendLine("INSERT INTO COMPANYLIST");
                query.AppendLine("(company_name, company_url, status, charge, note, created_date)");
                query.AppendLine("Values(");
                query.Append("'").Append(companyName).AppendLine("',");
                query.Append("'").Append(companyUrl).AppendLine("',");
                query.Append("'").Append(status).AppendLine("',");
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
                query.AppendLine("status='" + status + "'" + ",");
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
                return false;
            }
            finally
            {
                //DB切断
                base.CloseDbConnect(conn);
            }
        }

        public SelectModel GetChartData()
        {
            //DB接続
            var conn = base.OpenDbConnect();
            try
            {
                StringBuilder query0 = new StringBuilder();
                query0.AppendLine("SELECT * FROM COMPANYLIST");

                //クエリ実行
                DataTable dt0 = this.SqlSelect(conn, query0.ToString());
                this.dataCount = dt0.Rows.Count;

                //Query生成(未対応)
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT * FROM COMPANYLIST");
                query.AppendLine("WHERE status='未対応'");

                //クエリ実行
                DataTable dt = this.SqlSelect(conn, query.ToString());
                this.barCount1 = dt.Rows.Count;

                //Query生成(連絡済み)
                StringBuilder query1 = new StringBuilder();
                query1.AppendLine("SELECT * FROM COMPANYLIST");
                query1.AppendLine("WHERE status='連絡済み'");

                //クエリ実行
                DataTable dt1 = this.SqlSelect(conn, query1.ToString());
                this.barCount2 = dt1.Rows.Count;

                //Query生成(交渉中)
                StringBuilder query2 = new StringBuilder();
                query2.AppendLine("SELECT * FROM COMPANYLIST");
                query2.AppendLine("WHERE status='交渉中'");

                //クエリ実行
                DataTable dt2 = this.SqlSelect(conn, query2.ToString());
                this.barCount3 = dt2.Rows.Count;

                //Query生成(確定)
                StringBuilder query3 = new StringBuilder();
                query3.AppendLine("SELECT * FROM COMPANYLIST");
                query3.AppendLine("WHERE status='確定'");

                //クエリ実行
                DataTable dt3 = this.SqlSelect(conn, query3.ToString());
                this.barCount4 = dt3.Rows.Count;

                //Query生成(不可)
                StringBuilder query4 = new StringBuilder();
                query4.AppendLine("SELECT * FROM COMPANYLIST");
                query4.AppendLine("WHERE status='不可'");

                //クエリ実行
                DataTable dt4 = this.SqlSelect(conn, query4.ToString());
                this.barCount5 = dt4.Rows.Count;

                //Query生成(成清)
                StringBuilder queryNa = new StringBuilder();
                queryNa.AppendLine("SELECT * FROM COMPANYLIST");
                queryNa.AppendLine("WHERE charge='成清'");

                //クエリ実行
                DataTable dtNa = this.SqlSelect(conn, queryNa.ToString());
                this.pieCount1 = dtNa.Rows.Count;

                //Query生成(山田)
                StringBuilder queryYa = new StringBuilder();
                queryYa.AppendLine("SELECT * FROM COMPANYLIST");
                queryYa.AppendLine("WHERE charge='山田'");

                //クエリ実行
                DataTable dtYa = this.SqlSelect(conn, queryYa.ToString());
                this.pieCount2 = dtYa.Rows.Count;

                //Query生成(高木)
                StringBuilder queryTa = new StringBuilder();
                queryTa.AppendLine("SELECT * FROM COMPANYLIST");
                queryTa.AppendLine("WHERE charge='高木'");

                //クエリ実行
                DataTable dtTa = this.SqlSelect(conn, queryTa.ToString());
                this.pieCount3 = dtTa.Rows.Count;

                //Query生成(鈴木)
                StringBuilder querySu = new StringBuilder();
                querySu.AppendLine("SELECT * FROM COMPANYLIST");
                querySu.AppendLine("WHERE charge='鈴木'");

                //クエリ実行
                DataTable dtSu = this.SqlSelect(conn, querySu.ToString());
                this.pieCount4 = dtSu.Rows.Count;

                //Query生成(未定)
                StringBuilder queryMi = new StringBuilder();
                queryMi.AppendLine("SELECT * FROM COMPANYLIST");
                queryMi.AppendLine("WHERE charge=''");

                //クエリ実行
                DataTable dtMi = this.SqlSelect(conn, queryMi.ToString());
                this.pieCount5 = dtMi.Rows.Count;
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