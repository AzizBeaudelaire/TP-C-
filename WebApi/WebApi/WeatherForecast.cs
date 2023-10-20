using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public static class ListPosts
    {
        public static List<Post> listPosts = new();

        public static void AddPost(Post post)
        {
            listPosts.Add(post);
        }

        public static Post GetPostById(int taskId)
        {
            return listPosts.Find(task => task.TaskId == taskId);
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

    public class Post
    {
        public int TaskId { get; set; }
        public int Id { get; set; }
        public string Task { get; set; }

        public Post(string post)
        {
            Task = post;
            TaskId = ListPosts.listPosts.Count + 1;
            Id = TaskId;
        }
    }
}