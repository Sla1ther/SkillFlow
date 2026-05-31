using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        #endregion

        /// <summary>
        /// Initializes a new instance of the ProgressController class.
        /// </summary>
        /// <param name="progressService">The service for progress tracking operations.</param>
        public ProgressController(IProgressService progressService)
        {
            _progressService = progressService ?? throw new ArgumentNullException(nameof(progressService));
        }

        #region Actions

        /// <summary>
        /// Displays the progress dashboard for the current user.
        /// </summary>
        /// <returns>A view with the user's progress statistics and skill list.</returns>
        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var userId = GetCurrentUserId();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var statistics = new ProgressStatisticsViewModel
            {
                OverallProgressPercent = await _progressService.CalculateOverallProgressAsync(userId),
                CompletedSkillsCount = await _progressService.GetCompletedSkillsCountAsync(userId),
                TotalSkillsCount = await _progressService.GetTotalSkillsCountAsync(userId)
            };

            var userSkills = await _progressService.GetUserSkillsProgressAsync(userId);
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
            var userId = GetCurrentUserId();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var completedSkills = await _progressService.GetCompletedSkillsAsync(userId);
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
            var userId = GetCurrentUserId();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            if (progressPercent < 0 || progressPercent > 100)
                return BadRequest("Progress percentage must be between 0 and 100");

            var success = await _progressService.UpdateProgressAsync(userId, skillId, progressPercent);

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
            var userId = GetCurrentUserId();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var success = await _progressService.CompleteSkillAsync(userId, skillId);

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
            var userId = GetCurrentUserId();
            if (userId == null)
                return Unauthorized();

            var stats = new ProgressStatisticsViewModel
            {
                OverallProgressPercent = await _progressService.CalculateOverallProgressAsync(userId),
                CompletedSkillsCount = await _progressService.GetCompletedSkillsCountAsync(userId),
                TotalSkillsCount = await _progressService.GetTotalSkillsCountAsync(userId)
            };

            return Json(stats);
        }

        #endregion

        private string? GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
