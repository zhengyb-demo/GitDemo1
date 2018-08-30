using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZhengybDemo.Pages.Package
{
    public partial class Package1 : System.Web.UI.Page
    {
        //文件打包下载
        //获取相对路径，并将相对路径转为绝对路径
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Download();
            }
        }

        private void Download()
        {
            List<string> files = new List<string>();
            files.Add("D:\\我的程序\\ZhengybDemo\\ZhengybDemo\\DocTemplet\\工程复工申请报告1.doc");
            files.Add("D:\\我的程序\\ZhengybDemo\\ZhengybDemo\\DocTemplet\\工程复工申请报告2.doc");
            files.Add("D:\\我的程序\\ZhengybDemo\\ZhengybDemo\\DocTemplet\\工程复工申请报告3.doc");
            Download(HttpContext.Current.Response, files, "Package.rar");
        }

        /// <summary>
        /// 根据所选文件打包下载
        /// 编写日期：2018/2/22
        /// 编写人：郑亚波
        /// </summary>
        /// <param name="response"></param>
        /// <param name="files"></param>
        /// <param name="zipFileName"></param>
        public void Download(System.Web.HttpResponse response, IEnumerable<string> files, string zipFileName)
        {
            //根据所选文件打包下载  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            byte[] buffer = null;
            using (ICSharpCode.SharpZipLib.Zip.ZipFile file = ICSharpCode.SharpZipLib.Zip.ZipFile.Create(ms))
            {
                file.BeginUpdate();
                file.NameTransform = new MyNameTransfom();//通过这个名称格式化器，可以将里面的文件名进行一些处理。默认情况下，会自动根据文件的路径在zip中创建有关的文件夹。  

                foreach (var item in files)
                {
                    if (System.IO.File.Exists(item.ToString()))
                    {
                        file.Add(item);
                    }
                }
                file.CommitUpdate();
                buffer = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(buffer, 0, buffer.Length);
            }
            response.AddHeader("content-disposition", "attachment;filename=" + zipFileName);
            response.BinaryWrite(buffer);
            response.Flush();
            response.End();
        }

        public class MyNameTransfom : ICSharpCode.SharpZipLib.Core.INameTransform
        {

            #region INameTransform 成员

            public string TransformDirectory(string name)
            {
                return null;
            }

            public string TransformFile(string name)
            {
                return System.IO.Path.GetFileName(name);
            }

            #endregion
        }

    }
}