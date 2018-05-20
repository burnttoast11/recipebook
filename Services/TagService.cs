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
    public class TagService : ITagService
    {
        private RecipeBookContext _recipeDb;

        public TagService(RecipeBookContext recipeDb)
        {
            _recipeDb = recipeDb;
        }

        public async Task<List<Tag>> GetTags(string searchText)
        {
            if (searchText == null)
            {
                return await _recipeDb.Tags.ToListAsync();
            }

            return await _recipeDb.Tags
                .Where(t => t.Value.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToListAsync();
        }

        public async Task CreateTag(Tag tag)
        {
            var tagExists = await _recipeDb.Tags
                .AnyAsync(t => t.Value.Equals(tag.Value, StringComparison.OrdinalIgnoreCase));

            if (tagExists)
            {
                throw new ArgumentException("Tag already exists.");
            }

            _recipeDb.Attach<Tag>(tag);
            await _recipeDb.SaveChangesAsync();
        }

        public async Task UpdateTag(Tag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            var existingTag = await _recipeDb.Tags.FirstOrDefaultAsync(t => t.Id == tag.Id);

            if (existingTag == null)
            {
                throw new ArgumentException($"Tag with id {tag.Id} does not exist.");
            }

            existingTag.Value = tag.Value;

            await _recipeDb.SaveChangesAsync();
        }

        public async Task<RecipeTag> CreateRecipeTag(int recipeId, int tagId)
        {
            var recipeTag = new RecipeTag()
            {
                TagId = tagId,
                RecipeId = recipeId
            };

            _recipeDb.Add(recipeTag);
            await _recipeDb.SaveChangesAsync();

            return recipeTag;
        }

        public async Task<List<RecipeTag>> GetRecipeTags(int recipeId)
        {
            return await _recipeDb.RecipeTags.Where(r => r.RecipeId == recipeId).ToListAsync();
        }

        public async Task RemoveRecipeTag(int id)
        {
            var recipeTag = new RecipeTag()
            {
                Id = id
            };

            _recipeDb.Attach(recipeTag);
            _recipeDb.Remove(recipeTag);

            await _recipeDb.SaveChangesAsync();
        }
    }
}