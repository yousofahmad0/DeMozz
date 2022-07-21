using DeMozzWeb.Model;

namespace DeMozzWeb.Services
{
    public class GradeService
    {
        private IConfiguration _config;

        public GradeService(IConfiguration config)
        {
            _config = config;
        }
        public int CalculateGrade(CV cV, List<string> skills)
        {
            int grade = 0;
            grade += skills.Count * 10;
            if(cV.Gender == "Female")
            {
                grade += 10;
            }
            else if(cV.Gender == "Male")
            {
                grade += 5;
            }

            return grade;
        }

        public string GenerateSkills(List<string> skills)
        {
            return string.Join(",", skills);
        }


    }
}
