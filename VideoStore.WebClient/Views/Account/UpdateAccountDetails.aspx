<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<VideoStore.WebClient.ViewModels.UserViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	UpdateAccount
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>UpdateAccount</h2>

    <% using (Html.BeginForm()) { %>
        <%: Html.ValidationSummary(true, "Account update was unsuccessful. Please correct the errors and try again.") %>
        <div>
            <fieldset>
                <legend>Account Information</legend>                         
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.Email) %>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(m => m.Email) %>
                    <%: Html.ValidationMessageFor(m => m.Email) %>
                </div>

                <div class="editor-label">
                    <%: Html.LabelFor(m => m.Address) %>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(m => m.Address) %>
                    <%: Html.ValidationMessageFor(m => m.Address) %>
                </div>
                
                <p>
                    <input type="submit" value="Update Details" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
