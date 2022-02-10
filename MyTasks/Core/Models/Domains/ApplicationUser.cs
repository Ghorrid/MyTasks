using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;

namespace MyTasks.Core.Models.Domains
{
    public class ApplicationUser :IdentityUser
    {
        public ApplicationUser()
        {
            Tasks = new Collection<Task>();
            Categories = new Collection<Category> { new Category { Name = "Ogólna" } };
        }

        public ICollection<Task> Tasks { get; set; }

        public ICollection<Category> Categories { get; set; }

    }
}
