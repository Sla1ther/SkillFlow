namespace SkillFlow.ViewModels.Progress
{
    /// <summary>
    /// ProgressStatisticsViewModel displays aggregated progress statistics for a user.
    /// It contains overall progress percentage, skill completion counts, and completion rates.
    /// </summary>
    public class ProgressStatisticsViewModel
    {
        /// <summary>
        /// The overall progress percentage across all skills.
        /// </summary>
        public int OverallProgressPercent { get; set; }

        /// <summary>
        /// The number of completed skills.
        /// </summary>
        public int CompletedSkillsCount { get; set; }

        /// <summary>
        /// The total number of skills assigned to the user.
        /// </summary>
        public int TotalSkillsCount { get; set; }

        /// <summary>
        /// The completion rate as a percentage.
        /// </summary>
        public int CompletionRate => TotalSkillsCount > 0 ? (CompletedSkillsCount * 100) / TotalSkillsCount : 0;

        /// <summary>
        /// The number of skills in progress (started but not completed).
        /// </summary>
        public int SkillsInProgressCount => TotalSkillsCount - CompletedSkillsCount;
    }
}
