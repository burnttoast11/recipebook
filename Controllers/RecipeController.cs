using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeBookMVC.Entities;
using RecipeBookMVC.Models;
using RecipeBookMVC.Services;

namespace RecipeBookMVC.Controllers
{
    [Route("api/v1/[controller]")]
    public class RecipeController : Controller
    {
        private IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        // GET /id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Recipe), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var recipe = await _recipeService.GetRecipe(id);

                if (recipe == null)
                {
                    return NotFound();
                }

                return Ok(recipe);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody] Recipe recipe)
        {
            if (recipe == null)
            {
                return BadRequest();
            }

            try
            {
                await _recipeService.CreateRecipe(recipe);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return CreatedAtAction(nameof(CreateRecipe), new { id = recipe.Id }, recipe);
        }

        /// <summary>
        /// Updates properties of a recipe. This does not update directions or ingredients.
        /// Call <see cref="UpdateIngredients"/> or <see cref="UpdateDirections"/> to update these
        /// fields.
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateRecipe([FromBody] Recipe recipe)
        {
            if (recipe == null)
            {
                return BadRequest();
            }

            try
            {
                await _recipeService.UpdateRecipe(recipe);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveRecipe(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _recipeService.RemoveRecipe(id);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return Ok();
        }
    }
}