<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnumCombox.aspx.cs" Inherits="ZhengybDemo.Pages.Other.EnumCombox" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList runat="server" ID="tingzheng" /><br />
            <asp:RadioButtonList runat="server" ID="rdoTest" RepeatDirection="Horizontal" />
        </div>
    </form>
</body>
</html>
