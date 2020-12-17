using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_de_fin_de_semestre___Guillaume_et_Paul
{
    public class Jeu
    {
        private Dictionnaire[] mondico;
        private Plateau monplateau;

        /// <summary>
        /// Constructeur d'un jeu
        /// </summary>
        /// <param name="fichierDico">Noms des fichiers de dictionnaire (dans la langue souhaitée)</param>
        /// <param name="nomLangues">Noms des langues correspondant aux dictionnaires</param>
        /// <param name="fichierDes">Nom du fichier avec les dés</param>
        public Jeu(string[] fichierDico, string[] nomLangues, string fichierDes)
        {
            this.mondico = new Dictionnaire[fichierDico.Length];
            if (fichierDico != null && fichierDico.Length != 0 && nomLangues != null && nomLangues.Length != 0 && fichierDico.Length == nomLangues.Length)
            {
                for (int i = 0; i < fichierDico.Length; i++)
                {
                    this.mondico[i] = new Dictionnaire(fichierDico[i], nomLangues[i]);

                }
            }
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
        /// Retour du dictionnaire (pour l'IA)
        /// </summary>
        public Dictionnaire[] Mondico
        {
            get { return this.mondico; }
        }
        /// <summary>
        /// Vérification d'un mot
        /// - Appartenance au dictionnaire
        /// - Test de voisinage
        /// Ici, on ne fait qu'appeler les méthodes correspondantes, et afficher une erreur dans le cas où un des tests échouerai
        /// </summary>
        /// <param name="mot">Mot à analyser</param>
        /// <param name="choixLangue">Langue choisie pour le dictionnaire</param>
        /// <returns>Booléen : true = mot valide, false = mot invalide (non existant ou contrainte d'adjacence)</returns>
        public bool Verification(string mot, int choixLangue)
        {
            if (mot.Length > 15) return false;
            return (this.mondico[choixLangue].RechercheDichoRecursif(0, this.mondico[choixLangue].Mots[mot.Length].Length - 1, mot) && this.monplateau.Test_Plateau(mot, 0));
        }
    }
}
