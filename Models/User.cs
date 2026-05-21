namespace SkillFlow.Models
{
    using Microsoft.AspNetCore.Identity;
    /// <summary>
    /// User class represents an application user and extends the IdentityUser class provided by ASP.NET Core Identity.
    /// It includes a collection of UserSkillModel instances to track the user's skills and their progress.
    /// </summary>
    public class User : IdentityUser
    {
        public List<UserSkillModel> UserSkills { get; set; } = new List<UserSkillModel>();
    }
}
