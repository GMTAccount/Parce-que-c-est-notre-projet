using Parce_que_c_est_notre_projet;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Parce_que_c_est_notre_projet.Tests
{
    [TestClass()]
    public class JoueurTests
    {
        [TestMethod()]
        public void JoueurTestScore()
        {
            Joueur joueur = new Joueur("Test");
            Assert.AreEqual(0, joueur.Score);
        }
        [TestMethod()]
        public void JoueurTestNom()
        {
            Joueur joueur = new Joueur("Test");
            Assert.AreEqual("Test", joueur.Nom);
        }
        [TestMethod()]
        public void JoueurTestNull()
        {
            Joueur joueur = new Joueur("Test");
            Assert.AreEqual(0, joueur.MotTrouve.Count);
        }

        [TestMethod()]
        public void ContainTestVide()
        {
            Joueur joueur = new Joueur("Test");
            Assert.AreEqual(false, joueur.Contain("Mot"));
        }
        [TestMethod()]
        public void ContainTestNon()
        {
            Joueur joueur = new Joueur("Test");
            joueur.Add_Mot("Mot");
            Assert.AreEqual(false, joueur.Contain("Motte"));
        }
        [TestMethod()]
        public void Add_MotTest()
        {
            Joueur joueur = new Joueur("Test");
            joueur.Add_Mot("Mot");
            Assert.AreEqual(true, joueur.Contain("Mot"));
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Joueur joueur = new Joueur("Test");
            string s = joueur.toString();
            Assert.AreEqual("Le score de Test est de 0, grâce aux mots cités suivent :\n", s);
        }
    }
}