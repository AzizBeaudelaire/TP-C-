namespace WebApi.Models
{
    public static class ListPosts
    {
        public static List<Post> listPosts = new();
    }

    public class Post
    {
        public int TaskId { get; set; } // Changé pour être une propriété d'instance

        public int Id { get; set; }
        public string Task { get; set; }

        public Post(string post)
        {
            Task = post;
            TaskId = ListPosts.listPosts.Count + 1; // Incrémenté en fonction du nombre d'éléments dans la liste
            Id = TaskId;
        }
    }
}
