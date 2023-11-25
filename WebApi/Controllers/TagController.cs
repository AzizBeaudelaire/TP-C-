using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;
using WebApi.Data;

namespace WebApi.Controllers
{
    [Route("tag")]
    public class TagController : ControllerBase
    {
        private readonly TagDbContext _tagDbContext;

        public TagController(TagDbContext tagDbContext)
        {
            _tagDbContext = tagDbContext;
        }

        [HttpGet("TagBoard")]
        public ActionResult<IEnumerable<Tag>> GetTags()
        {
            return Ok(_tagDbContext.Tags.ToList());
        }

        [HttpPost("Create-a-tag")]
        public ActionResult<Tag> AddTag([FromBody] string tagName)
        {
            if (string.IsNullOrEmpty(tagName))
            {
                return BadRequest("Le contenu du tag ne peut pas être vide.");
            }

            var newTag = new Tag(tagName);
            _tagDbContext.Tags.Add(newTag);
            _tagDbContext.SaveChanges();

            return CreatedAtAction(nameof(GetTagById), new { id = newTag.Id }, newTag);
        }

        [HttpPut("Update-a-tag/{id}")]
        public ActionResult<string> UpdateTag(int id, [FromBody] string updatedTagName)
        {
            var tagToUpdate = _tagDbContext.Tags.Find(id);

            if (tagToUpdate == null)
            {
                return NotFound($"Tag avec l'ID {id} introuvable.");
            }

            tagToUpdate.Name = updatedTagName;
            _tagDbContext.SaveChanges();

            return Ok($"Tag avec l'ID {id} a été mis à jour.");
        }

        [HttpDelete("Delete-a-tag/{id}")]
        public ActionResult<string> DeleteTag(int id)
        {
            var tagToRemove = _tagDbContext.Tags.Find(id);

            if (tagToRemove == null)
            {
                return NotFound($"Tag avec l'ID {id} introuvable.");
            }

            _tagDbContext.Tags.Remove(tagToRemove);
            _tagDbContext.SaveChanges();

            return Ok($"Tag avec l'ID {id} a été supprimé.");
        }

        [HttpGet("GetTagById/{id}")]
        public ActionResult<Tag> GetTagById(int id)
        {
            var tag = _tagDbContext.Tags.Find(id);

            if (tag == null)
            {
                return NotFound($"Tag avec l'ID {id} introuvable.");
            }

            return Ok(tag);
        }
    }
}
