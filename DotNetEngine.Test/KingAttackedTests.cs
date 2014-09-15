using DotNetEngine.Engine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEngine.Test
{
    
    class KingAttackedTests
    {
        private MoveData _moveData = new MoveData();
       
        [TestCase("8/8/8/8/3k4/PPPPPPPP/8/8 w - - 0 1", true)]
        [TestCase("8/8/8/3k4/8/PPPPPPPP/8/8 w - - 0 1", false)]
        [TestCase("8/8/8/8/8/PPPPPPPP/3k4/8 w - - 0 1", false)]
        [TestCase("8/8/8/8/3k4/8/2N3N1/8 w - - 0 1", true)]
        [TestCase("8/8/8/8/4k3/8/2N3N1/8 w - - 0 1", false)]
        [TestCase("8/8/8/8/8/3k4/2K5/8 w - - 0 1", true)]
        [TestCase("8/8/8/8/4k3/8/2K5/8 w - - 0 1", false)]
        [TestCase("8/8/8/4k3/8/8/1B4B1/8 w - - 0 1", true)]
        [TestCase("8/8/4k3/8/8/8/1B4B1/8 w - - 0 1", false)]
        [TestCase("8/8/8/4k3/8/8/1Q4Q1/8 w - - 0 1", true)]
        [TestCase("8/8/4k3/8/8/8/1Q4Q1/8 w - - 0 1", false)]
        [TestCase("8/8/8/1R1k4/8/8/5R2/8 w - - 0 1", true)]
        [TestCase("8/8/4k3/1R6/8/8/5R2/8 w - - 0 1", false)]
        [TestCase("8/8/8/1R1k4/8/8/5Q2/8 w - - 0 1", true)]
        [TestCase("8/8/4k3/1R6/8/8/5Q2/8 w - - 0 1", false)]
        [TestCase("8/8/8/8/3K4/pppppppp/8/8 b - - 0 1", false)]
        [TestCase("8/8/8/3K4/8/pppppppp/8/8 b - - 0 1", false)]
        [TestCase("8/8/8/8/8/pppppppp/3K4/8 b - - 0 1", true)]
        [TestCase("8/8/8/8/3K4/8/2n3n1/8 b - - 0 1", true)]
        [TestCase("8/8/8/8/4K3/8/2n3n1/8 b - - 0 1", false)]
        [TestCase("8/8/8/8/8/3k4/2K5/8 b - - 0 1", true)]
        [TestCase("8/8/8/8/4k3/8/2K5/8 b - - 0 1", false)]
        [TestCase("8/8/8/4K3/8/8/1b4b1/8 b - - 0 1", true)]
        [TestCase("8/8/4K3/8/8/8/1b4b1/8 b - - 0 1", false)]
        [TestCase("8/8/8/4K3/8/8/1q4q1/8 b - - 0 1", true)]
        [TestCase("8/8/4K3/8/8/8/1q4q1/8 b - - 0 1", false)]
        [TestCase("8/8/8/1r1K4/8/8/5r2/8 b - - 0 1", true)]
        [TestCase("8/8/4K3/1r6/8/8/5r2/8 b - - 0 1", false)]
        [TestCase("8/8/8/1r1K4/8/8/5q2/8 b - - 0 1", true)]
        [TestCase("8/8/4K3/1r6/8/8/5q2/8 b - - 0 1", false)]
        public void Returns_Opposite_Side_Correctly_When_White_Attacking(string fen, bool expectedResult)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(fen);

            var result = gameState.IsOppositeSideKingAttacked(_moveData);            

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("8/8/8/8/3k4/PPPPPPPP/8/8 b - - 0 1", true)]
        [TestCase("8/8/8/3k4/8/PPPPPPPP/8/8 b - - 0 1", false)]
        [TestCase("8/8/8/8/8/PPPPPPPP/3k4/8 b - - 0 1", false)]
        [TestCase("8/8/8/8/3k4/8/2N3N1/8 b - - 0 1", true)]
        [TestCase("8/8/8/8/4k3/8/2N3N1/8 b - - 0 1", false)]
        [TestCase("8/8/8/8/8/3k4/2K5/8 b - - 0 1", true)]
        [TestCase("8/8/8/8/4k3/8/2K5/8 b - - 0 1", false)]
        [TestCase("8/8/8/4k3/8/8/1B4B1/8 b - - 0 1", true)]
        [TestCase("8/8/4k3/8/8/8/1B4B1/8 b - - 0 1", false)]
        [TestCase("8/8/8/4k3/8/8/1Q4Q1/8 b - - 0 1", true)]
        [TestCase("8/8/4k3/8/8/8/1Q4Q1/8 b - - 0 1", false)]
        [TestCase("8/8/8/1R1k4/8/8/5R2/8 b - - 0 1", true)]
        [TestCase("8/8/4k3/1R6/8/8/5R2/8 b - - 0 1", false)]
        [TestCase("8/8/8/1R1k4/8/8/5Q2/8 b - - 0 1", true)]
        [TestCase("8/8/4k3/1R6/8/8/5Q2/8 b - - 0 1", false)]
        [TestCase("8/8/8/8/3K4/pppppppp/8/8 w - - 0 1", false)]
        [TestCase("8/8/8/3K4/8/pppppppp/8/8 w - - 0 1", false)]
        [TestCase("8/8/8/8/8/pppppppp/3K4/8 w - - 0 1", true)]
        [TestCase("8/8/8/8/3K4/8/2n3n1/8 w - - 0 1", true)]
        [TestCase("8/8/8/8/4K3/8/2n3n1/8 w - - 0 1", false)]
        [TestCase("8/8/8/8/8/3k4/2K5/8 w - - 0 1", true)]
        [TestCase("8/8/8/8/4k3/8/2K5/8 w - - 0 1", false)]
        [TestCase("8/8/8/4K3/8/8/1b4b1/8 w - - 0 1", true)]
        [TestCase("8/8/4K3/8/8/8/1b4b1/8 w - - 0 1", false)]
        [TestCase("8/8/8/4K3/8/8/1q4q1/8 w - - 0 1", true)]
        [TestCase("8/8/4K3/8/8/8/1q4q1/8 w - - 0 1", false)]
        [TestCase("8/8/8/1r1K4/8/8/5r2/8 w - - 0 1", true)]
        [TestCase("8/8/4K3/1r6/8/8/5r2/8 w - - 0 1", false)]
        [TestCase("8/8/8/1r1K4/8/8/5q2/8 w - - 0 1", true)]
        [TestCase("8/8/4K3/1r6/8/8/5q2/8 w - - 0 1", false)]
        public void Returns_Current_Side_Correctly_When_Black_Attacking(string fen, bool expectedResult)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(fen);

            var result = gameState.IsCurrentSideKingAttacked(_moveData);        

            Assert.That(result, Is.EqualTo(expectedResult));
        }        
    }
}
