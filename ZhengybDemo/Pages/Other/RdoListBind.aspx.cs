using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZhengybDemo.Pages.Other
{
    public partial class RdoListBind : System.Web.UI.Page
    {
        //RadioButtonList 前台绑定 后台获取选中的Value
        //隐藏RadioButtonList的一列
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            lblState.Text = radlSex.SelectedValue;
            string aa = MoneyLY_3965.SelectedValue;
            this.MoneyLY_3965.Items[2].Attributes.Add("style", "display:none");
        }
    }
}