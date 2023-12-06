using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Web.Business.Models;

namespace Web.Business.Models
{
    // Classe statique contenant les opérations sur la liste des tâches (listPosts)
    public static class ListPosts
    {
        public static List<Post> listPosts = new List<Post>();

        // Méthode pour ajouter une tâche à la liste
        public static void AddPost(Post post)
        {
            listPosts.Add(post);
        }

        // Méthode pour obtenir une tâche par son ID
        public static Post GetPostById(int taskId)
        {
            return listPosts.Find(task => task.Id == taskId);
        }

        // Méthode pour obtenir la description d'une tâche par son ID
        public static string GetPostDescription(int taskId)
        {
            Post post = GetPostById(taskId);
            return post != null ? post.Task : "Tâche non trouvée";
        }

        // Méthode pour mettre à jour une tâche par son ID
        public static void UpdatePost(int taskId, Post updatedPost)
        {
            Post postToUpdate = GetPostById(taskId);
            if (postToUpdate != null)
            {
                postToUpdate.Task = updatedPost.Task;
            }
        }

        // Méthode pour supprimer une tâche par son ID
        public static void DeletePost(int taskId)
        {
            Post postToRemove = GetPostById(taskId);
            if (postToRemove != null)
            {
                listPosts.Remove(postToRemove);
            }
        }
    }
}