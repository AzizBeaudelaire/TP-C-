using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Web.Business.IRepository;
using Web.Business.Models;
using Web.Data.Context; // Ajoutez cet using pour Task
using WebApi.Models;
using WebApi.Data;
using DbContext = Web.Data.Context.DbContext;

public class DatabaseTodoRepository : ITodoRepository
{
    private readonly DbContext _dbContext;

    public DatabaseTodoRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Post>> GetAllAsync()
    {
        return await _dbContext.Posts.ToListAsync();
    }

    public async Task<Post> GetByIdAsync(long id)
    {
        return await _dbContext.Posts.SingleOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Post> AddAsync(Post item)
    {
        _dbContext.Posts.Add(item);
        await _dbContext.SaveChangesAsync();

        return item;
    }

    public async Task<Post> UpdateAsync(Post item)
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