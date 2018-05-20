using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeBookMVC.Entities;
using RecipeBookMVC.Services;

namespace RecipeBookMVC.Controllers
{
    [Route("api/v1/[controller]")]
    public class IngredientController : Controller
    {
        private IRecipeService _recipeService;

        public IngredientController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngredients(int id, [FromBody] List<IngredientItem> ingredients)
        {
            if (ingredients == null)
            {
                throw new ArgumentNullException(nameof(ingredients));
            }

            var duplicates = ingredients.GroupBy(i => i.Order)
              .Where(g => g.Count() > 1)
              .Select(y => y.Key)
              .ToList();

            if (duplicates.Count > 0)
            {
                return BadRequest($"Duplicate order property on ingredient. Order: {string.Join(", ", duplicates)}");
            }

            try
            {
                await _recipeService.UpdateRecipeIngredients(id, ingredients);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return NoContent();
        }
    }
}