using System.Collections.Generic;

namespace RecipeBookMVC.Entities
{
    /// <summary>
    /// Searchable tags that can be assigned to recipes.
    /// </summary>
    public class Picture
    {
        /// <summary>
        /// Id of this picture.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Path to picture.
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// If true, this picture is the primary picture of the recipe.
        /// </summary>
        public bool IsPrimary { get; set; }
    }
}