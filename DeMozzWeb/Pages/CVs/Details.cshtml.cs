using DeMozzWeb.Data;
using DeMozzWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeMozzWeb.Pages.CVs
{
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

        
    }
}
