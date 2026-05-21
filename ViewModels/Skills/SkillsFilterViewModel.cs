using SkillFlow.Models.Enums;

namespace SkillFlow.ViewModels.Skills
{
    /// <summary>
    /// SkillsFilterViewModel is a data transfer object used for filtering skills based on specific criteria such as direction and skill level.
    /// This view model is typically used in the presentation layer to capture user input for filtering skills and to validate that input
    /// before it is processed by the business logic layer.
    /// </summary>
    public class SkillsFilterViewModel
    {
        public int? DirectionId { get; set; }

        public SkillLevel? Level { get; set; }
    }
}

