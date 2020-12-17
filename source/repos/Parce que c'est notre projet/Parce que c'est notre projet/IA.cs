using System;
using System.Collections.Generic;
using System.Text;

namespace Parce_que_c_est_notre_projet
{
    class IA
    {
        private Jeu jeu;
        private List<string> motsTrouves;
        private List<string> dictionnaire;
        private string[,] valeurSup;
        /// <summary>
        /// Constructeur d'un joueur de type IA
        /// </summary>
        /// <param name="dico">Tableau de dictionnaire</param>
        /// <param name="langue">Langue souhaitée</param>
        public IA(Dictionnaire[] dico, int langue)
        {
            this.dictionnaire = new List<string>();
            this.motsTrouves = new List<string>();
            for (int i = 3; i < 16; i++)
            {
                foreach (string y in dico[langue].Mots[i])
                {
                    this.dictionnaire.Add(y);
                }
            }
            string[] nom = { "MotsPossibles.txt" };
            this.jeu = new Jeu(nom, "Des.txt");
            this.dictionnaire.Sort();
            jeu.Monplateau.MelangeValeurs();
            this.valeurSup = jeu.Monplateau.ValeurSup;
        }
        /// <summary>
        /// Recherche de TOUS les mots du dictionnaire dans la grille actuelle
        /// </summary>
        public void RechercheMots()
        {
            Console.WriteLine(this.dictionnaire.Count);
            Console.WriteLine(this.jeu.Monplateau.ToString());
            Console.WriteLine();
            for (int i = 0; i < this.dictionnaire.Count; i++)
            {
                if(this.jeu.Monplateau.Test_Plateau(this.dictionnaire[i], 0))
                {
                    this.motsTrouves.Add(this.dictionnaire[i]);
                    Console.WriteLine(this.dictionnaire[i] + ", numéro " + this.motsTrouves.Count);
                }
            }
        }
        /// <summary>
        /// Retour d'un mot trouvé de façon aléatoire (pour donner une chance aux joueurs en chair et en os)
        /// </summary>
        /// <param name="motsDejaDonnes">Liste des mots déjà données par l'IA durant la partie</param>
        /// <returns>Mot qu'elle a choisi de donner à ce tour</returns>
        public string DistributionMots(List<string> motsDejaDonnes)
        {
            string retour = "";
            if(this.motsTrouves != null && this.motsTrouves.Count != 0 && this.motsTrouves.Count > motsDejaDonnes.Count)
            {
                int posimot = 0;
                Random r = new Random();
                do
                {
                    posimot = r.Next(0, this.motsTrouves.Count);
                    retour = this.motsTrouves[posimot];
                }
                while (motsDejaDonnes.Contains(retour));
            }
            return retour;
        }
    }
}
