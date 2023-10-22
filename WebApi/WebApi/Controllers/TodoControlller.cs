using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;

        public TodoController(ILogger<TodoController> logger)
        {
            _logger = logger;
        }

        [HttpGet("TaskBoard")]
        public ActionResult<IEnumerable<Post>> Get()
        {
            // Tri des tâches par priorité en ordre croissant
            var sortedTasks = ListPosts.listPosts.OrderBy(task => task.Priority).ToList();
            return Ok(sortedTasks);
        }

        [HttpGet("Search/{id}")]
        public ActionResult<Post> GetTodoById(int id)
        {
            Post todo = ListPosts.GetPostById(id);

            if (todo == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }

            return Ok(todo);
        }

        [HttpPost("Add-a-task")]
        public ActionResult<Post> AddTodo([FromBody] Post model)
        {
            // Vérifiez si le modèle est valide, y compris la priorité (par exemple, le contenu n'est pas vide et la priorité est valide).
            if (string.IsNullOrEmpty(model.Task) || !Enum.IsDefined(typeof(Priority), model.Priority))
            {
                return BadRequest("Task content cannot be empty, and priority must be valid.");
            }

            // Générez un nouvel identifiant pour la tâche.
            model.Id = ListPosts.listPosts.Count + 1; // Utilisation de la propriété Id

            // Ajoutez la nouvelle tâche à la liste.
            ListPosts.AddPost(model);

            // Convention REST : retournez un code 201 Created avec un en-tête Location.
            // L'en-tête Location spécifie l'URL pour récupérer cet élément nouvellement créé.
            return CreatedAtAction(nameof(GetTodoById), new { id = model.Id }, model);
        }

        [HttpPut("Update-a-task/{taskId}")]
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

        [HttpPatch("Set-as-done/{id}")]
        public ActionResult SetTodoAsDone(long id)
        {
            Post post = ListPosts.listPosts.Find(t => t.Id == id);

            if (post == null)
            {
                return NotFound($"Post with ID {id} not found.");
            }

            if (post.IsDone)
            {
                return BadRequest("Post is already done.");
            }

            post.IsDone = true;

            return Ok();
        }

        [HttpDelete("Delete-a-task/{taskId}")]
        public ActionResult<string> DeleteTask(int taskId)
        {
            // Recherchez la tâche dans la liste par son ID
            Post taskToRemove = ListPosts.listPosts.Find(task => task.Id == taskId);

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
