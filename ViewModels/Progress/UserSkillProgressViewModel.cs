using SkillFlow.Models.Enums;

namespace SkillFlow.ViewModels.Progress
{
    /// <summary>
    /// UserSkillProgressViewModel displays progress information for a user's individual skill.
    /// </summary>
    public class UserSkillProgressViewModel
    {
        /// <summary>
        /// The user skill association identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The skill identifier.
        /// </summary>
        public int SkillId { get; set; }

        /// <summary>
        /// The name of the skill.
        /// </summary>
        public string SkillTitle { get; set; } = string.Empty;

        /// <summary>
        /// The skill level.
        /// </summary>
        public SkillLevel Level { get; set; }

        /// <summary>
        /// The direction/category name for the skill.
        /// </summary>
        public string DirectionName { get; set; } = string.Empty;

        /// <summary>
        /// The progress percentage for this skill.
        /// </summary>
        public int ProgressPercent { get; set; }

        /// <summary>
        /// Indicates whether the skill is completed.
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// The date when the skill was completed, if applicable.
        /// </summary>
        public DateTime? CompletedAt { get; set; }
    }
}
