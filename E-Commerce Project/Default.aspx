
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true"
    CodeFile="Default.aspx.cs"
    Inherits="_Default" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    Runat="Server">

<style type="text/css">

.hero {
    height: 380px;
    background: linear-gradient(90deg, #0b3d91, #1976d2);
    border-radius: 12px;
    margin: 20px 0;
    display: flex;
    align-items: center;
    padding: 50px;
    color: white;
}

.hero-text {
    width: 55%;
}

.hero-text h1 {
    font-size: 48px;
    margin-bottom: 15px;
}

.hero-text p {
    font-size: 20px;
    margin-bottom: 30px;
}

.shop-btn {
    background-color: #ffcc00;
    color: #111;
    padding: 14px 30px;
    text-decoration: none;
    font-weight: bold;
    border-radius: 6px;
    display: inline-block;
}

.hero-image {
    width: 45%;
    text-align: center;
    font-size: 130px;
}

.section-title {
    text-align: center;
    margin: 35px 0 20px 0;
    color: #0b3d91;
    font-size: 30px;
}

.categories {
    display: flex;
    justify-content: space-between;
    gap: 20px;
}

.category {
    background-color: white;
    width: 25%;
    padding: 25px;
    text-align: center;
    border-radius: 10px;
    box-shadow: 0 2px 10px #ddd;
}

.category-icon {
    font-size: 45px;
}

.category h3 {
    color: #0b3d91;
}

.featured-title {
    text-align: center;
    margin-top: 50px;
    margin-bottom: 25px;
    color: #0b3d91;
    font-size: 30px;
}

.product-grid {
    width: 100%;
    display: grid;
    grid-template-columns: repeat(4, 1fr);
    gap: 20px;
    margin-bottom: 40px;
}

.product-card {
    background-color: white;
    padding: 20px;
    text-align: center;
    border-radius: 10px;
    box-shadow: 0 2px 10px #ddd;
}

.product-image {
    height: 160px;
    display: flex;
    justify-content: center;
    align-items: center;
}

.product-image img {
    width: 150px;
    height: 150px;
    object-fit: contain;
}

.product-card h3 {
    color: #222;
    margin: 10px 0;
}

.product-description {
    color: #666;
    font-size: 14px;
    min-height: 35px;
}

.price {
    color: #e53935;
    font-size: 22px;
    font-weight: bold;
    margin-top: 10px;
}

.buy-btn,
.details-btn {
    display: inline-block;
    margin-top: 15px;
    padding: 10px 13px;
    color: white;
    text-decoration: none;
    border-radius: 5px;
    font-size: 14px;
}

.buy-btn {
    background-color: #0b3d91;
}

.details-btn {
    background-color: #28a745;
    margin-left: 4px;
}

.no-products {
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

<div class="hero">

    <div class="hero-text">

        <h1>Shop Smart. Shop Better.</h1>

        <p>
            Discover amazing products at the best prices.
            Everything you need, all in one place.
        </p>

        <a href="Products.aspx" class="shop-btn">
            SHOP NOW
        </a>

    </div>

    <div class="hero-image">
        🛍️
    </div>

</div>


<h2 class="section-title">
    Shop By Category
</h2>


<div class="categories">

    <div class="category">
        <div class="category-icon">📱</div>
        <h3>Mobiles</h3>
        <p>Latest smartphones and accessories</p>
    </div>

    <div class="category">
        <div class="category-icon">👕</div>
        <h3>Fashion</h3>
        <p>Trending clothes and fashion</p>
    </div>

    <div class="category">
        <div class="category-icon">💄</div>
        <h3>Beauty</h3>
        <p>Beauty and personal care products</p>
    </div>

    <div class="category">
        <div class="category-icon">🏠</div>
        <h3>Home</h3>
        <p>Products for your beautiful home</p>
    </div>

</div>


<h2 class="featured-title">
    Featured Products
</h2>


<div class="product-grid">

    <asp:Repeater
        ID="rptProducts"
        runat="server">

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

                <div class="price">
                    ₹<%# Eval("Price", "{0:0.00}") %>
                </div>

                <a
                    href='<%# "ProductDetails.aspx?id=" + Eval("ProductID") %>'
                    class="details-btn">
                    👁️ View Details
                </a>

                <a
                    href='<%# "Checkout.aspx?product=" + Eval("ProductID") + "&action=buy&quantity=1" %>'
                    class="buy-btn">
                    ⚡ Buy Now
                </a>

            </div>

        </ItemTemplate>

    </asp:Repeater>

</div>

</asp:Content>
