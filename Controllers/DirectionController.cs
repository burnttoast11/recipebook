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
    public class DirectionController : Controller
    {
        private IRecipeService _recipeService;

        public DirectionController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDirections(int id, [FromBody] List<DirectionItem> directions)
        {
            if (directions == null)
            {
                throw new ArgumentNullException(nameof(directions));
            }

            var duplicates = directions.GroupBy(i => i.StepNumber)
              .Where(g => g.Count() > 1)
              .Select(y => y.Key)
              .ToList();

            if (duplicates.Count > 0)
            {
                return BadRequest($"Duplicate order property on ingredient. Order: {string.Join(", ", duplicates)}");
            }

            try
            {
                await _recipeService.UpdateRecipeDirections(id, directions);
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