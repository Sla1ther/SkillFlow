using SkillFlow.Models.Enums;

namespace SkillFlow.ViewModels.Skills
{
    /// <summary>
    /// View model for displaying a skill as a card in the UI.
    /// Suitable for skill cards in lists, grids, and dashboard views.
    /// </summary>
    public class SkillCardViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string DirectionName { get; set; } = string.Empty;

        public SkillLevel Level { get; set; }

        public int ProgressPercent { get; set; }

        public bool IsCompleted { get; set; }


    }
}