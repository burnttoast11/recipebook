using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeBookMVC.Entities;
using RecipeBookMVC.Services;

namespace RecipeBookMVC.Controllers
{
    [Route("api/v1/[controller]")]
    public class TagController : Controller
    {
        private ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] Tag tag)
        {
            if (tag == null)
            {
                return BadRequest();
            }

            try
            {
                await _tagService.CreateTag(tag);
            }
            catch (ArgumentException)
            {
                // Tag with this name already exists. Not created.
                return StatusCode(409);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return CreatedAtAction(nameof(CreateTag), new { id = tag.Id }, tag);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Tag), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTags([FromQuery] string tag)
        {
            try
            {
                var tags = await _tagService.GetTags(tag);

                if (tags.Count == 0)
                {
                    return NotFound();
                }

                return Ok(tags);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTag([FromBody] Tag tag)
        {
            if (tag == null)
            {
                return BadRequest("tag");
            }

            try
            {
                await _tagService.UpdateTag(tag);
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