using Microsoft.VisualStudio.TestTools.UnitTesting;
using PolskaWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polska;

namespace PolskaWork.Tests
{


    //первый тест  скорее интеграционный тест, он проверяет программу целиком, обращаясь к библиотеке с классом.
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void UseCalcTest()
        {
            
            string expression = " - * / 15 - 7 + 1 1 3 + 2 + 1 1";
            Prefix prefix = new Prefix(expression);
            string expected = "5";

            decimal actual = prefix.Calc();

            Assert.AreEqual(expected, actual.ToString(), "Считает не верно!");
        }
    }


    [TestClass()]
    public class Prefix1
    {
        // список доступных операций
        private List<char> operations = new List<char> { '+', '-', '*', '/' };

        [TestMethod()]
        public void GetStringNumberTest()
        {
            string expr = " - * / 15 - 7 + 1 1 3 + 2 + 1 1";
            Prefix prefix = new Prefix(expr);
            int pos = 8;
            string expected = "15";

            string actual = prefix.GetStringNumber(expr, ref pos);

            Assert.AreEqual(expected, actual, "Считает не верно!");
        }

        [TestMethod()]
        public void GetStringNumberTest2()
        {
            string expr = " - * / 15 - 7 + 1 1 3 + 2 + 1 1";
            Prefix prefix = new Prefix(expr);
            int pos = 2;
            string expected = "";

            string actual = prefix.GetStringNumber(expr, ref pos);

            Assert.AreEqual(expected, actual, "Считает не верно!");
        }

        [TestMethod()]
        public void CalcTest()
        {
            string expr = " - * / 15 - 7 + 1 1 3 + 2 + 1 1";
            Prefix prefix = new Prefix(expr);
            decimal expected = 5;

            decimal actual = prefix.Calc();

            Assert.AreEqual(expected,actual,"Считает не верно!");
        }

    }
}