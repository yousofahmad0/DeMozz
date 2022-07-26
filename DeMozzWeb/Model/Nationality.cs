using System.ComponentModel.DataAnnotations;

namespace DeMozzWeb.Model
{
    public class Nationality
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
    }
}
