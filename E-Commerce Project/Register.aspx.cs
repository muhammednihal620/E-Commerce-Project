using System;
using System.Data.SqlClient;

public partial class Register : System.Web.UI.Page
{
    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        con = new SqlConnection(
            "Data Source=ASUSVIVOBOOK\\SQLEXPRESS;Initial Catalog=CheepCart;Integrated Security=True"
        );
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        con.Open();

        SqlCommand check = new SqlCommand(
            "SELECT COUNT(*) FROM Customers " +
            "WHERE Email=@Email OR Phone=@Phone",
            con
        );

        check.Parameters.AddWithValue("@Email", txtEmail.Text);
        check.Parameters.AddWithValue("@Phone", txtPhone.Text);

        int count = Convert.ToInt32(check.ExecuteScalar());

        if (count > 0)
        {
            con.Close();

            Label1.Text = "Email or Phone number is already registered.";
            Label1.ForeColor = System.Drawing.Color.Red;

            return;
        }

        SqlCommand cmd = new SqlCommand(
            "INSERT INTO Customers (FullName, Email, Phone, Password) " +
            "VALUES (@FullName, @Email, @Phone, @Password)",
            con
        );

        cmd.Parameters.AddWithValue("@FullName", txtName.Text);
        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
        cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
        cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

        cmd.ExecuteNonQuery();

        con.Close();

        Response.Write(
            "<script>alert('Registration successful!'); " +
            "window.location='Default.aspx';</script>"
        );
    }
}