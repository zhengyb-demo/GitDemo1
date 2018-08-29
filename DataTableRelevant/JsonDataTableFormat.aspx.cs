using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zhengyb.Bizlogic;

namespace ZhengybDemo.Pages.DataTableRelevant
{
    public partial class JsonDataTableFormat : System.Web.UI.Page
    {
        //1.json转为DataTable
        //2.DataTable转为json
        //2.DataTable列自定义 
        //3.两个DataTable进行列对比，删除多余字段
        protected void Page_Load(object sender, EventArgs e)
        {
            //GetData();
            DataTable titleList = SQLHelper.getDataTable("select * from fn_GetTitle('1220001,1220006,1220007','','')");
            DataTable dt_2 = titleList.DefaultView.ToTable(true, "itemName");
            string json = ToJsonDataTable(dt_2);
            json = DataTableToJsonWithJsonNet(dt_2);
            DataTable dt_1 = titleList.DefaultView.ToTable(true, "ReportType", "comment");
            json = DataTableToJsonWithJsonNet(dt_1);

        }
        protected DataView GetData()
        {
            DataTable dt = new DataTable();
            DataTable dv = GetCount().ToTable();

            StringBuilder Col = new StringBuilder();
            for (int i = 0; i < dv.Rows.Count; i++)
            {
                Col.AppendFormat("{0}|{0},", Convert.ToString(dv.Rows[i]["TableFiledName"]));

                //  Col.Append(string.Format("{0}|{0},", Convert.ToString(dv.Rows[i]["TableFiledName"])));
            }

            DataColumn dc;
            for (int i = 0; i < dv.Rows.Count; i++)
            {
                dc = new DataColumn(Convert.ToString(dv.Rows[i]["TableFiledName"]), System.Type.GetType("System.String"));
                dt.Columns.Add(dc);
            }

            string bb =
               "[{ \"Id\":243,\"地点\":\"南京雨后1\",\"报修类型\":\"保修类型1\",\"详细\":\"的范德萨发1\",\"CRDate\":\"2017/7/20 10:55:46\",\"CRUser\":\"baoguo\",\"Files\":\"\",\"intProcessStanceid\":\"891\",\"ModelCode\":\"LCSP\",\"ShenQingRen\":\"鲍国\"},{ \"Id\":244,\"地点\":\"南京雨后2\",\"报修类型\":\"保修类型2\",\"详细\":\"的范德萨发2\",\"CRDate\":\"2017/7/20 10:55:46\",\"CRUser\":\"baoguo\",\"Files\":\"\",\"intProcessStanceid\":\"891\",\"ModelCode\":\"LCSP\",\"ShenQingRen\":\"鲍国\"},{ \"Id\":245,\"地点\":\"南京雨后3\",\"报修类型\":\"保修类型3\",\"详细\":\"的范德萨发3\",\"CRDate\":\"2017/7/20 10:55:46\",\"CRUser\":\"baoguo\",\"Files\":\"\",\"intProcessStanceid\":\"891\",\"ModelCode\":\"LCSP\",\"ShenQingRen\":\"鲍国\"}]";
            DataTable dt1 = JsonToDataTables(bb);
            bool flag = false;
            ArrayList array = new ArrayList();
            for (int i = 0; i < dt1.Columns.Count; i++)
            {
                flag = false;
                string big = dt1.Columns[i].ColumnName;
                for (int j = 0; j < dv.Rows.Count; j++)
                {
                    string small = Convert.ToString(dv.Rows[j]["TableFiledName"]);
                    if (big == small)
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    array.Add(big);
                }
            }
            for (int i = 0; i < array.Count; i++)
            {
                dt1.Columns.Remove(Convert.ToString(array[i]));
            }
            string json = ToJsonDataTable(dt1);
            return dt1.DefaultView;
        }

        public DataView GetCount()
        {
            string connstr = @"Data Source=.\SQLExpress;Initial Catalog=qjygl1;user id=sa;password=11111";
            SqlConnection conn = new SqlConnection(connstr);
            string sql = string.Format(@"select model.TableFiledName
                    from[JH_Auth_ExtendMode] model
                    inner join[Yan_WF_PD] pd on model.PDID = pd.ID
                    where model.ComId = 10334 and pd.ProcessName = '校园报修'");
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt.DefaultView;
        }
        /// <summary>
        /// 将json转换为DataTable
        /// </summary>
        /// <param name="strJson">得到的json</param>
        /// <returns></returns>
        private DataTable JsonToDataTables(string strJson)
        {
            //转换json格式
            strJson = strJson.Replace(",\"", "*\"").Replace("\":", "\"#").ToString();
            //取出表名   
            var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            string strName = rg.Match(strJson).Value;
            DataTable tb = null;
            //去除表名   
            strJson = strJson.Substring(strJson.IndexOf("[") + 1);
            strJson = strJson.Substring(0, strJson.IndexOf("]"));

            //获取数据   
            rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(strJson);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value;
                string[] strRows = strRow.Split('*');

                //创建表   
                if (tb == null)
                {
                    tb = new DataTable();
                    tb.TableName = strName;
                    foreach (string str in strRows)
                    {
                        var dc = new DataColumn();
                        string[] strCell = str.Split('#');

                        if (strCell[0].Substring(0, 1) == "\"")
                        {
                            int a = strCell[0].Length;
                            dc.ColumnName = strCell[0].Substring(1, a - 2);
                        }
                        else
                        {
                            dc.ColumnName = strCell[0];
                        }
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }

                //增加内容   
                DataRow dr = tb.NewRow();
                for (int r = 0; r < strRows.Length; r++)
                {
                    dr[r] = strRows[r].Split('#')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }

            return tb;
        }

        private string ToJsonDataTable(DataTable dt)
        {
            StringBuilder json = new StringBuilder();
            json.Append("[");
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string columnWord = Convert.ToString(dr[i]);
                    if (columnWord.Length > 0)
                    {
                        columnWord = columnWord.Replace("\"", "\\\"");
                    }

                    if (i == 0)
                    {
                        if (dt.Columns.Count == 1)
                        {
                            json.Append("{\"" + dt.Columns[i].ColumnName + "\":\"" + columnWord + "\"},");
                        }
                        else
                        {
                            json.Append("{\"" + dt.Columns[i].ColumnName + "\":\"" + columnWord + "\",");
                        }
                    }
                    else if (i == dt.Columns.Count - 1)
                    {
                        json.Append("\"" + dt.Columns[i].ColumnName + "\":\"" + columnWord + "\"},");
                    }
                    else
                    {
                        json.Append("\"" + dt.Columns[i].ColumnName + "\":\"" + columnWord + "\",");
                    }
                }
            }
            if (json.ToString() != "[")
                json.Remove(json.Length - 1, 1);
            json.Append("]");
            return json.ToString();
        }
        public string DataTableToJsonWithJsonNet(DataTable table)
        {
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(table);
            return JsonString;
        }
    }
}