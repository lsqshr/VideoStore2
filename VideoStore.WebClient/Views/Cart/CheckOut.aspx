<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<VideoStore.Business.Entities.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	CheckOut
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>CheckOut</h2>

    <p>Thankyou <%=Model.Name %> for shopping at video store </p>
    <p>Your order has been submitted, and will be delivered to <%=Model.Address %> in a few days.</p>

</asp:Content>
