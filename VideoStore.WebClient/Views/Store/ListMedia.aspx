<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<VideoStore.WebClient.ViewModels.CatalogueViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ListMedia
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ListMedia</h2>

    <table>
        <tr>
            <th></th>
        </tr>

    <% foreach (var item in Model.MediaListPage) { %>
    
        <tr>
            <td>
                <%: item.Title %>
                Price: $<%: item.Price %>
                <br/>
                Stocks Remaining: <%: item.Stocks.Quantity %>

                <%if (item.Stocks.Quantity > 0)
                  { %>
                    <% using (Html.BeginForm("AddToCart", "Cart"))
                       { %>
                        <%= Html.Hidden("pMediaId", item.Id)%>
                        <%= Html.Hidden("pReturnUrl", ViewContext.HttpContext.Request.Url.PathAndQuery)%>
                    
                        <input type="submit" value="+ Add to Cart" />
                    <%} %>
                <%} %>
            </td>
        </tr>
    
    <% } %>

    </table>

</asp:Content>

