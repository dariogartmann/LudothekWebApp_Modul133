<%@ Page Title="My Rentals" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyRentals.aspx.cs" Inherits="LudothekWeb_M133.MyRentals" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section class="row">
        <div class="col-md-12">
            <h1>My Rentals</h1>
            <hr/>    
        </div>
    </section>
    <section class="row" id="myRentals" runat="server"></section>
</asp:Content>
