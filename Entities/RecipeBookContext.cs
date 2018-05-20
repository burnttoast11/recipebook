using Microsoft.EntityFrameworkCore;

namespace RecipeBookMVC.Entities
{
    /// <summary>
    /// Database used to store recipes.
    /// </summary>
    public class RecipeBookContext : DbContext
    {
        public RecipeBookContext(DbContextOptions<RecipeBookContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeTag>()
                .HasOne(pt => pt.Recipe)
                .WithMany(p => p.RecipeTags)
                .HasForeignKey(pt => pt.RecipeId);

            modelBuilder.Entity<RecipeTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.RecipeTags)
                .HasForeignKey(pt => pt.TagId);
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<DirectionItem> Directions { get; set; }
        public DbSet<IngredientItem> Ingredients { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<RecipeTag> RecipeTags { get; set; }
        public DbSet<Picture> Pictures { get; set; }
    }
}