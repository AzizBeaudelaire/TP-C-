using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("tag")]
    public class TagController : ControllerBase
    {
        private readonly List<Tag> tags = new List<Tag>();
        private int tagId = 1;

        [HttpGet("TagBoard")]
        public ActionResult<IEnumerable<Tag>> GetTags()
        {
            return Ok(tags);
        }

        [HttpPost("Create-a-tag")]
        public ActionResult<Tag> AddTag([FromBody] string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                return BadRequest("Tag content cannot be empty.");
            }

            var newTag = new Tag(tag); // Fournissez le nom (tag) comme argument au constructeur
            tags.Add(newTag);
            tagId++;

            return CreatedAtAction(nameof(GetTagById), new { id = newTag.Id }, newTag);
        }


        [HttpPut("Update-a-tag/{id}")]
        public ActionResult<string> UpdateTag(int id, [FromBody] string updatedTag)
        {
            var tagToUpdate = tags.Find(t => t.Id == id);

            if (tagToUpdate == null)
            {
                return NotFound($"Tag with ID {id} not found.");
            }

            if (string.IsNullOrEmpty(updatedTag))
            {
                return BadRequest("Tag content cannot be empty.");
            }

            tagToUpdate.Name = updatedTag;

            return Ok($"Tag with ID {id} has been updated.");
        }

        [HttpDelete("Delete-a-tag/{id}")]
        public ActionResult<string> DeleteTag(int id)
        {
            var tagToDelete = tags.Find(t => t.Id == id);

            if (tagToDelete == null)
            {
                return NotFound($"Tag with ID {id} not found.");
            }

            tags.Remove(tagToDelete);

            return Ok($"Tag with ID {id} has been deleted.");
        }

        [HttpGet("GetTagById/{id}")]
        public ActionResult<Tag> GetTagById(int id)
        {
            var tag = tags.Find(t => t.Id == id);

            if (tag == null)
            {
                return NotFound($"Tag with ID {id} not found.");
            }

            return Ok(tag);
        }
    }
}
