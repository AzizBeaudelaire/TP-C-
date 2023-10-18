using System;

namespace GestionTaches
{
    class Program
    {
        static void Main(string[] args)
        {
            GestionnaireTaches gestionnaire = new GestionnaireTaches();

            while (true)
            {
                Console.WriteLine("Gestionnaire de Tâches Personnelles");
                Console.WriteLine("1. Afficher les Tâches");
                Console.WriteLine("2. Ajouter une Tâche");
                Console.WriteLine("3. Supprimer une Tâche");
                Console.WriteLine("4. Marquer une Tâche comme Terminée");
                Console.WriteLine("5. Mettre une priorité");
                Console.WriteLine("6. Quitter");

                Console.Write("Veuillez choisir une option : ");
                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        gestionnaire.AfficherTaches();
                        break;
                    case "2":
                        gestionnaire.AjouterTache();
                        break;
                    case "3":
                        gestionnaire.SupprimerTache();
                        break;
                    case "4":
                        gestionnaire.MarquerTacheTerminee();
                        break;
                    case "5":
                        gestionnaire.MettrePrioritee();
                        break;
                    case "6":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Option non valide. Veuillez réessayer.");
                        break;
                }
            }
        }
    }
}

