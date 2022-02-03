using MyTasks.Core.Models.Domains;

namespace MyTasks.Core.Services
{
    public interface ITaskService
    {
        IEnumerable<Models.Domains.Task> Get(string userId, bool isExecuted = false, int categoryId = 0, string title = null);
      


        IEnumerable<Category> GetCategories();
      

        Models.Domains.Task Get(string userId, int id);
    

        void Update(Models.Domains.Task task);
      

        void Add(Models.Domains.Task task);


        void Delete(int id, string userId);
   

        void Finish(int id, string userId);
    
    }
}
