using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("board")]
    public class BoardController : ControllerBase
    {
        private Board board;

        public BoardController()
        {
            // Initialisez votre tableau (board) ici.
            board = new Board();
        }

        [HttpGet("TaskBoard")]
        public ActionResult<IEnumerable<PostGroup>> GetTaskBoard()
        {
            if (board != null && board.Todos != null)
            {
                var sortedTasks = board.Todos
                    .OrderBy(task => task.Priority)
                    .ThenBy(task => string.Join(",", task.Tags))
                    .ToList();

                return Ok(sortedTasks);
            }

            return NotFound("No tasks found.");
        }

        [HttpPatch("Set-as-done/{id}")]
        public ActionResult SetTodoAsDone(int id)
        {
            if (board != null && board.Todos != null)
            {
                var task = board.Todos.FirstOrDefault(t => t.Posts.Any(p => p.Id == id));

                if (task == null)
                {
                    return NotFound($"Task with ID {id} not found.");
                }

                if (task.Posts.Any(p => p.Id == id && p.IsDone))
                {
                    return BadRequest("Task is already done.");
                }

                task.Posts.FirstOrDefault(p => p.Id == id).IsDone = true;

                return Ok();
            }

            return NotFound("No tasks found.");
        }

        [HttpPost("AddTagToTask/{taskId}")]
        public ActionResult<string> AddTagToTask(int taskId, [FromBody] string tagName)
        {
            var task = board.Todos.FirstOrDefault(t => t.Posts.Any(p => p.Id == taskId));

            if (task == null)
            {
                return NotFound($"Task with ID {taskId} not found.");
            }

            if (task.Tags == null)
            {
                task.Tags = new List<string>();
            }

            task.Tags.Add(tagName);

            return Ok($"Tag '{tagName}' added to task with ID {taskId}.");
        }
    }
}
