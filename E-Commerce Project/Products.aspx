<%@ Page Title="Products" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="Products" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    Runat="Server">

<style type="text/css">

.products-title
{
    text-align: center;
    color: #0b3d91;
    font-size: 35px;
    margin: 30px 0 10px 0;
}

.products-subtitle
{
    text-align: center;
    color: #666;
    margin-bottom: 30px;
}

.product-grid
{
    width: 100%;
    display: grid;
    grid-template-columns: repeat(4, 1fr);
    gap: 25px;
    margin-bottom: 40px;
}

.product-card
{
    background-color: white;
    padding: 20px;
    text-align: center;
    border-radius: 10px;
    box-shadow: 0 2px 10px #ddd;
}

.product-image
{
    height: 180px;
    display: flex;
    justify-content: center;
    align-items: center;
}

.product-image img
{
    width: 150px;
    height: 150px;
    object-fit: contain;
}

.product-card h3
{
    color: #222;
    margin: 10px 0;
}

.product-description
{
    color: #666;
    font-size: 14px;
    min-height: 35px;
}

.price
{
    color: #e53935;
    font-size: 22px;
    font-weight: bold;
    margin-top: 10px;
}

.category
{
    color: #777;
    font-size: 13px;
    margin-top: 5px;
}

.buy-btn,
.details-btn
{
    display: inline-block;
    margin-top: 15px;
    padding: 10px 13px;
    color: white;
    text-decoration: none;
    border-radius: 5px;
    font-size: 14px;
}

.buy-btn
{
    background-color: #0b3d91;
}

.buy-btn:hover
{
    background-color: #082d6b;
    color: white;
}

.details-btn
{
    background-color: #28a745;
    margin-left: 4px;
}

.details-btn:hover
{
    background-color: #218838;
    color: white;
}

.no-products
{
    text-align: center;
    padding: 40px;
    color: #666;
    font-size: 18px;
}

</style>

</asp:Content>


<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    Runat="Server">

<h1 class="products-title">
    All Products
</h1>

<p class="products-subtitle">
    Explore our latest products at the best prices
</p>


<asp:GridView
    ID="GridView1"
    runat="server"
    AutoGenerateColumns="False"
    ShowHeader="False"
    GridLines="None"
    Width="100%">

    <Columns>

        <asp:TemplateField>

            <ItemTemplate>

                <div class="product-card">

                    <div class="product-image">

                        <img
                            src='<%# "Images/" + Eval("ImageName") %>'
                            alt='<%# Eval("ProductName") %>' />

                    </div>


                    <h3>
                        <%# Eval("ProductName") %>
                    </h3>


                    <p class="product-description">

                        <%# Eval("Description") %>

                    </p>


                    <div class="category">

                        Category:
                        <%# Eval("Category") %>

                    </div>


                    <div class="price">

                        ₹<%# Eval("Price", "{0:0.00}") %>

                    </div>


                    <a
                        href='<%# "ProductDetails.aspx?id=" + Eval("ProductID") %>'
                        class="buy-btn">

                        🛒 Add to Cart

                    </a>


                    <a
                        href='<%# "ProductDetails.aspx?id=" + Eval("ProductID") %>'
                        class="details-btn">

                        👁️ View Details

                    </a>

                </div>

            </ItemTemplate>

        </asp:TemplateField>

    </Columns>

</asp:GridView>


</asp:Content>