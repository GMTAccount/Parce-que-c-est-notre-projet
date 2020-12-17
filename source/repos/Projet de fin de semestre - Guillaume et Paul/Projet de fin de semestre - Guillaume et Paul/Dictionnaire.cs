using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Projet_de_fin_de_semestre___Guillaume_et_Paul
{
    public class Dictionnaire
    {
        //Champs
        private string langue;
        private SortedList<int, string[]> mots = new SortedList<int, string[]>();


        //Constructeur
        /// <summary>
        /// Constructeur d'un Dictionnaire
        /// </summary>
        /// <param name="filename">Nom du fichier contenant le dictionnaire</param>
        /// <param name="langue">Nom de la langue</param>
        public Dictionnaire(string filename, string langue)
        {
            this.langue = langue;
            bool testFace = true;
            bool testNbDe = true;
            StreamReader fichier = new StreamReader(filename);
            try
            {
                string[] ligne = null;
                do
                {
                    int cle = Convert.ToInt32(fichier.ReadLine());
                    if (cle != 0)
                    {
                        ligne = fichier.ReadLine().Split(" ");
                        this.mots.Add(cle, ligne);
                    }
                    else
                    {
                        ligne = null;
                    }
                }
                while (ligne != null);
            }
            catch (FileNotFoundException erreur)
            {
                Console.WriteLine("Le fichier correspondant n'a pas pu être trouvé");
                Console.WriteLine(erreur.Message);
            }
            catch (Exception erreur)
            {
                Console.WriteLine(erreur.Message);
            }
            finally
            {
                if (fichier != null)
                {
                    fichier.Close();
                }
            }
        }


        //Propriétés
        /// <summary>
        /// Retour du nom de langue
        /// </summary>
        public string Langue
        {
            get { return this.langue; }
        }
        /// <summary>
        /// Retour de la liste des mots du dictionnaire
        /// </summary>

        public SortedList<int, string[]> Mots
        {
            get { return this.mots; }
        }

        //Méthodes
        /// <summary>
        /// Recherche d'un mot dans le dictionnaire.
        /// Cette méthode se base sur une recherche dichotomique, et est récursive.
        /// </summary>
        /// <param name="debut">Rang de début de recherche actuelle</param>
        /// <param name="fin">Rang de fin de recherche actuelle</param>
        /// <param name="mot">Mot à analyser</param>
        /// <returns>Booléen : true = mot valide (dans le dictionnaire), false sinon</returns>
        public bool RechercheDichoRecursif(int debut, int fin, string mot)
        {
            int longueur = mot.Length;
            if (mot != null && mot.Length != 0) // Le mot doit comporter au moins 1 caractère pour être recherché
            {
                string[] tableau = this.mots[longueur]; // On prend en tableau à analyser tous les mots de la longueur correspondant au mot
                // Si entre le début et la fin, il n'y a qu'un mot
                if (fin - debut == 2 && tableau[debut + 1] == mot) return true; // Ce mot est égal, on retourne true
                if (fin - debut <= 2 && tableau[debut + 1] != mot) return false; // Ce mot est différent, on retourne false
                if (tableau[debut] == mot) return true; // Si le mot à l'index debut est égal, on retourne true
                if (tableau[fin] == mot) return true; // Si le mot à l'index fin est égal, on retourne true
                switch (Comparaison(tableau[(debut + fin) / 2], mot)) // On compare deux chaînes de caractères entre elles
                {
                    case -1:
                        {
                            // tableau[(debut + fin) / 2] plus petit que mot
                            debut = (debut + fin) / 2;
                            return RechercheDichoRecursif(debut, fin, mot);
                            break;
                        }
                    case 0:
                        {
                            // tableau[(debut + fin) / 2] égal à mot
                            return true;
                            break;
                        }
                    case 1:
                        {
                            // tableau[(debut + fin) / 2] plus grand que mot
                            fin = (debut + fin) / 2;
                            return RechercheDichoRecursif(debut, fin, mot);
                            break;
                        }
                    default:
                        {
                            return false;
                            break;
                        }
                }
            }
            return false;
        }
        /// <summary>
        /// Comparaison de deux chaînes de caractères
        /// </summary>
        /// <param name="a">Chaîne A</param>
        /// <param name="b">Chaîne B</param>
        /// <returns>Entier :
        /// -1 : la chaîne A est avant la chaîne B dans l'ordre alphabétique
        /// 0 : les chaînes A et B sont identiques
        /// 1 : la chaîne A est après la chaîne B dans l'ordre alphabétique
        /// </returns>
        public int Comparaison(string a, string b)
        {
            // Mise en majuscule des mots
            a = a.ToUpper();
            b = b.ToUpper();
            int retour = 0; // Valeur de retour, par défaut 0 (a et b identiques)
            int longueur = Math.Min(a.Length, b.Length); // Longueur de la plus petite chaîne de caractères, pour la comparaison (et ne pas être out of range)
            if (a != b) // Si les deux sont différents, alors on les compares, sinon, c'est fini
            {
                bool fin = true;
                for (int i = 0; i < longueur && fin; i++) // Comparaison caractère à caractère
                {
                    if (a[i] < b[i])
                    {
                        retour = -1;
                        fin = false;
                    }
                    else if (a[i] > b[i])
                    {
                        retour = 1;
                        fin = false;
                    }
                }
                // Si 2 mots sont identiques jusqu'à avoir parcouru toutes les lettres du plus court, alors l'autre est classé après celui-ci
                // Par exemple, change sera classé ainsi avant changer
                if (fin && a.Length != b.Length)
                {
                    if (a.Length > b.Length)
                    {
                        retour = 1;
                    }
                    else
                    {
                        retour = -1;
                    }
                }
            }
            return retour;
        }
        /// <summary>
        /// ToString d'un dictionnaire (pour l'affichage du nombre de mots et de la langue)
        /// </summary>
        /// <returns>Chaîne de caractères comportant des informations sur le dictionnaire</returns>
        public string toString()
        {
            string s = "La langue du dictionnaire utilisé est le " + this.langue + ", avec :";
            IList<string[]> motsParLongueur = this.mots.Values;
            IList<int> longueurs = this.mots.Keys;
            foreach (int y in longueurs)
            {
                s += "\n - " + this.mots[y].Length + " mots de " + y + " lettres";
            }
            return (s);
        }
    }
}
