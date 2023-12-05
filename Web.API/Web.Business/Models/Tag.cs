using WebApi.Models;

namespace Web.Business.Models
{
    // Classe représentant une étiquette (tag)
    public class Tag
    {
        public int Id { get; set; }    // Identifiant unique de l'étiquette
        public string Name { get; set; } // Nom de l'étiquette
        public int PostId { get; set; }
        public Post Post { get; set; }

        public Tag(string name)
        {
            Name = name;
        }
    }
}