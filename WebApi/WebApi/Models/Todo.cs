using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class PostGroup
    {
        public string Tag { get; set; }
        public List<Post> Posts { get; set; }
    }

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
        public List<string> Tags { get; set; }

        public Post(string task, bool isDone, Priority priority, List<string> tags)
        {
            Task = task;
            Id = ListPosts.listPosts.Count + 1;
            IsDone = isDone;
            Priority = priority;
            Tags = tags;
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
