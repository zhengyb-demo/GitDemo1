using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZhengybDemo.Pages.Other
{
    public partial class DiskMemory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.DriveType.ToString() == "Fixed")
                {
                    string strStyle = "";
                    if ((Math.Round((new decimal(drive.TotalFreeSpace)) / (1024 * 1024 * 1024), 2) / Math.Round((new decimal(drive.TotalSize)) / (1024 * 1024 * 1024), 2)) * 100 < 10)
                    {
                        strStyle = "color:red!important;";
                    }
                    else
                    {
                        strStyle = "";
                    }
                }
                string name = drive.Name;  //磁盘名称
                string typedrive = drive.DriveType.ToString();//类型
                string totalsize = Math.Round((new decimal(drive.TotalSize)) / (1024 * 1024 * 1024), 2).ToString();//总计大小（G位单位）
                string space = Math.Round((new decimal(drive.TotalFreeSpace)) / (1024 * 1024 * 1024), 2).ToString();//可用空间（G位单位）
                string percent = Math.Round((1 - (Math.Round((new decimal(drive.TotalFreeSpace)) / (1024 * 1024 * 1024), 2) / Math.Round((new decimal(drive.TotalSize)) / (1024 * 1024 * 1024), 2))) * 100, 2).ToString();//占用率
            }
            string aa = "2";
            int b = 1;
            int.TryParse(aa ?? "1", out b);
            string c = "22";
            int d = b;
        }
    }
}