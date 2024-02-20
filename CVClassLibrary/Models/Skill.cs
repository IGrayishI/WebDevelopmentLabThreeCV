using System.ComponentModel.DataAnnotations;

namespace CVClassLibrary.Models
{
    public class Skill
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Skill level is required.")]
        public int SkillLevel { get; set; }

        [Required(ErrorMessage = "Years of experience is required.")]
        public int YearsOfExperience { get; set; }

        public Skill()
        {
            Id = Guid.NewGuid();

        }


    }
}
