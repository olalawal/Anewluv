<%@ Control Language="C#" CodeBehind="ForeignKey.ascx.cs" Inherits="Shell.MVC2.Admin.ForeignKeyField" %>

<asp:HyperLink ID="HyperLink1" runat="server"
    Text="<%# GetDisplayString() %>"
    NavigateUrl="<%# GetNavigateUrl() %>"  />

