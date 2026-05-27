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

        [Required(ErrorMessage = "Direction name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Direction name must be between 3 and 100 characters")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        public List<SkillModel> Skills { get; set; } = new List<SkillModel>();
    }
}
