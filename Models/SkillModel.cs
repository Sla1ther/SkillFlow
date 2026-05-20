namespace SkillFlow.Models
{
    /// <summary>
    /// SkillModel represents an individual skill
    /// that users can learn. It contains properties for the skill's 
    /// title and a reference to the direction it belongs to. 
    /// This model is used to define specific skills within a 
    /// learning path, allowing users to track their progress and 
    /// focus on acquiring particular competencies within a broader category of knowledge.
    /// </summary>
    public class SkillModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int DirectionId { get; set; }

        public DirectionModel Direction { get; set; }
    }
}
