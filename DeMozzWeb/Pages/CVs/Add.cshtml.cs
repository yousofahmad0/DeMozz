using DeMozzWeb.Data;
using DeMozzWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DeMozzWeb.Pages.CVs
{

    public class AddModel : PageModel
    {
        private readonly DBConnection _db;

        [BindProperty]
        public CV CV { get; set; }

        public IEnumerable<SelectListItem> Natio { get; set; }
            = new List<SelectListItem>()
            {
                new SelectListItem{Value= "Lebanon", Text="Lebanon"},
                new SelectListItem{Value= "Palastine", Text="Palastine"},
                new SelectListItem{Value= "Japanese", Text="Japanese"},
                new SelectListItem{Value= "USA", Text="USA"},
                new SelectListItem{Value= "UK", Text="UK"}
            };

        public AddModel(DBConnection db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {

            CV cV = new CV();
            if (ModelState.IsValid)
            {
                await _db.CV.AddAsync(CV);
                await _db.SaveChangesAsync();
                TempData["success"] = "New CV added successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
