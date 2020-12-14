using System;
using System.Collections.Generic;
using System.Text;

namespace Parce_que_c_est_notre_projet
{
    class Joueur
    {
        //Champs
        string nom;
        int score;
        List<string> motsTrouves;


        //Constructeur
        public Joueur(int compt, string nom)
        {
            this.score = 0;
            this.motsTrouves = new List<string>();
            /*do
            {
                Console.WriteLine("Veuillez entrer le nom du joueur " + compt + " :");
                this.nom = Console.ReadLine().ToUpper();
            }*/
            this.nom = nom;
            while (this.nom == null && this.nom.Length != 0);
        }


        //Propriétés
        public string Nom
        {
            get { return this.nom; }
        }
        public int Score
        {
            get { return this.score; }
            set { this.score = this.score + value; }
        }
        public List<string> MotTrouve
        {
            get { return this.motsTrouves; }
        }


        //Méthodes
        public bool Contain(string mot)
        {
            mot = mot.ToUpper();
            bool test = false;
            if(this.motsTrouves != null && this.motsTrouves.Count != 0)
            {
                for (int i = 0; (i < this.motsTrouves.Count) && !test; i++)
                {
                    test = (this.motsTrouves[i] == mot);
                }
            }
            return test;
        }
        public void Add_Mot(string mot)
        {
            mot = mot.ToUpper();
            if (!Contain(mot) && this.motsTrouves != null && mot.Length > 2)
            {
                this.motsTrouves.Add(mot);
            }
        }
        public string toString()
        {
            string text = ("Le score de " + this.nom + " est de " + this.score + ", grâce aux mots cités suivent :\n");
            foreach (string y in this.motsTrouves)
            {
                text = text + y + " ";
            }
            return text;
        }
    }
}
