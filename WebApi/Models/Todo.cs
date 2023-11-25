using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    // Classe représentant une tâche (post)
    public class Post
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public bool IsDone { get; set; }
        public Priority TaskPriority { get; set; }

        public Post(string task, bool isDone, Priority priority)
        {
            Task = task;
            Id = ListPosts.listPosts.Count + 1;
            IsDone = isDone;
            TaskPriority = priority;
        }
    }
}
