using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using RecipeBookMVC.Entities;
using RecipeBookMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;

namespace RecipeBookMVC.Services
{
    public class RecipeService : IRecipeService
    {
        private RecipeBookContext _recipeDb;

        public RecipeService(RecipeBookContext recipeDb)
        {
            _recipeDb = recipeDb;
        }
        public async Task<Recipe> GetRecipe(int id)
        {
            return await _recipeDb.Recipes
                .Include(r => r.Directions)
                .Include(r => r.Ingredients)
                .SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task CreateRecipe(Recipe recipe)
        {
            _recipeDb.Attach<Recipe>(recipe);

            await _recipeDb.SaveChangesAsync();
        }

        /// <summary>
        /// Updates fields of a reicipe. Does not update directions or ingredients for the recipe.
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        public async Task UpdateRecipe(Recipe recipe)
        {
            // This method does not update directions or ingredients.
            recipe.Directions = null;
            recipe.Ingredients = null;

            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe));
            }

            if (!await _recipeDb.Recipes.AnyAsync(r => r.Id == recipe.Id))
            {
                throw new ArgumentException($"No recipe found with ID '{recipe.Id}'.");
            }

            _recipeDb.Attach<Recipe>(recipe);
            _recipeDb.Entry<Recipe>(recipe).State = EntityState.Modified;

            await _recipeDb.SaveChangesAsync();
        }

        /// <summary>
        /// Update all directions in a recipe. Left over directions will be deleted.
        /// </summary>
        /// <param name="recipeId">Id of recipe to update.</param>
        /// <param name="directions">List of directions to save for this recipe.</param>
        public async Task UpdateRecipeDirections(int recipeId, List<DirectionItem> directions)
        {
            if (directions == null)
            {
                throw new ArgumentNullException(nameof(directions));
            }

            if (!await _recipeDb.Recipes.AnyAsync(r => r.Id == recipeId))
            {
                throw new ArgumentException($"No recipe with id {recipeId}.");
            }

            // Remove all existing directions and re-save new directions.
            var existingDirections = await _recipeDb.Directions.Where(d => d.RecipeId == recipeId).ToListAsync();

            // Find existing directions with a step number not on the new list of directions.
            var existingToRemove = existingDirections.Where(ed => !directions.Any(d => d.StepNumber == ed.StepNumber)).ToList();
            if (existingToRemove.Count > 0)
            {
                _recipeDb.Directions.RemoveRange(existingToRemove);
            }

            foreach (var direction in directions)
            {
                // Replace existing item.
                var existingToReplace = existingDirections.FirstOrDefault(d => d.StepNumber == direction.StepNumber);

                if (existingToReplace != null)
                {
                    existingToReplace.Text = direction.Text;
                }
                else
                {
                    // Add if new.
                    direction.RecipeId = recipeId;
                    _recipeDb.Attach(direction);
                }
            }

            await _recipeDb.SaveChangesAsync();
        }

        /// <summary>
        /// Update all ingredients in a recipe. Left over ingredients will be deleted.
        /// </summary>
        /// <param name="recipeId">Id of recipe to update.</param>
        /// <param name="ingredients">List of ingredients to save for this recipe.</param>
        public async Task UpdateRecipeIngredients(int recipeId, List<IngredientItem> ingredients)
        {
            if (ingredients == null)
            {
                throw new ArgumentNullException(nameof(ingredients));
            }

            if (!await _recipeDb.Recipes.AnyAsync(r => r.Id == recipeId))
            {
                throw new ArgumentException($"No recipe with id {recipeId}.");
            }

            // Remove all existing directions and re-save new directions.
            var existingDirections = await _recipeDb.Ingredients.Where(d => d.RecipeId == recipeId).ToListAsync();

            // Find existing directions with a step number not on the new list of directions.
            var existingToRemove = existingDirections.Where(ed => !ingredients.Any(d => d.Order == ed.Order)).ToList();
            if (existingToRemove.Count > 0)
            {
                _recipeDb.Ingredients.RemoveRange(existingToRemove);
            }

            foreach (var direction in ingredients)
            {
                // Replace existing item.
                var existingToReplace = existingDirections.FirstOrDefault(d => d.Order == direction.Order);

                if (existingToReplace != null)
                {
                    existingToReplace.Text = direction.Text;
                }
                else
                {
                    // Add if new.
                    direction.RecipeId = recipeId;
                    _recipeDb.Attach(direction);
                }
            }

            await _recipeDb.SaveChangesAsync();
        }

        public async Task RemoveRecipe(int id) {
            var recipe = new Recipe() {
                Id = id
            };
            
            _recipeDb.Attach(recipe);
            _recipeDb.Remove(recipe);

            await _recipeDb.SaveChangesAsync();
        }
    }
}