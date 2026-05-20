namespace SkillFlow.Models
{
    /// <summary>
    /// Represents a user's progress and completion status for a specific skill.
    /// </summary>
    /// <remarks>This model associates a user with a skill and tracks their completion state and progress
    /// percentage. It can be used to display or update a user's skill achievements within an application.</remarks>
    public class UserSkillModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int SkillId { get; set; }
        public bool IsCompleted { get; set; }
        public int ProgressPercent { get; set; }
        public DateTime? CompletedAt { get; set; }
        //public User User { get; set; }
        public SkillModel Skill { get; set; }
    }
}
