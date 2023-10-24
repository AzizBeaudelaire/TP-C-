using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public enum Priority
    {
        Low,
        Medium,
        High
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

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Tag(string name)
        {
            Name = name;
        }
    }

    public class PostGroup
    {
        [Display(Order = 2)]
        public List<Post> Posts { get; set; }

        [Display(Order = 0)]
        public Priority Priority { get; set; }

        [Display(Order = 1)]
        public List<string> Tags { get; set; }
    }

    public class Board
    {
        public List<PostGroup> Todos { get; set; }
    }

    public static class ListPosts
    {
        public static List<Post> listPosts = new List<Post>();

        public static void AddPost(Post post)
        {
            listPosts.Add(post);
        }

        public static Post GetPostById(int taskId)
        {
            return listPosts.Find(task => task.Id == taskId);
        }

        public static string GetPostDescription(int taskId)
        {
            Post post = GetPostById(taskId);
            return post != null ? post.Task : "Post not found";
        }

        public static void UpdatePost(int taskId, Post updatedPost)
        {
            Post postToUpdate = GetPostById(taskId);
            if (postToUpdate != null)
            {
                postToUpdate.Task = updatedPost.Task;
            }
        }

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
