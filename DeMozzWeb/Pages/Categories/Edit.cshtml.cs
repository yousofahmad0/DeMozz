using DeMozzWeb.Data;
using DeMozzWeb.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeMozzWeb.Pages.Categories
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly DBConnection _db;
        [BindProperty]
        public Category Category { get; set; }
        public EditModel(DBConnection db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            Category = _db.Category.Find(id);
            //Category = _db.Category.First(u => u.Id == id);//if there is more than one,
            //                                               //ignore every thing and return the first
            //                                               //FirstOrDefault if nothing found it will return null
            //Category = _db.Category.Single(u => u.Id == id);//one entity to be returned or throw exception,
            //                                                //SingeOrDefault if nothing found it will return null
            //Category = _db.Category.Where(u => u.Id == id).FirstOrDefault();//Where return all the matches
        }

        public async Task<IActionResult> OnPost()
        {
            if(Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Category.Name","The Display Order cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Category.Update(Category);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category updated successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
