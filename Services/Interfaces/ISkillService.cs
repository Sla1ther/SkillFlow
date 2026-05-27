using SkillFlow.Models;
using SkillFlow.Models.Enums;
using SkillFlow.ViewModels.Skills;

namespace SkillFlow.Services.Interfaces
{
    /// <summary>
    /// ISkillService defines the contract for skill-related business logic operations, including methods for 
    /// retrieving, creating, updating, and deleting skill entities. 
    /// This interface abstracts the underlying data access implementation,
    /// </summary>
    public interface ISkillService
    {
        /// <summary>
        /// Gets all skills from the data source. This method is used to retrieve a list of all skill entities,
        /// including their details such as title, level, and direction.
        /// </summary>
        /// <returns>A collection of SkillModel objects representing all skills.</returns>
        public IEnumerable<SkillModel> GetAllSkills();
        /// <summary>
        /// Retrieves the skill with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the skill to retrieve.</param>
        /// <returns>A <see cref="SkillModel"/> representing the skill with the specified identifier, or <c>null</c> if no such
        /// skill exists.</returns>
        public SkillModel GetSkillById(int id);
        /// <summary>
        /// Adds a new skill to the underlying data store.
        /// </summary>
        /// <param name="skill">The skill to add. Cannot be null.</param>
        public void CreateSkill(SkillModel skill);
        /// <summary>
        /// Updates the specified skill with new information.
        /// </summary>
        /// <param name="skill">The skill model containing updated data. Cannot be null.</param>
        public void UpdateSkill(SkillModel skill);
        /// <summary>
        /// Deletes the skill with the specified identifier from the underlying data store. 
        /// If no skill with the given identifier exists, this method should handle it gracefully (e.g., by doing nothing or throwing a specific exception).
        /// </summary>
        /// <param name="id">The unique identifier of the skill to delete.</param>
        public void DeleteSkill(int id);
        /// <summary>
        /// Retrieves a filtered list of skills based on the specified filter criteria.
        /// </summary>
        /// <param name="filter">The filter criteria to apply. Cannot be null.</param>
        /// <returns>A collection of SkillModel objects that match the filter criteria.</returns>
        public IEnumerable<SkillModel> GetFilteredSkills(SkillsFilterViewModel filter);
    }
}
