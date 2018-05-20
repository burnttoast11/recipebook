using System.Collections.Generic;

namespace RecipeBookMVC.Entities
{
    /// <summary>
    /// Searchable tags that can be assigned to recipes.
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Id of this tag.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Value of this tag.
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// List of recipes with this tag.
        /// </summary>
        public virtual ICollection<RecipeTag> RecipeTags { get; set; }
    }
}