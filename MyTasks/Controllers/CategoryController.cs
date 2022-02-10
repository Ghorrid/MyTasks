using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Services;
using MyTasks.Core.ViewModels;
using MyTasks.Persistence.Extensions;

namespace MyTasks.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {

            _categoryService = categoryService;
        }

        public IActionResult Categories()
        {
            var userId = User.GetUserId();
            var vm = _categoryService.GetCategories(userId);            
            return View(vm);
        }

        public IActionResult Category(int id = 0)
        {
            var userId = User.GetUserId();

            var category = id == 0 ?
                new Category { UserId = userId }
                : _categoryService.Get(userId, id);
            var vm = new CategoryViewModel
            {
                Category = category,
                Heading = id == 0 ? "Dodawanie nowej kategorii" : "Edycja kategorii",
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Category(Category category)
        {
            var userId = User.GetUserId();

            category.UserId = userId;
            if (!ModelState.IsValid)
            {
                var vm = new CategoryViewModel
                {
                    Category = category,
                    Heading = category.Id == 0 ? "Dodawanie nowej kategorii" : "Edycja kategorii",
                };
                return View("Category", vm);

            }

            if (category.Id == 0)
                _categoryService.AddCategory(category);
            else _categoryService.UpdateCategory(category);


            return RedirectToAction("Categories");
        }

        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {

            try
            {
                var userId = User.GetUserId();
                _categoryService.DeleteCategory(id, userId);

            }
            catch (Exception ex)
            {
                //logowanie do pliku
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new { success = true });
        }

    }
}
