using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApi.Business;
using WebApi.Models;

namespace WebApi.Data
{
    public class DatabaseBoardRepository : IBoardRepository
    {
        private readonly BoardDbContext _boardDbContext;

        public DatabaseBoardRepository(BoardDbContext boardDbContext)
        {
            _boardDbContext = boardDbContext;
        }

        public async Task<List<PostGroup>> GetAllAsync()
        {
            return await _boardDbContext.Todos.ToListAsync();
        }

        public async Task<PostGroup?> GetByIdAsync(int id)
        {
            var postGroup = await _boardDbContext.Todos
                .Include(todo => todo.Posts) // Assurez-vous que les Posts sont inclus dans la requête
                .FirstOrDefaultAsync(todo => todo.Posts.Any(post => post.Id == id));

            return postGroup;
        }


        public async Task<PostGroup> AddAsync(PostGroup item)
        {
            _boardDbContext.Todos.Add(item);
            await _boardDbContext.SaveChangesAsync();

            return item;
        }

        public async Task<PostGroup> UpdateAsync(PostGroup item)
        {
            _boardDbContext.Attach(item);
            await _boardDbContext.SaveChangesAsync();

            return item;
        }

        public async Task DeleteAsync(PostGroup item)
        {
            _boardDbContext.Todos.Remove(item);
            await _boardDbContext.SaveChangesAsync();
        }
    }
}