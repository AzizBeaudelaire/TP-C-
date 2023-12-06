using Microsoft.EntityFrameworkCore;
using Web.Business.IRepository;
using Web.Business.Models;
using Web.Data.Context;
using DbContext = Web.Data.Context.TodoDbContext;

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

        public async Task<Post?> GetByIdAsync(int id)
        {
            try
            {
                var postGroup = await _dbContext.Posts
                    .Include(todo => todo.Id) // Assurez-vous que les Posts sont inclus dans la requête
                    .FirstOrDefaultAsync(t => t.Id == id);

                return postGroup;
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

        public async Task<PostGroup> UpdateAsync(PostGroup item)
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
}
