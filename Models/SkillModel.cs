namespace SkillFlow.Models
{
    public class SkillModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int DirectionId { get; set; }

        public DirectionModel Direction { get; set; }
    }
}
