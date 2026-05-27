using SkillFlow.Models.Enums;

namespace SkillFlow.ViewModels.Progress
{
    /// <summary>
    /// DashboardViewModel combines user progress statistics with user skill progress data for display on the dashboard.
    /// </summary>
    public class DashboardViewModel
    {
        /// <summary>
        /// User's progress statistics.
        /// </summary>
        public ProgressStatisticsViewModel Statistics { get; set; } = new ProgressStatisticsViewModel();

        /// <summary>
        /// List of user skills with their progress information.
        /// </summary>
        public List<UserSkillProgressViewModel> UserSkills { get; set; } = new List<UserSkillProgressViewModel>();

        /// <summary>
        /// List of completed skills for quick reference.
        /// </summary>
        public List<UserSkillProgressViewModel> CompletedSkills { get; set; } = new List<UserSkillProgressViewModel>();
    }

}
