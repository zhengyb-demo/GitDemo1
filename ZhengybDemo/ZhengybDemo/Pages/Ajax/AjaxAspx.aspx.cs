using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZhengybDemo.Pages.Ajax
{
    public partial class AjaxAspx : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string aa = Request.Params["paramA"];
            Response.Write("服务器返回的数据:" + aa);
            Response.Flush();
            Response.End();
        }
    }
}