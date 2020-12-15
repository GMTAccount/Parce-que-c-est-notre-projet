using System;
using System.Collections.Generic;
using System.IO;

namespace Parce_que_c_est_notre_projet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Bienvenue dans Boogle !";
            Console.ForegroundColor = ConsoleColor.Green;
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
            duree = 1;
            Console.WriteLine("Début du chronomètre maintenant");
            DateTime dateDebut = DateTime.Now;
            //DateTime dateFin = DateTime.Now + TimeSpan.FromMinutes(duree) + TimeSpan.FromSeconds(duree);
            DateTime dateFin = DateTime.Now + TimeSpan.FromSeconds(40);
            while (DateTime.Compare(DateTime.Now, dateFin) < 0)
            {
                for (int i = 0; i < nbJoueurs; i++)
                {
                    Console.Title = (tab[i].Nom + " est en train de jouer");
                    boogle.Monplateau.Valeurs();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("C'est au tour de " + tab[i].Nom + " de jouer");
                    Console.WriteLine();
                    DateTime finChrono = DateTime.Now + TimeSpan.FromSeconds(20);
                    while (DateTime.Compare(DateTime.Now, finChrono) < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(boogle.Monplateau.ToString());
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Saisissez un nouveau mot trouvé");
                        ConsoleKeyInfo key;
                        string mot = "";
                        do
                        {
                            key = Console.ReadKey();
                            switch (key.Key)
                            {
                                case ConsoleKey.Enter:
                                    {
                                        break;
                                    }
                                case ConsoleKey.Backspace:
                                    {
                                        if(mot.Length > 0)
                                        {
                                            string mot1 = "";
                                            for(int j = 0; j < mot.Length - 1; j++)
                                            {
                                                mot1 = mot1 + mot[j];
                                            }
                                            mot = mot1;
                                        }
                                        break;
                                    }
                                case ConsoleKey.Delete:
                                    {
                                        mot = "";
                                        break;
                                    }
                                default:
                                    {
                                        mot = mot + key.KeyChar;
                                        break;
                                    }
                            }
                        }
                        while (DateTime.Compare(DateTime.Now, finChrono) < 0 && key.Key != ConsoleKey.Enter);
                        mot = mot.ToUpper();
                        Console.WriteLine(mot);
                        if (mot.Length > 2 && DateTime.Compare(DateTime.Now, finChrono) < 0) 
                        {
                            if (!tab[i].Contain(mot))
                            {
                                if (boogle.Verification(mot))
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
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Ce mot n'existe pas ou n'est pas dans la grille actuelle.");
                                }
                            }
                            else if (DateTime.Compare(DateTime.Now, finChrono) < 0 && key.Key != ConsoleKey.Enter)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Vous avez déjà trouvé ce mot, on ne peut pas le remettre.");
                            }
                        }
                        else if (DateTime.Compare(DateTime.Now, finChrono) < 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Le mot saisi est trop court.");
                        }
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            }
            SortedList<int, string> tableauScores = new SortedList<int, string>();
            for(int i = 0; i < tab.Length; i++)
            {
                tableauScores.Add(tab[i].Score, tab[i].Nom);
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Et voici les scores à la fin : ");
            Console.ForegroundColor = ConsoleColor.White;
            IList<int> scores = tableauScores.Keys;
            IList<string> noms = tableauScores.Values;
            for(int i = tableauScores.Count - 1; i >= 0; i--)
            {
                Console.WriteLine((tableauScores.Count - i) + " " + noms[i] + " " + scores[i]);
            }
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
