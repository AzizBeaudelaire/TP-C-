using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Web.Business.Models;
using Web.Data.Context;
using WebApi.Data;
using DbContext = Web.Data.Context.DbContext;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private readonly DbContext _dbContext;

        public TodoController(ILogger<TodoController> logger, DbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("TodoBoard")]
        public ActionResult<IEnumerable<Post>> GetTodoBoard()
        {
            // Récupérer toutes les tâches disponibles dans la base de données.
            var todoBoard = _dbContext.Posts.ToList();

            // Retourner la liste des tâches.
            return Ok(todoBoard);
        }
        
        [HttpGet("Search/{id}")]
        public ActionResult<Post> GetTodoById(int id)
        {
            // Action pour rechercher une tâche par son ID en utilisant la base de données.
            Post todo = _dbContext.Posts.SingleOrDefault(t => t.Id == id);

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
            if (string.IsNullOrEmpty(model.Task) || !Enum.IsDefined(typeof(PriorityLevel), model.TaskPriority.Level))
            {
                // Retourne une réponse BadRequest avec un message d'erreur.
                return BadRequest("Le contenu de la tâche ne peut pas être vide, et la priorité doit être valide.");
            }

            // Ajoutez la nouvelle tâche à la base de données.
            _dbContext.Posts.Add(model);
            _dbContext.SaveChanges();

            // Convention REST : retournez un code 201 Created avec un en-tête Location.
            // L'en-tête Location spécifie l'URL pour récupérer cet élément nouvellement créé.
            return CreatedAtAction(nameof(GetTodoById), new { id = model.Id }, model);
        }

        [HttpPut("Update-a-task/{taskId}")]
        public ActionResult<string> UpdateTask(int taskId, [FromBody] Post updatedPost)
        {
            // Recherchez la tâche dans la base de données par son ID
            Post taskToUpdate = _dbContext.Posts.SingleOrDefault(t => t.Id == taskId);

            if (taskToUpdate is null)
            {
                // Retourne une réponse 404 NotFound avec un message d'erreur.
                return NotFound($"Tâche avec l'ID {taskId} introuvable.");
            }

            // Mettez à jour le contenu de la tâche et la priorité.
            taskToUpdate.Task = updatedPost.Task;
            taskToUpdate.IsDone = updatedPost.IsDone;
            taskToUpdate.TaskPriority = updatedPost.TaskPriority;

            // Enregistrez les modifications dans la base de données.
            _dbContext.SaveChanges();

            // Retourne une réponse Ok avec un message de réussite.
            return Ok($"Tâche avec l'ID {taskId} a été mise à jour.");
        }

        [HttpDelete("Delete-a-task/{taskId}")]
        public ActionResult<string> DeleteTask(int taskId)
        {
            // Recherchez la tâche dans la base de données par son ID
            Post taskToRemove = _dbContext.Posts.SingleOrDefault(t => t.Id == taskId);

            if (taskToRemove is null)
            {
                // Retourne une réponse 404 NotFound avec un message d'erreur.
                return NotFound($"Tâche avec l'ID {taskId} introuvable.");
            }

            // Supprimez la tâche de la base de données.
            _dbContext.Posts.Remove(taskToRemove);
            _dbContext.SaveChanges();

            // Retourne une réponse Ok avec un message de réussite.
            return Ok($"Tâche avec l'ID {taskId} a été supprimée.");
        }
    }
}