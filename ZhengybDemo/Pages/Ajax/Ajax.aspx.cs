using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZhengybDemo.Pages.Ajax
{
    public partial class Ajax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string Method(string Par1, string Par2)
        {

            string aa="";

            return "Par1:" + Par1 + ",Par2:" + Par2;
        }
    }
}