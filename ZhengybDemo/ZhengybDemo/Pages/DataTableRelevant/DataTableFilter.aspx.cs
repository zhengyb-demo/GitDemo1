using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZhengybDemo.Pages.DataTableRelevant
{
    public partial class DataTableFilter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //创建 表 
            DataTable tables = new DataTable();
            //添加  创建 列 
            //第一列
            DataColumn cums = new DataColumn();
            cums.ColumnName = "UserName";
            cums.DataType = typeof(string);
            tables.Columns.Add(cums);
            cums = new DataColumn();
            cums.ColumnName = "WQD";
            cums.DataType = typeof(string);
            tables.Columns.Add(cums);
            DataRow drw = tables.NewRow();
            for (int i = 0; i < 10; i++)
            {
                drw = tables.NewRow();
                drw["UserName"] = "baoguo";
                drw["WQD"] = DateTime.Now.ToString("yyyy-MM-dd");
                tables.Rows.Add(drw);
            }

            DataView dv = tables.DefaultView;
            dv.RowFilter = "WQD not in ('2018-01-06','2018-01-05')";  //返回的是过滤后的DataView
            DataRow[] dv1 = dv.Table.Select("WQD not in ('2018-01-07','2018-01-08')");//返回的是DataRow集合

        }
    }
    
}