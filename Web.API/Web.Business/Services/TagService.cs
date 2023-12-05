using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Web.Data.Context;
using Web.Business.Dto;
using Web.Business.Models;
using WebApi.Models;

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

        // ... autres méthodes

        public IEnumerable<TagDto> GetAllTags()
        {
            var tags = _dbContext.Tags.ToList();
            return _mapper.Map<IEnumerable<TagDto>>(tags);
        }

        public TagDto GetTagById(int tagId)
        {
            var tag = _dbContext.Tags.FirstOrDefault(t => t.Id == tagId);
            return _mapper.Map<TagDto>(tag);
        }

        public TagDto CreateTag(TagDto tagDto)
        {
            if (tagDto == null)
                throw new ArgumentNullException(nameof(tagDto));

            var tag = _mapper.Map<Tag>(tagDto);

            _dbContext.Tags.Add(tag);
            _dbContext.SaveChanges();

            return _mapper.Map<TagDto>(tag);
        }

        public void UpdateTag(int tagId, TagDto updatedTagDto)
        {
            var existingTag = _dbContext.Tags.FirstOrDefault(t => t.Id == tagId);

            if (existingTag != null)
            {
                _mapper.Map(updatedTagDto, existingTag);
                _dbContext.SaveChanges();
            }
        }
        
        public void DeleteTag(int tagId)
        {
            var tagToDelete = _dbContext.Tags.FirstOrDefault(t => t.Id == tagId);

            if (tagToDelete != null)
            {
                _dbContext.Tags.Remove(tagToDelete);
                _dbContext.SaveChanges();
            }
        }

        // ... autres méthodes
    }
}