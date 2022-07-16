using System.ComponentModel.DataAnnotations;

namespace DeMozzWeb.Model
{
    public class CV
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "First Name")]
        public string FN { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name ="Last Name")]
        public string LN { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public String DateOfBirth{ get; set; }

        [Required]
        public string Nationality { get; set; }

        //[Required]
        //[RegularExpression("Male|Female|Others", ErrorMessage = "The Gender must be either 'Male', 'Female' or 'Others' only.")]
        public string Gender { get; set; }

        public string Skills { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }
  
        public string File { get; set; }

        [Range(0, 100)]
        public int Grade { get; set; }
    }
}
