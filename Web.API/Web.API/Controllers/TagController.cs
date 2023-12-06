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
    [Route("tag")]
    public class TagController : ControllerBase
    {
        private readonly DbContext _dbContext;

        public TagController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("TagBoard")]
        public ActionResult<IEnumerable<Tag>> GetTags()
        {
            return Ok(_dbContext.Tags.ToList());
        }

        [HttpPost("Create-a-tag")]
        public ActionResult<Tag> AddTag([FromBody] string tagName)
        {
            if (string.IsNullOrEmpty(tagName))
            {
                return BadRequest("Le contenu du tag ne peut pas être vide.");
            }

            var newTag = new Tag(tagName);
            newTag.Name = tagName;
            _dbContext.Tags.Add(newTag);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetTagById), new { id = newTag.Id }, newTag);
        }

        [HttpPut("Update-a-tag/{id}")]
        public ActionResult<string> UpdateTag(int id, [FromBody] string updatedTagName)
        {
            var tagToUpdate = _dbContext.Tags.Find(id);

            if (tagToUpdate == null)
            {
                return NotFound($"Tag avec l'ID {id} introuvable.");
            }

            tagToUpdate.Name = updatedTagName;
            _dbContext.SaveChanges();

            return Ok($"Tag avec l'ID {id} a été mis à jour.");
        }

        [HttpDelete("Delete-a-tag/{id}")]
        public ActionResult<string> DeleteTag(int id)
        {
            var tagToRemove = _dbContext.Tags.Find(id);

            if (tagToRemove == null)
            {
                return NotFound($"Tag avec l'ID {id} introuvable.");
            }

            _dbContext.Tags.Remove(tagToRemove);
            _dbContext.SaveChanges();

            return Ok($"Tag avec l'ID {id} a été supprimé.");
        }

        [HttpGet("GetTagById/{id}")]
        public ActionResult<Tag> GetTagById(int id)
        {
            var tag = _dbContext.Tags.Find(id);

            if (tag == null)
            {
                return NotFound($"Tag avec l'ID {id} introuvable.");
            }

            return Ok(tag);
        }
    }
}
