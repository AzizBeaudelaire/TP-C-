using System;
using System.Collections.Generic;
using System.Linq;
using Web.Business.Dto;
using Web.Business.Models;
using Web.Business.IServices;
using Web.Data.Context;

namespace Web.Business.Services
{
    public class TagService : ITagService
    {
        private readonly TodoDbContext _dbContext;
        private readonly IMapper _mapper;

        public TagService(TodoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IEnumerable<TagDto> GetAllTags()
        {
            try
            {
                var tags = _dbContext.Tags.ToList();
                return _mapper.Map<IEnumerable<TagDto>>(tags);
            }
            catch (Exception ex)
            {
                // Gérer l'erreur (journalisation, notification, etc.)
                // Vous pouvez également renvoyer une liste vide ou null, ou relancer l'exception selon vos besoins.
                throw new Exception("Une erreur s'est produite lors de la récupération des tags.", ex);
            }
        }

        public TagDto GetTagById(int tagId)
        {
            try
            {
                var tag = _dbContext.Tags.FirstOrDefault(t => t.Id == tagId);
                return _mapper.Map<TagDto>(tag);
            }
            catch (Exception ex)
            {
                throw new Exception($"Une erreur s'est produite lors de la récupération du tag avec l'ID {tagId}.", ex);
            }
        }

        public TagDto CreateTag(TagDto tagDto)
        {
            try
            {
                if (tagDto == null)
                    throw new ArgumentNullException(nameof(tagDto));

                var tag = _mapper.Map<Tag>(tagDto);

                _dbContext.Tags.Add(tag);
                _dbContext.SaveChanges();

                return _mapper.Map<TagDto>(tag);
            }
            catch (Exception ex)
            {
                throw new Exception("Une erreur s'est produite lors de la création du tag.", ex);
            }
        }

        public void UpdateTag(int tagId, TagDto updatedTagDto)
        {
            try
            {
                var existingTag = _dbContext.Tags.FirstOrDefault(t => t.Id == tagId);

                if (existingTag != null)
                {
                    _mapper.Map(updatedTagDto, existingTag);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Une erreur s'est produite lors de la mise à jour du tag avec l'ID {tagId}.", ex);
            }
        }

        public void DeleteTag(int tagId)
        {
            try
            {
                var tagToDelete = _dbContext.Tags.FirstOrDefault(t => t.Id == tagId);

                if (tagToDelete != null)
                {
                    _dbContext.Tags.Remove(tagToDelete);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Une erreur s'est produite lors de la suppression du tag avec l'ID {tagId}.", ex);
            }
        }
    }
}
