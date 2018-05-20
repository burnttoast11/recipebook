namespace RecipeBookMVC.Entities
{
    /// <summary>
    /// An ingredient in a recipe.
    /// </summary>
    public class IngredientItem
    {
        /// <summary>
        /// Id of this ingredient.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id of the recipe that this ingredient belongs to.
        /// </summary>
        public int RecipeId { get; set; }
        /// <summary>
        /// The text displayed for this ingredient.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Specifies the display order of this ingredient.
        /// </summary>
        /// <returns></returns>
        public int Order { get; set; }
    }
}