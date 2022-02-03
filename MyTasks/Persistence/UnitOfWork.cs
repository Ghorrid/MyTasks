using MyTasks.Core;
using MyTasks.Core.Repositories;
using MyTasks.Persistance;
using MyTasks.Persistence.Repositories;

namespace MyTasks.Persistence
{
    public class UnitOfWork : IUnitOfWork // odpowiedzialny tylko za operacje na bazie danych
    {
        private readonly IApplicationDbContext _context;
        public UnitOfWork(IApplicationDbContext context)
        {
            _context = context;
            Task = new TaskRepository(context);
        }

        public ITaskRepository Task { get; set; }

        public void Complete()
        {
            _context.SaveChanges();
        }


    }
}
