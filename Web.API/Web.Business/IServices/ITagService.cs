using System.Collections.Generic;
using WebApi.Models;
using System.Linq;
using Web.Business;
using Web.Business.Models;

namespace Web.Business.IServices
{
    public interface ITagService
    {
        IEnumerable<Tag> GetAllTags();
        Tag GetTagById(int tagId);
        Tag CreateTag(Tag tag);
        void UpdateTag(int tagId, Tag updatedTag);
        void DeleteTag(int tagId);
    }
}