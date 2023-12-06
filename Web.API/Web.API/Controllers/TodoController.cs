// TodoController.cs

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
            try
            {
                var todoBoard = _dbContext.Posts.ToList();
                return Ok(todoBoard);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }

        [HttpGet("Search/{id}")]
        public ActionResult<Post> GetTodoById(int id)
        {
            try
            {
                Post todo = _dbContext.Posts.SingleOrDefault(t => t.Id == id);

                if (todo == null)
                {
                    return NotFound($"Tâche avec l'ID {id} introuvable.");
                }

                return Ok(todo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }

        [HttpPost("Add-a-task")]
        public ActionResult<Post> AddTodo([FromBody] Post model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Task) || !Enum.IsDefined(typeof(PriorityLevel), model.TaskPriority.Level))
                {
                    return BadRequest("Le contenu de la tâche ne peut pas être vide, et la priorité doit être valide.");
                }

                _dbContext.Posts.Add(model);
                _dbContext.SaveChanges();

                return CreatedAtAction(nameof(GetTodoById), new { id = model.Id }, model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }

        [HttpPut("Update-a-task/{taskId}")]
        public ActionResult<string> UpdateTask(int taskId, [FromBody] Post updatedPost)
        {
            try
            {
                Post taskToUpdate = _dbContext.Posts.SingleOrDefault(t => t.Id == taskId);

                if (taskToUpdate is null)
                {
                    return NotFound($"Tâche avec l'ID {taskId} introuvable.");
                }

                taskToUpdate.Task = updatedPost.Task;
                taskToUpdate.IsDone = updatedPost.IsDone;
                taskToUpdate.TaskPriority = updatedPost.TaskPriority;

                _dbContext.SaveChanges();

                return Ok($"Tâche avec l'ID {taskId} a été mise à jour.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }

        [HttpDelete("Delete-a-task/{taskId}")]
        public ActionResult<string> DeleteTask(int taskId)
        {
            try
            {
                Post taskToRemove = _dbContext.Posts.SingleOrDefault(t => t.Id == taskId);

                if (taskToRemove is null)
                {
                    return NotFound($"Tâche avec l'ID {taskId} introuvable.");
                }

                _dbContext.Posts.Remove(taskToRemove);
                _dbContext.SaveChanges();

                return Ok($"Tâche avec l'ID {taskId} a été supprimée.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }
    }
}
