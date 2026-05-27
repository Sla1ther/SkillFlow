using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SkillFlow.Models.Enums;

namespace SkillFlow.Models
{
    /// <summary>
    /// SkillModel represents an individual skill
    /// that users can learn. It contains properties for the skill's 
    /// title and a reference to the direction it belongs to. 
    /// This model is used to define specific skills within a 
    /// learning path, allowing users to track their progress and 
    /// focus on acquiring particular competencies within a broader category of knowledge.
    /// </summary>
    public class SkillModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Skill title is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Skill title must be between 3 and 100 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Skill level is required")]
        public SkillLevel Level { get; set; }

        [Required(ErrorMessage = "Direction is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid direction")]
        [ForeignKey("Direction")]
        public int DirectionId { get; set; }

        public DirectionModel Direction { get; set; } = new DirectionModel();
    }
}
