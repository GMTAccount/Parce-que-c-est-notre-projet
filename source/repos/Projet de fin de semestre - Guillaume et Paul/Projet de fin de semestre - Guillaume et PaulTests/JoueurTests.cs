using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projet_de_fin_de_semestre___Guillaume_et_Paul;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_de_fin_de_semestre___Guillaume_et_Paul.Tests
{
    [TestClass()]
    public class JoueurTests
    {
        /// <summary>
        /// Test du retour du score
        /// </summary>
        [TestMethod()]
        public void JoueurTestScore()
        {
            Joueur joueur = new Joueur("Test");
            Assert.AreEqual(0, joueur.Score);
        }
        /// <summary>
        /// Test d'augmentation du score
        /// </summary>
        [TestMethod()]
        public void JoueurTestScorePlus()
        {
            Joueur joueur = new Joueur("Test");
            joueur.Score = 20;
            Assert.AreEqual(20, joueur.Score);
        }
        /// <summary>
        /// Test du retour du nom
        /// </summary>
        [TestMethod()]
        public void JoueurTestNom()
        {
            Joueur joueur = new Joueur("Test");
            Assert.AreEqual("Test", joueur.Nom);
        }
        /// <summary>
        /// Test de la liste des mots trouvés
        /// </summary>
        [TestMethod()]
        public void JoueurTestNull()
        {
            Joueur joueur = new Joueur("Test");
            Assert.AreEqual(0, joueur.MotTrouve.Count);
        }
        /// <summary>
        /// Test de la présence d'un mot qui n'est pas dans la liste (liste vide)
        /// </summary>
        [TestMethod()]
        public void ContainTestVide()
        {
            Joueur joueur = new Joueur("Test");
            Assert.AreEqual(false, joueur.Contain("Mot"));
        }
        /// <summary>
        /// Test de la présence d'un mot qui n'est pas dans la liste (liste non vide)
        /// </summary>
        [TestMethod()]
        public void ContainTestNon()
        {
            Joueur joueur = new Joueur("Test");
            joueur.Add_Mot("Mot");
            Assert.AreEqual(false, joueur.Contain("Motte"));
        }
        /// <summary>
        /// Test d'ajout d'un mot et vérification de l'appartenance de ce mot
        /// </summary>
        [TestMethod()]
        public void Add_MotTest()
        {
            Joueur joueur = new Joueur("Test");
            joueur.Add_Mot("Mot");
            Assert.AreEqual(true, joueur.Contain("Mot"));
        }
        /// <summary>
        /// Test du ToString
        /// </summary>
        [TestMethod()]
        public void ToStringTest()
        {
            Joueur joueur = new Joueur("Test");
            string s = joueur.toString();
            Assert.AreEqual("Le score de Test est de 0, grâce aux mots cités suivent :\n", s);
        }
    }
}