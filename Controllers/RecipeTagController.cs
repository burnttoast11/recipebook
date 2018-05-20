using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeBookMVC.Entities;
using RecipeBookMVC.Services;

namespace RecipeBookMVC.Controllers
{
    [Route("api/v1/[controller]")]
    public class RecipeTagController : Controller
    {
        private ITagService _tagService;

        public RecipeTagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipeTag([FromBody] RecipeTag recipeTag)
        {
            if (recipeTag == null)
            {
                return BadRequest();
            }

            if (recipeTag.TagId <= 0)
            {
                return StatusCode(422, new { message = "Tag ID must be greater than 0." });
            }

            if (recipeTag.RecipeId <= 0)
            {
                return StatusCode(422, new { message = "Recipe ID must be greater than 0." });
            }

            try
            {
                await _tagService.CreateRecipeTag(recipeTag.RecipeId, recipeTag.TagId);

                return CreatedAtAction(nameof(CreateRecipeTag), recipeTag);
            }
            catch (ArgumentException)
            {
                // Tag or recipe not found.
                return StatusCode(422, new { message = "Tag id or recipe id was not valid."});
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetRecipeTags([FromQuery] int recipeId)
        {
            try
            {
                List<RecipeTag> tags = await _tagService.GetRecipeTags(recipeId);

                if (tags.Count == 0)
                {
                    return Ok();
                }

                return Ok(tags);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveRecipeTag([FromQuery] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _tagService.RemoveRecipeTag(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return NoContent();
        }
    }
}