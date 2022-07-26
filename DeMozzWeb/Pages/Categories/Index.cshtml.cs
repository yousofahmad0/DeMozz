using DeMozzWeb.Data;
using DeMozzWeb.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeMozzWeb.Pages.Categories
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly DBConnection _db;
        public IEnumerable<Category> Categories { get; set; }

        public IndexModel(DBConnection db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Categories = _db.Category;
        }
    }
}
