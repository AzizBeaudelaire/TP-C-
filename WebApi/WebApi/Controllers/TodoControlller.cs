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
        
        
    }
}