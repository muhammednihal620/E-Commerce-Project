using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class MyOrders : System.Web.UI.Page
{
    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        con = new SqlConnection(
            "Data Source=ASUSVIVOBOOK\\SQLEXPRESS;" +
            "Initial Catalog=CheepCart;" +
            "Integrated Security=True"
        );

        if (Session["CustomerID"] == null)
        {
            Response.Redirect("Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            LoadMyOrders();
        }
    }


    // =========================================
    // LOAD ALL ORDERS
    // =========================================

    protected void LoadMyOrders()
    {
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(
            "SELECT OrderID, ProductName, Price, Quantity, " +
            "Total, OrderDate, PaymentMethod, OrderStatus " +
            "FROM OrderDetails " +
            "WHERE CustomerID=@CustomerID " +
            "ORDER BY OrderID DESC",
            con
        );

        da.SelectCommand.Parameters.AddWithValue(
            "@CustomerID",
            Convert.ToInt32(Session["CustomerID"])
        );

        DataTable dt = new DataTable();

        da.Fill(dt);

        GridView1.DataSource = dt;
        GridView1.DataBind();

        con.Close();

        if (dt.Rows.Count == 0)
        {
            Label1.Text =
                "📦 You have no orders yet.";
        }
        else
        {
            Label1.Text = "";
        }
    }


    // =========================================
    // SEARCH ORDER ID
    // =========================================

    protected void btnSearch_Click(
        object sender,
        EventArgs e)
    {
        int orderID;

        if (!int.TryParse(
            txtSearchOrderID.Text.Trim(),
            out orderID))
        {
            Label1.Text =
                "Please enter a valid Order ID.";

            return;
        }

        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(
            "SELECT OrderID, ProductName, Price, Quantity, " +
            "Total, OrderDate, PaymentMethod, OrderStatus " +
            "FROM OrderDetails " +
            "WHERE CustomerID=@CustomerID " +
            "AND OrderID=@OrderID " +
            "ORDER BY OrderID DESC",
            con
        );

        da.SelectCommand.Parameters.AddWithValue(
            "@CustomerID",
            Convert.ToInt32(Session["CustomerID"])
        );

        da.SelectCommand.Parameters.AddWithValue(
            "@OrderID",
            orderID
        );

        DataTable dt = new DataTable();

        da.Fill(dt);

        GridView1.DataSource = dt;
        GridView1.DataBind();

        con.Close();

        if (dt.Rows.Count == 0)
        {
            Label1.Text =
                "❌ Order #" + orderID +
                " not found.";
        }
        else
        {
            Label1.Text =
                "✅ Order #" + orderID +
                " found.";
        }
    }


    // =========================================
    // SHOW ALL
    // =========================================

    protected void btnShowAll_Click(
        object sender,
        EventArgs e)
    {
        txtSearchOrderID.Text = "";

        ddlStatusFilter.SelectedValue = "All";

        Label1.Text = "";

        LoadMyOrders();
    }


    // =========================================
    // STATUS FILTER
    // =========================================

    protected void ddlStatusFilter_SelectedIndexChanged(
        object sender,
        EventArgs e)
    {
        string status =
            ddlStatusFilter.SelectedValue;

        if (status == "All")
        {
            LoadMyOrders();
            return;
        }

        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(
            "SELECT OrderID, ProductName, Price, Quantity, " +
            "Total, OrderDate, PaymentMethod, OrderStatus " +
            "FROM OrderDetails " +
            "WHERE CustomerID=@CustomerID " +
            "AND OrderStatus=@OrderStatus " +
            "ORDER BY OrderID DESC",
            con
        );

        da.SelectCommand.Parameters.AddWithValue(
            "@CustomerID",
            Convert.ToInt32(Session["CustomerID"])
        );

        da.SelectCommand.Parameters.AddWithValue(
            "@OrderStatus",
            status
        );

        DataTable dt = new DataTable();

        da.Fill(dt);

        GridView1.DataSource = dt;
        GridView1.DataBind();

        con.Close();

        if (dt.Rows.Count == 0)
        {
            Label1.Text =
                "📦 No " + status +
                " orders found.";
        }
        else
        {
            Label1.Text =
                "Showing " + status +
                " orders.";
        }
    }


    // =========================================
    // CANCEL ORDER
    // =========================================

    protected void GridView1_RowCommand(
        object sender,
        GridViewCommandEventArgs e)
    {
        if (e.CommandName == "CancelOrder")
        {
            int orderID =
                Convert.ToInt32(e.CommandArgument);

            con.Open();

            SqlCommand cmd = new SqlCommand(
                "UPDATE OrderDetails " +
                "SET OrderStatus='Cancelled' " +
                "WHERE OrderID=@OrderID " +
                "AND CustomerID=@CustomerID " +
                "AND OrderStatus='Placed'",
                con
            );

            cmd.Parameters.AddWithValue(
                "@OrderID",
                orderID
            );

            cmd.Parameters.AddWithValue(
                "@CustomerID",
                Convert.ToInt32(
                    Session["CustomerID"]
                )
            );

            int result =
                cmd.ExecuteNonQuery();

            con.Close();

            if (result > 0)
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "cancelSuccess",
                    "alert('Order cancelled successfully!');",
                    true
                );
            }
            else
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "cancelError",
                    "alert('This order cannot be cancelled.');",
                    true
                );
            }

            LoadMyOrders();
        }
    }
}