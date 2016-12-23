<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LudothekWeb_M133._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <section class="row">
        <div class="col-md-12">
            <h1>Available Games</h1>
            <hr/>    
        </div>
    </section>
    <section class="row" id="gamesList" runat="server"></section>
</asp:Content>
