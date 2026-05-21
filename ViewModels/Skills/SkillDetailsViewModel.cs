using SkillFlow.Models.Enums;

namespace SkillFlow.ViewModels.Skills
{
    /// <summary>
    /// SkillDetailsViewModel is a data transfer object used for displaying detailed information about a skill.
    /// It contains properties for the skill's identifier, title, description, level, associated direction, progress, and completion status.
    /// This view model is typically used in the presentation layer to display detailed information about a skill.
    /// </summary>
    public class SkillDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public SkillLevel Level { get; set; }

        public string DirectionName { get; set; } = string.Empty;

        public int ProgressPercent { get; set; }

        public bool IsCompleted { get; set; }
    }
}
