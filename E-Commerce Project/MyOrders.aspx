<%@ Page Title="My Orders" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="MyOrders.aspx.cs" Inherits="MyOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style type="text/css">

.orders-page
{
    background-color: #f5f7fa;
    padding: 30px;
    margin-top: 20px;
    border-radius: 10px;
}

.orders-header
{
    background-color: white;
    padding: 25px;
    border-radius: 10px;
    box-shadow: 0 2px 8px #ddd;
    margin-bottom: 20px;
}

.orders-header h1
{
    margin: 0;
    color: #0b3d91;
    font-size: 30px;
}

.orders-header p
{
    color: #777;
    margin-top: 8px;
}

.message
{
    display: block;
    background-color: white;
    padding: 20px;
    border-radius: 8px;
    color: #666;
    font-size: 17px;
    margin-top: 15px;
}

.orders-table
{
    width: 100%;
    border-collapse: collapse;
    background-color: white;
    margin-top: 20px;
    box-shadow: 0 2px 10px #ddd;
}

.orders-table th
{
    background-color: #0b3d91;
    color: white;
    padding: 14px 10px;
    text-align: center;
    font-size: 14px;
}

.orders-table td
{
    padding: 14px 10px;
    border-bottom: 1px solid #eeeeee;
    text-align: center;
    color: #333;
    font-size: 14px;
}

.orders-table tr:hover td
{
    background-color: #f8fbff;
}

.cancel-btn
{
    background-color: #e53935;
    color: white;
    border: none;
    padding: 9px 14px;
    border-radius: 5px;
    cursor: pointer;
    font-size: 13px;
    font-weight: bold;
}

.cancel-btn:hover
{
    background-color: #c62828;
}
.view-btn
{
    display: inline-block;
    background-color: #28a745;
    color: white;
    padding: 9px 14px;
    border-radius: 5px;
    text-decoration: none;
    font-size: 13px;
    font-weight: bold;
    margin-right: 5px;
}

.view-btn:hover
{
    background-color: #218838;
}
.status-placed
{
    background-color: #fff3cd;
    color: #856404;
    padding: 7px 12px;
    border-radius: 15px;
    font-weight: bold;
}

.status-preparing
{
    background-color: #cfe2ff;
    color: #084298;
    padding: 7px 12px;
    border-radius: 15px;
    font-weight: bold;
}

.status-shipped
{
    background-color: #e2d9f3;
    color: #59359a;
    padding: 7px 12px;
    border-radius: 15px;
    font-weight: bold;
}

.status-delivered
{
    background-color: #d1e7dd;
    color: #0f5132;
    padding: 7px 12px;
    border-radius: 15px;
    font-weight: bold;
}

.status-cancelled
{
    background-color: #f8d7da;
    color: #842029;
    padding: 7px 12px;
    border-radius: 15px;
    font-weight: bold;
}
.order-filter
{
    background-color: white;
    padding: 20px;
    border-radius: 8px;
    box-shadow: 0 2px 8px #ddd;
    margin-top: 20px;
    margin-bottom: 20px;
}

.search-box
{
    width: 200px;
    padding: 10px;
    border: 1px solid #ccc;
    border-radius: 5px;
    font-size: 14px;
}

.search-btn
{
    background-color: #0b3d91;
    color: white;
    border: none;
    padding: 10px 18px;
    border-radius: 5px;
    margin-left: 5px;
    cursor: pointer;
}

.show-btn
{
    background-color: #6c757d;
    color: white;
    border: none;
    padding: 10px 18px;
    border-radius: 5px;
    margin-left: 5px;
    cursor: pointer;
}

.status-filter
{
    padding: 10px;
    margin-left: 10px;
    border: 1px solid #ccc;
    border-radius: 5px;
    font-size: 14px;
}

</style>

</asp:Content>


<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    Runat="Server">

<div class="orders-page">

    <div class="orders-header">

        <h1>📦 My Orders</h1>

        <p>
            View your orders and manage your recent purchases.
        </p>

    </div>


    <asp:Label ID="Label1"
        runat="server"
        CssClass="message">
    </asp:Label>
    <!-- SEARCH AND FILTER -->

<div class="order-filter">

    <asp:TextBox ID="txtSearchOrderID"
        runat="server"
        CssClass="search-box"
        placeholder="Enter Order ID">
    </asp:TextBox>

    <asp:Button ID="btnSearch"
        runat="server"
        Text="Search"
        CssClass="search-btn"
        OnClick="btnSearch_Click" />

    <asp:Button ID="btnShowAll"
        runat="server"
        Text="Show All"
        CssClass="show-btn"
        OnClick="btnShowAll_Click" />


    <asp:DropDownList ID="ddlStatusFilter"
        runat="server"
        CssClass="status-filter"
        AutoPostBack="true"
        OnSelectedIndexChanged="ddlStatusFilter_SelectedIndexChanged">

        <asp:ListItem Text="All Status" Value="All"></asp:ListItem>

        <asp:ListItem Text="Placed" Value="Placed"></asp:ListItem>

        <asp:ListItem Text="Preparing" Value="Preparing"></asp:ListItem>

        <asp:ListItem Text="Shipped" Value="Shipped"></asp:ListItem>

        <asp:ListItem Text="Delivered" Value="Delivered"></asp:ListItem>

        <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>

    </asp:DropDownList>

</div>


    <asp:GridView ID="GridView1"
        runat="server"
        AutoGenerateColumns="False"
        CssClass="orders-table"
        OnRowCommand="GridView1_RowCommand">

        <Columns>

             <asp:TemplateField HeaderText="Status">

    <ItemTemplate>

        <asp:Label
            ID="lblStatus"
            runat="server"
            Text='<%# Eval("OrderStatus") %>'
            CssClass='<%# "status-" + Eval("OrderStatus").ToString().ToLower() %>'>
        </asp:Label>

    </ItemTemplate>

</asp:TemplateField>

            <asp:BoundField
                DataField="ProductName"
                HeaderText="Product" />

            <asp:BoundField
                DataField="Price"
                HeaderText="Price" />

            <asp:BoundField
                DataField="Quantity"
                HeaderText="Quantity" />

            <asp:BoundField
                DataField="Total"
                HeaderText="Total" />

            <asp:BoundField
                DataField="OrderDate"
                HeaderText="Order Date"
                DataFormatString="{0:dd-MM-yyyy hh:mm tt}" />

            <asp:BoundField
                DataField="PaymentMethod"
                HeaderText="Payment" />

            

           <asp:TemplateField HeaderText="Action">

    <ItemTemplate>

        <asp:HyperLink
            ID="lnkView"
            runat="server"
            Text="👁 View Details"
            NavigateUrl='<%# "OrderDetails.aspx?id=" + Eval("OrderID") %>'
            CssClass="view-btn">
        </asp:HyperLink>

        <asp:Button
            ID="btnCancel"
            runat="server"
            Text="Cancel Order"
            CssClass="cancel-btn"
            CommandName="CancelOrder"
            CommandArgument='<%# Eval("OrderID") %>'
            Visible='<%# Eval("OrderStatus").ToString() == "Placed" %>'
            OnClientClick="return confirm('Are you sure you want to cancel this order?');" />

    </ItemTemplate>

</asp:TemplateField>

           

        </Columns>

    </asp:GridView>

</div>

</asp:Content>