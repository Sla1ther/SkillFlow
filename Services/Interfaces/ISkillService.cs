using SkillFlow.Models;
namespace SkillFlow.Services.Interfaces
{
    /// <summary>
    /// ISkillService defines the contract for skill-related business logic operations, including methods for 
    /// retrieving, creating, updating, and deleting skill entities. 
    /// This interface abstracts the underlying data access implementation,
    /// </summary>
    public interface ISkillService
    {
        public IEnumerable<SkillModel> GetAllSkills();
        public SkillModel GetSkillById(int id);
        public void CreateSkill(SkillModel skill);
        public void UpdateSkill(SkillModel skill);
        public void DeleteSkill(int id);
    }
}
