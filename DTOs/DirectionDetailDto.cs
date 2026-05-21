namespace SkillFlow.DTOs
{
    /// <summary>
    /// DTO for transferring direction data with associated skills between layers.
    /// </summary>
    public class DirectionDetailDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public List<SkillDto> Skills { get; set; } = new List<SkillDto>();
    }
}
