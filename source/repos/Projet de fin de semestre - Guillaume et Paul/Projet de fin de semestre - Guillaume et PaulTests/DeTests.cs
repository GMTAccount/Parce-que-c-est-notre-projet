﻿using Projet_de_fin_de_semestre___Guillaume_et_Paul;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_de_fin_de_semestre___Guillaume_et_Paul.Tests
{
    [TestClass()]
    public class DeTests
    {
        /// <summary>
        /// Test de création d'un dé avec peu de faces
        /// </summary>
        [TestMethod()]
        public void DeTestMauvaisNombreFaces()
        {
            De test = new De("A;B;C", 6);
            Assert.AreEqual(3, test.Des.Length);
        }
        /// <summary>
        /// Test du ToString
        /// </summary>
        [TestMethod()]
        public void toStringTest()
        {
            De test = new De("A;B;C", 3);
            Assert.AreEqual("A B C ", test.toString());
        }
    }
}