using MyTasks.Core;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Services;

namespace MyTasks.Persistence.Services
{
    public class TaskService :ITaskService  // tu umieszczamy w metodach logike biznesowa
    {
        private readonly IUnitOfWork _unitOfWork;
        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public IEnumerable<Core.Models.Domains.Task> Get(string userId, bool isExecuted = false, int categoryId = 0, string title = null)
        {
            return _unitOfWork.Task.Get(userId, isExecuted, categoryId, title);
      
        }


        public IEnumerable<Category> GetCategories()
        {
            return _unitOfWork.Task.GetCategories();
        }

        public Core.Models.Domains.Task Get(string userId, int id)
        {
            return _unitOfWork.Task.Get(userId,id);
        }

        public void Update(Core.Models.Domains.Task task)
        {
            _unitOfWork.Task.Update(task);
            _unitOfWork.Complete();

        }

        public void Add(Core.Models.Domains.Task task)
        {
            _unitOfWork.Task.Add(task);
            _unitOfWork.Complete();

        }

        public void Delete(int id, string userId)
        {
            _unitOfWork.Task.Delete(id, userId);
            _unitOfWork.Complete();

        }

        public void Finish(int id, string userId)
        {
            _unitOfWork.Task.Finish(id, userId);

            //wysylanie maila
            //inne zadanie
            //
            _unitOfWork.Complete();

        }


    }
}
