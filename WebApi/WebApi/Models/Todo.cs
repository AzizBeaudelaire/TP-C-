using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    // Énumération pour définir les priorités possibles d'une tâche
    public enum Priority
    {
        Low,     // Basse priorité
        Medium,  // Priorité moyenne
        High     // Haute priorité
    }

    // Classe représentant une tâche (post)
    public class Post
    {
        public int Id { get; set; }            // Identifiant unique de la tâche
        public string Task { get; set; }       // Contenu de la tâche
        public bool IsDone { get; set; }       // Indique si la tâche est terminée
        public Priority Priority { get; set; } // Priorité de la tâche

        public Post(string task, bool isDone, Priority priority)
        {
            Task = task;
            Id = ListPosts.listPosts.Count + 1; // Attribution d'un nouvel identifiant unique
            IsDone = isDone;
            Priority = priority;
        }
    }

    // Classe représentant une étiquette (tag)
    public class Tag
    {
        public int Id { get; set; }    // Identifiant unique de l'étiquette
        public string Name { get; set; } // Nom de l'étiquette

        public Tag(string name)
        {
            Name = name;
        }
    }

    // Classe représentant un groupe de tâches avec des propriétés pour l'affichage
    public class PostGroup
    {
        [Display(Order = 2)]
        public List<Post> Posts { get; set; }  // Liste des tâches dans le groupe

        [Display(Order = 0)]
        public Priority Priority { get; set; } // Priorité du groupe

        [Display(Order = 1)]
        public List<string> Tags { get; set; } // Liste des étiquettes associées au groupe
    }

    // Classe représentant le tableau (board) de tâches
    public class Board
    {
        public List<PostGroup> Todos { get; set; } // Liste des groupes de tâches
    }

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
};