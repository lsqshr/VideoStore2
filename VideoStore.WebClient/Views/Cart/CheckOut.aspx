<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<VideoStore.Business.Entities.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	CheckOut
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>CheckOut</h2>

    <p>Thankyou <%=Model.Name %> for shopping at video store </p>
    <p>Your order has been submitted and a fund transfer has been requested, following information will be kept sending to your email address:<%=Model.Email %>, please check your email to see how it goes.</p>

</asp:Content>
