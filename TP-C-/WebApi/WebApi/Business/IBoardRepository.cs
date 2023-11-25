using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Business
{
    public interface IBoardRepository
    {
        Task<List<PostGroup>> GetAllAsync();
        Task<PostGroup?> GetByIdAsync(int id);
        Task<PostGroup> AddAsync(PostGroup item);
        Task<PostGroup> UpdateAsync(PostGroup item);
        Task DeleteAsync(PostGroup item);
    }
}