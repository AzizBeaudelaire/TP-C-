using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    // Classe représentant un groupe de tâches avec des propriétés pour l'affichage
    public class PostGroup
    {
        [Display(Order = 2)]
        public List<Post> Posts { get; set; }  // Liste des tâches dans le groupe

        [Display(Order = 0)]
        public Priority Priority { get; set; } // Priorité du groupe

        [Display(Order = 1)]
        public List<string> Tags { get; set; } // Liste des étiquettes associées au groupe
    }
}