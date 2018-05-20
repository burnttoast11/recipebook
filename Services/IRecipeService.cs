using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeBookMVC.Entities;

namespace RecipeBookMVC.Services
{
    public interface IRecipeService
    {
        Task<Recipe> GetRecipe(int id);
        Task CreateRecipe(Recipe recipe);
        Task UpdateRecipe(Recipe recipe);
        Task UpdateRecipeDirections(int recipeId, List<DirectionItem> directions);
        Task UpdateRecipeIngredients(int recipeId, List<IngredientItem> ingredient);
        Task RemoveRecipe(int id);
    }
}