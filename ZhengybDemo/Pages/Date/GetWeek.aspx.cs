using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZhengybDemo.Pages.Date
{
    public partial class GetWeek : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;//输入日期
            int today = (int)now.DayOfWeek;
            string bb = string.Format("本周开始日期{0:d},结束日期{1:d}", now.AddDays(-today), now.AddDays(6 - today));
            string cc = string.Format("上周开始日期{0:d},结束日期{1:d}", now.AddDays(-today - 7), now.AddDays(-today - 1));

            string week = Week(DateTime.Now.DayOfWeek);//获取今天是星期几

            DateTime sp1 = DateTime.Now;    //时间差
            TimeSpan sp = sp1.Subtract(DateTime.Now.AddDays(2));
            int day = sp.Days;
            int hours = sp.Hours;
            int days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); //得出当前月份的天数
        }

        public string Week(DayOfWeek dt)
        {
            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = weekdays[Convert.ToInt32(dt)];
            return week;
        }
    }
}