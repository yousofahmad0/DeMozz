using DeMozzWeb.Data;
using DeMozzWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeMozzWeb.Pages.Categories
{
    public class CreateCategoryModel : PageModel
    {
        private readonly DBConnection _db;
        [BindProperty]
        public Category Category { get; set; }
        public CreateCategoryModel(DBConnection db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if(Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Category.Name","The Display Order cannot exactly match the Name.");

            }
            if (ModelState.IsValid)
            {
                await _db.Category.AddAsync(Category);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category created successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
