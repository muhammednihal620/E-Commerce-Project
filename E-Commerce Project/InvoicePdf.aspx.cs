using System;
using System.IO;
using System.Web;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class InvoicePdf : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            MemoryStream ms = new MemoryStream();

            Document doc = new Document(PageSize.A4);

            PdfWriter.GetInstance(doc, ms);

            doc.Open();

            Font titleFont = FontFactory.GetFont(
                "Helvetica", 20, Font.BOLD);

            Font normalFont = FontFactory.GetFont(
                "Helvetica", 12, Font.NORMAL);

            doc.Add(new Paragraph("E-COMMERCE INVOICE", titleFont));
            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph("Invoice Number: INV-1001", normalFont));
            doc.Add(new Paragraph("Date: " +
                DateTime.Now.ToString("dd-MM-yyyy"), normalFont));

            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph("Customer Details", titleFont));
            doc.Add(new Paragraph("Customer Name: Test Customer", normalFont));
            doc.Add(new Paragraph(" "));

            doc.Add(new Paragraph("Order Details", titleFont));
            doc.Add(new Paragraph("Product: Smartphone", normalFont));
            doc.Add(new Paragraph("Quantity: 1", normalFont));
            doc.Add(new Paragraph("Price: Rs. 15999", normalFont));
            doc.Add(new Paragraph("Total Amount: Rs. 15999", normalFont));

            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph("Thank you for shopping with us!",
                normalFont));

            doc.Close();

            byte[] pdfBytes = ms.ToArray();
            ms.Close();

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader(
                "Content-Disposition",
                "attachment; filename=Invoice.pdf");

            Response.BinaryWrite(pdfBytes);
            Response.Flush();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.Write("PDF Error: " + ex.Message);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}