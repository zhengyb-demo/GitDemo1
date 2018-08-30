using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZhengybDemo.Pages.Word
{
    /// <summary>
    /// html标签格式去除
    /// </summary>
    public partial class htmlClear : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string aa = "<h2 style=\"border-bottom-color:#cccccc;border-bottom-width:2px;border-bottom-style:solid;padding:0px 4px 0px 0px;margin:0px 0px 10px;text-align:center;\" class=\"ue_t\">[键入文章标题]</h2><p><strong><span style=\"font-size:12px;\">摘要</span></strong><span style=\"font-size:12px;\" class=\"ue_t\">：这里可以输入很长很长很长很长很长很长很长很长很差的摘要</span></p><p style=\"line-height:1.5em;\"><strong>标题 1</strong></p><p style=\"text-indent:2em;\"><span style=\"font-size:14px;\" class=\"ue_t\">这里可以输入很多内容，可以图文混排，可以有列表等。</span></p><p style=\"line-height:1.5em;\"><strong>标题 2</strong></p><p style=\"text-indent:2em;\"><span style=\"font-size:14px;\" class=\"ue_t\">来个列表瞅瞅：</span></p><ol style=\"list-style-type: lower-alpha;\" class=\" list-paddingleft-2\"><li><p class=\"ue_t\">列表 1</p></li><li><p class=\"ue_t\">列表 2</p></li><ol style=\"list-style-type: lower-roman;\" class=\" list-paddingleft-2\"><li><p class=\"ue_t\">多级列表 1</p></li><li><p class=\"ue_t\">多级列表 2</p></li></ol><li><p class=\"ue_t\">列表 3<br/></p></li></ol><p style=\"line-height:1.5em;\"><strong>标题 3</strong></p><p style=\"text-indent:2em;\"><span style=\"font-size:14px;\" class=\"ue_t\">来个文字图文混排的</span></p><p style=\"text-indent:2em;\"><span style=\"font-size:14px;\" class=\"ue_t\">这里可以多行</span></p><p style=\"text-indent:2em;\"><span style=\"font-size:14px;\" class=\"ue_t\">右边是图片</span></p><p style=\"text-indent:2em;\"><span style=\"font-size:14px;\" class=\"ue_t\">绝对没有问题的，不信你也可以试试看</span></p><p><br/></p><p><br/></p>";

            aa = xxHTML(aa);
        }

        /// <summary>
        /// html标签格式去除
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public string xxHTML(string html)
        {

            html = html.Replace("(<style)+[^<>]*>[^\0]*(</style>)+", "");
            html = html.Replace(@"\<img[^\>] \>", "");
            html = html.Replace(@"<p>", "");
            html = html.Replace(@"</p>", "");


            System.Text.RegularExpressions.Regex regex0 =
            new System.Text.RegularExpressions.Regex("(<style)+[^<>]*>[^\0]*(</style>)+", System.Text.RegularExpressions.RegexOptions.Multiline);
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S] </script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" on[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S] </iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S] </frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>] \>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记  
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性   
            html = regex0.Replace(html, ""); //过滤href=javascript: (<A>) 属性   


            //html = regex10.Replace(html, "");  
            html = regex3.Replace(html, "");// _disibledevent="); //过滤其它控件的on...事件  
            html = regex4.Replace(html, ""); //过滤iframe  
            html = regex5.Replace(html, ""); //过滤frameset  
            html = regex6.Replace(html, ""); //过滤frameset  
            html = regex7.Replace(html, ""); //过滤frameset  
            html = regex8.Replace(html, ""); //过滤frameset  
            html = regex9.Replace(html, "");
            //html = html.Replace(" ", "");  
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            html = html.Replace(" ", "");
            return html;
        }
    }
}