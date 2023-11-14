using System;
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
            // Action pour obtenir la liste des tags.
            return Ok(tags);
        }

        [HttpPost("Create-a-tag")]
        public ActionResult<Tag> AddTag([FromBody] string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                // Si le contenu du tag est vide, renvoyez une réponse BadRequest avec un message d'erreur.
                return BadRequest("Le contenu du tag ne peut pas être vide.");
            }

            // Créez un nouveau tag à partir de la chaîne de caractères fournie.
            var newTag = new Tag(tag);
            newTag.Id = tagId; // Attribuez un ID unique au tag.
            tags.Add(newTag);
            tagId++;

            // Renvoyez une réponse Created avec l'URL pour obtenir ce tag nouvellement créé.
            return CreatedAtAction(nameof(GetTagById), new { id = newTag.Id }, newTag);
        }

        [HttpPut("Update-a-tag/{id}")]
        public ActionResult<string> UpdateTag(int id, [FromBody] string updatedTag)
        {
            var tagToUpdate = tags.Find(t => t.Id == id);

            if (tagToUpdate == null)
            {
                // Si le tag n'est pas trouvé, renvoyez une réponse NotFound avec un message d'erreur.
                return NotFound($"Tag avec l'ID {id} introuvable.");
            }

            if (string.IsNullOrEmpty(updatedTag))
            {
                // Si le contenu du tag mis à jour est vide, renvoyez une réponse BadRequest avec un message d'erreur.
                return BadRequest("Le contenu du tag ne peut pas être vide.");
            }

            // Mettez à jour le contenu du tag.
            tagToUpdate.Name = updatedTag;

            // Renvoyez une réponse Ok avec un message de succès.
            return Ok($"Tag avec l'ID {id} a été mis à jour.");
        }

        [HttpDelete("Delete-a-tag/{id}")]
        public ActionResult<string> DeleteTag(int id)
        {
            var tagToDelete = tags.Find(t => t.Id == id);

            if (tagToDelete == null)
            {
                // Si le tag n'est pas trouvé, renvoyez une réponse NotFound avec un message d'erreur.
                return NotFound($"Tag avec l'ID {id} introuvable.");
            }

            // Supprimez le tag de la liste.
            tags.Remove(tagToDelete);

            // Renvoyez une réponse Ok avec un message de succès.
            return Ok($"Tag avec l'ID {id} a été supprimé.");
        }

        [HttpGet("GetTagById/{id}")]
        public ActionResult<Tag> GetTagById(int id)
        {
            var tag = tags.Find(t => t.Id == id);

            if (tag == null)
            {
                // Si le tag n'est pas trouvé, renvoyez une réponse NotFound avec un message d'erreur.
                return NotFound($"Tag avec l'ID {id} introuvable.");
            }

            // Renvoyez le tag trouvé en réponse.
            return Ok(tag);
        }
    }
}
