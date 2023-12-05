using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Web.Data.Context;
using Web.Business.Dto;
using Web.Business.Models;
using WebApi.Models;

namespace Web.Business.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoDbContext _todoDbContext;

        public TodoService(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public IEnumerable<PostGroup> GetTodoBoard()
        {
            return _todoDbContext.Posts.ToList();
        }

        public PostGroup GetTodoById(int id)
        {
            return _todoDbContext.Posts.SingleOrDefault(t => t.Id == id);
        }

        public PostGroup AddTodo(PostGroup model)
        {
            _todoDbContext.Posts.Add(model);
            _todoDbContext.SaveChanges();
            return model;
        }

        public string UpdateTask(int taskId, PostGroup updatedPost)
        {
            PostGroup taskToUpdate = _todoDbContext.Post.SingleOrDefault(t => t.Id == taskId);

            if (taskToUpdate is null)
            {
                return $"Tâche avec l'ID {taskId} introuvable.";
            }

            taskToUpdate.Posts = updatedPost.Posts;
            taskToUpdate.IsDone = updatedPost.IsDone;
            taskToUpdate.TaskPriority = updatedPost.TaskPriority;

            _todoDbContext.SaveChanges();

            return $"Tâche avec l'ID {taskId} a été mise à jour.";
        }

        public string DeleteTask(int taskId)
        {
            PostGroup taskToRemove = _todoDbContext.Posts.SingleOrDefault(t => t.Id == taskId);

            if (taskToRemove is null)
            {
                return $"Tâche avec l'ID {taskId} introuvable.";
            }

            _todoDbContext.Posts.Remove(taskToRemove);
            _todoDbContext.SaveChanges();

            return $"Tâche avec l'ID {taskId} a été supprimée.";
        }
    }
}