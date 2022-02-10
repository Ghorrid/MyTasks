using MyTasks.Core;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Repositories;

namespace MyTasks.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IApplicationDbContext _context;

        public CategoryRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public void UpdateCategory(Category category)
        {
            var categoryToUpdate = _context.Categories.Single(x => x.Id == category.Id);

            categoryToUpdate.Name = category.Name;

        }


        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
        }

        public void DeleteCategory(int id, string userId)
        {
            var categoryToDelete = _context.Categories.Single(x => x.Id == id && x.UserId == userId);
            _context.Categories.Remove(categoryToDelete);
        }

        public IEnumerable<Category> GetCategories(string userId)
        {
            return _context.Categories.Where(x=>x.UserId == userId).OrderBy(x => x.Name).ToList();
        }

        public Category Get(string userId, int id)
        {
            return _context.Categories.Where(x =>x.UserId==userId && x.Id==id).FirstOrDefault();
        }
    }
}
