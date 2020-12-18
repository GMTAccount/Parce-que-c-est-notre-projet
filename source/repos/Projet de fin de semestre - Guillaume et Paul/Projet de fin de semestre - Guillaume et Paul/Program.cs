using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Timers;

namespace Projet_de_fin_de_semestre___Guillaume_et_Paul
{
    public class Program
    {
        private static void FinTemps(Object source, ElapsedEventArgs e)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Vous n'avez plus le temps, c'est fini");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Appuyez sur Enter pour continuer");
        }
        static void Main(string[] args)
        {
            // Création de deux timers, l'un pour le temps d'une partie, l'autre pour le temps d'un tour
            Timer temps = new Timer();
            Console.Title = "Bienvenue dans Boggle !";
            Console.ForegroundColor = ConsoleColor.Green;
            string[] fichierDico = { "MotsPossibles.txt" };
            string[] nomLangues = { "Français" };
            string fichierDes = "Des.txt";
            int duree = 0;
            bool test = false;
            int nbJoueurs = 0;
            int langueJeu = 0;
            do
            {
                Console.WriteLine("Veuillez choisir une langue :");
                for (int i = 0; i < nomLangues.Length; i++)
                {
                    Console.WriteLine((i + 1) + " - " + nomLangues[i]);
                }
                Console.WriteLine("(Entrez le numéro correspondant à votre choix)");
                test = int.TryParse(Console.ReadLine(), out langueJeu);
                langueJeu--;
            }
            while (!test && (langueJeu < 0 || langueJeu >= nomLangues.Length));
            do
            {
                Console.WriteLine("Combien de joueurs autour de la table ?");
                Console.WriteLine("(Pour jouer contre notre invincible IA, tapez 1)");
                test = int.TryParse(Console.ReadLine(), out nbJoueurs);
            }
            while (!test && nbJoueurs <= 0);
            Jeu boogle = new Jeu(fichierDico, nomLangues, fichierDes);
            if (nbJoueurs > 1)
            {
                Joueur[] tab = new Joueur[nbJoueurs];
                for (int i = 0; i < nbJoueurs; i++)
                {
                    string nomJoueur = "";
                    Console.Clear();
                    do
                    {
                        Console.WriteLine("Veuillez entrer le nom du joueur n°" + (i + 1) + " :");
                        nomJoueur = Console.ReadLine().ToUpper();
                    }
                    while (nomJoueur == null && nomJoueur.Length != 0);
                    tab[i] = new Joueur(nomJoueur.ToUpper());
                }
                Console.Clear();
                do
                {
                    Console.WriteLine("Combien de fois voulez-vous jouer chacun ?");
                    test = int.TryParse(Console.ReadLine(), out duree);
                }
                while (!test && duree <= 0);
                duree = duree * nbJoueurs;
                Console.Title = "Attention, le jeu va commmencer !!!";
                Console.WriteLine("Pour lancer le chronomètre, appuyez sur Enter");
                ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
                do
                {
                    keyInfo = Console.ReadKey();
                }
                while (keyInfo.Key != ConsoleKey.Enter);
                Console.Clear();
                DateTime dateDebut = DateTime.Now;
                DateTime dateFin = DateTime.Now + TimeSpan.FromMinutes(duree);
                while (DateTime.Compare(DateTime.Now, dateFin) < 0)
                {
                    for (int i = 0; i < nbJoueurs; i++)
                    {
                        Console.Title = (tab[i].Nom + " est en train de jouer");
                        boogle.Monplateau.MelangeValeurs();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("C'est au tour de " + tab[i].Nom + " de jouer");
                        Console.WriteLine();
                        DateTime finChrono = DateTime.Now + TimeSpan.FromSeconds(60);
                        temps.Interval = 60000;
                        temps.Elapsed += FinTemps;
                        temps.AutoReset = true;
                        temps.Enabled = true;
                        temps.Start();
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
                                if(DateTime.Compare(DateTime.Now, finChrono) < 0)
                                {
                                    key = Console.ReadKey();
                                }
                                else
                                {
                                    key = new ConsoleKeyInfo('a', ConsoleKey.Enter, false, false, false);
                                }
                                switch (key.Key)
                                {
                                    case ConsoleKey.Enter:
                                    case ConsoleKey.Spacebar:
                                        {
                                            break;
                                        }
                                    case ConsoleKey.Backspace:
                                        {
                                            if (mot.Length > 0)
                                            {
                                                string mot1 = "";
                                                for (int j = 0; j < mot.Length - 1; j++)
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
                                    if (boogle.Verification(mot, langueJeu))
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
                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Ce mot n'existe pas ou n'est pas dans la grille actuelle.");
                                        Console.ReadKey();
                                    }
                                }
                                else if (DateTime.Compare(DateTime.Now, finChrono) < 0 && key.Key != ConsoleKey.Enter)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Vous avez déjà trouvé ce mot, on ne peut pas le remettre.");
                                    Console.ReadKey();
                                }
                            }
                            else if (DateTime.Compare(DateTime.Now, finChrono) < 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Le mot saisi est trop court.");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Vous avez dépassé le temps règlementaire.");
                            }
                            Console.Clear();
                        }
                        tab[i].ClearAllList();
                    }
                }
                SortedList<int, string> tableauScores = new SortedList<int, string>();
                for (int i = 0; i < tab.Length; i++)
                {
                    if (tableauScores.Count != 0 && tableauScores.ContainsKey(tab[i].Score))
                    {
                        tableauScores[tab[i].Score] = tableauScores[tab[i].Score] + ", " + tab[i].Nom;
                    }
                    else
                    {
                        tableauScores[tab[i].Score] = tab[i].Nom;
                    }
                }
                Console.Title = "Le moment fatidique des scores est enfin là !";
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Et voici les scores à la fin : ");
                Console.ForegroundColor = ConsoleColor.White;
                IList<int> scores = tableauScores.Keys;
                IList<string> noms = tableauScores.Values;
                for (int i = tableauScores.Count - 1; i >= 0; i--)
                {
                    Console.WriteLine((tableauScores.Count - i) + " " + noms[i] + " " + scores[i]);
                }
            }
            else
            {
                string nomJoueur = "";
                Console.Clear();
                do
                {
                    Console.WriteLine("Veuillez entrer votre nom :");
                    nomJoueur = Console.ReadLine().ToUpper();
                }
                while (nomJoueur == null && nomJoueur.Length != 0);
                Joueur joueur = new Joueur(nomJoueur.ToUpper());
                Console.Clear();
                do
                {
                    Console.WriteLine("Combien de fois voulez-vous affronter notre terrible IA ?");
                    test = int.TryParse(Console.ReadLine(), out duree);
                }
                while (!test && duree <= 0);
                Console.Clear();
                int choix = 0;
                do
                {
                    Console.WriteLine("Pour notre IA, souhaitez-vous qu'elle soit" +
                        "\n1 - Un peu imbattable" +
                        "\n2 - Invincible sans plus" +
                        "\n3 - ELLE VA TOUT DÉTRIURE" +
                        "\nVeuillez saisir le chiffre correspondant" +
                        "\n(PS : cherchez pas, vous allez perdre, y a pas d'autres issues. MOUHAHAHAHA)");
                    test = int.TryParse(Console.ReadLine(), out choix);
                }
                while (!test && (choix <= 0 || choix > 3));
                int[] intervalleValeursIA = null;
                switch (choix)
                {
                    case 1:
                        {
                            intervalleValeursIA = new int[] { 3, 6 };
                            break;
                        }
                    case 2:
                        {
                            intervalleValeursIA = new int[] { 4, 8 };
                            break;
                        }
                    default:
                        {
                            intervalleValeursIA = new int[] { 5, 10 };
                            break;
                        }
                }
                IA winner = new IA(boogle, langueJeu);
                Console.Clear();
                DateTime dateDebut = DateTime.Now;
                DateTime dateFin = DateTime.Now + TimeSpan.FromMinutes(duree);
                while (DateTime.Compare(DateTime.Now, dateFin) < 0)
                {
                    Console.Title = (joueur.Nom + " est en train de sauver son honneur face à notre formidable IA");
                    boogle.Monplateau.MelangeValeurs();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("C'est à votre tour de jouer");
                    Console.WriteLine();
                    DateTime finChrono = DateTime.Now + TimeSpan.FromSeconds(60);
                    temps.Interval = 60000;
                    temps.Elapsed += FinTemps;
                    temps.AutoReset = true;
                    temps.Enabled = true;
                    temps.Start();
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
                                case ConsoleKey.Spacebar:
                                    {
                                        break;
                                    }
                                case ConsoleKey.Backspace:
                                    {
                                        if (mot.Length > 0)
                                        {
                                            string mot1 = "";
                                            for (int j = 0; j < mot.Length - 1; j++)
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
                            if (!joueur.Contain(mot))
                            {
                                if (boogle.Verification(mot, langueJeu))
                                {
                                    joueur.Add_Mot(mot);
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
                                    joueur.Score = score;
                                    Console.WriteLine(joueur.toString());
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Ce mot n'existe pas ou n'est pas dans la grille actuelle.");
                                    Console.ReadKey();
                                }
                            }
                            else if (DateTime.Compare(DateTime.Now, finChrono) < 0 && key.Key != ConsoleKey.Enter)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Vous avez déjà trouvé ce mot, on ne peut pas le remettre.");
                                Console.ReadKey();
                            }
                        }
                        else if (DateTime.Compare(DateTime.Now, finChrono) < 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Le mot saisi est trop court.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Vous avez dépassé le temps règlementaire.");
                        }
                        Console.Clear();
                    }
                    joueur.ClearAllList();
                    boogle.Monplateau.MelangeValeurs();
                    Console.WriteLine(boogle.Monplateau.ToString());
                    winner.RechercheMots(boogle.Monplateau);
                    Random r = new Random();
                    int nbMots = r.Next(intervalleValeursIA[0], intervalleValeursIA[1] + 1);
                    for (int i = 0; i < nbMots; i++)
                    {
                        string motIA = winner.DistributionMots();
                        int score = 0;
                        if (motIA != "" && motIA != null && motIA.Length > 0)
                        {
                            switch (motIA.Length)
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
                            winner.Score = score;
                        }
                    }
                    Console.WriteLine(winner.ToString());
                    winner.ClearAllLists();
                }
                Console.Title = "Le moment fatidique des scores est enfin là !";
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Et le grand gagnant est ...");
                Console.ForegroundColor = ConsoleColor.White;
                if (joueur.Score > winner.Score)
                {
                    Console.WriteLine("VOUS, avec vos magnifiques " + joueur.Score + " points !");
                    Console.WriteLine("On ne sais pas comment vous avez fait, mais vous avez terminé l'IA et ses " + winner.Score + " points.");
                    Console.WriteLine("Félicitations !");
                }
                else if (joueur.Score < winner.Score)
                {
                    Console.WriteLine("C'est pas pour rien que son nom de variable est 'winner' !");
                    Console.WriteLine("L'IA vous a vaincu avec ses " + winner.Score + " points.");
                    Console.WriteLine("Normal qu'avec vos " + joueur.Score + " points, vous n'ayez rien pu faire !");
                }
                else
                {

                }
            }
        }
    }
}
