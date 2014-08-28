using DotNetEngine.Engine;
using NUnit.Framework;
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
			var gameState = FenUtility.LoadStateFromFen("rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2");

			var output = GameStateUtility.ConvertBitBoardsToConsoleOutput(gameState);
		}

		/// <summary>
		/// This test is only useful for checking the output of the converter.
		/// </summary>
		[Test]
		public void ArrayBoardOutput()
		{
			var gameState = FenUtility.LoadStateFromFen("rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2");

			var output = GameStateUtility.ConvertBoardArrayToConsoleOutput(gameState);
		}
	}
}
