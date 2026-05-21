namespace SkillFlow.DTOs
{
    /// <summary>
    /// DTO for transferring user data with associated skills between layers.
    /// </summary>
    public class UserDetailDto
    {
        public string Id { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public List<UserSkillDetailDto> UserSkills { get; set; } = new List<UserSkillDetailDto>();
    }
}
