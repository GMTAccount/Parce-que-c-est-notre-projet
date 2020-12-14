using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Parce_que_c_est_notre_projet
{
    class De
    {
        // Champs
        private string[] des;


        //Constructeur
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
        public string[]Des
        {
            get { return this.des; }
        }


        //Méthodes 
        public string Lance(Random r)
        {
            int position = r.Next(0, this.des.Length);
            return this.des[position];
        }

        public string toString()
        {
            string retour = "";
            foreach(string element in this.des)
            {
                retour = retour + element + " ";
            }
            return retour;
        }
    }
}
