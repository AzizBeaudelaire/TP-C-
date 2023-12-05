using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Web.Business.IRepository;
using Web.Business.Models;
using Web.Data.Context;
using WebApi.Models;
using DbContext = Web.Data.Context.DbContext;

namespace WebApi.Data
{
    public class DatabaseTagRepository : ITagRepository
    {
        private readonly DbContext _dbContext;

        public DatabaseTagRepository(DbContext tagDbContext)
        {
            _dbContext = tagDbContext;
        }

        public async Task<List<Tag>> GetAllAsync()
        {
            return await _dbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetByIdAsync(int id)
        {
            return await _dbContext.Tags.SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tag> AddAsync(Tag item)
        {
            _dbContext.Tags.Add(item);
            await _dbContext.SaveChangesAsync();

            return item;
        }

        public async Task<Tag> UpdateAsync(Tag item)
        {
            _dbContext.Attach(item);
            await _dbContext.SaveChangesAsync();

            return item;
        }

        public async Task DeleteAsync(Tag item)
        {
            _dbContext.Tags.Remove(item);
            await _dbContext.SaveChangesAsync();
        }
    }
}