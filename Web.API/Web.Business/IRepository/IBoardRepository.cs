using Web.Business.Models;

namespace Web.Business.IRepository
{
    public interface IBoardRepository
    {
        Task<List<Post>> GetAllAsync();
        Task<Post?> GetByIdAsync(int id);
        Task<Post> AddAsync(Post item);
        Task<PostGroup> UpdateAsync(PostGroup item);
        Task DeleteAsync(Post item);
    }
}