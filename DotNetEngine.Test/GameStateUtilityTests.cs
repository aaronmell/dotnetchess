using DotNetEngine.Engine;
using NUnit.Framework;
using System.IO;

namespace DotNetEngine.Test
{
	public class GameStateUtilityTests
	{
        /// <summary>
		/// This test is only useful for checking the output of the converter.
		/// </summary>
		[Test]
		public void BitBoardOutput()
        {
	        var gameState = GameStateUtility.LoadStateFromFen("rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2");

	        gameState.ConvertBitBoardsToConsoleOutput();
        }

		/// <summary>
		/// This test is only useful for checking the output of the converter.
		/// </summary>
		[Test]
		public void ArrayBoardOutput()
		{
			var gameState = GameStateUtility.LoadStateFromFen("rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2");

            gameState.ConvertBoardArrayToConsoleOutput();
		}

        [Test]
        [ExpectedException(typeof(InvalidDataException))]
        public void Throws_Exception_If_Invalid_Number_Of_Fields()
        {
            GameStateUtility.LoadStateFromFen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 ");
        }

        [Test]
        public void Sets_Number_Of_Half_Moves_Correctly()
        {
            var gameState = GameStateUtility.LoadStateFromFen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(0));
        }

        [Test]
        public void Sets_Number_Of_Total_Moves_Correctly()
        {
            var gameState = GameStateUtility.LoadStateFromFen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");

            Assert.That(gameState.TotalMoveCount, Is.EqualTo(1));
        }

        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", true)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", false)]
        public void Sets_WhiteToMove_Correctly(string input, bool result)
        {
            var gameState = GameStateUtility.LoadStateFromFen(input);

            Assert.That(gameState.WhiteToMove, Is.EqualTo(result));
        }

        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 3)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", 3)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w Kk - 0 1", 2)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b Kk - 0 1", 2)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w Qq - 0 1", 1)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b Qq - 0 1", 1)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b - - 0 1", 0)]
        public void Sets_CanCastle_Correctly(string input, int result)
        {
            var gameState = GameStateUtility.LoadStateFromFen(input);

            Assert.That(gameState.CurrentWhiteCastleStatus, Is.EqualTo(result));
            Assert.That(gameState.CurrentBlackCastleStatus, Is.EqualTo(result));
        }

        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b - - 0 1", 0)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b - a1 0 1", 0)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b - h8 0 1", 63)]
        public void Sets_EnPassant_Correctly(string input, int result)
        {
            var gameState = GameStateUtility.LoadStateFromFen(input);
            Assert.That(gameState.EnpassantTargetSquare, Is.EqualTo(result));
        }
	}
}
