using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parce_que_c_est_notre_projet;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parce_que_c_est_notre_projet.Tests
{
    [TestClass()]
    public class DictionnaireTests
    {
        [TestMethod()]
        public void DictionnaireTestNON()
        {
            Dictionnaire test = new Dictionnaire("Non.cs", "FR");
            Assert.IsNull(test);
        }
        [TestMethod()]
        public void DictionnaireTestOUI()
        {
            Dictionnaire test = new Dictionnaire("MotsPossibles.cs", "FR");
            Assert.AreEqual("FR", test.Langue);
            Assert.IsNotNull(test.Mots);
        }
        [TestMethod()]
        public void RechercheDichoNON()
        {
            Dictionnaire test = new Dictionnaire("MotsPossibles.cs", "FR");
            Assert.AreEqual(false, test.RechercheDichoRecursif(0, test.Mots.Count, "WORD"));
        }
        [TestMethod()]
        public void RechercheDichoOUI()
        {
            Dictionnaire test = new Dictionnaire("MotsPossibles.cs", "FR");
            Assert.AreEqual(true, test.RechercheDichoRecursif(0, test.Mots.Count, "MOT"));
        }
        [TestMethod()]
        public void ComparaisonSup()
        {
            Dictionnaire test = new Dictionnaire("MotsPossibles.cs", "FR");
            Assert.AreEqual(-1, test.Comparaison("BATEAU", "AVION"));
        }
        [TestMethod()]
        public void ComparaisonInf()
        {
            Dictionnaire test = new Dictionnaire("MotsPossibles.cs", "FR");
            Assert.AreEqual(1, test.Comparaison("AVION", "BATEAU"));
        }
    }
}