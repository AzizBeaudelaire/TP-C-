using System;
using System.Collections.Generic;
using System.Linq;
using Web.Business.Models;
using Web.Data.Context;
using Web.Business.IServices;

namespace WebApi.Services
{
    public class PriorityService : IPriorityService
    {
        private readonly TodoDbContext _context;

        public PriorityService(TodoDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<Priority> GetPriorities()
        {
            try
            {
                return _context.Priorities.ToList();
            }
            catch (Exception ex)
            {
                // Gérer l'erreur (journalisation, notification, etc.)
                // Vous pouvez également renvoyer une liste vide ou null, ou relancer l'exception selon vos besoins.
                throw new Exception("Une erreur s'est produite lors de la récupération des priorités.", ex);
            }
        }

        public Priority GetPriorityById(int id)
        {
            try
            {
                return _context.Priorities.SingleOrDefault(p => p.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Une erreur s'est produite lors de la récupération de la priorité avec l'ID {id}.", ex);
            }
        }

        public void AddPriority(Priority priority)
        {
            try
            {
                _context.Priorities.Add(priority);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Une erreur s'est produite lors de l'ajout de la priorité.", ex);
            }
        }

        public void UpdatePriority(int id, Priority updatedPriority)
        {
            try
            {
                var existingPriority = _context.Priorities.SingleOrDefault(p => p.Id == id);

                if (existingPriority != null)
                {
                    existingPriority.Name = updatedPriority.Name;
                    existingPriority.Level = updatedPriority.Level;

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Une erreur s'est produite lors de la mise à jour de la priorité avec l'ID {id}.", ex);
            }
        }

        public void DeletePriority(int id)
        {
            try
            {
                var priorityToDelete = _context.Priorities.SingleOrDefault(p => p.Id == id);

                if (priorityToDelete != null)
                {
                    _context.Priorities.Remove(priorityToDelete);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Une erreur s'est produite lors de la suppression de la priorité avec l'ID {id}.", ex);
            }
        }
    }
}
