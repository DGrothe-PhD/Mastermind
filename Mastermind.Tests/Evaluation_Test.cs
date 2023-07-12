using MastermindVariante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind.Tests
{
    [TestFixture]
    internal class Evaluation_Test
    {
        private Form1 silentForm1;
        private Evaluation Evaluation;

        [SetUp]
        public void Setup()
        {
            silentForm1 = new Form1();
            Evaluation = new(silentForm1);
        }


        [Test]
        public void WordPoints_Test()
        {
            string guessedWord = "ZOBEL";
            List<Points> expected = new List<Points>
            {
                new Points(5, 0),
                new Points(0, 2),
                new Points(2, 1),
                new Points(1, 0),
                new Points(0, 0),
                new Points(1, 1),
            };

            List<Points> result = new List<Points>();
            Evaluation.SetSolution("ZOBEL");
            result.Add(Evaluation.WordPoints(guessedWord));
            
            Evaluation.SetSolution("LIZZY");
            result.Add(Evaluation.WordPoints(guessedWord));
            
            Evaluation.SetSolution("ROBBE");
            result.Add(Evaluation.WordPoints(guessedWord));

            Evaluation.SetSolution("SAGEN");
            result.Add(Evaluation.WordPoints(guessedWord));

            Evaluation.SetSolution("JAPAN");
            result.Add(Evaluation.WordPoints(guessedWord));

            Evaluation.SetSolution("LEIER");
            result.Add(Evaluation.WordPoints(guessedWord));

            CollectionAssert.AreEqual(expected, result);
        }
    }
}