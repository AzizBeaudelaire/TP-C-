using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;  // Ajoutez cet using pour Task
using WebApi.Business;
using WebApi.Models;
using WebApi.Data;

public class DatabaseTodoRepository : ITodoRepository
{
    private readonly TodoDbContext _todoDbContext;

    public DatabaseTodoRepository(TodoDbContext todoDbContext)
    {
        _todoDbContext = todoDbContext;
    }

    public async Task<List<Post>> GetAllAsync()
    {
        return await _todoDbContext.Posts.ToListAsync();
    }

    public async Task<Post> GetByIdAsync(long id)
    {
        return await _todoDbContext.Posts.SingleOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Post> AddAsync(Post item)
    {
        _todoDbContext.Posts.Add(item);
        await _todoDbContext.SaveChangesAsync();

        return item;
    }

    public async Task<Post> UpdateAsync(Post item)
    {
        _todoDbContext.Attach(item);
        await _todoDbContext.SaveChangesAsync();

        return item;
    }

    public async Task DeleteAsync(Post item)
    {
        _todoDbContext.Posts.Remove(item);
        await _todoDbContext.SaveChangesAsync();
    }
}