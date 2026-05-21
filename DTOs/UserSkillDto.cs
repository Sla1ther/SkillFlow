namespace SkillFlow.DTOs
{
    /// <summary>
    /// DTO for transferring user skill data between layers.
    /// </summary>
    public class UserSkillDto
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public int SkillId { get; set; }

        public bool IsCompleted { get; set; }

        public int ProgressPercent { get; set; }

        public DateTime? CompletedAt { get; set; }
    }
}
