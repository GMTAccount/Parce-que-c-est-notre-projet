using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_de_fin_de_semestre___Guillaume_et_Paul
{
    public class De
    {
        // Champs
        private string[] des;


        //Constructeur
        /// <summary>
        /// Constructeur d'un dé
        /// </summary>
        /// <param name="ligneDe">Ligne caractérisant un dé</param>
        /// <param name="nbFaces">Nombre de faces du dé</param>
        public De(string ligneDe, int nbFaces = 6)
        {
            bool testFace = true;
            if (ligneDe != null)
            {
                this.des = ligneDe.Split(";");
                if (this.des.Length != nbFaces)
                {
                    testFace = false;
                    Console.WriteLine("Erreur : le nombre de faces est différent de " + nbFaces);
                }
            }
        }


        //Propriété
        /// <summary>
        /// Retour du tableau de chaînes de caractères correspondant à un dé
        /// </summary>
        public string[] Des
        {
            get { return this.des; }
        }


        //Méthodes 
        /// <summary>
        /// Lancer d'un dé
        /// </summary>
        /// <param name="r">Générateur aléatoire</param>
        /// <returns>Valeur du dé sur la face correspondante (choisie aléatoirement)</returns>
        public string Lance(Random r)
        {
            int position = r.Next(0, this.des.Length); // Génération d'un entier aléatoire (intervalle de valeurs possibles borné)
            return this.des[position];
        }
        /// <summary>
        /// ToString d'un dé
        /// </summary>
        /// <returns>Chaîne de caractères comportant les caractéristiques d'un dé (toutes les valeurs possibles)</returns>
        public string toString()
        {
            string retour = "";
            foreach (string element in this.des)
            {
                retour = retour + element + " ";
            }
            return retour;
        }
    }
}
