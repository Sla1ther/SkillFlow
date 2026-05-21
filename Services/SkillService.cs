using Microsoft.EntityFrameworkCore;
using SkillFlow.Data;
using SkillFlow.Models;
using SkillFlow.Services.Interfaces;

namespace SkillFlow.Services
{
    /// <summary>
    /// SkillService provides business logic operations for managing skills.
    /// It handles CRUD operations (Create, Read, Update, Delete) for skill entities.
    /// </summary>
    public class SkillService : ISkillService
    {
        private readonly AppDbContext _db;

        /// <summary>
        /// Initializes a new instance of the SkillService class.
        /// </summary>
        /// <param name="dbContext">The database context used for data access operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when dbContext is null.</exception>
        public SkillService(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Retrieves all skills from the database.
        /// </summary>
        /// <returns>An enumerable collection of all SkillModel instances.</returns>
        public IEnumerable<SkillModel> GetAllSkills()
        {
            return _db.Skills
                .Include(s => s.Direction)
                .AsEnumerable();
        }

        /// <summary>
        /// Retrieves a specific skill by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the skill to retrieve.</param>
        /// <returns>The SkillModel with the specified id, or null if not found.</returns>
        public SkillModel GetSkillById(int id)
        {
            return _db.Skills
                .Include(s => s.Direction)
                .FirstOrDefault(s => s.Id == id) ?? new SkillModel();
        }

        /// <summary>
        /// Creates a new skill in the database.
        /// </summary>
        /// <param name="skill">The SkillModel instance to create.</param>
        /// <exception cref="ArgumentNullException">Thrown when skill is null.</exception>
        /// <exception cref="ArgumentException">Thrown when skill title is empty or skill's direction does not exist.</exception>
        public void CreateSkill(SkillModel skill)
        {
            
            if (skill != null && !string.IsNullOrWhiteSpace(skill.Title))
            {
                var directionExists = _db.Directions.Any(d => d.Id == skill.DirectionId);
                if (directionExists)
                {
                    _db.Skills.Add(skill);
                    _db.SaveChanges();
                }
            }

            
        }

        /// <summary>
        /// Updates an existing skill in the database.
        /// </summary>
        /// <param name="skill">The SkillModel instance with updated values.</param>
        /// <exception cref="ArgumentNullException">Thrown when skill is null.</exception>
        /// <exception cref="ArgumentException">Thrown when skill title is empty or skill not found.</exception>
        public void UpdateSkill(SkillModel skill)
        {
            if (skill != null && !string.IsNullOrWhiteSpace(skill.Title))
            {
                var existingSkill = _db.Skills.FirstOrDefault(s => s.Id == skill.Id);
                if (existingSkill != null)
                {
                    existingSkill.Title = skill.Title;
                    existingSkill.Level = skill.Level;
                    existingSkill.DirectionId = skill.DirectionId;

                    _db.Skills.Update(existingSkill);
                    _db.SaveChanges();
                }
            }
               
        }

        /// <summary>
        /// Deletes a skill from the database by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the skill to delete.</param>
        /// <exception cref="ArgumentException">Thrown when skill not found or skill has associated user skills.</exception>
        public void DeleteSkill(int id)
        {
            var skill = _db.Skills.FirstOrDefault(s => s.Id == id);
            if (skill != null)
            {
                var hasUserSkills = _db.UserSkills.Any(us => us.SkillId == id);
                if (!hasUserSkills)
                {
                    _db.Skills.Remove(skill);
                    _db.SaveChanges();
                }
            }
        }
    }
}
