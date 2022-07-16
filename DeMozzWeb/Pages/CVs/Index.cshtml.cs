using DeMozzWeb.Data;
using DeMozzWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeMozzWeb.Pages.CVs
{
    public class IndexModel : PageModel
    {
        private readonly DBConnection _db;
        public IEnumerable<CV> CVs { get; set; }

        public IndexModel(DBConnection db)
        {
            _db = db;
        }

        public void OnGet()
        {
            CVs = _db.CV;
        }
    }
}
