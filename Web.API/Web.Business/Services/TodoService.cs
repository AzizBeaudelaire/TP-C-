using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Web.Data.Context;
using Web.Business.Dto;
using Web.Business.Models;
using WebApi.Models;
using Web.Business.IServices;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                return _todoDbContext.Posts.ToList();
            }
            catch (Exception ex)
            {
                // Gérer l'erreur (journalisation, notification, etc.)
                // Vous pouvez également renvoyer une liste vide ou null, ou relancer l'exception selon vos besoins.
                throw new Exception("Une erreur s'est produite lors de la récupération du tableau de tâches.", ex);
            }
        }

        public PostGroup GetTodoById(int id)
        {
            try
            {
                return _todoDbContext.Posts.SingleOrDefault(t => t.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Une erreur s'est produite lors de la récupération de la tâche avec l'ID {id}.", ex);
            }
        }

        public PostGroup AddTodo(PostGroup model)
        {
            try
            {
                _todoDbContext.Posts.Add(model);
                _todoDbContext.SaveChanges();
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception("Une erreur s'est produite lors de l'ajout de la tâche.", ex);
            }
        }

        public string UpdateTask(int taskId, PostGroup updatedPost)
        {
            try
            {
                PostGroup taskToUpdate = _todoDbContext.Posts.SingleOrDefault(t => t.Id == taskId);

                if (taskToUpdate is null)
                {
                    return $"Tâche avec l'ID {taskId} introuvable.";
                }

                taskToUpdate.Posts = updatedPost.Posts;
                taskToUpdate.Posts = updatedPost.Posts;
                taskToUpdate.Priority = updatedPost.Priority;

                _todoDbContext.SaveChanges();

                return $"Tâche avec l'ID {taskId} a été mise à jour.";
            }
            catch (Exception ex)
            {
                return $"Une erreur s'est produite lors de la mise à jour de la tâche avec l'ID {taskId}.";
            }
        }

        public string DeleteTask(int taskId)
        {
            try
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
            catch (Exception ex)
            {
                return $"Une erreur s'est produite lors de la suppression de la tâche avec l'ID {taskId}.";
            }
        }
    }
}
