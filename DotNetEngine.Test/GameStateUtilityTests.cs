using DotNetEngine.Engine;
using NUnit.Framework;
using System.IO;
using System.Text;

namespace DotNetEngine.Test
{
	public class GameStateUtilityTests
	{
        [Test]
		public void GetAllWhitePiecesBoard_Returns_Correct_Board_When_Filled()
		{
			var gameState = new GameState
			{
				WhiteBishops = 0x000000000000FFFFul,
				WhiteKing =    0x00000000FFFF0000ul,
				WhiteKnights = 0x000000FF00000000ul,
				WhitePawns =   0x0000FF0000000000ul,
				WhiteQueen =   0x00FF000000000000ul,
				WhiteRooks =   0xFF00000000000000ul
			};

			var board = gameState.WhitePieces;

			Assert.That(board, Is.EqualTo(0xFFFFFFFFFFFFFFFFul));
		}

		[Test]
		public void GetAllWhitePiecesBoard_Returns_Correct_Board_When_Empty()
		{
			var gameState = new GameState
			{
				WhiteBishops = 0,
				WhiteKing = 0,
				WhiteKnights = 0,
				WhitePawns = 0,
				WhiteQueen = 0,
				WhiteRooks = 0
			};

            var board = gameState.WhitePieces;

			Assert.That(board, Is.EqualTo(0));
		}

		[Test]
		public void GetAllBlackPiecesBoard_Returns_Correct_Board_When_Filled()
		{
			var gameState = new GameState
			{
				BlackBishops = 0x000000000000FFFFul,
				BlackKing = 0x00000000FFFF0000ul,
				BlackKnights = 0x000000FF00000000ul,
				BlackPawns = 0x0000FF0000000000ul,
				BlackQueen = 0x00FF000000000000ul,
				BlackRooks = 0xFF00000000000000ul
			};

            var board = gameState.BlackPieces;

			Assert.That(board, Is.EqualTo(0xFFFFFFFFFFFFFFFFul));
		}

		[Test]
		public void GetAllBlackPiecesBoard_Returns_Correct_Board_When_Empty()
		{
			var gameState = new GameState
			{
				BlackBishops = 0,
				BlackKing = 0,
				BlackKnights = 0,
				BlackPawns = 0,
				BlackQueen = 0,
				BlackRooks = 0
			};

            var board = gameState.BlackPieces;

			Assert.That(board, Is.EqualTo(0));
		}


		/// <summary>
		/// This test is only useful for checking the output of the converter.
		/// </summary>
		[Test]
		public void BitBoardOutput()
		{
			var gameState = GameStateUtility.LoadStateFromFen("rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2");

            var output = gameState.ConvertBitBoardsToConsoleOutput();
		}

		/// <summary>
		/// This test is only useful for checking the output of the converter.
		/// </summary>
		[Test]
		public void ArrayBoardOutput()
		{
			var gameState = GameStateUtility.LoadStateFromFen("rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2");

            var output = gameState.ConvertBoardArrayToConsoleOutput();
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
