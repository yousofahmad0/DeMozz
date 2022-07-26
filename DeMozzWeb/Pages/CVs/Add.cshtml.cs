using DeMozzWeb.Data;
using DeMozzWeb.ImageUploadService;
using DeMozzWeb.Model;
using DeMozzWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DeMozzWeb.Pages.CVs
{
    [Authorize]
    public class AddModel : PageModel
    {
        
        private readonly DBConnection _db;
        private readonly GradeService GD;
        private readonly IImageUploadService ImS;

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

        //[BindProperty]
        //public CV CV { get; set; }

        //[BindProperty]
        //[EmailAddress]
        //[Display(Name = "Confirm Email")]
        //public string ConfirmEmail { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

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
            nationality = _db.Nationality.ToList();
            Random rnd = new Random();

            x = rnd.Next(1,21);
            y = rnd.Next(20,51);

            v = x + y;
        }

        public async Task<IActionResult> OnPost(IFormFile Im)
        {
            
            //if(Im != null){
            //    string type = Im.FileName.Substring(Im.FileName.IndexOf('.') + 1);
            //    //string type = Im.ContentType;
            //    if (type != "jpg" || type != "png" || type != "jpeg")
            //    {
            //        ModelState.AddModelError("MustBeImage", "We only accept jpg, jpeg or png.");
            //    }
            //}
            
            
            if (sum != v)
            {
                ModelState.AddModelError("Sum Validation", "The summation is incorrect!");
            }
            

            if (ModelState.IsValid)
            {
                CV CV = new CV();
                if (Im != null) 
                {
                   CV.File = await ImS.UploadImageAsync(Im);
                }
                CV.FN = Input.FN;
                CV.LN = Input.LN;
                CV.DateOfBirth = Input.DateOfBirth;
                CV.Nationality = Input.Nationality;
                CV.Gender = Input.Gender;
                CV.Email = Input.Email;

                CV.Grade = GD.CalculateGrade(Input.Gender, CheckedSkills);
                CV.Skills = GD.GenerateSkills(CheckedSkills);
                await _db.CV.AddAsync(CV);
                await _db.SaveChangesAsync();
                TempData["success"] = "New CV added successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }

        public class InputModel
        {
            [Required]
            [StringLength(30)]
            [Display(Name = "First Name")]
            public string FN { get; set; }

            [Required]
            [StringLength(30)]
            [Display(Name = "Last Name")]
            public string LN { get; set; }

            [Required]
            [Display(Name = "Date of Birth")]
            public String DateOfBirth { get; set; }

            [Required]
            public string Nationality { get; set; }

            [Required]
            //[RegularExpression("Male|Female|Others", ErrorMessage = "The Gender must be either 'Male', 'Female' or 'Others' only.")]
            public string Gender { get; set; }

            [EmailAddress]
            [Required]
            public string Email { get; set; }

            [BindProperty]
            [EmailAddress]
            [Compare(nameof(Email), ErrorMessage = "Email and Confiration email did not match!")]
            [Display(Name = "Confirm Email")]
            public string ConfirmEmail { get; set; }

            public string Skills { get; set; }

            public string File { get; set; }

            [Range(0, 100)]
            public int Grade { get; set; }
        }
    }
}
