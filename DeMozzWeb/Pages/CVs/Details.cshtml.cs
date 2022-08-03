using DeMozzWeb.Data;
using DeMozzWeb.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> OnPost(int? id)
        {
            CV = await _db.CV.FirstOrDefaultAsync(m => m.Id == id);

            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

            graphics.DrawString("First Name : " + CV.FN, font, PdfBrushes.Black, new PointF(0, 0));
            graphics.DrawString("Last Name : " + CV.LN, font, PdfBrushes.Black, new PointF(0, 50));
            graphics.DrawString("Email : " + CV.Email, font, PdfBrushes.Black, new PointF(0, 100));
            graphics.DrawString("Gender : " + CV.Gender, font, PdfBrushes.Black, new PointF(0, 150));
            graphics.DrawString("Nationality : " + CV.Nationality.ToString(), font, PdfBrushes.Black, new PointF(0, 200));
            graphics.DrawString("Date : " + CV.DateOfBirth, font, PdfBrushes.Black, new PointF(0, 250));
            graphics.DrawString("Skills : " + CV.Skills, font, PdfBrushes.Black, new PointF(0, 300));
            graphics.DrawString("Grade : " + CV.Grade, font, PdfBrushes.Black, new PointF(0, 350));

            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0;
            string contentType = "application/pdf";
            string fileName = CV.FN+" "+CV.LN+".pdf";
            return File(stream, contentType, fileName);
        }
      
        


    }
}
