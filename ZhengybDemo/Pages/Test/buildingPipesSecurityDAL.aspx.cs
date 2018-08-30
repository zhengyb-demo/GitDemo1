using Zhengyb.Bizlogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZhengybDemo.Pages.Test
{
    public partial class buildingPipesSecurityDAL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Dictionary<string, int> klist = new Dictionary<string, int>();
                klist.Add("aa", 1);
                klist.Add("bb", 2);
                klist.Add("cc", 3);
                klist.Add("dd", 4);
                klist.Add("ee", 5);
                klist.Add("ff", 6);

                // QueryPageList("2018", 436, 0);
                DataTable titleList = SQLHelper.getDataTable("select * from fn_GetTitle('1220001,1220006,1220007','','')");
                DataTable dt_1 = titleList.DefaultView.ToTable(true, "ReportType", "comment");
                DataTable dt_2 = titleList.DefaultView.ToTable(true, "itemName");
                // QueryPageList1("2018", 122, 12202);
                getNewResult("", "2018", "");
                string aa = "";
            }
        }


        public string QueryPageList(string year, int FormTableID, int GROUPID)
        {
            ArrayList list_csNum = new ArrayList();
            ArrayList list_BItem = new ArrayList();
            ArrayList list_SItem = new ArrayList();
            ArrayList list_cnt = new ArrayList();
            ArrayList list_BItemName = new ArrayList();

            ArrayList list_SItemName = new ArrayList();

            ArrayList list_toltal = new ArrayList();

            var whereSql = new StringBuilder();
            StringBuilder sqlsb = new StringBuilder("");




            //查询大项  ex.中压管 低压盘管 低压立管  阀门箱 调压箱 过滤器 调压器
            sqlsb.Append("SELECT  Code, Comment FROM  T_FORM_FormTableCommonCode ");
            sqlsb.Append("WHERE FormTableID = '" + FormTableID + "'  AND GROUPID ='" + GROUPID + "' order by iorder ");
            String sqlText = sqlsb.ToString();
            DataTable dt = SQLHelper.getDataTable(sqlText);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list_BItem.Add(dt.Rows[i][0]);
                list_BItemName.Add(dt.Rows[i][1]);

                sqlsb.Remove(0, sqlsb.Length);
                //查询回报项(得到低压立管、中压管、低压盘管等的回报项名及合并单元格数)
                sqlsb.Append("SELECT ID,ItemName from T_FORM_FormTableReportItem ");
                sqlsb.Append("where ReportType ='" + dt.Rows[i][0].ToString() + "' AND ISFAULT='1' ");
                sqlText = sqlsb.ToString();
                DataTable dt_left = SQLHelper.getDataTable(sqlText);
                list_csNum.Add(dt_left.Rows.Count);
                for (int n = 0; n < dt_left.Rows.Count; n++)
                {
                    list_SItem.Add(dt_left.Rows[n][0]);
                    list_SItemName.Add(dt_left.Rows[n][1]);
                }
            }


            string yearMonth = "";
            for (int v = 1; v <= 12; v++)
            {
                if (v < 10)
                {
                    yearMonth = year + "0" + v.ToString();
                }
                else
                {
                    yearMonth = year + v.ToString();
                }
                int sumNum = 0;
                ArrayList rowlist = new ArrayList();
                rowlist.Add(yearMonth);
                for (int j = 0; j < list_SItem.Count; j++)
                {
                    sqlsb.Remove(0, sqlsb.Length);
                    dt.Clear();
                    //根据查询条件(所选年)按月统计回报隐患次数
                    sqlsb.Append("select count(*) ");
                    sqlsb.Append(" from [IPMS4S_SLSW_" + year + "].[dbo].[T_Form_PLData]");
                    sqlsb.Append(" where FormTableReportItemID='" + list_SItem.ToArray().GetValue(j).ToString() + "' ");
                    sqlsb.Append(" and ReportValue <> '' and ReportValue <>'无' and ReportValue <>'完好' and ReportValue <>'正常' and ReportValue <>'否' ");
                    sqlsb.Append("and SUBSTRING(CONVERT(varchar(7), CreateDate, 112), 0, 7)='" + yearMonth + "'");
                    //sqlsb.Append("SUBSTRING(CONVERT(varchar(7), CreateDate, 112), 0, 5) = '" + year + "'");
                    sqlText = sqlsb.ToString();
                    dt = SQLHelper.getDataTable(sqlText);

                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        rowlist.Add(dt.Rows[k][0]);
                        sumNum += Int32.Parse(dt.Rows[k][0].ToString());
                    }

                }
                rowlist.Add(sumNum);

                list_cnt.Insert(v - 1, rowlist);

            }

            ArrayList list_totNum = new ArrayList();
            list_totNum.Add("合计");
            int[] tot = new int[((ArrayList)list_cnt.ToArray().GetValue(0)).Count - 1];
            for (int i = 0; i < list_cnt.Count; i++)
            {
                ArrayList rList = (ArrayList)list_cnt.ToArray().GetValue(i);
                int colValue = 0;
                for (int m = 1; m < rList.Count; m++)
                {
                    colValue = Int32.Parse(rList.ToArray().GetValue(m).ToString());
                    tot[m - 1] += colValue;
                }

            }

            for (int i = 0; i < tot.Length; i++)
            {
                list_totNum.Add(tot[i]);
            }

            //list_cnt.Add(list_totNum);
            list_toltal.Add(list_BItem);
            list_toltal.Add(list_SItem);
            list_toltal.Add(list_cnt);
            list_toltal.Add(list_csNum);
            list_toltal.Add(list_BItemName);
            list_toltal.Add(list_SItemName);
            list_toltal.Add(list_totNum);

            StringBuilder tableSb = new StringBuilder("");
            if ("Export".Equals(""))
            {
                tableSb.Append("<table border='1'>");
                tableSb.Append("<thead>");
                tableSb.Append("<tr >");
                tableSb.Append("<td  rowspan='2'  style='background-color:Gray'>检查月份</td>");
                for (int i = 0; i < list_BItemName.Count; i++)
                {

                    tableSb.Append("<td style='text-align:center;background-color:Gray' colspan='" + list_csNum.ToArray().GetValue(i) + "'>" + list_BItemName.ToArray().GetValue(i).ToString() + "</td>");


                }

                tableSb.Append("<td  rowspan='2' style='background-color:Gray'>小计</td>");
                tableSb.Append("</tr>");

                tableSb.Append("<tr>");
                for (int i = 0; i < list_BItemName.Count; i++)
                {
                    string sItemName = list_SItemName.ToArray().GetValue(i).ToString();
                    /* if (sItemName.Length > 4)
                     {
                         string s1 = sItemName.Substring(0, 4);
                         string s2 = sItemName.Substring(4, sItemName.Length - 4);
                         sItemName = s1 + "<br/>" + s2;
                     }*/
                    sItemName = getStr(sItemName, 4);
                    tableSb.Append("<td style='background-color:Gray'>" + sItemName + "</td>");
                }
                tableSb.Append("</tr>");

                tableSb.Append("</thead>");
            }
            else
            {
                tableSb.Append("<table class='report' style='width:auto;overflow-y:scroll;'>");
                tableSb.Append("<thead>");
                tableSb.Append("<tr>");
                tableSb.Append("<td  rowspan='2'>检查月份</td>");
                for (int i = 0; i < list_BItemName.Count; i++)
                {
                    tableSb.Append("<td  colspan='" + list_csNum.ToArray().GetValue(i) + "'>" + list_BItemName.ToArray().GetValue(i).ToString() + "</td>");

                }

                tableSb.Append("<td  rowspan='2'>小计</td>");
                tableSb.Append("</tr>");

                tableSb.Append("<tr>");
                for (int i = 0; i < list_SItemName.Count; i++)
                {
                    string sItemName = list_SItemName.ToArray().GetValue(i).ToString();

                    //  sItemName = getStr(sItemName, 4);
                    tableSb.Append("<td>" + sItemName + "</td>");
                }
                tableSb.Append("</tr>");

                tableSb.Append("</thead>");

            }

            tableSb.Append("<tbody id='dataBody'>");
            for (int i = 0; i < 12; i++)
            {
                ArrayList rowList = (ArrayList)list_cnt.ToArray().GetValue(i);
                tableSb.Append("<tr>");
                for (int j = 0; j < rowList.Count; j++)
                {

                    tableSb.Append("<td>");
                    tableSb.Append(rowList.ToArray().GetValue(j).ToString());
                    tableSb.Append("</td>");
                }
                tableSb.Append("</tr>");
            }
            tableSb.Append("<tr>");
            for (int i = 0; i < list_totNum.Count; i++)
            {
                tableSb.Append("<td>");
                tableSb.Append(list_totNum.ToArray().GetValue(i).ToString());
                tableSb.Append("</td>");
            }
            tableSb.Append("</tr>");
            tableSb.Append("</tbody>");
            tableSb.Append("</table>");
            string retStr = tableSb.ToString();
            return retStr;
        }

        //截断字符串
        private string getStr(string strItem, int n)
        {
            int strLen = strItem.Length;
            string strTemp = "";
            if (strLen >= n)
            {
                strTemp = strItem.Substring(0, n);
                strItem = strItem.Substring(n, strLen - n);
                return strTemp + "<br />" + getStr(strItem, n);
            }
            else
            {
                return strItem;
            }
        }


        public void QueryPageList1(string year, int FormTableID, int GROUPID)
        {
            DataTable titleList = SQLHelper.getDataTable("select * from fn_GetTitle('1220001,1220006,1220007','','')");
            DataTable dt_1 = titleList.DefaultView.ToTable(true, "ReportType", "comment");
            DataTable dt_2 = titleList.DefaultView.ToTable(true, "ReportType", "ID", "itemName");
            DataTable dtData = SQLHelper.getDataTable("SELECT TOP 100 FormTableReportItemID,ReportValue,CreateDate,* from [ESafetyDB01_2017].[dbo].[T_Form_PLData] D  where LEN(ReportValue) >1 ");
            StringBuilder sb = new StringBuilder();
            var styleType = "";
            sb.AppendFormat("<table class='report' style='width:{0}px;overflow-y:scroll;'>", 220 + titleList.Rows.Count * 50);
            sb.Append("<thead>");
            sb.Append("<tr>");
            sb.AppendFormat("<td style='width:100px' rowspan='3' {0} >{1}</td>", styleType, "检查月份");
            for (int i = 0; i < dt_1.Rows.Count; i++)
            {
                var tempList = from dr in titleList.AsEnumerable()
                               where dr["ReportType"].ToString() == dt_1.Rows[i]["ReportType"].ToString()
                               select titleList;
                sb.AppendFormat("<td  colspan='{0}' {1}>{2}</td>", tempList.Count(), styleType, dt_1.Rows[i]["comment"].ToString()).AppendLine();
            }

            sb.AppendFormat("<td  rowspan='3' {0}>小计</td>", styleType);
            sb.Append("</tr>");
            sb.Append("<tr>");
            for (int i = 0; i < dt_2.Rows.Count; i++)
            {
                var tempList = from dr in titleList.AsEnumerable()
                               where dr["ID"].ToString() == dt_2.Rows[i]["ID"].ToString()
                               select titleList;
                sb.AppendFormat("<td  colspan='{0}' {2}>{1}</td>", tempList.Count(), dt_2.Rows[i]["itemName"].ToString(), styleType).AppendLine();
                  
            }
            sb.Append("</tr>");
            sb.Append("<tr>");
            for (int i = 0; i < titleList.Rows.Count; i++)
            {
                sb.AppendFormat("<td {0}>{1}</td>", styleType, titleList.Rows[i]["columnName"].ToString());


            }
            sb.Append("</tr>");
            sb.Append("</thead>");
            sb.Append("<tbody id='dataBody'>");
            for (int i = 0; i < 12; i++)
            {
                sb.Append("<tr>");
                sb.AppendFormat("<td>{0}</td>", year + "-" + (i + 1).ToString().PadLeft(2, '0')).AppendLine();
                var tempList = from dr in dtData.AsEnumerable()
                               where Convert.ToDateTime(dr["CreateDate"]).Year.ToString() == year
                               && Convert.ToDateTime(dr["CreateDate"]).Month == i + 1
                               select dtData;
                for (int j = 0; j < titleList.Rows.Count; j++)
                {
                    var tempListsub = from dr in dtData.AsEnumerable()
                                      where dr["FormTableReportItemID"].ToString() == titleList.Rows[j]["ID"].ToString()
                                      && dr["ReportValue"].ToString() == titleList.Rows[j]["columnName"].ToString()
                                      && Convert.ToDateTime(dr["CreateDate"]).Year.ToString() == year
                                      && Convert.ToDateTime(dr["CreateDate"]).Month == i + 1
                                      select titleList;
                    sb.AppendFormat("<td>{0}</td>", tempListsub.Count()).AppendLine();
                }

                sb.AppendFormat("<td>{0}</td>", tempList.Count()).AppendLine();
                sb.Append("</tr>");
            }
            sb.Append("<tr>");
            sb.AppendFormat("<td>{0}</td>", "合计").AppendLine();

            for (int j = 0; j < titleList.Rows.Count; j++)
            {
                var tempListsub = from dr in dtData.AsEnumerable()
                                  where dr["FormTableReportItemID"].ToString() == titleList.Rows[j]["ID"].ToString()
                                  && dr["ReportValue"].ToString() == titleList.Rows[j]["columnName"].ToString()
                                  && Convert.ToDateTime(dr["CreateDate"]).Year.ToString() == year
                                  select titleList;
                sb.AppendFormat("<td>{0}</td>", tempListsub.Count()).AppendLine();
            }

            sb.AppendFormat("<td>{0}</td>", dtData.Rows.Count).AppendLine();
            sb.Append("</tr>");
            sb.Append("</tbody>");
            sb.Append("</table>");
        }

        private string getNewResult(string itemNames, string year, string type)
        {
          
            StringBuilder sb = new StringBuilder();
           
            DataTable titleList = SQLHelper.getDataTable("select * from fn_GetTitleByLeak('1220002','')");
            DataTable dt_2 = titleList.DefaultView.ToTable(true, "ID", "itemName");
            DataTable dtData = SQLHelper.getDataTable(@"select FormTableReportItemID,ReportValue,D.CreateDate from [ESafetyDB01_2018].[dbo].[T_Form_PLData] D
                    left join T_FORM_FormTableReportItem R ON
                    R.ID=D.FormTableReportItemID 
                    where (R.ReportType='1220002' or  R.ReportType like '1200006%') and ReportValue!='无' and  ReportValue!='否' and R.IsFault=1 and YEAR(D.CreateDate)=2018");
            var styleType = "";
            var titleid = "";
            for (int i = 0; i < 1; i++)
            {
                titleid = titleList.Rows[i]["ID"].ToString().Substring(0, 3);


            }
            if ("Export".Equals(type))
            {
                styleType = "style='background-color:Gray' ";
                sb.Append("<table border='1'>");
            }
            else
            {
                //  sb.AppendFormat("<table class='report' style='width:{0}px;overflow-y:scroll;'>", 220 + titleList.Rows.Count * 50);
                sb.Append("<table class='report' style='width:100%;overflow-y:scroll;overflow-x:scroll;'>");
            }
            sb.Append("<thead>");
            sb.Append("<tr>");
            sb.AppendFormat("<td style='width:100px' rowspan='3' {0} >{1}</td>", styleType, "检查月份");
            if (itemNames == "")
            {
                var tempList = from dr in titleList.AsEnumerable()
                               where dr["FormName"].ToString() == "楼栋地面设备检查"
                               select titleList;
                sb.AppendFormat("<td  colspan='{2}' {0} >{1}</td>", styleType, "楼栋地面设备检查", tempList.Count());

                tempList = from dr in titleList.AsEnumerable()
                           where dr["FormName"].ToString() == "楼栋公共管线检查"
                           select titleList;
                sb.AppendFormat("<td  colspan='{2}' {0} >{1}</td>", styleType, "楼栋公共管线检查", tempList.Count() + 1);
            }
            else if (titleid == "120")
            {
                var tempList = from dr in titleList.AsEnumerable()
                               where dr["FormName"].ToString() == "楼栋地面设备检查"
                               select titleList;
                sb.AppendFormat("<td  colspan='{2}' {0} >{1}</td>", styleType, "楼栋地面设备检查", tempList.Count() + 1);

            }
            else if (titleid == "122")
            {
                var tempList = from dr in titleList.AsEnumerable()
                               where dr["FormName"].ToString() == "楼栋公共管线检查"
                               select titleList;
                sb.AppendFormat("<td  colspan='{2}' {0} >{1}</td>", styleType, "楼栋公共管线检查", tempList.Count() + 1);
            }

            sb.Append("</tr>");
            sb.Append("<tr>");
            for (int i = 0; i < dt_2.Rows.Count; i++)
            {
                var rowspan = "";
                var tempList = from dr in titleList.AsEnumerable()
                               where dr["ID"].ToString() == dt_2.Rows[i]["ID"].ToString()
                               select titleList;
                if (tempList.Count() == 1)
                {
                    rowspan = " rowspan='2'";
                }
                sb.AppendFormat("<td  colspan='{0}' {2} {3}>{1}</td>", tempList.Count(), dt_2.Rows[i]["itemName"].ToString(), styleType, rowspan).AppendLine();

            }
            // sb.Append("<td  rowspan='3' style='background-color:Gray'>小计</td>");
            sb.AppendFormat("<td  rowspan='2' {0}>小计</td>", styleType);
            sb.Append("</tr>");
            sb.Append("<tr>");
            for (int i = 0; i < titleList.Rows.Count; i++)
            {
                var tempList = from dr in titleList.AsEnumerable()
                               where dr["ID"].ToString() == titleList.Rows[i]["ID"].ToString()
                               select titleList;
                if (tempList.Count() > 1)
                {
                    sb.AppendFormat("<td {0}>{1}</td>", styleType, titleList.Rows[i]["columnName"].ToString());
                }
            }
            sb.Append("</tr>");
            sb.Append("</thead>");
            sb.Append("<tbody id='dataBody'>");
            for (int i = 0; i < 12; i++)
            {
                sb.Append("<tr>");
                sb.AppendFormat("<td>{0}</td>", year + "-" + (i + 1).ToString().PadLeft(2, '0')).AppendLine();
                var tempList = from dr in dtData.AsEnumerable()
                               where Convert.ToDateTime(dr["CreateDate"]).Year.ToString() == year
                               && Convert.ToDateTime(dr["CreateDate"]).Month == i + 1
                               select dtData;
                for (int j = 0; j < titleList.Rows.Count; j++)
                {
                    var tempListsub = from dr in dtData.AsEnumerable()
                                      where dr["FormTableReportItemID"].ToString() == titleList.Rows[j]["ID"].ToString()
                                      && dr["ReportValue"].ToString() == titleList.Rows[j]["columnName"].ToString()
                                      && Convert.ToDateTime(dr["CreateDate"]).Year.ToString() == year
                                      && Convert.ToDateTime(dr["CreateDate"]).Month == i + 1
                                      select titleList;
                    sb.AppendFormat("<td>{0}</td>", tempListsub.Count()).AppendLine();
                }

                sb.AppendFormat("<td>{0}</td>", tempList.Count()).AppendLine();
                sb.Append("</tr>");
            }
            sb.Append("<tr>");
            sb.AppendFormat("<td>{0}</td>", "合计").AppendLine();

            for (int j = 0; j < titleList.Rows.Count; j++)
            {
                var tempListsub = from dr in dtData.AsEnumerable()
                                  where dr["FormTableReportItemID"].ToString() == titleList.Rows[j]["ID"].ToString()
                                  && dr["ReportValue"].ToString() == titleList.Rows[j]["columnName"].ToString()
                                  && Convert.ToDateTime(dr["CreateDate"]).Year.ToString() == year
                                  select titleList;
                sb.AppendFormat("<td>{0}</td>", tempListsub.Count()).AppendLine();
            }

            sb.AppendFormat("<td>{0}</td>", dtData.Rows.Count).AppendLine();
            sb.Append("</tr>");
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }
    }
}