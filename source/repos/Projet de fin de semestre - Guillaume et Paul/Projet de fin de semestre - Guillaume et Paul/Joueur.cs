using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_de_fin_de_semestre___Guillaume_et_Paul
{
    public class Joueur
    {
        //Champs
        string nom; // Nom du joueur
        int score; // Score du joueur
        List<string> motsTrouves; // Liste des mots trouvés durant un seul tour par un joueur


        //Constructeur
        /// <summary>
        /// Constructeur d'un joueur
        /// </summary>
        /// <param name="nom">Nom du joueur</param>
        public Joueur(string nom)
        {
            this.score = 0;
            this.motsTrouves = new List<string>();
            this.nom = nom;
        }


        //Propriétés
        /// <summary>
        /// Retour du nom de joueur (à des fins d'affichage)
        /// </summary>
        public string Nom
        {
            get { return this.nom; }
        }
        /// <summary>
        /// Retour et modification du score (affichage et prise en compte des mots corrects)
        /// </summary>
        public int Score
        {
            get { return this.score; }
            set { this.score = this.score + value; }
        }
        /// <summary>
        /// Retour de la liste des mots trouvés (pour éviter les doublons)
        /// </summary>
        public List<string> MotTrouve
        {
            get { return this.motsTrouves; }
        }


        //Méthodes
        /// <summary>
        /// Vérification d'un mot, pour savoir s'il a été déjà donné par le passé
        /// </summary>
        /// <param name="mot">Mot à rechercher</param>
        /// <returns>Booléen : true = ce mot a déjà été donné, false sinon</returns>
        public bool Contain(string mot)
        {
            mot = mot.ToUpper(); // Mise du mot en majuscule
            bool test = false;
            if (this.motsTrouves != null && this.motsTrouves.Count != 0) // Cette méthode ne fonctionne que si au moins un mot a été donné
            {
                for (int i = 0; (i < this.motsTrouves.Count) && !test; i++) // Parcours de toute la liste à la recherche d'un mot identique à celui saisi
                {
                    test = (this.motsTrouves[i] == mot);
                }
            }
            return test;
        }


        /// <summary>
        /// Ajout d'un mot dans la liste des mots trouvés par un joueur
        /// </summary>
        /// <param name="mot">Mot à ajouter</param>
        public void Add_Mot(string mot)
        {
            mot = mot.ToUpper(); // Mise en majuscule du mot
            if (!Contain(mot) && this.motsTrouves != null && mot.Length > 2)
            {
                this.motsTrouves.Add(mot);
            }
        }
        /// <summary>
        /// ToString d'un joueur (pour l'affichage)
        /// </summary>
        /// <returns>Chaîne de caractères avec le nom du joueur, son score et les mots qu'il a trouvé</returns>
        public string toString()
        {
            string text = ("Le score de " + this.nom + " est de " + this.score + ", grâce aux mots cités suivent :\n");
            if (this.motsTrouves != null && this.motsTrouves.Count != 0)
            {
                foreach (string y in this.motsTrouves)
                {
                    text = text + y + " ";
                }
            }
            return text;
        }
        /// <summary>
        /// Suppression de tous les mots trouvés par un joueur au cours d'une partie
        /// (Utile pour le changement de tours)
        /// </summary>
        public void ClearAllList()
        {
            if (this.motsTrouves.Count != 0 && this.motsTrouves != null)
            {
                this.motsTrouves.RemoveRange(0, this.motsTrouves.Count);
            }
        }
    }
}
