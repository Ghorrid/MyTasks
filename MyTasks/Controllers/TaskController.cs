using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTasks.Core.Models;
using MyTasks.Core.ViewModels;
using MyTasks.Persistance;
using MyTasks.Persistence.Extensions;
using MyTasks.Persistence.Repositories;
using System.Security.Claims;

namespace MyTasks.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private TaskRepository _taskRepository;


        public TaskController(ApplicationDbContext context)
        {
            _taskRepository = new TaskRepository(context);
        }

        
        public IActionResult Tasks()
        {
            var userId = User.GetUserId();


            var vm = new TasksViewModel
            {
                FilterTasks = new FilterTasks(),
                Tasks = _taskRepository.Get(userId),
                Categories = _taskRepository.GetCategories()
            };
            return View(vm);
        }
        
        [HttpPost]
        public IActionResult Tasks(TasksViewModel viewModel)
        {
            var userId = User.GetUserId();

            var tasks = _taskRepository.Get(userId, viewModel.FilterTasks.IsExecuted,
                viewModel.FilterTasks.CategoryId,
                viewModel.FilterTasks.Title);

 
            return PartialView("_TasksTable.cshtml", tasks);
        }

        public IActionResult Task(int id =0)
        {
            var userId = User.GetUserId();

            var task = id == 0 ?
                new Core.Models.Domains.Task { UserId = userId, Term = DateTime.Today } :
                _taskRepository.Get(userId, id);

            var vm = new TaskViewModel
            {
                Task = task,
                Heading = id == 0 ? "Dodawanie nowego zadania" : "Edycja zadania",                
                Categories = _taskRepository.GetCategories()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Task(Core.Models.Domains.Task task)
        {
            var userId = User.GetUserId();

            task.UserId = userId;
            if (!ModelState.IsValid)
            {
                var vm = new TaskViewModel
                {
                    Task = task,
                    Heading = task.Id == 0 ? "Dodawanie nowego zadania" : "Edycja zadania",
                    Categories = _taskRepository.GetCategories()
                };
                return View("Task", vm);

            }

            if (task.Id == 0)
                _taskRepository.Add(task);
            else _taskRepository.Update(task);

            return RedirectToAction("Tasks");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {

            try
            {
                var userId = User.GetUserId();
                _taskRepository.Delete(id, userId);
            }
            catch (Exception ex)
            {
                //logowanie do pliku
                return Json(new  { success = false, message = ex.Message });
            }

            return Json(new  { success = true});
        }



        [HttpPost]
        public IActionResult Finish(int id)
        {

            try
            {
                var userId = User.GetUserId();
                _taskRepository.Finish(id, userId);
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
