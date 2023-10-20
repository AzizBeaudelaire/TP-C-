using Microsoft.AspNetCore.Server.Kestrel.Transport.Quic;

namespace WebApi.Models
{
    public static class ListPosts {
        public static List<Post> listPosts = new();
    }
    
    public class Post
    {
        public static int TaskId { get; set; }
        
        public int Id { get; set; }
        public string Task { get; set; }
        public Post(string post)
        {
            Task = post;
            TaskId = TaskId + 1;
            Id = TaskId;
        }
    }
}
