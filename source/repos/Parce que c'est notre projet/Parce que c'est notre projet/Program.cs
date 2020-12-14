using System;
using System.IO;

namespace Parce_que_c_est_notre_projet
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] fichierDico = { "MotsPossibles.txt" };
            string fichierDes = "Des.txt";
            string langue = "FR";
            Jeu boogle = new Jeu(fichierDico, fichierDes);
            int duree = 0;
            bool test = false;
            int nbJoueurs = 0;
            /*do
            {
                Console.WriteLine("Combien de joueurs autour de la table ?");
                test = int.TryParse(Console.ReadLine(), out nbJoueurs);
            }
            while (!test && nbJoueurs <= 0);
            Joueur[] tab = new Joueur[nbJoueurs];
            for (int i = 0; i < nbJoueurs; i++)
            {
                Console.Clear();
                tab[i] = new Joueur(i + 1);
            }
            Console.Clear();
            do
            {
                Console.WriteLine("Combien de fois voulez-vous jouer chacun ?");
                test = int.TryParse(Console.ReadLine(), out duree);
                duree = duree * nbJoueurs;
            }
            while (!test && duree <= 0);*/
            nbJoueurs = 2;
            Joueur[] tab = new Joueur[nbJoueurs];
            tab[0] = new Joueur(1, "Paul");
            tab[1] = new Joueur(2, "Guillaume");
            duree = 2;
            Console.WriteLine("Début du chronomètre maintenant");
            DateTime dateDebut = DateTime.Now;
            DateTime dateFin = DateTime.Now + TimeSpan.FromMinutes(duree) + TimeSpan.FromSeconds(duree);
            DateTime.Compare(DateTime.Now, DateTime.Now + TimeSpan.FromMinutes(5));
            while(DateTime.Compare(DateTime.Now, dateFin) < 0)
            {
                boogle.Monplateau.Valeurs();
                for (int i = 0; i < nbJoueurs; i++)
                {
                    Console.WriteLine("C'est au tour de " + tab[i].Nom + " de jouer");
                    Console.WriteLine();
                    Console.WriteLine(boogle.Monplateau.ToString());
                    Console.WriteLine();
                    DateTime finChrono = DateTime.Now + TimeSpan.FromSeconds(30);
                    while (DateTime.Compare(DateTime.Now, finChrono) < 0)
                    {
                        Console.WriteLine("Saisissez un nouveau mot trouvé");
                        string mot = Console.ReadLine();
                        if (mot.Length > 2 && !tab[i].Contain(mot) && boogle.Verification(mot))
                        {
                            tab[i].Add_Mot(mot);
                            int score = 0;
                            switch (mot.Length)
                            {
                                case 3:
                                    {
                                        score = 2;
                                        break;
                                    }
                                case 4:
                                    {
                                        score = 3;
                                        break;
                                    }
                                case 5:
                                    {
                                        score = 4;
                                        break;
                                    }
                                case 6:
                                    {
                                        score = 5;
                                        break;
                                    }
                                default:
                                    {
                                        score = 11;
                                        break;
                                    }
                            }
                            tab[i].Score = score;
                            Console.WriteLine(tab[i].toString());
                        }
                    }
                }
            }
        }
    }
}
