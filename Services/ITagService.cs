using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeBookMVC.Entities;

namespace RecipeBookMVC.Services
{
    /// <summary>
    /// Contains methods for interacting with tags.
    /// </summary>
    public interface ITagService
    {
        /// <summary>
        /// Gets all tags matching the given text. All tags are returned if no search string is given.
        /// </summary>
        /// <param name="searchText">Search for tags containing this text.</param>
        /// <returns>All matching tags.</returns>
        Task<List<Tag>> GetTags(string searchText);

        /// <summary>
        /// Creates a new tag.
        /// </summary>
        /// <param name="tag">Tag that will be created.</param>
        Task CreateTag(Tag tag);
        Task<RecipeTag> CreateRecipeTag(int id, int recipeId);

        /// <summary>
        /// Updates the text of the tag with the given ID.
        /// </summary>
        /// <param name="tag">Tag to update.</param>
        Task UpdateTag(Tag tag);
        Task<List<RecipeTag>> GetRecipeTags(int recipeId);
        Task RemoveRecipeTag(int id);
    }
}