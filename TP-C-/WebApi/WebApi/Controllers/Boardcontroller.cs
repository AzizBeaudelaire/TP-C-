using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;
using WebApi.Data;
using WebApi.Business;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("board")]
    public class BoardController : ControllerBase
    {
        private readonly BoardDbContext _context;

        public BoardController(BoardDbContext context)
        {
            _context = context;
        }

        [HttpGet("TaskBoard")]
        public ActionResult<IEnumerable<PostGroup>> GetTaskBoard()
        {
            var sortedTasks = _context.Todos
                .OrderBy(taskGroup => taskGroup.Priority.Level)
                .ThenBy(taskGroup => string.Join(",", taskGroup.Tags))
                .ToList();

            return Ok(sortedTasks);
        }

        [HttpGet("SearchTasks")]
        public ActionResult<IEnumerable<PostGroup>> SearchTasks([FromQuery] string keyword)
        {
            var matchingTasks = _context.Todos
                .Where(taskGroup => taskGroup.Posts.Any(post => post.Task.Contains(keyword) || taskGroup.Tags.Contains(keyword)))
                .OrderBy(taskGroup => taskGroup.Priority.Level)
                .ThenBy(taskGroup => string.Join(",", taskGroup.Tags))
                .ToList();

            return Ok(matchingTasks);
        }

        [HttpPatch("Set-as-done/{id}")]
        public ActionResult SetTodoAsDone(int id)
        {
            var task = _context.Todos.FirstOrDefault(t => t.Posts.Any(p => p.Id == id));

            if (task == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }

            if (task.Posts.Any(p => p.Id == id && p.IsDone))
            {
                return BadRequest("Task is already done.");
            }

            _context.Todos.FirstOrDefault(t => t.Posts.Any(p => p.Id == id)).Posts.FirstOrDefault(p => p.Id == id).IsDone = true;

            _context.SaveChanges(); // Assurez-vous de sauvegarder les modifications dans la base de données.

            return Ok();
        }

        [HttpPost("AddTagToTask/{taskId}")]
        public ActionResult<string> AddTagToTask(int taskId, [FromBody] string tagName)
        {
            var task = _context.Todos.FirstOrDefault(t => t.Posts.Any(p => p.Id == taskId));

            if (task == null)
            {
                return NotFound($"Task with ID {taskId} not found.");
            }

            if (task.Tags == null)
            {
                task.Tags = new List<string>();
            }

            task.Tags.Add(tagName);

            _context.SaveChanges(); // Assurez-vous de sauvegarder les modifications dans la base de données.

            return Ok($"Tag '{tagName}' added to task with ID {taskId}.");
        }
    }
}
