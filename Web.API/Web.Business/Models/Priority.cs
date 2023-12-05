namespace Web.Business.Models
{
    // Énumération pour définir les priorités possibles d'une tâche
    public enum PriorityLevel
    {
        Low,     // Basse priorité
        Medium,  // Priorité moyenne
        High     // Haute priorité
    }

    // Classe représentant une priorité
    public class Priority
    {
        public PriorityLevel Level { get; set; }

        public Priority(PriorityLevel level)
        {
            Level = level;
        }
    }
}