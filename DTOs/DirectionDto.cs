namespace SkillFlow.DTOs
{
    /// <summary>
    /// DTO for transferring direction data between layers.
    /// </summary>
    public class DirectionDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
