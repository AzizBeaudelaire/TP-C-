using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Business
{
    public interface ITodoRepository
    {
        Task<List<Post>> GetAllAsync();
        Task<Post?> GetByIdAsync(long id);
        Task<Post> AddAsync(Post item);
        Task<Post> UpdateAsync(Post item);
        Task DeleteAsync(Post item);
    }
}