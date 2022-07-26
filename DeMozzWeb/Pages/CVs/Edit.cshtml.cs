using DeMozzWeb.Data;
using DeMozzWeb.Model;
using DeMozzWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DeMozzWeb.Pages.CVs
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly DBConnection _db;
        private readonly GradeService GD;

        public IEnumerable<Nationality> nationality { get; set; }

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
        public EditModel(DBConnection db,GradeService gd)
        {
            _db = db;
            GD = gd;
        }

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

        public List<string> CheckedBe4 { get; set; }

        public void OnGet(int id)
        {
            nationality = _db.Nationality.ToList();
            CV = _db.CV.Find(id);
            CheckedBe4 = CV.Skills.Split(',').ToList();

            Random rnd = new Random();

            x = rnd.Next(1, 21);
            y = rnd.Next(20, 51);

            v = x + y;
        }

        public async Task<IActionResult> OnPost()
        {
            if (sum != v)
            {
                ModelState.AddModelError("Sum Validation", "The summation is incorrect!");
            }

            if (ModelState.IsValid)
            {
                CV.Grade = GD.CalculateGrade(CV.Gender, CheckedSkills);
                CV.Skills = GD.GenerateSkills(CheckedSkills);
                _db.CV.Update(CV);
                await _db.SaveChangesAsync();
                TempData["success"] = "New CV added successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}

