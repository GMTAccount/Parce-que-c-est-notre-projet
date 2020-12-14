using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Parce_que_c_est_notre_projet
{
    class Dictionnaire
    {
        //Champs
        private string langue;
        private SortedList<int, string[]> mots = new SortedList<int, string[]>();


        //Constructeur
        public Dictionnaire(StreamReader fichier, string langue)
        {
            this.langue = langue;
            bool testFace = true;
            bool testNbDe = true;
            try
            {
                string[] ligne = null;
                do
                {
                    int cle = Convert.ToInt32(fichier.ReadLine());
                    if(cle != 0)
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
        public string Langue
        {
            get { return this.langue; }
        }

        public SortedList<int, string[]> Mots
        {
            get { return this.mots; }
        }

        //Méthodes
        public bool RechercheDichoRecursif(int debut, int fin, string mot)
        {
            Console.WriteLine("OUI");
            int longueur = mot.Length;
            string[] tableau = this.mots[longueur];
            if (mot != null && mot.Length != 0)
            {
                if (fin - debut == 2 && tableau[debut + 1] == mot) return true;
                if (fin - debut == 2 && tableau[debut + 1] != mot) return false;
                if (tableau[debut] == mot) return true;
                if (tableau[fin] == mot) return true;
                switch(Compare(tableau[(debut + fin) / 2], mot))
                {
                    case -1:
                        {
                            debut = (debut + fin) / 2;
                            return RechercheDichoRecursif(debut, fin, mot);
                            break;
                        }
                    case 0:
                        {
                            return true;
                            break;
                        }
                    case 1:
                        {
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

        public static int Compare(string stra, string strb)
        {
            int retour = 0;
            int longueur = strb.Length;
            if (stra != strb)
            {
                bool fin = true;
                for (int i = 0; i < longueur && fin; i++)
                {
                    if (stra[i] < strb[i])
                    {
                        retour = -1;
                        fin = false;
                    }
                    else if (stra[i] > strb[i])
                    {
                        retour = 1;
                        fin = false;
                    }
                }
            }
            return retour;
        }

        public string toString()
        {
            string s = "La langue du dictionnaire utilisé est le " + this.langue + ", avec :";
            IList<string[]> motsParLongueur = this.mots.Values;
            IList<int> longueurs = this.mots.Keys;
            foreach(int y in longueurs)
            {
                s += "\n - " + this.mots[y].Length + " mots de " + y + " lettres";
            }
            return (s);
        }
    }
}