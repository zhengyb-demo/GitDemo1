<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RdoListBind.aspx.cs" Inherits="ZhengybDemo.Pages.Other.RdoListBind" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   <div>
            <asp:RadioButtonList
                ID="radlSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem Selected="True">区镇</asp:ListItem>
                <asp:ListItem>单位</asp:ListItem>
                <asp:ListItem>其他</asp:ListItem>
            </asp:RadioButtonList>
            <asp:RadioButtonList ID="MoneyLY_3965" runat="server" RepeatDirection="Horizontal" Width="97%" Height="100%">
                <asp:ListItem Value="区镇财政" Text="区镇财政"></asp:ListItem>
                <asp:ListItem Value="单位自筹" Text="单位自筹"></asp:ListItem>
                <asp:ListItem Value="其他" Text="其他"></asp:ListItem>
            </asp:RadioButtonList>

            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="提交" OnClick="btnSubmit_OnClick" />
            <hr />
            你选择的资金来源为：<asp:Label ID="lblState" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
