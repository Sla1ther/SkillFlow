using SkillFlow.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SkillFlow.ViewModels.Skills
{
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
