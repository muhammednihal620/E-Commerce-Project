using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class AdminOrders : System.Web.UI.Page
{
SqlConnection con;

protected void Page_Load(object sender, EventArgs e)
{
    // Prevent browser cache
    Response.Cache.SetCacheability(
        System.Web.HttpCacheability.NoCache);

    Response.Cache.SetNoStore();

    Response.Cache.SetExpires(
        DateTime.UtcNow.AddDays(-1));

    Response.AppendHeader(
        "Pragma",
        "no-cache");

    Response.AppendHeader(
        "Cache-Control",
        "no-cache, no-store, must-revalidate");


    // Admin Login Check
    if (Session["Admin"] == null ||
        Session["Admin"].ToString() != "1")
    {
        Response.Redirect(
            "AdminLogin.aspx",
            true);

        return;
    }


    con = new SqlConnection(
        "Data Source=ASUSVIVOBOOK\\SQLEXPRESS;" +
        "Initial Catalog=CheepCart;" +
        "Integrated Security=True"
    );


    if (!IsPostBack)
    {
        LoadOrders();
    }
}


// LOAD ALL ORDERS

protected void LoadOrders()
{
    con.Open();


    SqlDataAdapter da =
        new SqlDataAdapter(
            "SELECT OrderID, CustomerName, " +
            "ProductName, Price, Quantity, Total, " +
            "OrderDate, PaymentMethod, OrderStatus " +
            "FROM OrderDetails " +
            "ORDER BY OrderID DESC",
            con
        );


    DataTable dt =
        new DataTable();


    da.Fill(dt);


    GridView1.DataSource = dt;

    GridView1.DataBind();


    con.Close();
}


// SEARCH ORDER ID

protected void btnSearch_Click(
    object sender,
    EventArgs e)
{
    int orderID;


    if (!int.TryParse(
        txtSearchOrderID.Text.Trim(),
        out orderID))
    {
        lblMessage.Text =
            "Please enter a valid Order ID.";

        lblMessage.ForeColor =
            System.Drawing.Color.Red;

        return;
    }


    con.Open();


    SqlDataAdapter da =
        new SqlDataAdapter(
            "SELECT OrderID, CustomerName, " +
            "ProductName, Price, Quantity, Total, " +
            "OrderDate, PaymentMethod, OrderStatus " +
            "FROM OrderDetails " +
            "WHERE OrderID=@OrderID",
            con
        );


    da.SelectCommand.Parameters.AddWithValue(
        "@OrderID",
        orderID
    );


    DataTable dt =
        new DataTable();


    da.Fill(dt);


    GridView1.DataSource = dt;

    GridView1.DataBind();


    con.Close();


    if (dt.Rows.Count == 0)
    {
        lblMessage.Text =
            "Order #" +
            orderID +
            " not found.";

        lblMessage.ForeColor =
            System.Drawing.Color.Red;
    }
    else
    {
        lblMessage.Text =
            "Order #" +
            orderID +
            " found.";

        lblMessage.ForeColor =
            System.Drawing.Color.Green;
    }
}


// SHOW ALL ORDERS

protected void btnShowAll_Click(
    object sender,
    EventArgs e)
{
    txtSearchOrderID.Text = "";

    lblMessage.Text = "";

    LoadOrders();
}


// UPDATE STATUS

protected void GridView1_RowCommand(
    object sender,
    GridViewCommandEventArgs e)
{
    if (e.CommandName ==
        "UpdateStatus")
    {
        int orderID =
            Convert.ToInt32(
                e.CommandArgument);


        GridViewRow row =
            (GridViewRow)
            ((Button)e.CommandSource)
            .NamingContainer;


        DropDownList ddlStatus =
            (DropDownList)
            row.FindControl(
                "ddlStatus");


        string status =
            ddlStatus.SelectedValue;


        con.Open();


        SqlCommand cmd =
            new SqlCommand(
                "UPDATE OrderDetails " +
                "SET OrderStatus=@OrderStatus " +
                "WHERE OrderID=@OrderID",
                con
            );


        cmd.Parameters.AddWithValue(
            "@OrderStatus",
            status
        );


        cmd.Parameters.AddWithValue(
            "@OrderID",
            orderID
        );


        int result =
            cmd.ExecuteNonQuery();


        con.Close();


        if (result > 0)
        {
            lblMessage.Text =
                "Order #" +
                orderID +
                " status updated successfully!";

            lblMessage.ForeColor =
                System.Drawing.Color.Green;
        }
        else
        {
            lblMessage.Text =
                "Order status update failed.";

            lblMessage.ForeColor =
                System.Drawing.Color.Red;
        }


        // Reload orders
        // If search was used, show searched order again

        if (txtSearchOrderID.Text.Trim() != "")
        {
            btnSearch_Click(null, null);
        }
        else
        {
            LoadOrders();
        }
    }
}


// LOGOUT

protected void btnLogout_Click(
    object sender,
    EventArgs e)
{
    Session["Admin"] = null;

    Session.Clear();

    Session.RemoveAll();

    Session.Abandon();


    Response.Cache.SetCacheability(
        System.Web.HttpCacheability.NoCache);

    Response.Cache.SetNoStore();

    Response.Cache.SetExpires(
        DateTime.UtcNow.AddDays(-1));

    Response.AppendHeader(
        "Pragma",
        "no-cache");

    Response.AppendHeader(
        "Cache-Control",
        "no-cache, no-store, must-revalidate");


    Response.Redirect(
        "AdminLogin.aspx",
        true);
}


}
