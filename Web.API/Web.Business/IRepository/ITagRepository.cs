using Web.Business.Models;

namespace Web.Business.IRepository
{
    public interface ITagRepository
    {
        Task<List<Tag>> GetAllAsync();
        Task<Tag?> GetByIdAsync(int id);
        Task<Tag> AddAsync(Tag item);
        Task<Tag> UpdateAsync(Tag item);
        Task DeleteAsync(Tag item);
    }
}