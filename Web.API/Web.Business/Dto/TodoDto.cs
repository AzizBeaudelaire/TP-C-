namespace Web.Business.Dto;
{
    public class TodoDto
    {
        public int Id { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public List<string> Tags { get; set; }
        public string PostGroupName { get; set; }
        public string PriorityName { get; set; }
    }
}
