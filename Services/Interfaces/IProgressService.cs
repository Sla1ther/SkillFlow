using SkillFlow.Models;

namespace SkillFlow.Services.Interfaces
{
    /// <summary>
    /// IProgressService defines the contract for progress tracking business logic operations.
    /// It handles calculating and updating user progress across skills.
    /// </summary>
    public interface IProgressService
    {
        /// <summary>
        /// Marks a specific skill as completed for a user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="skillId">The skill's unique identifier.</param>
        /// <returns>True if successfully marked as completed; otherwise false.</returns>
        Task<bool> CompleteSkillAsync(string userId, int skillId);

        /// <summary>
        /// Updates the progress percentage for a user's skill.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="skillId">The skill's unique identifier.</param>
        /// <param name="progressPercent">The progress percentage (0-100).</param>
        /// <returns>True if successfully updated; otherwise false.</returns>
        Task<bool> UpdateProgressAsync(string userId, int skillId, int progressPercent);

        
        /// <summary>
        /// Calculates the overall learning progress percentage for a user across all skills.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <returns>The overall progress percentage (0-100). Returns 0 if user has no skills.</returns>
        Task<int> CalculateOverallProgressAsync(string userId);

        /// <summary>
        /// Gets the progress for a specific user skill.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <param name="skillId">The skill's unique identifier.</param>
        /// <returns>The UserSkillModel if found; otherwise null.</returns>
        Task<UserSkillModel?> GetUserSkillProgressAsync(string userId, int skillId);

        /// <summary>
        /// Gets all skills with progress for a specific user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <returns>An enumerable collection of UserSkillModel instances for the user.</returns>
        Task<IEnumerable<UserSkillModel>> GetUserSkillsProgressAsync(string userId);

        /// <summary>
        /// Gets all completed skills for a specific user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <returns>An enumerable collection of completed UserSkillModel instances.</returns>
        Task<IEnumerable<UserSkillModel>> GetCompletedSkillsAsync(string userId);

        /// <summary>
        /// Gets the count of completed skills for a user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <returns>The number of completed skills.</returns>
        Task<int> GetCompletedSkillsCountAsync(string userId);

        /// <summary>
        /// Gets the total count of skills assigned to a user.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <returns>The total number of skills assigned to the user.</returns>
        Task<int> GetTotalSkillsCountAsync(string userId);
    }
}
