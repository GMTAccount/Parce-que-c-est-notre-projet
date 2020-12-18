using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projet_de_fin_de_semestre___Guillaume_et_Paul;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_de_fin_de_semestre___Guillaume_et_Paul.Tests
{
    [TestClass()]
    public class PlateauTests
    {
        /// <summary>
        /// Test de la création d'un plateau
        /// </summary>
        [TestMethod()]
        public void PlateauTest()
        {
            string[,] matrice = { { "J", "L", "R", "T" }, { "A", "Z", "E", "M" }, { "E", "E", "I", "G" }, { "W", "H", "O", "E" } };
            Plateau nouveau = new Plateau("Des.txt", matrice);
            Assert.IsNotNull(nouveau);
        }
        /// <summary>
        /// Test du ToString
        /// </summary>
        [TestMethod()]
        public void ToStringTest()
        {
            string[,] matrice = { { "J", "L", "R", "T" }, { "A", "Z", "E", "M" }, { "E", "E", "I", "G" }, { "W", "H", "O", "E" } };
            Plateau nouveau = new Plateau("Des.txt", matrice);
            Assert.AreEqual("J L R T \nA Z E M \nE E I G \nW H O E \n", nouveau.ToString());
        }
        /// <summary>
        /// Test de la méthode Test_Plateau (mot présent)
        /// </summary>
        [TestMethod()]
        public void Test_PlateauTestOUI()
        {
            string[,] matrice = { { "J", "L", "R", "T" }, { "A", "Z", "E", "M" }, { "E", "E", "I", "G" }, { "W", "H", "O", "E" } };
            Plateau nouveau = new Plateau("Des.txt", matrice);
            bool test = nouveau.Test_Plateau("ZIEEM", 0);
            Assert.AreEqual(true, test);
        }
        /// <summary>
        /// Test de la méthode Test_Plateau (mot non présent)
        /// </summary>
        [TestMethod()]
        public void Test_PlateauTestNON()
        {
            string[,] matrice = { { "J", "L", "R", "T" }, { "A", "Z", "E", "M" }, { "E", "E", "I", "G" }, { "W", "H", "O", "E" } };
            Plateau nouveau = new Plateau("Des.txt", matrice);
            bool test = nouveau.Test_Plateau("ZIEEEEEEM", 0);
            Assert.AreEqual(false, test);
        }
    }
}