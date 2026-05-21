using SkillFlow.Models.Enums;

namespace SkillFlow.ViewModels.Skills
{
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
