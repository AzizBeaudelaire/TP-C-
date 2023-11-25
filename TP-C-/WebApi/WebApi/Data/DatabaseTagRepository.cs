using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApi.Business;
using WebApi.Models;

namespace WebApi.Data
{
    public class DatabaseTagRepository : ITagRepository
    {
        private readonly TagDbContext _tagDbContext;

        public DatabaseTagRepository(TagDbContext tagDbContext)
        {
            _tagDbContext = tagDbContext;
        }

        public async Task<List<Tag>> GetAllAsync()
        {
            return await _tagDbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetByIdAsync(int id)
        {
            return await _tagDbContext.Tags.SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tag> AddAsync(Tag item)
        {
            _tagDbContext.Tags.Add(item);
            await _tagDbContext.SaveChangesAsync();

            return item;
        }

        public async Task<Tag> UpdateAsync(Tag item)
        {
            _tagDbContext.Attach(item);
            await _tagDbContext.SaveChangesAsync();

            return item;
        }

        public async Task DeleteAsync(Tag item)
        {
            _tagDbContext.Tags.Remove(item);
            await _tagDbContext.SaveChangesAsync();
        }
    }
}