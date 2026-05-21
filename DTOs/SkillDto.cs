using SkillFlow.Models.Enums;

namespace SkillFlow.DTOs
{
    /// <summary>
    /// DTO for transferring skill data between layers.
    /// </summary>
    public class SkillDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public SkillLevel Level { get; set; }

        public int DirectionId { get; set; }
    }
}
