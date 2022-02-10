using MyTasks.Core;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Services;

namespace MyTasks.Persistence.Services
{
    public class CategoryService : ICategoryService

    {

        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public void AddCategory(Category category)
        {
            _unitOfWork.Category.AddCategory(category);
            _unitOfWork.Complete();
        }

        public void DeleteCategory(int id, string userId)
        {
            _unitOfWork.Category.DeleteCategory(id,userId);
            _unitOfWork.Complete();
        }

        public Category Get(string userId, int id)
        {
            return _unitOfWork.Category.Get(userId, id);
        }

        public IEnumerable<Category> GetCategories(string userId)
        {
            return _unitOfWork.Category.GetCategories(userId);
        }

        public void UpdateCategory(Category category)
        {
            _unitOfWork.Category.UpdateCategory(category);
            _unitOfWork.Complete();
        }
    }
}
