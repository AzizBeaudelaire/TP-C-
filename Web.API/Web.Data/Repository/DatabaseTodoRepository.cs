using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Web.Business.IRepository;
using Web.Business.Models;
using Web.Data.Context;
using WebApi.Models;
using WebApi.Data;
using DbContext = Web.Data.Context.TodoDbContext;

public class DatabaseTodoRepository : ITodoRepository
{
    private readonly DbContext _dbContext;

    public DatabaseTodoRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Post>> GetAllAsync()
    {
        try
        {
            return await _dbContext.Posts.ToListAsync();
        }
        catch (Exception ex)
        {
            // Gérer l'erreur (journalisation, notification, etc.)
            // Vous pouvez également renvoyer une liste vide ou null, ou relancer l'exception selon vos besoins.
            throw new Exception("Une erreur s'est produite lors de la récupération de toutes les tâches.", ex);
        }
    }

    public async Task<Post> GetByIdAsync(long id)
    {
        try
        {
            return await _dbContext.Posts.SingleOrDefaultAsync(t => t.Id == id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Une erreur s'est produite lors de la récupération de la tâche avec l'ID {id}.", ex);
        }
    }

    public async Task<Post> AddAsync(Post item)
    {
        try
        {
            _dbContext.Posts.Add(item);
            await _dbContext.SaveChangesAsync();

            return item;
        }
        catch (Exception ex)
        {
            throw new Exception("Une erreur s'est produite lors de l'ajout de la tâche.", ex);
        }
    }

    public async Task<Post> UpdateAsync(Post item)
    {
        try
        {
            _dbContext.Attach(item);
            await _dbContext.SaveChangesAsync();

            return item;
        }
        catch (Exception ex)
        {
            throw new Exception("Une erreur s'est produite lors de la mise à jour de la tâche.", ex);
        }
    }

    public async Task DeleteAsync(Post item)
    {
        try
        {
            _dbContext.Posts.Remove(item);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Une erreur s'est produite lors de la suppression de la tâche.", ex);
        }
    }
}
