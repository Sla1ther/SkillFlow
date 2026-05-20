using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillFlow.Models;

namespace SkillFlow.Data.Configs
{
    /// <summary>
    /// Provides configuration for the Direction entity type in the Entity Framework Core model.
    /// </summary>
    /// <remarks>This class defines the database table mapping, property constraints, and relationships for
    /// the DirectionModel entity. It is typically used by the Entity Framework Core infrastructure and is not intended
    /// to be called directly from application code.</remarks>
    public class DirectionConfig : IEntityTypeConfiguration<DirectionModel>
    {
        /// <summary>
        /// Configures the Direction model to database table mapping, relationships, and constraints.
        /// </summary>
        /// <param name="builder">Direction model builder</param>
        public void Configure(EntityTypeBuilder<DirectionModel> builder)
        {
            builder.ToTable("Directions");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Description)
                .HasMaxLength(500);

            builder.HasMany(d => d.Skills)
                .WithOne(s => s.Direction)
                .HasForeignKey(s => s.DirectionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
