namespace RecipeBookMVC.Entities
{
    /// <summary>
    /// A step in the directions of a recipe.
    /// </summary>
    public class DirectionItem
    {
        /// <summary>
        /// Id of this step.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Text for a single step in a recipe.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Id of recipe this step belongs to.
        /// </summary>
        public int RecipeId { get; set; }
        /// <summary>
        /// Defines the order of the directions.
        /// </summary>
        /// <returns></returns>
        public int StepNumber {get;set;}
    }
}