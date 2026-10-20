<%@ Page Title="Checkout"
    Language="C#"
    MasterPageFile="~/Site.master"
    AutoEventWireup="true"
    CodeFile="Checkout.aspx.cs"
    Inherits="Checkout" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    Runat="Server">

<style type="text/css">

.checkout-page {
    max-width: 1100px;
    margin: 0 auto;
}

.checkout-title {
    text-align: center;
    color: #0b3d91;
    font-size: 36px;
    margin-bottom: 5px;
}

.checkout-subtitle {
    text-align: center;
    color: #666;
    margin-bottom: 30px;
}

.checkout-container {
    display: flex;
    gap: 25px;
    align-items: flex-start;
}

.checkout-left,
.checkout-right {
    background-color: white;
    padding: 25px;
    border-radius: 10px;
    box-shadow: 0 3px 12px rgba(0,0,0,0.10);
}

.checkout-left {
    width: 60%;
}

.checkout-right {
    width: 40%;
}

.section-title {
    color: #0b3d91;
    font-size: 22px;
    font-weight: bold;
    border-bottom: 2px solid #eeeeee;
    padding-bottom: 10px;
    margin-bottom: 20px;
}

.form-row {
    margin-bottom: 16px;
}

.form-label {
    display: block;
    font-weight: bold;
    color: #333;
    margin-bottom: 6px;
}

.form-input {
    width: 100%;
    height: 42px;
    border: 1px solid #cccccc;
    border-radius: 5px;
    padding: 10px;
    font-size: 15px;
}

.form-textarea {
    width: 100%;
    height: 85px;
    border: 1px solid #cccccc;
    border-radius: 5px;
    padding: 10px;
    font-size: 15px;
    resize: vertical;
}

.payment-select {
    width: 100%;
    height: 42px;
    border: 1px solid #cccccc;
    border-radius: 5px;
    padding: 8px;
    font-size: 15px;
}

.order-grid {
    width: 100%;
    border-collapse: collapse;
}

.order-grid th {
    background-color: #0b3d91;
    color: white;
    padding: 10px;
}

.order-grid td {
    padding: 10px;
    border-bottom: 1px solid #ddd;
}

.total-row {
    display: flex;
    justify-content: space-between;
    border-top: 2px solid #ddd;
    margin-top: 15px;
    padding-top: 15px;
    font-size: 21px;
    font-weight: bold;
}

.total-price {
    color: #e53935;
}

.place-order-btn {
    width: 100%;
    height: 50px;
    margin-top: 20px;
    border: none;
    border-radius: 6px;
    background-color: #0b3d91;
    color: white;
    font-size: 17px;
    font-weight: bold;
    cursor: pointer;
}

.place-order-btn:hover {
    background-color: #082d6b;
}

.secure-text {
    text-align: center;
    color: #666;
    font-size: 13px;
    margin-top: 15px;
}

.back-cart {
    display: block;
    text-align: center;
    margin-top: 12px;
    color: #0b3d91;
    text-decoration: none;
}

@media screen and (max-width: 800px) {

    .checkout-container {
        display: block;
    }

    .checkout-left,
    .checkout-right {
        width: 100%;
        margin-bottom: 20px;
    }

}

</style>

</asp:Content>


<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    Runat="Server">

<div class="checkout-page">

    <h1 class="checkout-title">
        🛒 Checkout
    </h1>

    <p class="checkout-subtitle">
        Complete your order safely and securely
    </p>


    <div class="checkout-container">


        <!-- DELIVERY DETAILS -->

        <div class="checkout-left">

            <div class="section-title">
                📦 Delivery Details
            </div>


            <div class="form-row">

                <asp:Label ID="Label1"
                    runat="server"
                    Text="Customer Name"
                    CssClass="form-label">
                </asp:Label>

                <asp:TextBox ID="txtName"
                    runat="server"
                    CssClass="form-input"
                    placeholder="Enter your full name">
                </asp:TextBox>

            </div>


            <div class="form-row">

                <asp:Label ID="Label2"
                    runat="server"
                    Text="Mobile Number"
                    CssClass="form-label">
                </asp:Label>

                <asp:TextBox ID="txtPhone"
                    runat="server"
                    CssClass="form-input"
                    placeholder="Enter mobile number">
                </asp:TextBox>

            </div>


            <div class="form-row">

                <asp:Label ID="Label3"
                    runat="server"
                    Text="Address"
                    CssClass="form-label">
                </asp:Label>

                <asp:TextBox ID="txtAddress"
                    runat="server"
                    CssClass="form-textarea"
                    TextMode="MultiLine"
                    placeholder="Enter delivery address">
                </asp:TextBox>

            </div>


            <div class="form-row">

                <asp:Label ID="Label4"
                    runat="server"
                    Text="Pincode"
                    CssClass="form-label">
                </asp:Label>

                <asp:TextBox ID="txtPincode"
                    runat="server"
                    CssClass="form-input"
                    placeholder="Enter 6 digit pincode">
                </asp:TextBox>

            </div>


            <div class="form-row">

                <asp:Label ID="Label5"
                    runat="server"
                    Text="Landmark"
                    CssClass="form-label">
                </asp:Label>

                <asp:TextBox ID="txtLandmark"
                    runat="server"
                    CssClass="form-input"
                    placeholder="Enter nearby landmark">
                </asp:TextBox>

            </div>


            <div class="form-row">

                <asp:Label ID="Label6"
                    runat="server"
                    Text="Payment Method"
                    CssClass="form-label">
                </asp:Label>

                <asp:DropDownList ID="ddlPayment"
                    runat="server"
                    CssClass="payment-select">

                    <asp:ListItem Value="Cash on Delivery">
                        Cash on Delivery
                    </asp:ListItem>

                    <asp:ListItem Value="UPI">
                        UPI
                    </asp:ListItem>

                    <asp:ListItem Value="Credit / Debit Card">
                        Credit / Debit Card
                    </asp:ListItem>

                </asp:DropDownList>

            </div>

        </div>


        <!-- ORDER SUMMARY -->

        <div class="checkout-right">

            <div class="section-title">
                🧾 Order Summary
            </div>


            <asp:GridView ID="GridView1"
                runat="server"
                AutoGenerateColumns="False"
                CssClass="order-grid"
                GridLines="None">

                <Columns>

                    <asp:BoundField
                        DataField="ProductName"
                        HeaderText="Product" />

                    <asp:BoundField
                        DataField="Price"
                        HeaderText="Price" />

                    <asp:BoundField
                        DataField="Quantity"
                        HeaderText="Qty" />

                    <asp:BoundField
                        DataField="Total"
                        HeaderText="Total" />

                </Columns>

            </asp:GridView>


            <div class="total-row">

                <span>Grand Total</span>

                <span class="total-price">
                    ₹<asp:Label ID="lblGrandTotal"
                        runat="server">
                    </asp:Label>
                </span>

            </div>


            <asp:Button ID="Button1"
                runat="server"
                Text="🛍 Place Order"
                CssClass="place-order-btn"
                OnClick="Button1_Click" />


            <div class="secure-text">
                🔒 Your order information is secure
            </div>


            <a href="Cart.aspx"
                class="back-cart">
                ← Back to Cart
            </a>

        </div>

    </div>

</div>

</asp:Content>