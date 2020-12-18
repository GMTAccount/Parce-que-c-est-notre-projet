using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_de_fin_de_semestre___Guillaume_et_Paul
{
    public class IA
    {
        private Jeu jeu; // Jeu actuel
        private List<string> motsTrouves; // Liste de tous les mots trouvés par l'IA sur une grille
        private List<string> dictionnaire; // Liste de tous les mots dans le dictionnaire de la langue choisie
        private string[,] valeurSup; // Grille des valeurs supérieures
        private List<string> motsDejaDonnes; // Liste de tous les mots donnés par l'IA en un tour
        private int score;
        /// <summary>
        /// Constructeur d'un joueur de type IA
        /// </summary>
        /// <param name="dico">Tableau de dictionnaire</param>
        /// <param name="langue">Langue souhaitée</param>
        public IA(Jeu jeu, int langue)
        {
            this.dictionnaire = new List<string>();
            this.motsTrouves = new List<string>();
            this.motsDejaDonnes = new List<string>();
            this.score = 0;
            for (int i = 3; i < 16; i++)
            {
                foreach (string y in jeu.Mondico[langue].Mots[i])
                {
                    this.dictionnaire.Add(y);
                }
            }
            this.jeu = jeu;
            this.dictionnaire.Sort();
            this.valeurSup = jeu.Monplateau.ValeurSup;
        }
        /// <summary>
        /// Retour de tous les mots trouvées par l'IA qui ont été donnés
        /// </summary>
        public List<string> MotsDejaDonnes
        {
            get { return this.motsDejaDonnes; }
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
        /// Recherche de TOUS les mots du dictionnaire dans la grille actuelle
        /// </summary>
        /// <param name="plateauActuel">Plateau de jeu dans lequel l'IA doit trouver les mots</param>
        public void RechercheMots(Plateau plateauActuel)
        {
            for (int i = 0; i < this.dictionnaire.Count; i++)
            {
                if (plateauActuel.Test_Plateau(this.dictionnaire[i], 0))
                {
                    this.motsTrouves.Add(this.dictionnaire[i]);
                }
            }
        }
        /// <summary>
        /// Retour d'un mot trouvé de façon aléatoire (pour donner une chance aux joueurs en chair et en os)
        /// </summary>
        /// <param name="motsDejaDonnes">Liste des mots déjà données par l'IA durant la partie</param>
        /// <returns>Mot qu'elle a choisi de donner à ce tour</returns>
        public string DistributionMots()
        {
            string retour = "";
            if (this.motsTrouves != null && this.motsTrouves.Count != 0 && this.motsTrouves.Count > this.motsDejaDonnes.Count)
            {
                int posimot = 0;
                Random r = new Random();
                do
                {
                    posimot = r.Next(0, this.motsTrouves.Count);
                    retour = this.motsTrouves[posimot];
                }
                while (this.motsDejaDonnes.Contains(retour));
                this.motsDejaDonnes.Add(retour);
            }
            return retour;
        }
        /// <summary>
        /// Effaçage de tous les mots trouvés et donnés
        /// </summary>
        public void ClearAllLists()
        {
            this.motsDejaDonnes.RemoveRange(0, this.motsDejaDonnes.Count);
            this.motsTrouves.RemoveRange(0, this.motsTrouves.Count);
        }
        /// <summary>
        /// ToString d'une IA
        /// </summary>
        /// <returns>Chaîne de caractères comportant le nombre de points et les mots trouvés</returns>
        public string ToString()
        {
            string retour = "Notre superbe IA a actuellement " + this.score + " points, grâce à :";
            if (this.motsDejaDonnes != null && this.motsDejaDonnes.Count != 0)
            {
                foreach (string y in this.motsDejaDonnes)
                {
                    retour = retour + y + " ";
                }
            }
            return retour;
        }
    }
}
