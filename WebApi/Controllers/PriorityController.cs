using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriorityController : ControllerBase
    {
        private readonly ILogger<PriorityController> _logger;

        public PriorityController(ILogger<PriorityController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Search/{id}")]
        public ActionResult<Post> GetPriorityById(int id)
        {
            Post post = ListPosts.GetPostById(id);

            if (post == null)
            {
                return NotFound($"Tâche avec l'ID {id} introuvable.");
            }

            return Ok(new { TaskPriority = post.TaskPriority });
        }

        [HttpPost("Add-a-Priority")]
        public ActionResult<Post> AddPriority([FromBody] Priority priority)
        {
            Post model = new Post($"Task with Priority Level {priority.Level}", false, priority)
            {
                Id = ListPosts.listPosts.Count + 1
            };

            ListPosts.AddPost(model);

            ListPosts.listPosts = ListPosts.listPosts.OrderBy(p => p.TaskPriority.Level).ToList();

            return CreatedAtAction(nameof(GetPriorityById), new { id = model.Id }, new { TaskPriority = model.TaskPriority });
        }

        [HttpPut("Update-a-Priority/{taskId}")]
        public ActionResult<string> UpdateTask(int taskId, [FromBody] Priority priority)
        {
            Post taskToUpdate = ListPosts.listPosts.Find(task => task.Id == taskId);

            if (taskToUpdate is null)
            {
                return NotFound($"Tâche avec l'ID {taskId} introuvable.");
            }

            taskToUpdate.TaskPriority = priority;

            // Tri des tâches par priorité (niveau) croissante
            ListPosts.listPosts = ListPosts.listPosts.OrderBy(p => p.TaskPriority.Level).ToList();

            return Ok($"Tâche avec l'ID {taskId} a été mise à jour.");
        }

        [HttpDelete("Delete-a-Priority/{taskId}")]
        public ActionResult<string> DeleteTask(int taskId)
        {
            Post taskToRemove = ListPosts.listPosts.Find(task => task.Id == taskId);

            if (taskToRemove is null)
            {
                return NotFound($"Tâche avec l'ID {taskId} introuvable.");
            }

            ListPosts.listPosts.Remove(taskToRemove);

            return Ok($"Tâche avec l'ID {taskId} a été supprimée.");
        }
    }
}
