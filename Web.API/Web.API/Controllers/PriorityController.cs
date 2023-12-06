// PriorityController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Web.API.Models;
using Web.Business.Models;
using Web.Data.Context;
using WebApi.Data;
using DbContext = Web.Data.Context.DbContext;

namespace Web.API.Controllers
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
            try
            {
                Post post = ListPosts.GetPostById(id);

                if (post == null)
                {
                    return NotFound($"Tâche avec l'ID {id} introuvable.");
                }

                return Ok(new { TaskPriority = post.TaskPriority });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }

        [HttpPost("Add-a-Priority")]
        public ActionResult<PostGroup> AddPriority([FromBody] Priority priority)
        {
            try
            {
                PostGroup model = new PostGroup($"Task with Priority Level {priority.Level}", false, priority)
                {
                    Priority = ListPosts.listPosts.Count + 1
                };

                ListPosts.AddPost(model.Posts[0]);

                ListPosts.listPosts = ListPosts.listPosts.OrderBy(p => p.TaskPriority.Level).ToList();

                return CreatedAtAction(nameof(GetPriorityById), new { id = model.Priority }, model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }

        [HttpPut("Update-a-Priority/{taskId}")]
        public ActionResult<string> UpdateTask(int taskId, [FromBody] Priority? priority)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }

        [HttpDelete("Delete-a-Priority/{taskId}")]
        public ActionResult<string> DeleteTask(int taskId)
        {
            try
            {
                Post taskToRemove = ListPosts.listPosts.Find(task => task.Id == taskId);

                if (taskToRemove is null)
                {
                    return NotFound($"Tâche avec l'ID {taskId} introuvable.");
                }

                ListPosts.listPosts.Remove(taskToRemove);

                return Ok($"Tâche avec l'ID {taskId} a été supprimée.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }
    }
}
