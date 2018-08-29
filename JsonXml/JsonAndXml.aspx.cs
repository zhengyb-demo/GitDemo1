using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace ZhengybDemo.Pages.JsonXml
{
    public partial class JsonAndXml : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string LoginID = "32110219670824191X";
            string Json = GetUser(LoginID);
            Dictionary<string, object> Content = (Dictionary<string, object>)fastJSON.JSON.Instance.ToObject(Json);
        }

        public string GetUser(string userid)
        {
            string access_token = IsExistAccess_Token();
            string posturl = "https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token=" + access_token + "&userid=";

            posturl += userid;

            System.Net.HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(posturl);
            request.Method = "Get";
            request.ContentType = "application/x-www-form-urlencoded";
            HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string content = reader.ReadToEnd();

            return content;

        }
        public string IsExistAccess_Token()
        {

            string Token = string.Empty;
            DateTime YouXRQ;
            // 读取XML文件中的数据，并显示出来 ，注意文件路径  
            string filepath = "";

            if (HttpContext.Current != null)
            {
                filepath = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);
            }


            filepath += "\\WeiXinService\\XMLAccess_token.xml";

            StreamReader str = new StreamReader(filepath, System.Text.Encoding.UTF8);
            XmlDocument xml = new XmlDocument();
            xml.Load(str);
            str.Close();
            str.Dispose();
            Token = xml.SelectSingleNode("xml").SelectSingleNode("Access_Token").InnerText;
            YouXRQ = Convert.ToDateTime(xml.SelectSingleNode("xml").SelectSingleNode("Access_YouXRQ").InnerText);


            return Token;
        }
    }
}