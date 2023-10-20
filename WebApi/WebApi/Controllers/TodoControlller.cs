    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using WebApi.Models;

    namespace WebApi.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class TodoController : ControllerBase {
            
            private readonly ILogger<TodoController> _logger;

            public TodoController(ILogger<TodoController> logger)
            {
                _logger = logger;
            }
            
            [HttpGet("list")]
            public ActionResult<IEnumerable<Post>> Get() {
                return Ok(ListPosts.listPosts);
            }
            
            [HttpPost("add/{content}")]
            public ActionResult<Post> Get(string content ) {
                Post Post = new Post(content);
                ListPosts.listPosts.Add(Post);
                return Ok($"Post:  {Post.Task} id: {Post.TaskId} has been created");
            }
            
             [HttpPut("update/{taskId}")]
            public ActionResult<string> UpdateTask(int taskId, [FromBody] string newContent)
            {
                // Recherchez la tâche dans la liste par son ID
                Post taskToUpdate = ListPosts.listPosts.Find(task => task.Id == taskId);

                if (taskToUpdate == null)
                {
                    return NotFound($"Task with ID {taskId} not found.");
                }

                // Mettez à jour le contenu de la tâche
                taskToUpdate.Task = newContent;

                return Ok($"Task with ID {taskId} has been updated.");
            }
            
            [HttpDelete("delete/{taskId}")]
            public ActionResult<string> DeleteTask(int taskId)
            {
                // Recherchez la tâche dans la liste par son ID
                Post taskToRemove = ListPosts.listPosts.Find(task => task.TaskId == taskId);

                if (taskToRemove == null)
                {
                    return NotFound($"Task with ID {taskId} not found.");
                }

                // Supprimez la tâche de la liste
                ListPosts.listPosts.Remove(taskToRemove);

                return Ok($"Task with ID {taskId} has been deleted.");
            }
        }
    }