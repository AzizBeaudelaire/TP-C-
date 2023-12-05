using WebApi.Models;

namespace Web.Business.Models
    {
        public class Post
        {
            public int Id { get; set; }
            public string Task { get; set; }
            public bool IsDone { get; set; }
            
            public int PriorityId { get; set; } // Ajout de cette propriété
            public Priority? TaskPriority { get; set; }
            public List<Tag> Tags { get; set; } // Ajout de cette propriété
        }
    }