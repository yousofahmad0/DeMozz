using DeMozzWeb.Data;
using DeMozzWeb.ImageUploadService;
using DeMozzWeb.Model;
using DeMozzWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DeMozzWeb.Pages.CVs
{

    public class AddModel : PageModel
    {
        private readonly DBConnection _db;
        private readonly GradeService GD;
        private readonly IImageUploadService ImS;

        public IEnumerable<SelectListItem> Natio { get; set; }
            = new List<SelectListItem>()
            {
                new SelectListItem{Value= "Lebanon", Text="Lebanon"},
                new SelectListItem{Value= "Palastine", Text="Palastine"},
                new SelectListItem{Value= "Japanese", Text="Japanese"},
                new SelectListItem{Value= "USA", Text="USA"},
                new SelectListItem{Value= "UK", Text="UK"}
            };

        public List<string> skills { get; set; }
            = new List<string>()
            {
                "PHP",
                "C",
                "C++",
                "Java",
                "C#"
            };

        [BindProperty]
        public CV CV { get; set; }

        [BindProperty]
        [EmailAddress]
        [Display(Name = "Confirm Email")]
        public string ConfirmEmail { get; set; }

        [BindProperty]
        public int sum { get; set; }
        [BindProperty]
        public int x { get; set; }
        [BindProperty]
        public int y { get; set; }

        [BindProperty]
        public int v { get; set; }

        [BindProperty]
        public List<string> CheckedSkills { get; set; }

        public AddModel(DBConnection db, GradeService gd,IImageUploadService ims)
        {
            _db = db;
            GD = gd;
            ImS = ims;
        }

        public void OnGet()
        {
            Random rnd = new Random();

            x = rnd.Next(1,21);
            y = rnd.Next(20,51);

            v = x + y;
        }

        public async Task<IActionResult> OnPost(IFormFile Im)
        {
            
            if (sum != v)
            {
                ModelState.AddModelError("Sum Validation", "The summation is incorrect!");
            }
            if (CV.Email != ConfirmEmail)
            {
                ModelState.AddModelError("Email Validation", "The Confirmation Email is wrong!");
            }

            if (ModelState.IsValid)
            {
                if (Im != null)
                {
                   CV.File = await ImS.UploadImageAsync(Im);
                }
                
                CV.Grade = GD.CalculateGrade(CV, CheckedSkills);
                CV.Skills = GD.GenerateSkills(CheckedSkills);
                await _db.CV.AddAsync(CV);
                await _db.SaveChangesAsync();
                TempData["success"] = "New CV added successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
         
    }
}
