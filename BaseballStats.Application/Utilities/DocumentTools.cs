using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace BaseballStats.Application.Utilities;

public static class DocumentTools
{
    public static Document OpenDocument(string folderName, string fileName, bool landscape = false)
    {
        Directory.CreateDirectory(folderName);
        PdfWriter writer = new($"{folderName}/{fileName}");
        PdfDocument pdfDocument = new(writer);

        if (landscape)
        {
            pdfDocument.SetDefaultPageSize(PageSize.LETTER.Rotate());
        }

        return new Document(pdfDocument);
    }
    
    public static void AddTitle(this Document document, string title)
    {
        document.Add(new Paragraph(title).SetTextAlignment(TextAlignment.CENTER).SetFontSize(20));
    }
    
    public static void AddLine(this Document document, bool isDash = false)
    {
        LineSeparator ls = new(isDash ? new DashedLine() : new SolidLine());
        document.Add(ls);
    }
}