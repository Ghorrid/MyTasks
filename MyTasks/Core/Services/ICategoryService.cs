using MyTasks.Core.Models.Domains;

namespace MyTasks.Core.Services
{
    public interface ICategoryService
    {
        void UpdateCategory(Models.Domains.Category category);

        void AddCategory(Models.Domains.Category category);

        void DeleteCategory(int id, string userId);

        IEnumerable<Category> GetCategories(string userId);

        Category Get(string userId, int id);
    }
}
