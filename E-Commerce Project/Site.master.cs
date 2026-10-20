using System;

public partial class Site : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(
            System.Web.HttpCacheability.NoCache
        );

        Response.Cache.SetNoStore();

        Response.Cache.SetExpires(
            DateTime.UtcNow.AddDays(-1)
        );

        if (Session["CustomerID"] != null)
        {
            lnkLogin.Visible = false;
            lnkRegister.Visible = false;
            lnkMyOrders.Visible = true;
            btnLogout.Visible = true;
        }
        else
        {
            lnkLogin.Visible = true;
            lnkRegister.Visible = true;
            lnkMyOrders.Visible = false;
            btnLogout.Visible = false;
        }

        LoadCartCount();
    }

    protected void LoadCartCount()
    {
        int count = 0;

        if (Session["Cart"] != null)
        {
            System.Data.DataTable cart =
                (System.Data.DataTable)Session["Cart"];

            foreach (System.Data.DataRow row in cart.Rows)
            {
                count += Convert.ToInt32(row["Quantity"]);
            }
        }

        lblCartCount.Text = "(" + count.ToString() + ")";
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();

        Response.Redirect("Default.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string search = txtSearch.Text.Trim();

        if (search != "")
        {
            Response.Redirect(
                "Products.aspx?search=" +
                Server.UrlEncode(search)
            );
        }
        else
        {
            Response.Redirect("Products.aspx");
        }
    }
}