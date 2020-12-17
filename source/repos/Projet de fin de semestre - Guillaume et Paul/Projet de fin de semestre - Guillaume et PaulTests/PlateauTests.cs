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
        [TestMethod()]
        public void PlateauTest()
        {
            string[,] matrice = { { "J", "L", "R", "T" }, { "A", "Z", "E", "M" }, { "E", "E", "I", "G" }, { "W", "H", "O", "E" } };
            Plateau nouveau = new Plateau("De.cs", matrice);
            Assert.IsNotNull(nouveau);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            string[,] matrice = { { "J", "L", "R", "T" }, { "A", "Z", "E", "M" }, { "E", "E", "I", "G" }, { "W", "H", "O", "E" } };
            Plateau nouveau = new Plateau("De.cs", matrice);
            Assert.AreEqual("J L R T \nA Z E M \nE E I G \nW H O E \n", nouveau.ToString());
        }

        [TestMethod()]
        public void Test_PlateauTest()
        {
            string[,] matrice = { { "J", "L", "R", "T" }, { "A", "Z", "E", "M" }, { "E", "E", "I", "G" }, { "W", "H", "O", "E" } };
            Plateau nouveau = new Plateau("De.cs", matrice);
            bool test = nouveau.Test_Plateau("ZIEEM", 0);
            Assert.AreEqual(true, test);
        }
    }
}