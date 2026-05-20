using System.ComponentModel.DataAnnotations;

namespace SkillFlow.Models
{
    /// <summary>
    /// DirectionModel represents a learning path or category 
    /// that groups related skills together. It contains properties for the 
    /// direction's name, description, and a list of associated skills. 
    /// This model is used to organize skills into meaningful categories for users to follow in their learning journey.
    /// </summary>
    public class DirectionModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public List<SkillModel> Skills { get; set; } = new List<SkillModel>();
    }
}
