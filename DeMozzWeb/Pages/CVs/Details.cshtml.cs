using DeMozzWeb.Data;
using DeMozzWeb.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SelectPdf;

namespace DeMozzWeb.Pages.CVs
{
    [Authorize]
    public class DetailsModel : PageModel
    {

        private readonly DBConnection _db;

        public CV CV { get; set; }

        public List<string> Skills { get; set; }

        public DetailsModel(DBConnection db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            CV = _db.CV.Find(id);
            Skills = CV.Skills.Split(',').ToList();
        }

        //public IActionResult GeneratePdf(string html)
        //{
        //    html = html.Replace("StrTag", "<").Replace("EndTag", ">");

        //    HtmlToPdf htmlToPdf = new HtmlToPdf();
        //    PdfDocument pdfDocument = htmlToPdf.ConvertHtmlString(html);

        //    byte[] pdf = pdfDocument.Save();
        //    pdfDocument.Close();

        //    string pdfname = CV.FN + "_" + CV.LN + ".pdf";
        //    return File(
        //        pdf,
        //        "application/pdf",
        //        pdfname
        //        );
        //}
        public IActionResult OnPostGeneratePdf(string html)
        {
            HtmlToPdf oHtmlToPdf = new HtmlToPdf();
            PdfDocument oPdfDocument = oHtmlToPdf.ConvertHtmlString(html);
            byte[] pdf = oPdfDocument.Save();
            oPdfDocument.Close();
            return File(pdf, "application/pdf", CV.FN + "_" + CV.LN + ".pdf");
        }


    }
}
