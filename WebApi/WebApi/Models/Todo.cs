using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public static class ListPosts
    {
        public static List<Post> listPosts = new(); // Crée une liste statique de tâches pour stocker les données.

        public static void AddPost(Post post)
        {
            listPosts.Add(post); // Ajoute une tâche à la liste.
        }

        public static Post GetPostById(int taskId)
        {
            return listPosts.Find(task => task.Id == taskId); // Recherche une tâche par son ID.
        }


        public static string GetPostDescription(int taskId)
        {
            Post post = GetPostById(taskId); // Obtient la tâche par ID.
            return post != null ? post.Task : "Post not found"; // Renvoie la description de la tâche ou un message si la tâche n'est pas trouvée.
        }

        public static void UpdatePost(int taskId, Post updatedPost)
        {
            Post postToUpdate = GetPostById(taskId); // Obtient la tâche à mettre à jour par ID.
            if (postToUpdate != null)
            {
                postToUpdate.Task = updatedPost.Task; // Met à jour le contenu de la tâche.
            }
        }

        public static void DeletePost(int taskId)
        {
            Post postToRemove = GetPostById(taskId); // Obtient la tâche à supprimer par ID.
            if (postToRemove != null)
            {
                listPosts.Remove(postToRemove); // Supprime la tâche de la liste.
            }
        }
    }

    public class Post
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public bool IsDone { get; set; }
        public Priority Priority { get; set; }

        public Post(string task, bool isDone, Priority priority)
        {
            Task = task;
            Id = ListPosts.listPosts.Count + 1;
            IsDone = isDone;
            Priority = priority;
        }
    }
    public enum Priority
    {
        Low,
        Medium,
        High
    }
}