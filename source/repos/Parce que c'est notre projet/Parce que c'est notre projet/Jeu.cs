using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Parce_que_c_est_notre_projet
{
    class Jeu
    {
        /*
        Reste à faire : 
        - Finir le Program
        - Revoir les méthodes récursives
        */
        private Dictionnaire[] mondico;
        private Plateau monplateau;

        public Jeu(string[] fichierDico, string fichierDes)
        {
            this.mondico = new Dictionnaire[fichierDico.Length];
            /*for(int i = 0; i < fichierDico.Length; i++)
            {
                Console.WriteLine("Veuillez donner un nom à votre langue : ");
                this.mondico[i] = new Dictionnaire(new StreamReader(fichierDico[i]), Console.ReadLine());

            }*/
            this.mondico[0] = new Dictionnaire(new StreamReader(fichierDico[0]), "FR");
            this.monplateau = new Plateau(fichierDes);
        }
        public Plateau Monplateau
        {
            get { return this.monplateau; }
        }
        public bool Verification(string mot)
        {
            return (this.mondico[0].RechercheDichoRecursif(0, this.mondico[0].Mots[mot.Length].Length - 1, mot) && this.monplateau.Test_Plateau(mot, 0));
        }
    }
}
