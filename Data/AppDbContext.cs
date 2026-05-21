using Microsoft.EntityFrameworkCore;
using SkillFlow.Models;

namespace SkillFlow.Data
{
    /// <summary>
    /// AppDbContext is the primary class responsible for interacting with the database.
    /// It manages the entity sets for directions, skills, and user skills, and configures
    /// the relationships between these entities.
    /// </summary>
    public class AppDbContext : DbContext
    {
        #region Global
        #region DbSets
        public DbSet<DirectionModel> Directions { get; set; }
        public DbSet<SkillModel> Skills { get; set; }
        public DbSet<UserSkillModel> UserSkills { get; set; }
        #endregion
        /// <summary>
        /// Initializes a new instance of the AppDbContext class using the specified options.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext. Must not be null.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        #region OnModelCreating
        /// <summary>
        /// Configures the entity framework model for the context by defining relationships and constraints between
        /// entities.
        /// </summary>
        /// <remarks>Overrides the default model configuration to specify entity relationships, such as
        /// foreign keys and navigation properties. Call the base implementation before applying custom
        /// configurations.</remarks>
        /// <param name="modelBuilder">The builder used to construct the model for the context. Cannot be null.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
        #endregion
        #endregion
    }
}
