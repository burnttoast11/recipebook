using System.Collections.Generic;

namespace RecipeBookMVC.Entities
{
    /// <summary>
    /// Entity for a recipe.
    /// </summary>
    public class Recipe
    {
        /// <summary>
        /// Unique id of this recipe.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of this recipe.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Brief description of this recipe.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Preperation time for this recipe.
        /// </summary>
        public int PrepTime { get; set; }
        /// <summary>
        /// Cook time for this recipe.
        /// </summary>
        public int CookTime { get; set; }
        /// <summary>
        /// Total time required to make this recipe.
        /// </summary>
        public int TotalTime { get; set; }
        /// <summary>
        /// List of ingredients used to make this recipe.
        /// </summary>
        public virtual ICollection<IngredientItem> Ingredients { get; set; }
        /// <summary>
        /// List of directions describing how to make this recipe.
        /// </summary>
        public virtual ICollection<DirectionItem> Directions { get; set; }
        /// <summary>
        /// List of tags applied to this recipe.
        /// </summary>
        public virtual ICollection<RecipeTag> RecipeTags { get; set; }
    }
}