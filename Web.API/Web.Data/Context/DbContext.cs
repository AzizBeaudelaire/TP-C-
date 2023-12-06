using Microsoft.EntityFrameworkCore;
using Web.Business.Models;
using WebApi.Models;
using Web.Business.Dto;
using Web.Business.IServices;
using System.Collections.Generic;

namespace Web.Data.Context
{
    public class TodoDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostGroup> PostGroups { get; set; }

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