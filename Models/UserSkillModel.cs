using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillFlow.Models
{
    /// <summary>
    /// Represents a user's progress and completion status for a specific skill.
    /// </summary>
    /// <remarks>This model associates a user with a skill and tracks their completion state and progress
    /// percentage. It can be used to display or update a user's skill achievements within an application.</remarks>
    public class UserSkillModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;

        public User User { get; set; } = new User();

        [Required(ErrorMessage = "Skill is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid skill")]
        [ForeignKey("Skill")]
        public int SkillId { get; set; }

        public SkillModel Skill { get; set; } = new SkillModel();

        public bool IsCompleted { get; set; }

        [Required(ErrorMessage = "Progress percentage is required")]
        [Range(0, 100, ErrorMessage = "Progress percentage must be between 0 and 100")]
        public int ProgressPercent { get; set; }

        public DateTime? CompletedAt { get; set; }

        

        
    }
}
