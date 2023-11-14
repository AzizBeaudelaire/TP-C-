using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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

        [HttpGet("Search/{id}")]
        public ActionResult<Post> GetTodoById(int id)
        {
            // Action pour rechercher une tâche par son ID en utilisant la méthode GetPostById de ListPosts.
            Post todo = ListPosts.GetPostById(id);

            // Vérifie si la tâche est introuvable.
            if (todo == null)
            {
                // Retourne une réponse 404 NotFound avec un message.
                return NotFound($"Tâche avec l'ID {id} introuvable.");
            }

            // Retourne la tâche trouvée en réponse.
            return Ok(todo);
        }

        [HttpPost("Add-a-task")]
        public ActionResult<Post> AddTodo([FromBody] Post model)
        {
            // Vérifiez si le modèle est valide, y compris la priorité (par exemple, le contenu n'est pas vide et la priorité est valide).
            if (string.IsNullOrEmpty(model.Task) || !Enum.IsDefined(typeof(Priority), model.Priority))
            {
                // Retourne une réponse BadRequest avec un message d'erreur.
                return BadRequest("Le contenu de la tâche ne peut pas être vide, et la priorité doit être valide.");
            }

            // Générez un nouvel identifiant pour la tâche.
            model.Id = ListPosts.listPosts.Count + 1; // Utilisation de la propriété Id

            // Ajoutez la nouvelle tâche à la liste en utilisant la méthode AddPost de ListPosts.
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

            if (taskToUpdate is null)
            {
                // Retourne une réponse 404 NotFound avec un message d'erreur.
                return NotFound($"Tâche avec l'ID {taskId} introuvable.");
            }

            // Mettez à jour le contenu de la tâche.
            taskToUpdate.Task = newContent;

            // Retourne une réponse Ok avec un message de réussite.
            return Ok($"Tâche avec l'ID {taskId} a été mise à jour.");
        }

        [HttpDelete("Delete-a-task/{taskId}")]
        public ActionResult<string> DeleteTask(int taskId)
        {
            // Recherchez la tâche dans la liste par son ID
            Post taskToRemove = ListPosts.listPosts.Find(task => task.Id == taskId);

            if (taskToRemove is null)
            {
                // Retourne une réponse 404 NotFound avec un message d'erreur.
                return NotFound($"Tâche avec l'ID {taskId} introuvable.");
            }

            // Supprimez la tâche de la liste.
            ListPosts.listPosts.Remove(taskToRemove);

            // Retourne une réponse Ok avec un message de réussite.
            return Ok($"Tâche avec l'ID {taskId} a été supprimée.");
        }
    }
}
