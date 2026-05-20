using Microsoft.EntityFrameworkCore;
using SkillFlow.Models;

namespace SkillFlow.Data.Configs
{
    /// <summary>
    /// Provides configuration for the UserSkillModel entity type in the Entity Framework Core model.
    /// </summary>
    /// <remarks>Implements IEntityTypeConfiguration<UserSkillModel> to define table mapping, primary keys,
    /// property constraints, and relationships for the UserSkillModel entity. This configuration should be applied within
    /// the OnModelCreating method of your DbContext.</remarks>
    public class UserSkillConfig : IEntityTypeConfiguration<UserSkillModel>
    {
        /// <summary>
        /// Configures the entity type mapping for the UserSkillModel entity.
        /// </summary>
        /// <remarks>This method sets up the table name, primary key, and foreign key relationships for
        /// the UserSkillModel entity when using Entity Framework Core's model builder.</remarks>
        /// <param name="builder">The builder used to configure the UserSkillModel entity type.</param>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserSkillModel> builder)
        {
            builder.ToTable("UserSkills");
            builder.HasKey(us => us.Id);

            builder.HasOne(us => us.Skill)
                   .WithMany()
                   .HasForeignKey(us => us.SkillId);
        }
    }
}
