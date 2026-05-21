using Microsoft.EntityFrameworkCore;
using SkillFlow.Models;

namespace SkillFlow.Data.Configs
{
    /// <summary>
    /// Provides configuration for the SkillModel entity type in the Entity Framework Core model.
    /// </summary>
    /// <remarks>Implements IEntityTypeConfiguration<SkillModel> to define table mapping, primary keys,
    /// property constraints, and relationships for the SkillModel entity. This configuration should be applied within
    /// the OnModelCreating method of your DbContext.</remarks>
    public class SkillConfig : IEntityTypeConfiguration<SkillModel>
    {
        /// <summary>
        /// Configures the entity type mapping for the SkillModel entity.
        /// </summary>
        /// <remarks>Call this method within the OnModelCreating method to define table mapping, keys,
        /// property constraints, and relationships for the SkillModel entity.</remarks>
        /// <param name="builder">The builder used to configure the SkillModel entity type.</param>
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SkillModel> builder)
        {
            builder.ToTable("Skills");
            builder.HasKey(s => s.Id);


            builder.HasOne(s => s.Direction)
                   .WithMany(d => d.Skills)
                   .HasForeignKey(s => s.DirectionId);
        }
    }
}
