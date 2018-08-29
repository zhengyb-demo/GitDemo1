using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zhengyb.Bizlogic;

namespace ZhengybDemo.Pages.Other
{
    public partial class log4netDemo : System.Web.UI.Page
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);  
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                log.Info("aabbccddeeffgg");
                LogHelper.WriteLog("正常调用日志");
                int a = Convert.ToInt32("zb");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("出现问题，日志呈现",ex);
            }
        }
    }
}