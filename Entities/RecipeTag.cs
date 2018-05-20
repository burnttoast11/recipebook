using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBookMVC.Entities
{
    /// <summary>
    /// Represents a tag on a recipe.
    /// </summary>
    public class RecipeTag
    {
        /// <summary>
        /// Unique id of this recipe.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id of tag.
        /// </summary>
        public int TagId { get; set; }
        /// <summary>
        /// Reference to tag.
        /// </summary>
        public Tag Tag { get; set; }
        /// <summary>
        /// Recipe id.
        /// </summary>
        public int RecipeId { get; set; }
        /// <summary>
        /// Reference to tag.
        /// </summary>
        public Recipe Recipe { get; set; }
    }
}