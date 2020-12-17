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
        [TestMethod()]
        public void DictionnaireTestOUI()
        {
            Dictionnaire test = new Dictionnaire("MotsPossibles.txt", "FR");
            Assert.AreEqual("FR", test.Langue);
        }
        [TestMethod()]
        public void RechercheDichoNON()
        {
            Dictionnaire test = new Dictionnaire("MotsPossibles.txt", "FR");
            Assert.AreEqual(false, test.RechercheDichoRecursif(0, test.Mots.Count, "WORD"));
        }
        [TestMethod()]
        public void RechercheDichoOUI()
        {
            Dictionnaire test = new Dictionnaire("MotsPossibles.txt", "FR");
            string mot = "mot";
            int nbMots = 0;
            IList<string[]> values = test.Mots.Values;
            for(int i = 0; i < values.Count; i++)
            {
                nbMots = values[i].Length + nbMots;
            }
            bool rechercheMot = test.RechercheDichoRecursif(0, nbMots, "MOT");
            Assert.AreEqual(true, rechercheMot);
        }
        [TestMethod()]
        public void ComparaisonSup()
        {
            Dictionnaire test = new Dictionnaire("MotsPossibles.txt", "FR");
            Assert.AreEqual(1, test.Comparaison("BATEAU", "AVION"));
        }
        [TestMethod()]
        public void ComparaisonInf()
        {
            Dictionnaire test = new Dictionnaire("MotsPossibles.txt", "FR");
            Assert.AreEqual(-1, test.Comparaison("AVION", "BATEAU"));
        }
    }
}