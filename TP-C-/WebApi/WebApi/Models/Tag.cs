namespace WebApi.Models
{
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
}