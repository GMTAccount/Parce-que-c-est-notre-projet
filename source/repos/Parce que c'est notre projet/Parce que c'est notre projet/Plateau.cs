using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Parce_que_c_est_notre_projet
{
    public class Plateau
    {
        // Champs
        private List<De> des = new List<De>();
        private string[,] valeurSup;


        //Constructeur
        /// <summary>
        /// Constructeur d'un plateau
        /// </summary>
        /// <param name="filename">Nom du fichier de dés</param>
        /// <param name="matrice">Matrice pour les tests unitaires (null sinon)</param>
        public Plateau(string filename, string[,] matrice = null)
        {
            StreamReader fichier = new StreamReader(filename);
            bool testNbDe = true;
            try
            {
                string ligne = "";
                do
                {
                    ligne = fichier.ReadLine();
                    this.des.Add(new De(ligne));
                }
                while (ligne != null);
                if(matrice == null)
                {
                    this.valeurSup = new string[(int)Math.Pow(des.Count, 0.5), (int)Math.Pow(des.Count, 0.5)];
                }
                else
                {
                    this.valeurSup = matrice;
                }
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
        /// <summary>
        /// Retour des valeurs supérieures (pour l'IA)
        /// </summary>
        public string[,] ValeurSup
        {
            get { return this.valeurSup; }
        }
        //Méthodes
        /// <summary>
        /// Mélange des valeurs des dés (nouveau tirage)
        /// La valeur prise en compte deviendra celle de la face supérieure
        /// </summary>
        public void MelangeValeurs()
        {
            // Pour chaque case de la matrice, un nouveau lancer a lieu
            Random r = new Random();
            int compt = 0;
            for (int i = 0; i < this.valeurSup.GetLength(0); i++)
            {
                for(int j = 0; j < this.valeurSup.GetLength(1); j++)
                {
                    this.valeurSup[i,j] = this.des[compt].Lance(r);
                    compt++;
                }
            }
        }
        /// <summary>
        /// ToString d'un plateau
        /// </summary>
        /// <returns>Chaîne de caractères avec les valeurs actuelles de la face supérieure (destinée à l'affichage)</returns>
        public string ToString()
        {
            string s = "";
            if(this.valeurSup != null && valeurSup.GetLength(0)!=0 && valeurSup.GetLength(1)!=0)
            {
                for (int i = 0; i < this.valeurSup.GetLength(0); i++)
                {
                    for (int j = 0; j < this.valeurSup.GetLength(1); j++)
                    {
                        s += this.valeurSup[i, j] + " ";
                    }
                    s += "\n";
                }
            }
            return s;
        }
        /// <summary>
        /// Test de la condition d'adjacence et la présence d'un mot dans un plateau
        /// </summary>
        /// <param name="mot">Mot à analyser</param>
        /// <param name="posimot">Position du caractère du mot en cours d'étude (par défaut, 0)</param>
        /// <param name="positionMot">Liste comportant les cases déjà étudies (par défaut null)</param>
        /// <returns>Booléen : true = le mot respecte la condition d'adjecence et est présent, false sinon</returns>
        public bool Test_Plateau(string mot, int posimot, List<int> positionMot = null)
        {
            bool estPresent = false;
            if(mot != null && mot.Length != 0) // On vérifie si le mot peut être analysé
            {
                // Analyse pour la première position
                // On recherche d'abord si la première lettre du mot est bien dans le plateau
                if (posimot == 0) 
                {
                    // Parcours de toutes les cases du plateau pour trouver la première lettre du mot
                    for (int i = 0; i < this.valeurSup.GetLength(0) && !estPresent; i++)
                    {
                        for (int j = 0; j < this.valeurSup.GetLength(1) && !estPresent; j++)
                        {
                            if (this.valeurSup[i, j] == Convert.ToString(mot[0]))
                            {
                                // La première lettre du mot est dans le plateau, à la case [i, j]
                                positionMot = new List<int>(); // On initialise la liste comportant les coordonnées des cases déjà étudiées
                                // On ajoute à cette liste la position actuelle (d'abord i, puis j)
                                positionMot.Add(i);
                                positionMot.Add(j);
                                estPresent = Test_Plateau(mot, posimot + 1, positionMot); // On va ensuite chercher les autres lettres du mot
                                if (!estPresent) // Si la recherche de la ligne ci-dessus a échoué, on retire les positions de la liste
                                {
                                    positionMot.RemoveRange(0, positionMot.Count);
                                }
                                else
                                {
                                    // Si on a trouvé le mot, on retourne directement la valeur true
                                    return estPresent;
                                }
                            }
                        }
                    }
                }
                // On recherche ensuite toutes les autres lettres (dernière lettre exclue du else if)
                else if (posimot < mot.Length - 1) 
                {
                    // Analyse des cases voisines à celle que l'on a trouvée précédemment (et qui nous est communiquée par la liste positionMot)
                    for (int i = positionMot[positionMot.Count - 2] - 1; i <= positionMot[positionMot.Count - 2] + 1 && !estPresent; i++)
                    {
                        for (int j = positionMot[positionMot.Count - 1] - 1; j <= positionMot[positionMot.Count - 1] + 1 && !estPresent; j++)
                        {
                            if (((i >= 0 && i < this.valeurSup.GetLength(0)) && (j >= 0 && j < this.valeurSup.GetLength(1))) && (i != positionMot[positionMot.Count - 2] || j != positionMot[positionMot.Count - 1]) && this.valeurSup[i, j] == Convert.ToString(mot[posimot]))
                            {
                                // On a trouvé une lettre qui est égale à la posimot-ième lettre de notre mot, et qui n'est pas sur la case sur laquelle on est centré
                                // (On vérifie aussi si les coordonnées ne sont pas out of range)
                                bool test = true;
                                for (int k = 0; (k < positionMot.Count - 1 && test); k += 2) // On teste si la case correspondant aux conditions ci-dessus n'est pas déjà dans le chemin en cours d'étude (éviter les retours en arrière et doublons)
                                {
                                    if (i == positionMot[k] && j == positionMot[k + 1])
                                    {
                                        test = false;
                                    }
                                }
                                if (test) // La lettre n'appartient pas à la liste des cases du chemin en cours, on peut donc aller travailler dessus
                                {
                                    // On ajoute à cette liste la position actuelle (d'abord i, puis j)
                                    positionMot.Add(i);
                                    positionMot.Add(j);
                                    estPresent = Test_Plateau(mot, posimot + 1, positionMot); // On va ensuite chercher les autres lettres du mot
                                    positionMot.RemoveAt(positionMot.Count - 1); // Par défaut, on retire ces positions de la liste (pour éviter des tests inutiles)
                                    positionMot.RemoveAt(positionMot.Count - 1);
                                }
                                if (estPresent) // Si on a en retour true, on le retourne immédiatement, mettant fin à la récursivité
                                {
                                    return estPresent;
                                }
                            }
                        }
                    }
                }
                // On recherche enfin la dernière lettre
                else
                {
                    // Analyse des cases voisines à celle que l'on a trouvée précédemment (et qui nous est communiquée par la liste positionMot)
                    for (int i = positionMot[positionMot.Count - 2] - 1; i <= positionMot[positionMot.Count - 2] + 1 && !estPresent; i++)
                    {
                        for (int j = positionMot[positionMot.Count - 1] - 1; j <= positionMot[positionMot.Count - 1] + 1 && !estPresent; j++)
                        {
                            if (((i >= 0 && i < this.valeurSup.GetLength(0)) && (j >= 0 && j < this.valeurSup.GetLength(1))) && (i != positionMot[positionMot.Count - 2] || j != positionMot[positionMot.Count - 1]) && this.valeurSup[i, j] == Convert.ToString(mot[posimot]))
                            {
                                // On a trouvé une lettre qui est égale à la posimot-ième lettre de notre mot, et qui n'est pas sur la case sur laquelle on est centré
                                // (On vérifie aussi si les coordonnées ne sont pas out of range)
                                bool test = true;
                                for (int k = 0; k < positionMot.Count - 1; k += 2) // On teste si la case correspondant aux conditions ci-dessus n'est pas déjà dans le chemin en cours d'étude (éviter les retours en arrière et doublons)
                                {
                                    if (i == positionMot[k] && j == positionMot[k + 1])
                                    {
                                        test = false;
                                    }
                                }
                                if (test) // Si la dernière lettre est dans le voisinage de l'avant dernière et n'a pas été parcourue, on termine la récursivité, en retournant true
                                {
                                    return true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            
            return estPresent; // Dans le cas où l'on a pas trouvé le mot (ou il ne correspond pas aux critères de longueur), on retourne false par défaut
        }
    }
}
