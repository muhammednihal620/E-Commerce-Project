using System;
using System.Data;
using System.Data.SqlClient;

public partial class Products : System.Web.UI.Page
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
            SearchProducts();
        }
    }


    protected void SearchProducts()
    {
        string search =
            Request.QueryString["search"];


        if (string.IsNullOrEmpty(search))
        {
            ShowAllProducts();
            return;
        }


        con.Open();


        SqlDataAdapter da =
            new SqlDataAdapter(
                "SELECT " +
                "p.ProductID, " +
                "p.ProductName, " +
                "p.Description, " +
                "p.Category, " +
                "v.Price, " +
                "pi.ImageName " +

                "FROM Products p " +

                "INNER JOIN ProductVariants v " +
                "ON p.ProductID = v.ProductID " +

                "LEFT JOIN ProductImages pi " +
                "ON v.VariantID = pi.VariantID " +
                "AND pi.IsMain = 1 " +

                "WHERE " +
                "p.ProductName LIKE @Search " +
                "OR p.Description LIKE @Search " +
                "OR p.Category LIKE @Search",

                con
            );


        da.SelectCommand.Parameters.AddWithValue(
            "@Search",
            "%" + search + "%"
        );


        DataTable dt =
            new DataTable();


        da.Fill(dt);


        GridView1.DataSource = dt;

        GridView1.DataBind();


        con.Close();
    }


    protected void ShowAllProducts()
    {
        con.Open();


        SqlDataAdapter da =
            new SqlDataAdapter(
                "SELECT " +
                "p.ProductID, " +
                "p.ProductName, " +
                "p.Description, " +
                "p.Category, " +
                "v.Price, " +
                "pi.ImageName " +

                "FROM Products p " +

                "INNER JOIN ProductVariants v " +
                "ON p.ProductID = v.ProductID " +

                "LEFT JOIN ProductImages pi " +
                "ON v.VariantID = pi.VariantID " +
                "AND pi.IsMain = 1 " +

                "ORDER BY p.ProductID DESC",

                con
            );


        DataTable dt =
            new DataTable();


        da.Fill(dt);


        GridView1.DataSource = dt;

        GridView1.DataBind();


        con.Close();


        if (dt.Rows.Count == 0)
        {
            GridView1.EmptyDataText =
                "📦 No products available.";

            GridView1.DataBind();
        }
    }
}