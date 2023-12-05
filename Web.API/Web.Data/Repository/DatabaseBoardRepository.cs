using Microsoft.EntityFrameworkCore;
using Web.Business.IRepository;
using Web.Business.Models;
using Web.Data.Context;
using DbContext = Web.Data.Context.DbContext;

namespace Web.Data.Repository
{
    public class DatabaseBoardRepository : IBoardRepository
    {
        private readonly DbContext _dbContext;

        public DatabaseBoardRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Post>> GetAllAsync()
        {
            return await _dbContext.Posts.ToListAsync();
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            var postGroup = await _dbContext.Posts
                .Include(todo => todo.Id) // Assurez-vous que les Posts sont inclus dans la requête
                .FirstOrDefaultAsync(t => t.Id == id);

            return postGroup;
        }


        public async Task<Post> AddAsync(Post item)
        {
            _dbContext.Posts.Add(item);
            await _dbContext.SaveChangesAsync();

            return item;
        }

        public async Task<PostGroup> UpdateAsync(PostGroup item)
        {
            _dbContext.Attach(item);
            await _dbContext.SaveChangesAsync();

            return item;
        }

        public async Task DeleteAsync(Post item)
        {
            _dbContext.Posts.Remove(item);
            await _dbContext.SaveChangesAsync();
        }
    }
}