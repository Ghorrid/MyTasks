﻿using Microsoft.EntityFrameworkCore;
using MyTasks.Core;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Repositories;
using MyTasks.Persistance;
using System.Collections.ObjectModel;

namespace MyTasks.Persistence.Repositories
{
    public class TaskRepository :ITaskRepository
    {
        private readonly IApplicationDbContext _context;
        public TaskRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Core.Models.Domains.Task> Get (string userId, bool isExecuted=false, int categoryId = 0, string title =null)
        {

            var tasks = _context.Tasks.Include(x => x.Category)
                .Where(x => x.UserId == userId && x.IsExecuted == isExecuted);
            
            if (categoryId !=0)
               tasks= tasks.Where(x => x.CategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(title))
                tasks = tasks.Where(x => x.Title.Contains(title));

            return tasks.OrderBy(x => x.Term).ToList();
        }


        public IEnumerable<Category> GetCategories(string userId)
        {
            return _context.Categories.Where(x=> x.UserId == userId).OrderBy (x=>x.Name).ToList();
        }

        public Core.Models.Domains.Task Get(string userId, int id)
        {            
            return _context.Tasks.Single(x => x.UserId == userId && x.Id == id);
        }

        public void Update(Core.Models.Domains.Task task)
        {
            var taskToUpdate = _context.Tasks.Single(x => x.Id == task.Id);

            taskToUpdate.Term = task.Term;
            taskToUpdate.Title = task.Title;
            taskToUpdate.Description = task.Description;
            taskToUpdate.IsExecuted = task.IsExecuted;
            taskToUpdate.CategoryId = task.CategoryId;

        }

        public void Add(Core.Models.Domains.Task task)
        {
            _context.Tasks.Add(task);

        }

        public void Delete(int id, string userId)
        {
            var taskToDelete = _context.Tasks.Single(x => x.Id == id && x.UserId ==userId );
            _context.Tasks.Remove(taskToDelete);


        }

        public void Finish(int id, string userId)
        {
            var taskToFinish = _context.Tasks.Single(x => x.Id == id && x.UserId == userId);
            taskToFinish.IsExecuted = true;

        }

    }
}
