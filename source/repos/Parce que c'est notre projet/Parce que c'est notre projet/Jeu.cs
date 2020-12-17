using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Parce_que_c_est_notre_projet
{
    class Jeu
    {
        private Dictionnaire[] mondico;
        private Plateau monplateau;

        /// <summary>
        /// Constructeur d'un jeu
        /// </summary>
        /// <param name="fichierDico">Noms des fichiers de dictionnaire (dans la langue souhaitée)</param>
        /// <param name="fichierDes">Nom du fichier avec les dés</param>
        public Jeu(string[] fichierDico, string fichierDes)
        {
            this.mondico = new Dictionnaire[fichierDico.Length];
            /*for(int i = 0; i < fichierDico.Length; i++)
            {
                Console.WriteLine("Veuillez donner un nom à votre langue : ");
                this.mondico[i] = new Dictionnaire(fichierDico[i], Console.ReadLine());

            }*/
            this.mondico[0] = new Dictionnaire(fichierDico[0], "FR");
            this.monplateau = new Plateau(fichierDes);
        }
        /// <summary>
        /// Retour du plateau assicié au jeu en cours (pour la méthode Test_Plateau et l'affichage, principalement)
        /// </summary>
        public Plateau Monplateau
        {
            get { return this.monplateau; }
        }
        /// <summary>
        /// Vérification d'un mot
        /// - Appartenance au dictionnaire
        /// - Test de voisinage
        /// Ici, on ne fait qu'appeler les méthodes correspondantes, et afficher une erreur dans le cas où un des tests échouerai
        /// </summary>
        /// <param name="mot">Mot à analyser</param>
        /// <returns>Booléen : true = mot valide, false = mot invalide (non existant ou contrainte d'adjacence)</returns>
        public bool Verification(string mot)
        {
            if (mot.Length > 15) return false;
            return (this.mondico[0].RechercheDichoRecursif(0, this.mondico[0].Mots[mot.Length].Length - 1, mot) && this.monplateau.Test_Plateau(mot, 0));
        }
    }
}
