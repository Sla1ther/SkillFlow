using SkillFlow.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SkillFlow.ViewModels.Skills
{
    /// <summary>
    /// EditSkillViewModel is a data transfer object used for editing existing skills.
    /// It contains properties for the skill's identifier, title, description, level, and associated direction.
    /// This view model is typically used in the presentation layer to capture user input when
    /// editing an existing skill and to validate that input before it is processed by the business logic layer.
    /// </summary>
    public class EditSkillViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public SkillLevel Level { get; set; }

        [Required]
        public int DirectionId { get; set; }
    }
}
