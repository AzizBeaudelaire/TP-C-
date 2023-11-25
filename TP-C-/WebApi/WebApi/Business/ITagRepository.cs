using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Business
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