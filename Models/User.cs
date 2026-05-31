namespace SkillFlow.Models
{
    /// <summary>
    /// User class represents an application user with 
    /// properties for Id, Email, Password, PasswordHash, Salt, and a list of UserSkills. 
    /// It is used for authentication and authorization purposes in the application.
    /// </summary>
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public List<UserSkillModel> UserSkills { get; set; } = new List<UserSkillModel>();

    }
}
