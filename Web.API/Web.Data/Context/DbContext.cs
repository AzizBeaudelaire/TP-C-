using Microsoft.EntityFrameworkCore;
using Web.Business.Models;
using WebApi.Models;

namespace Web.Data.Context
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext(DbContextOptions<DbContext> options) : base(options)
        {
        }

        public DbSet<Post>? Posts { get; set; }
        public DbSet<Tag>? Tags { get; set; }
        public DbSet<PostGroup>? PostGroups { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Post>()
                .HasOne(p => p.TaskPriority)
                .WithMany()
                .HasForeignKey(p => p.PriorityId);*/

            modelBuilder.Entity<Tag>()
                .HasOne(t => t.Post)
                .WithMany(p => p.Tags)
                .HasForeignKey(t => t.PostId);

            base.OnModelCreating(modelBuilder);
        }
    }
}