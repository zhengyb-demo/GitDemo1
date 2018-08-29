<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ajax.aspx.cs" Inherits="ZhengybDemo.Pages.Ajax.Ajax" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        function ajaxDetailData() {
            var ajaxUrl = "http://218.4.136.118:8086/mockjs/231/infodetail";
            $.ajax({
                url: ajaxUrl,
                type: "post",
                data: { 'pageIndex': 1, 'pageSize': 6 },
                dataType: "json",
                success: function (rdata) { alert(rdata.result); },
                error: function () {
                    alert("12");
                }
            });
        };
        $(document).ready(function () {
            $("#btn1").click(function () {
                $.ajax({
                    type: "post",
                    data: "{Par1:\"abc\",Par2:\"bcd\"}",
                    contentType: "application/json;utf-8",
                    url: "../../WebServer/WebService.asmx/MyMethod",
                    success: function (data) { alert(data.d); },
                    error: function (XMLHttpRequest, textStrThrown) { alert(errorThrown); }
                });
            });
            $("#btn2").click(function () {
                var a = "../../Ashx/AjaxAshx.ashx";
                $.ajax({
                    type: "post",
                    data: { paramA: "aa", paramB: "bb", responseType: "json" },
                    url: "../../Ashx/AjaxAshx.ashx",
                    dataType: "json",
                    success: function (data) { alert(data.ParamB); },
                    error: function (XMLHttpRequest, textStatus, errorThrown) { alert(errorThrown); }
                });
            });
            $("#btn3").click(function () {
                $.ajax({
                    type: "post",
                    data: "{Par1:\"aa\",Par2:\"bb\"}",
                    contentType: "application/json;utf-8",
                    url: "Ajax.aspx/Method",
                    success: function (data) { alert(data.d); },
                    error: function (XMLHttpRequest, textStatus, errorThrown) { alert(errorThrown); }
                });
            });
            $("#btn4").click(function () {
                $.ajax({
                    type: "post",
                    data: { paramA: "aa", paramB: "bb" },
                    url: "AjaxAspx.aspx",
                    success: function (data) { alert(data); },
                    error: function (XMLHttpRequest, textStatus, errorThrown) { alert(errorThrown); }
                });
            });
            $("#btn5").click(function () {
                $.ajax({
                    type: "post",
                    data: "{Paras:\"abc\",ValidateData:\"bcd\"}",
                    contentType: "application/json;utf-8",
                    url: "http://space.zje.net.cn/ZJEduFrontMobile/WebService/XXTAppService.asmx/InitializeArea",
                    success: function (data) { alert(data.dataType); },
                    error: function (XMLHttpRequest, textStatus, errorThrown) { alert(errorThrown); }
                });
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="button" id="btn1" value="WebService方法" class="btn150" /><br />
            <br />
            <input type="button" id="btn2" value="请求一般处理程序ashx" class="btn150" /><br />
            <br />
            <input type="button" id="btn3" value="PageMethod方法" class="btn150" /><br />
            <br />
            <input type="button" id="btn4" value="请求aspx页面" class="btn150" /><br />
            <br />
            <input type="button" id="btn5" value="OtherWebserver" class="btn150" /><br />
            <br />
            <input type="button" id="btn6" value="ServerInternet" class="btn150" onclick="ajaxDetailData()"/><br />
            <br />
            
        </div>
    </form>
</body>
</html>
