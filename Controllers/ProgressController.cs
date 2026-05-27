using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillFlow.Models;
using SkillFlow.Services.Interfaces;
using SkillFlow.ViewModels.Progress;

namespace SkillFlow.Controllers
{
    /// <summary>
    /// ProgressController is responsible for handling HTTP requests related to user progress tracking,
    /// including dashboard display, progress updates, and skill completion.
    /// </summary>
    [Authorize]
    public class ProgressController : Controller
    {
        #region Fields
        private readonly IProgressService _progressService;
        private readonly UserManager<User> _userManager;
        #endregion

        /// <summary>
        /// Initializes a new instance of the ProgressController class.
        /// </summary>
        /// <param name="progressService">The service for progress tracking operations.</param>
        /// <param name="userManager">The user manager for identity operations.</param>
        public ProgressController(IProgressService progressService, UserManager<User> userManager)
        {
            _progressService = progressService ?? throw new ArgumentNullException(nameof(progressService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        #region Actions

        /// <summary>
        /// Displays the progress dashboard for the current user.
        /// </summary>
        /// <returns>A view with the user's progress statistics and skill list.</returns>
        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Redirect("~/Identity/Account/Login");

            var statistics = new ProgressStatisticsViewModel
            {
                OverallProgressPercent = await _progressService.CalculateOverallProgressAsync(user.Id),
                CompletedSkillsCount = await _progressService.GetCompletedSkillsCountAsync(user.Id),
                TotalSkillsCount = await _progressService.GetTotalSkillsCountAsync(user.Id)
            };

            var userSkills = await _progressService.GetUserSkillsProgressAsync(user.Id);
            var userSkillViewModels = userSkills.Select(us => new UserSkillProgressViewModel
            {
                Id = us.Id,
                SkillId = us.SkillId,
                SkillTitle = us.Skill?.Title ?? string.Empty,
                Level = us.Skill?.Level ?? 0,
                DirectionName = us.Skill?.Direction?.Name ?? string.Empty,
                ProgressPercent = us.ProgressPercent,
                IsCompleted = us.IsCompleted,
                CompletedAt = us.CompletedAt
            }).ToList();

            var completedSkills = userSkillViewModels.Where(us => us.IsCompleted).ToList();

            var dashboard = new DashboardViewModel
            {
                Statistics = statistics,
                UserSkills = userSkillViewModels,
                CompletedSkills = completedSkills
            };

            return View(dashboard);
        }

        /// <summary>
        /// Displays a list of completed skills for the current user.
        /// </summary>
        /// <returns>A view with the user's completed skills.</returns>
        [HttpGet]
        public async Task<IActionResult> CompletedSkills()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Redirect("~/Identity/Account/Login");

            var completedSkills = await _progressService.GetCompletedSkillsAsync(user.Id);
            var viewModels = completedSkills.Select(us => new UserSkillProgressViewModel
            {
                Id = us.Id,
                SkillId = us.SkillId,
                SkillTitle = us.Skill?.Title ?? string.Empty,
                Level = us.Skill?.Level ?? 0,
                DirectionName = us.Skill?.Direction?.Name ?? string.Empty,
                ProgressPercent = us.ProgressPercent,
                IsCompleted = us.IsCompleted,
                CompletedAt = us.CompletedAt
            }).ToList();

            return View(viewModels);
        }

        /// <summary>
        /// Updates the progress for a specific skill.
        /// </summary>
        /// <param name="skillId">The skill identifier.</param>
        /// <param name="progressPercent">The new progress percentage (0-100).</param>
        /// <returns>A redirect to the dashboard or error page.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProgress(int skillId, int progressPercent)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Redirect("~/Identity/Account/Login");

            if (progressPercent < 0 || progressPercent > 100)
                return BadRequest("Progress percentage must be between 0 and 100");

            var success = await _progressService.UpdateProgressAsync(user.Id, skillId, progressPercent);

            if (success)
                return RedirectToAction(nameof(Dashboard));

            return BadRequest("Failed to update skill progress");
        }

        /// <summary>
        /// Marks a skill as completed for the current user.
        /// </summary>
        /// <param name="skillId">The skill identifier.</param>
        /// <returns>A redirect to the dashboard or error page.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteSkill(int skillId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Redirect("~/Identity/Account/Login");

            var success = await _progressService.CompleteSkillAsync(user.Id, skillId);

            if (success)
                return RedirectToAction(nameof(Dashboard));

            return BadRequest("Failed to mark skill as completed");
        }

        /// <summary>
        /// Gets progress statistics in JSON format for AJAX calls.
        /// </summary>
        /// <returns>JSON with progress statistics.</returns>
        [HttpGet]
        public async Task<IActionResult> GetProgressStats()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var stats = new ProgressStatisticsViewModel
            {
                OverallProgressPercent = await _progressService.CalculateOverallProgressAsync(user.Id),
                CompletedSkillsCount = await _progressService.GetCompletedSkillsCountAsync(user.Id),
                TotalSkillsCount = await _progressService.GetTotalSkillsCountAsync(user.Id)
            };

            return Json(stats);
        }

        #endregion
    }
}
