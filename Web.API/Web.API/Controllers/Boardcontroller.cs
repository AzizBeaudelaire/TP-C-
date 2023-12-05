// BoardController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Business.Models;
using Web.Data.Context;
using WebApi.Data;
using WebApi.Models;
using DbContext = Web.Data.Context.DbContext;

namespace Web.API.Controllers;

[ApiController]
[Route("board")]
public class BoardController : ControllerBase
{
    private readonly DbContext _context;

    public BoardController(DbContext context)
    {
        _context = context;
    }

    [HttpGet("TaskBoard")]
    public ActionResult<IEnumerable<PostGroup>> GetTaskBoard()
    {
        var sortedTasks = _context.PostGroups
            .OrderBy(postGroup => postGroup.Priority.Level)
            .Include(postGroup => postGroup.Tags)
            .Include(postGroup => postGroup.Posts)
            .ToList();

        return Ok(sortedTasks);
    }

    [HttpGet("SearchTasks")]
    public ActionResult<IEnumerable<PostGroup>> SearchTasks([FromQuery] string keyword)
    {
        var matchingTasks = _context.PostGroups
            .Where(postGroup => postGroup.Tags.Any(tag => tag.Name.Contains(keyword)) || postGroup.Posts.Any(post => post.Task.Contains(keyword)))
            .OrderBy(postGroup => postGroup.Priority.Level)
            .ThenBy(postGroup => string.Join(",", postGroup.Tags.Select(tag => tag.Name)))
            .ToList();

        return Ok(matchingTasks);
    }

    [HttpPatch("Set-as-done/{id}")]
    public ActionResult SetTodoAsDone(int id)
    {
        var task = _context.PostGroups
            .SelectMany(postGroup => postGroup.Posts, (postGroup, post) => new { postGroup, post })
            .FirstOrDefault(t => t.post.Id == id);

        if (task == null)
        {
            return NotFound($"Task with ID {id} not found.");
        }

        if (task.post.IsDone)
        {
            return BadRequest("Task is already done.");
        }

        task.post.IsDone = true;
        _context.SaveChanges();

        return Ok();
    }

    [HttpPost("AddTagToTask/{taskId}")]
    public ActionResult<string> AddTagToTask(int taskId, [FromBody] string tagName)
    {
        var task = _context.PostGroups
            .SelectMany(postGroup => postGroup.Posts, (postGroup, post) => new { postGroup, post })
            .FirstOrDefault(t => t.post.Id == taskId);

        if (task == null)
        {
            return NotFound($"Task with ID {taskId} not found.");
        }

        if (task.post.Tags == null)
        {
            task.post.Tags = new List<Tag>();
        }

        var newTag = new Tag (tagName);
        task.post.Tags.Add(newTag);

        _context.SaveChanges();

        return Ok($"Tag '{tagName}' added to task with ID {taskId}.");
    }
}