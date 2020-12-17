using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parce_que_c_est_notre_projet;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parce_que_c_est_notre_projet.Tests
{
    [TestClass()]
    public class DeTests
    {
        [TestMethod()]
        public void DeTestMauvaisNombreFaces()
        {
            De test = new De("A;B;C", 6);
            Assert.AreEqual(3, test.Des.Length);
        }
        [TestMethod()]
        public void toStringTest()
        {
            De test = new De("A;B;C", 3);
            Assert.AreEqual("A B C ", test.toString());
        }
    }
}