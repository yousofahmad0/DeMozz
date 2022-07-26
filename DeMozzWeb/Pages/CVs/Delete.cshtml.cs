using DeMozzWeb.Data;
using DeMozzWeb.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeMozzWeb.Pages.CVs
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly DBConnection _db;
        [BindProperty]
        public CV CV { get; set; }
        public DeleteModel(DBConnection db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            CV = _db.CV.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {

            var CVToDel = _db.CV.Find(CV.Id);
            if (CVToDel != null)
            {
                if (!string.IsNullOrWhiteSpace(CVToDel.File))
                {
                    string path = ".\\wwwroot" + CVToDel.File;
                    FileInfo im = new FileInfo(path);
                    im.Delete();
                }
                

                _db.Remove(CVToDel);
                await _db.SaveChangesAsync();
                TempData["success"] = "CV deleted successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
