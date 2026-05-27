using Microsoft.EntityFrameworkCore;
using SkillFlow.Data;
using SkillFlow.Models;
using SkillFlow.Services.Interfaces;

namespace SkillFlow.Services
{
    /// <summary>
    /// ProgressService provides business logic operations for tracking and managing user progress across skills.
    /// It handles calculating and updating user progress metrics.
    /// </summary>
    public class ProgressService : IProgressService
    {
        private readonly AppDbContext _db;

        /// <summary>
        /// Initializes a new instance of the ProgressService class.
        /// </summary>
        /// <param name="db">The database context used for data access operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when db is null.</exception>
        public ProgressService(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Marks a specific skill as completed for a user.
        /// </summary>
        public async Task<bool> CompleteSkillAsync(string userId, int skillId)
        {
            try
            {
                var userSkill = await _db.UserSkills
                    .FirstOrDefaultAsync(us => us.UserId == userId && us.SkillId == skillId);

                if (userSkill == null)
                    return false;

                userSkill.IsCompleted = true;
                userSkill.ProgressPercent = 100;
                userSkill.CompletedAt = DateTime.UtcNow;

                _db.UserSkills.Update(userSkill);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Updates the progress percentage for a user's skill.
        /// </summary>
        public async Task<bool> UpdateProgressAsync(string userId, int skillId, int progressPercent)
        {
            try
            {
                if (progressPercent < 0 || progressPercent > 100)
                    return false;

                var userSkill = await _db.UserSkills
                    .FirstOrDefaultAsync(us => us.UserId == userId && us.SkillId == skillId);

                if (userSkill == null)
                    return false;

                userSkill.ProgressPercent = progressPercent;

                if (progressPercent == 100 && !userSkill.IsCompleted)
                {
                    userSkill.IsCompleted = true;
                    userSkill.CompletedAt = DateTime.UtcNow;
                }

                _db.UserSkills.Update(userSkill);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        
        /// <summary>
        /// Calculates the overall learning progress percentage for a user across all skills.
        /// </summary>
        public async Task<int> CalculateOverallProgressAsync(string userId)
        {
            var userSkills = await _db.UserSkills
                .Where(us => us.UserId == userId)
                .ToListAsync();

            if (userSkills.Count == 0)
                return 0;

            var totalProgress = userSkills.Sum(us => us.ProgressPercent);
            return (int)Math.Round((double)totalProgress / userSkills.Count);
        }

        /// <summary>
        /// Gets the progress for a specific user skill.
        /// </summary>
        public async Task<UserSkillModel?> GetUserSkillProgressAsync(string userId, int skillId)
        {
            return await _db.UserSkills
                .Include(us => us.Skill)
                .Include(us => us.Skill.Direction)
                .FirstOrDefaultAsync(us => us.UserId == userId && us.SkillId == skillId);
        }

        /// <summary>
        /// Gets all skills with progress for a specific user.
        /// </summary>
        public async Task<IEnumerable<UserSkillModel>> GetUserSkillsProgressAsync(string userId)
        {
            return await _db.UserSkills
                .Where(us => us.UserId == userId)
                .Include(us => us.Skill)
                .Include(us => us.Skill.Direction)
                .OrderBy(us => us.Skill.Direction.Name)
                .ThenBy(us => us.Skill.Title)
                .ToListAsync();
        }

        /// <summary>
        /// Gets all completed skills for a specific user.
        /// </summary>
        public async Task<IEnumerable<UserSkillModel>> GetCompletedSkillsAsync(string userId)
        {
            return await _db.UserSkills
                .Where(us => us.UserId == userId && us.IsCompleted)
                .Include(us => us.Skill)
                .Include(us => us.Skill.Direction)
                .OrderByDescending(us => us.CompletedAt)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the count of completed skills for a user.
        /// </summary>
        public async Task<int> GetCompletedSkillsCountAsync(string userId)
        {
            return await _db.UserSkills
                .CountAsync(us => us.UserId == userId && us.IsCompleted);
        }

        /// <summary>
        /// Gets the total count of skills assigned to a user.
        /// </summary>
        public async Task<int> GetTotalSkillsCountAsync(string userId)
        {
            return await _db.UserSkills
                .CountAsync(us => us.UserId == userId);
        }
    }
}
