using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Timers;

namespace Projet_de_fin_de_semestre___Guillaume_et_Paul
{
    public class Program
    {
        static bool suggestionIA = false; // Booléen qui nous informera s'il est possible de voir la suggestion de l'IA ou non
        /// <summary>
        /// Méthode enclenchée à la fin du temps de réponse imparti
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        static void FinTemps(Object source, ElapsedEventArgs e)
        {
            Console.Beep(1750, 500);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Vous n'avez plus le temps, c'est fini");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Appuyez sur Enter pour continuer");
        }
        /// <summary>
        /// Méthode qui enclenche la suggestion d'un mot par l'IA lorsque le joueur est en difficulté (n'a pas trouvé de mots au bout de 40 secondes)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        static void SuggestionIA(Object source, ElapsedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Notre IA a une petite suggestion pour vous !");
            Console.WriteLine("Appuyez sur Espace pour la découvrir");
            suggestionIA = true;
        }
        static void Main(string[] args)
        {
            // Création de deux timers, l'un pour le temps d'un tour, l'autre pour que l'IA conseille un mot au joueur si celui-ci n'en a trouvé aucun
            Timer temps = new Timer(); // Timer de temps de jeu
            Timer tempsIA = new Timer(); // Timer avant l'affichage de la proposition d'aide de l'IA
            tempsIA.Interval = 40000; // Durée d'attente avant affichage (et activation) de l'aide de l'IA
            tempsIA.Elapsed += SuggestionIA; // On attache au timer la méthode d'activation de l'aide de l'IA
            temps.Interval = 60000; // Durée d'attente avant affichage de fin de tour
            temps.Elapsed += FinTemps; // On attache au timer la méthode d'affichage de fin de tour
            Console.Title = "Bienvenue dans Boggle !";
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            string[] fichierDico = { "MotsPossibles.txt" };
            string[] nomLangues = { "Français" };
            string fichierDes = "Des.txt";
            Console.WriteLine("Bienvenue dans Boogle !");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
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
            while (!test || (langueJeu < 0 || langueJeu >= nomLangues.Length));
            Console.WriteLine();
            do
            {
                Console.WriteLine("Combien de joueurs autour de la table ?");
                Console.WriteLine("(Pour jouer contre notre invincible IA, tapez 1)");
                test = int.TryParse(Console.ReadLine(), out nbJoueurs);
            }
            while (!test || nbJoueurs <= 0);
            Jeu boogle = new Jeu(fichierDico, nomLangues, fichierDes);
            if (nbJoueurs > 1)
            {
                Joueur[] tab = new Joueur[nbJoueurs];
                for (int i = 0; i < nbJoueurs; i++)
                {
                    string nomJoueur = "";
                    Console.WriteLine();
                    do
                    {
                        Console.WriteLine("Veuillez entrer le nom du joueur n°" + (i + 1) + " :");
                        nomJoueur = Console.ReadLine().ToUpper();
                    }
                    while (nomJoueur == null || nomJoueur.Length == 0);
                    tab[i] = new Joueur(nomJoueur.ToUpper());
                }
                Console.WriteLine();
                do
                {
                    Console.WriteLine("Combien de fois voulez-vous jouer chacun ?");
                    test = int.TryParse(Console.ReadLine(), out duree);
                }
                while (!test || duree <= 0);
                duree = duree * nbJoueurs;
                Console.WriteLine();
                Console.Title = "Attention, le jeu va commmencer !!!";
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Pour lancer le chronomètre, appuyez sur Enter");
                Console.WriteLine("Que le meilleur gagne !");
                ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
                do
                {
                    keyInfo = Console.ReadKey();
                }
                while (keyInfo.Key != ConsoleKey.Enter);
                Console.Clear();
                DateTime dateFin = DateTime.Now + TimeSpan.FromMinutes(duree);
                while (DateTime.Compare(DateTime.Now, dateFin) < 0)
                {
                    for (int i = 0; i < nbJoueurs; i++)
                    {
                        tempsIA.AutoReset = false;
                        temps.AutoReset = false;
                        temps.Enabled = true;
                        tempsIA.Enabled = true;
                        Console.Title = (tab[i].Nom + " est en train de jouer");
                        boogle.Monplateau.MelangeValeurs();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("C'est au tour de " + tab[i].Nom + " de jouer");
                        Console.WriteLine();
                        DateTime finChrono = DateTime.Now + TimeSpan.FromSeconds(60);
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
                                if (DateTime.Compare(DateTime.Now, finChrono) < 0)
                                {
                                    key = Console.ReadKey();
                                }
                                else
                                {
                                    key = new ConsoleKeyInfo('a', ConsoleKey.Enter, false, false, false);
                                }
                                if(DateTime.Compare(DateTime.Now, finChrono) < 0)
                                {
                                    switch (key.Key)
                                    {
                                        case ConsoleKey.Enter:
                                            {
                                                break;
                                            }
                                        case ConsoleKey.Spacebar:
                                            {
                                                if (tab[i].MotTrouve.Count == 0 && suggestionIA)
                                                {
                                                    IA iA = new IA(boogle, langueJeu);
                                                    iA.RechercheMots(boogle.Monplateau);
                                                    Console.WriteLine("Notre IA vous suggère : " + iA.DistributionMots());
                                                    Console.WriteLine("Appuyez sur Enter pour continuer et pouvoir saisir ce mot");
                                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                                    tempsIA.Enabled = false;
                                                    Console.ReadKey();
                                                }
                                                mot = "";
                                                suggestionIA = false;
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
                                        tempsIA.Enabled = false;
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
                                        Console.WriteLine();
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Ce mot n'existe pas ou n'est pas dans la grille actuelle.");
                                        Console.WriteLine();
                                    }
                                }
                                else if (DateTime.Compare(DateTime.Now, finChrono) < 0 && key.Key != ConsoleKey.Enter)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Vous avez déjà trouvé ce mot, on ne peut pas le remettre.");
                                    Console.WriteLine();
                                }
                            }
                            else if (DateTime.Compare(DateTime.Now, finChrono) < 0 && mot != "")
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Le mot saisi est trop court.");
                                Console.WriteLine();
                            }
                            Console.WriteLine();
                        }
                        tab[i].ClearAllList();
                        Console.Clear();
                        temps.Enabled = false;
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
                Console.Beep(3000, 500);
                Console.Beep(2000, 500);
                Console.Beep(3000, 1000);
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine();
                string nomJoueur = "";
                do
                {
                    Console.WriteLine("Veuillez entrer votre nom :");
                    nomJoueur = Console.ReadLine().ToUpper();
                }
                while (nomJoueur == null || nomJoueur.Length == 0);
                Joueur joueur = new Joueur(nomJoueur.ToUpper());
                Console.WriteLine();
                do
                {
                    Console.WriteLine("Combien de fois voulez-vous affronter notre terrible IA ?");
                    test = int.TryParse(Console.ReadLine(), out duree);
                }
                while (!test || duree <= 0);
                Console.WriteLine();
                int choix = 0;
                do
                {
                    Console.WriteLine("Pour notre IA, souhaitez-vous qu'elle soit" +
                        "\n1 - Un peu imbattable" +
                        "\n2 - Invincible sans plus" +
                        "\n3 - ELLE VA TOUT DÉTRUIRE" +
                        "\nVeuillez saisir le chiffre correspondant" +
                        "\n(PS : cherchez pas, vous allez perdre, y a pas d'autres issues. MOUHAHAHAHA)");
                    test = int.TryParse(Console.ReadLine(), out choix);
                }
                while (!test || (choix <= 0 || choix > 3));
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
                    temps.AutoReset = false;
                    temps.Enabled = true;
                    Console.Title = (joueur.Nom + " est en train de sauver son honneur face à notre formidable IA");
                    boogle.Monplateau.MelangeValeurs();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("C'est à votre tour de jouer");
                    Console.WriteLine();
                    DateTime finChrono = DateTime.Now + TimeSpan.FromSeconds(60);
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
                            if(DateTime.Compare(DateTime.Now, finChrono) < 0)
                            {
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
                        Console.WriteLine();
                    }
                    temps.Enabled = false;
                    joueur.ClearAllList();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
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
                Console.WriteLine();
                Console.Title = "Le moment fatidique des scores est enfin là !";
                Console.ForegroundColor = ConsoleColor.DarkYellow;
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
                    Console.WriteLine("Aussi bizarre que cela puisse paraître, y a comme qui dirait une égalité");
                    Console.WriteLine("Vous et notre IA avez le même nombre de points : " + joueur.Score + " points.");
                    Console.WriteLine("Même les probas ne pouvaient prévoir ça !");
                }
                Console.Beep(3000, 500);
                Console.Beep(2000, 500);
                Console.Beep(3000, 1000);
                Console.ReadKey();
            }
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine();
            Console.Title = "C'était Boogle, par Guillaume TADJER et Paul SORET";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Ce jeu a été réalisé par Guillaume TADJER et Paul SORET");
            Console.WriteLine("ESILV A2 S3 TD N (2)");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("L'intégralité de ce code est placé sous licence CC-BY-SA 4.0 International");
            Console.WriteLine("PLus d'informations sur ce que vous pouvez en faire sur : https://creativecommons.org/licenses/by-sa/4.0/");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Présenté le 18 décembre 2020");
            Console.ReadKey();
        }
    }
}
