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


        //Méthodes
        public void Valeurs()
        {
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

        public string ToString()
        {
            string s = "";
            if(this.valeurSup != null)
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

    }
}
