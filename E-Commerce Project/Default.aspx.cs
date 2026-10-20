using System;
using System.Data;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        con = new SqlConnection(
            "Data Source=ASUSVIVOBOOK\\SQLEXPRESS;" +
            "Initial Catalog=CheepCart;" +
            "Integrated Security=True"
        );

        if (!IsPostBack)
        {
            LoadProducts();
        }
    }

    protected void LoadProducts()
    {
        try
        {
            con.Open();

            SqlDataAdapter da =
                new SqlDataAdapter(
                    "SELECT TOP 4 " +
                    "p.ProductID, " +
                    "p.ProductName, " +
                    "p.Description, " +
                    "v.Price, " +
                    "(SELECT TOP 1 ImageName " +
                    " FROM ProductImages " +
                    " WHERE ProductID = p.ProductID " +
                    " AND IsMain = 1 " +
                    " ORDER BY ImageID) AS ImageName " +
                    "FROM Products p " +
                    "INNER JOIN ProductVariants v " +
                    "ON p.ProductID = v.ProductID " +
                    "WHERE v.VariantID = " +
                    "(SELECT MIN(v2.VariantID) " +
                    " FROM ProductVariants v2 " +
                    " WHERE v2.ProductID = p.ProductID) " +
                    "ORDER BY p.ProductID DESC",
                    con
                );

            DataTable dt =
                new DataTable();

            da.Fill(dt);

            rptProducts.DataSource = dt;
            rptProducts.DataBind();

            con.Close();
        }
        catch (Exception ex)
        {
            if (con.State ==
                System.Data.ConnectionState.Open)
            {
                con.Close();
            }

            Response.Write(
                "<h3 style='color:red;'>Database Error</h3>" +
                "<p style='color:red;'>" +
                ex.Message +
                "</p>"
            );
        }
    }
}