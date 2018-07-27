using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZhengybDemo.Pages.Other
{
    public partial class Path : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string aa = urlconvertor(@"D:\我的程序\ZhengybDemo\ZhengybDemo\Pages\Other");
            string bb = urlconvertorlocal("path.aspx");

            string attPath_Source = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;//获取模板的文件夹路径
            string FileDir = System.Web.HttpContext.Current.Server.MapPath("../other");//获取服务器上的物理路径(绝对路径)

            if (!Directory.Exists(FileDir))
                Directory.CreateDirectory(FileDir);  //如果没有该文件夹，则创建该路径下的文件夹
        }
        //本地路径转换成URL相对路径
        private string urlconvertor(string imagesurl1)
        {
            string tmpRootDir = Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
            string imagesurl2 = imagesurl1.Replace(tmpRootDir, ""); //转换成相对路径
            imagesurl2 = imagesurl2.Replace(@"/", @"/");
            //imagesurl2 = imagesurl2.Replace(@"Aspx_Uc/", @"");
            return imagesurl2;
        }
        //相对路径转换成服务器本地物理路径
        private string urlconvertorlocal(string imagesurl1)
        {
            string tmpRootDir = Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
            string imagesurl2 = tmpRootDir + imagesurl1.Replace(@"/", @"/"); //转换成绝对路径
            return imagesurl2;
        }
    }
}