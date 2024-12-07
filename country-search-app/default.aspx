<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="country_search_app._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Country Search App</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container mt-5">
        <div class="form-group">
            <asp:TextBox ID="txtCountry" runat="server" Placeholder="Search by country name..." CssClass="form-control mb-2"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary mb-3" OnClick="btnSearch_Click" />
        </div>
        <div class="text-center">
            <asp:Image runat="server" ID="imgFlag" CssClass="img-fluid mb-3"/>
            <div class="mb-2">
                <asp:Label ID="lblCountry" runat="server" Text="" CssClass="h4"></asp:Label>
            </div>
            <div class="mb-2">
                <asp:Label ID="lblOfficial" runat="server" Text="" CssClass="h5"></asp:Label>
            </div>
            <div class="mb-2">
                <asp:Label ID="lblCapital" runat="server" Text="" CssClass="h5"></asp:Label>
            </div>
            <div class="mb-2">
                <asp:Label ID="lblPopulation" runat="server" Text="" CssClass="h5"></asp:Label>
            </div>
            <div class="mb-2">
                <asp:Label ID="lblArea" runat="server" Text="" CssClass="h5"></asp:Label>
            </div>
        </div>

        <asp:Label ID="lblError" runat="server" CssClass="d-none alert alert-danger mt-3" role="alert"></asp:Label>
    </form>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/jquery-3.7.0.min.js"></script>
    <script src="Scripts/modernizr-2.8.3.js"></script>
</body>
</html>
