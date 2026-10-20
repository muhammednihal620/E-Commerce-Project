
using System;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

public partial class AdminProducts : System.Web.UI.Page
{
    SqlConnection con;
    int variantCount = 1;

    protected void Page_Init(object sender, EventArgs e)
    {
        string postedCount = Request.Form[hfVariantCount.UniqueID];

        if (!String.IsNullOrEmpty(postedCount))
        {
            int count;

            if (Int32.TryParse(postedCount, out count))
            {
                if (count >= 1 && count <= 100)
                {
                    variantCount = count;
                }
            }
        }

        hfVariantCount.Value = variantCount.ToString();
        CreateVariantRows(variantCount);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(
            System.Web.HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        Response.AppendHeader("Pragma", "no-cache");
        Response.AppendHeader(
            "Cache-Control",
            "no-cache, no-store, must-revalidate");

        if (Session["Admin"] == null ||
            Session["Admin"].ToString() != "1")
        {
            Response.Redirect("AdminLogin.aspx", true);
            return;
        }

        con = new SqlConnection(
            "Data Source=ASUSVIVOBOOK\\SQLEXPRESS;" +
            "Initial Catalog=CheepCart;" +
            "Integrated Security=True");
    }

    private void CreateVariantRows(int count)
    {
        phVariants.Controls.Clear();

        for (int i = 0; i < count; i++)
        {
            AddVariantRow(i);
        }
    }

    private void AddVariantRow(int index)
    {
        Panel box = new Panel();
        box.ID = "variantBox" + index;
        box.CssClass = "variant-box";

        Label heading = new Label();
        heading.CssClass = "variant-heading";
        heading.Text = "Variant " + (index + 1);
        box.Controls.Add(heading);

        AddField(box, "Color", "txtColor" + index);
        AddField(box, "Size", "txtSize" + index);
        AddField(box, "Model", "txtModel" + index);
        AddField(box, "MRP (Original Price)", "txtMRP" + index);
        AddField(box, "Selling Price (Offer Price)", "txtPrice" + index);
        AddField(box, "Stock", "txtStock" + index);

        Panel imageRow = new Panel();
        imageRow.CssClass = "form-row";

        Label imageLabel = new Label();
        imageLabel.Text = "Variant Image";
        imageLabel.AssociatedControlID =
            "fuVariantImage" + index;

        FileUpload fuVariantImage = new FileUpload();
        fuVariantImage.ID = "fuVariantImage" + index;

        imageRow.Controls.Add(imageLabel);
        imageRow.Controls.Add(fuVariantImage);
        box.Controls.Add(imageRow);

        phVariants.Controls.Add(box);
    }

    private void AddField(
        Panel box,
        string caption,
        string controlID)
    {
        Panel row = new Panel();
        row.CssClass = "form-row";

        Label label = new Label();
        label.Text = caption;
        label.AssociatedControlID = controlID;

        TextBox txt = new TextBox();
        txt.ID = controlID;
        txt.CssClass = "textbox";

        row.Controls.Add(label);
        row.Controls.Add(txt);

        box.Controls.Add(row);
    }

    private Control FindControlRecursive(
        Control root,
        string id)
    {
        if (root.ID == id)
        {
            return root;
        }

        foreach (Control child in root.Controls)
        {
            Control found = FindControlRecursive(child, id);

            if (found != null)
            {
                return found;
            }
        }

        return null;
    }

    protected void btnAddVariant_Click(object sender, EventArgs e)
    {
        if (variantCount >= 100)
        {
            ShowError("Maximum 100 variants allowed.");
            return;
        }

        AddVariantRow(variantCount);

        variantCount++;
        hfVariantCount.Value = variantCount.ToString();

        lblMessage.Text =
            "New variant added. Enter its details and image.";
        lblMessage.ForeColor = System.Drawing.Color.Green;
    }

    protected void btnAddProduct_Click(object sender, EventArgs e)
    {
        if (txtProductName.Text.Trim() == "")
        {
            ShowError("Please enter product name.");
            return;
        }

        if (!fuImage1.HasFile &&
            !fuImage2.HasFile &&
            !fuImage3.HasFile &&
            !fuImage4.HasFile)
        {
            bool hasVariantImage = false;

            for (int i = 0; i < variantCount; i++)
            {
                FileUpload upload =
                    FindControlRecursive(
                        phVariants,
                        "fuVariantImage" + i) as FileUpload;

                if (upload != null && upload.HasFile)
                {
                    hasVariantImage = true;
                    break;
                }
            }

            if (!hasVariantImage)
            {
                ShowError("Please select at least one product image.");
                return;
            }
        }

        List<decimal> mrps = new List<decimal>();
        List<decimal> prices = new List<decimal>();
        List<int> stocks = new List<int>();

        for (int i = 0; i < variantCount; i++)
        {
            TextBox txtMRP =
                FindControlRecursive(
                    phVariants, "txtMRP" + i) as TextBox;

            TextBox txtPrice =
                FindControlRecursive(
                    phVariants, "txtPrice" + i) as TextBox;

            TextBox txtStock =
                FindControlRecursive(
                    phVariants, "txtStock" + i) as TextBox;

            decimal mrp;
            decimal price;
            int stock;

            if (txtMRP == null ||
                !Decimal.TryParse(txtMRP.Text.Trim(), out mrp) ||
                mrp <= 0)
            {
                ShowError("Enter valid MRP for Variant " + (i + 1));
                return;
            }

            if (txtPrice == null ||
                !Decimal.TryParse(txtPrice.Text.Trim(), out price) ||
                price <= 0 || price > mrp)
            {
                ShowError(
                    "Selling Price must be greater than 0 and not more than MRP for Variant " +
                    (i + 1));
                return;
            }

            if (txtStock == null ||
                !Int32.TryParse(txtStock.Text.Trim(), out stock) ||
                stock < 0)
            {
                ShowError("Enter valid stock for Variant " + (i + 1));
                return;
            }

            mrps.Add(mrp);
            prices.Add(price);
            stocks.Add(stock);
        }

        string folderPath = Server.MapPath("~/Images/");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        bool transactionStarted = false;
        SqlTransaction transaction = null;

        try
        {
            con.Open();

            transaction = con.BeginTransaction();
            transactionStarted = true;

            SqlCommand productCmd = new SqlCommand(
                "INSERT INTO Products " +
                "(ProductName, Description, Category) " +
                "VALUES (@ProductName, @Description, @Category); " +
                "SELECT SCOPE_IDENTITY();",
                con, transaction);

            productCmd.Parameters.AddWithValue(
                "@ProductName", txtProductName.Text.Trim());

            productCmd.Parameters.AddWithValue(
                "@Description", txtDescription.Text.Trim());

            productCmd.Parameters.AddWithValue(
                "@Category", txtCategory.Text.Trim());

            int productID = Convert.ToInt32(
                productCmd.ExecuteScalar());

            int firstVariantID = 0;

            for (int i = 0; i < variantCount; i++)
            {
                TextBox txtColor =
                    FindControlRecursive(
                        phVariants, "txtColor" + i) as TextBox;

                TextBox txtSize =
                    FindControlRecursive(
                        phVariants, "txtSize" + i) as TextBox;

                TextBox txtModel =
                    FindControlRecursive(
                        phVariants, "txtModel" + i) as TextBox;

                if (txtColor == null ||
                    txtSize == null ||
                    txtModel == null)
                {
                    throw new Exception(
                        "Variant fields could not be found.");
                }

                SqlCommand variantCmd = new SqlCommand(
                    "INSERT INTO ProductVariants " +
                    "(ProductID, Color, Size, Model, MRP, Price, Stock) " +
                    "VALUES " +
                    "(@ProductID, @Color, @Size, @Model, @MRP, @Price, @Stock); " +
                    "SELECT SCOPE_IDENTITY();",
                    con, transaction);

                variantCmd.Parameters.AddWithValue(
                    "@ProductID", productID);

                variantCmd.Parameters.AddWithValue(
                    "@Color", txtColor.Text.Trim());

                variantCmd.Parameters.AddWithValue(
                    "@Size", txtSize.Text.Trim());

                variantCmd.Parameters.AddWithValue(
                    "@Model", txtModel.Text.Trim());

                variantCmd.Parameters.AddWithValue(
                    "@MRP", mrps[i]);

                variantCmd.Parameters.AddWithValue(
                    "@Price", prices[i]);

                variantCmd.Parameters.AddWithValue(
                    "@Stock", stocks[i]);

                int variantID = Convert.ToInt32(
                    variantCmd.ExecuteScalar());

                if (i == 0)
                {
                    firstVariantID = variantID;
                }

                FileUpload variantUpload =
                    FindControlRecursive(
                        phVariants,
                        "fuVariantImage" + i) as FileUpload;

                if (variantUpload != null &&
                    variantUpload.HasFile)
                {
                    SaveImage(
                        variantUpload,
                        productID,
                        variantID,
                        folderPath,
                        false,
                        transaction);
                }
            }

            SaveImage(
                fuImage1, productID, firstVariantID,
                folderPath, true, transaction);

            SaveImage(
                fuImage2, productID, firstVariantID,
                folderPath, false, transaction);

            SaveImage(
                fuImage3, productID, firstVariantID,
                folderPath, false, transaction);

            SaveImage(
                fuImage4, productID, firstVariantID,
                folderPath, false, transaction);

            transaction.Commit();
            transactionStarted = false;

            lblMessage.Text =
                "Product and " + variantCount +
                " variant(s) added successfully!";
            lblMessage.ForeColor = System.Drawing.Color.Green;

            txtProductName.Text = "";
            txtDescription.Text = "";
            txtCategory.Text = "";

            variantCount = 1;
            hfVariantCount.Value = "1";
            CreateVariantRows(1);
        }
        catch (Exception ex)
        {
            if (transactionStarted && transaction != null)
            {
                try
                {
                    transaction.Rollback();
                }
                catch
                {
                }
            }

            lblMessage.Text = "Error: " + ex.Message;
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
        finally
        {
            if (con != null &&
                con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
    }

    protected void SaveImage(
        FileUpload upload,
        int productID,
        int variantID,
        string folderPath,
        bool isMain,
        SqlTransaction transaction)
    {
        if (upload == null || !upload.HasFile)
        {
            return;
        }

        string extension =
            Path.GetExtension(upload.FileName).ToLower();

        if (extension != ".jpg" &&
            extension != ".jpeg" &&
            extension != ".png")
        {
            throw new Exception(
                "Only JPG, JPEG and PNG images are supported.");
        }

        string uniqueName =
            Guid.NewGuid().ToString("N") + extension;

        string imagePath =
            Path.Combine(folderPath, uniqueName);

        upload.SaveAs(imagePath);

        SqlCommand imageCmd = new SqlCommand(
            "INSERT INTO ProductImages " +
            "(ProductID, VariantID, ImageName, IsMain) " +
            "VALUES (@ProductID, @VariantID, @ImageName, @IsMain)",
            con, transaction);

        imageCmd.Parameters.AddWithValue(
            "@ProductID", productID);

        imageCmd.Parameters.AddWithValue(
            "@VariantID", variantID);

        imageCmd.Parameters.AddWithValue(
            "@ImageName", uniqueName);

        imageCmd.Parameters.AddWithValue(
            "@IsMain", isMain);

        imageCmd.ExecuteNonQuery();
    }

    private void ShowError(string message)
    {
        lblMessage.Text = message;
        lblMessage.ForeColor = System.Drawing.Color.Red;
    }
}

