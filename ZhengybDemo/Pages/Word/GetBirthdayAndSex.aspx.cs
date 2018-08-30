using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZhengybDemo.Pages.Word
{
    public partial class GetBirthdayAndSex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sex="";
            string s = BirthdayAndSex("320321199210171670",out sex);
        }

        /// <summary> 
        /// 根据身份证号获取出生日期和性别 
        /// </summary> 
        /// <param name="identityCard">输入的身份证号码</param> 
        /// <returns>出身日期</returns> 
        public string BirthdayAndSex(string identityCard, out string sex)
        {
            string birthday = "";
            sex = "";
            if (string.IsNullOrEmpty(identityCard))
            {
                return "身份证号码不能为空！";//身份证号码不能为空，如果为空返回 
            }
            else
            {
                if (identityCard.Length != 15 && identityCard.Length != 18)//身份证号码只能为15位或18位其它不合法 
                {
                    return "身份证号码为15位或18位，请检查！";
                }
            }
            if (identityCard.Length == 18)//处理18位的身份证号码从号码中得到生日和性别代码 
            {
                birthday = identityCard.Substring(6, 4) + "-" + identityCard.Substring(10, 2) + "-" + identityCard.Substring(12, 2);
                sex = identityCard.Substring(14, 3);
            }
            if (identityCard.Length == 15)
            {
                birthday = "19" + identityCard.Substring(6, 2) + "-" + identityCard.Substring(8, 2) + "-" + identityCard.Substring(10, 2);
                sex = identityCard.Substring(12, 3);
            }
            if (int.Parse(sex) % 2 == 0)//性别代码为偶数是女性奇数为男性 
            {
                sex = "女";
            }
            else
            {
                sex = "男";
            }
            return birthday;
        }

    }
}