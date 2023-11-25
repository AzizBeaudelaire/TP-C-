using System.Collections.Generic;

namespace WebApi.Models
{
    // Classe représentant le tableau (board) de tâches
    public class Board
    {
        public List<PostGroup> Todos { get; set; } // Liste des groupes de tâches
    }
}