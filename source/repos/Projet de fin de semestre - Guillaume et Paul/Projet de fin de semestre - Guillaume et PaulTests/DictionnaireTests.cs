using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projet_de_fin_de_semestre___Guillaume_et_Paul;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_de_fin_de_semestre___Guillaume_et_Paul.Tests
{
    [TestClass()]
    public class DictionnaireTests
    {
        /// <summary>
        /// Test du retour de la langue d'un dictionnaire
        /// </summary>
        [TestMethod()]
        public void DictionnaireTestOUI()
        {
            Dictionnaire test = new Dictionnaire("MotsPossibles.txt", "FR");
            Assert.AreEqual("FR", test.Langue);
        }
        /// <summary>
        /// Test de la recherche dichitomique (mot inexistant)
        /// </summary>
        [TestMethod()]
        public void RechercheDichoNON()
        {
            Dictionnaire test = new Dictionnaire("MotsPossibles.txt", "FR");
            Assert.AreEqual(false, test.RechercheDichoRecursif(0, test.Mots.Count, "WORD"));
        }
        /// <summary>
        /// Test de la recherche dichitomique (mot inexistant)
        /// </summary>
        [TestMethod()]
        public void RechercheDichoOUI()
        {
            Dictionnaire test = new Dictionnaire("MotsPossibles.txt", "FR");
            string mot = "FAUX";
            int nbMots = 0;
            bool rechercheMot = test.RechercheDichoRecursif(0, test.Mots[mot.Length].Length - 1, mot);
            Assert.AreEqual(true, rechercheMot);
        }
        /// <summary>
        /// Test de la méthode Comparaison, test 1
        /// </summary>
        [TestMethod()]
        public void ComparaisonSup()
        {
            Dictionnaire test = new Dictionnaire("MotsPossibles.txt", "FR");
            Assert.AreEqual(1, test.Comparaison("BATEAU", "AVION"));
        }
        /// <summary>
        /// Test de la méthode Comparaison, test 1
        /// </summary>
        [TestMethod()]
        public void ComparaisonInf()
        {
            Dictionnaire test = new Dictionnaire("MotsPossibles.txt", "FR");
            Assert.AreEqual(-1, test.Comparaison("AVION", "BATEAU"));
        }
    }
}