using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Parce_que_c_est_notre_projet
{
    class Plateau
    {
        // Champs
        private List<De> des = new List<De>();
        private string[,] valeurSup;


        //Constructeur
        /// <summary>
        /// Constructeur d'un plateau
        /// </summary>
        /// <param name="filename">Nom du fichier de dés</param>
        public Plateau(string filename)
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
                this.valeurSup = new string[(int) Math.Pow(des.Count, 0.5), (int) Math.Pow(des.Count, 0.5)];
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
        public Plateau()
        {
            string filename = "Des.txt";
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
                this.valeurSup = new string[4, 4] { { "J", "L", "R", "T"}, { "A", "Z", "E", "M"}, { "E", "E", "I", "G" }, { "W", "H", "O", "E" } } ;
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
        /// Test de la condition d'adjacence dans un plateau
        /// </summary>
        /// <param name="mot">Mot à analyser</param>
        /// <param name="posimot">Position du caractère du mot en cours d'étude (par défaut, 0)</param>
        /// <param name="positionMot">Liste comportant les cases déjà étudies (par défaut nulle)</param>
        /// <returns></returns>
        public bool Test_Plateau(string mot, int posimot, List<int> positionMot = null)
        {
            Console.WriteLine(mot);
            bool estPresent = false;
            if(posimot == 0)
            {
                for(int i = 0; i < this.valeurSup.GetLength(0) && !estPresent; i++)
                {
                    for (int j = 0; j < this.valeurSup.GetLength(1) && !estPresent; j++)
                    {
                        if(this.valeurSup[i,j] == Convert.ToString(mot[0]))
                        {
                            positionMot = new List<int>();
                            positionMot.Add(i);
                            positionMot.Add(j);
                            estPresent = Test_Plateau(mot, posimot + 1, positionMot);
                            if (!estPresent)
                            {
                                positionMot.RemoveRange(0, positionMot.Count);
                            }
                            else
                            {
                                return estPresent;
                            }
                        }
                    }
                }
            }
            else if (posimot < mot.Length - 1)
            {
                for (int i = positionMot[positionMot.Count - 2] - 1; i <= positionMot[positionMot.Count - 2] + 1 && !estPresent; i++)
                {
                    for (int j = positionMot[positionMot.Count - 1] - 1; j <= positionMot[positionMot.Count - 1] + 1 && !estPresent; j++)
                    {
                        if (((i >= 0 && i < this.valeurSup.GetLength(0)) && (j >= 0 && j < this.valeurSup.GetLength(1))) && (i != positionMot[positionMot.Count - 2] || j != positionMot[positionMot.Count - 1]))
                        {
                            Console.WriteLine(posimot + " : " + i + ", " + j);
                            if(this.valeurSup[i, j] == Convert.ToString(mot[posimot]))
                            {
                                Console.WriteLine("OK pour la position " + i + ", " + j);
                                bool test = true;
                                for (int k = 0; (k < positionMot.Count - 1 && test); k += 2)
                                {
                                    if (i == positionMot[k] && j == positionMot[k + 1])
                                    {
                                        test = false;
                                    }
                                }
                                if (test)
                                {
                                    positionMot.Add(i);
                                    positionMot.Add(j);
                                    estPresent = Test_Plateau(mot, posimot + 1, positionMot);
                                    positionMot.RemoveAt(positionMot.Count - 1);
                                    positionMot.RemoveAt(positionMot.Count - 1);
                                }
                                if (estPresent)
                                {
                                    return estPresent;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = positionMot[positionMot.Count - 2] - 1; i <= positionMot[positionMot.Count - 2] + 1 && !estPresent; i++)
                {
                    for (int j = positionMot[positionMot.Count - 1] - 1; j <= positionMot[positionMot.Count - 1] + 1 && !estPresent; j++)
                    {
                        if (((i >= 0 && i < this.valeurSup.GetLength(0)) && (j >= 0 && j < this.valeurSup.GetLength(1))) && (i != positionMot[positionMot.Count - 2] || j != positionMot[positionMot.Count - 1]))
                        {
                            Console.WriteLine(posimot + " : " + i + ", " + j);
                            bool test = true;
                            for (int k = 0; k < positionMot.Count - 1; k += 2)
                            {
                                if (i == positionMot[k] && j == positionMot[k + 1])
                                {
                                    test = false;
                                }
                            }
                            if (test && this.valeurSup[i, j] == Convert.ToString(mot[posimot]))
                            {
                                Console.WriteLine("GO");
                                return true;
                                break;
                            }
                        }
                    }
                }
            }
            return estPresent;
        }
    }
}
