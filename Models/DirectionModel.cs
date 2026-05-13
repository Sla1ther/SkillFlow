using System.ComponentModel.DataAnnotations;

namespace SkillFlow.Models
{
    public class DirectionModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}
