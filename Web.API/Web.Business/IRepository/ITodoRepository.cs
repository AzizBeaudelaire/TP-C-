using Web.Business.Models;

namespace Web.Business.IRepository
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