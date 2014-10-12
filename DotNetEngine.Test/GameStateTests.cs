using DotNetEngine.Engine.Helpers;
using DotNetEngine.Engine.Objects;
using NUnit.Framework;
using System.IO;

namespace DotNetEngine.Test
{
	public class GameStateTests
	{
        private static readonly ZobristHash _zobristHash = new ZobristHash();

        /// <summary>
		/// This test is only useful for checking the output of the converter.
		/// </summary>
		[Test]
		public void BitBoardOutput()
        {
	        var gameState = new GameState("rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2", _zobristHash);

	        gameState.ConvertBitBoardsToConsoleOutput();
        }

		/// <summary>
		/// This test is only useful for checking the output of the converter.
		/// </summary>
		[Test]
		public void ArrayBoardOutput()
		{
            var gameState = new GameState("rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2", _zobristHash);

            gameState.ConvertBoardArrayToConsoleOutput();
		}

        [Test]
        [ExpectedException(typeof(InvalidDataException))]
        public void Throws_Exception_If_Invalid_Number_Of_Fields()
        {
// ReSharper disable once ObjectCreationAsStatement
            new GameState("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w ", _zobristHash);
        }

	    [Test]
        public void Sets_Number_Of_Half_Moves_Correctly()
        {
            var gameState = new GameState("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", _zobristHash);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(0));
        }

        [Test]
        public void Sets_Number_Of_Total_Moves_Correctly()
        {
            var gameState = new GameState("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", _zobristHash);

            Assert.That(gameState.TotalMoveCount, Is.EqualTo(1));
        }

        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", true)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", false)]
        public void Sets_WhiteToMove_Correctly(string input, bool result)
        {
            var gameState = new GameState(input, _zobristHash);

            Assert.That(gameState.WhiteToMove, Is.EqualTo(result));
        }

        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 3)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", 3)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w Kk - 0 1", 1)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b Kk - 0 1", 1)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w Qq - 0 1", 2)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b Qq - 0 1", 2)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b - - 0 1", 0)]
        public void Sets_CanCastle_Correctly(string input, int result)
        {
            var gameState = new GameState(input, _zobristHash);

            Assert.That(gameState.CurrentWhiteCastleStatus, Is.EqualTo(result));
            Assert.That(gameState.CurrentBlackCastleStatus, Is.EqualTo(result));
        }

        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b - - 0 1", 0)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b - a1 0 1", 0)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b - h8 0 1", 63)]
        public void Sets_EnPassant_Correctly(string input, int result)
        {
            var gameState = new GameState(input, _zobristHash);
            Assert.That(gameState.EnpassantTargetSquare, Is.EqualTo(result));
        }

        [Test]
	    public void Returns_Three_Move_Repetition()
	    {
	        var gameState = new GameState("8/8/2k5/8/8/8/2K5/8 w - - 0 1", _zobristHash);

	        var move = 0U;
	        move.SetMovingPiece(MoveUtility.WhiteKing);
	        move.SetFromMove(10);
	        move.SetToMove(18);

            gameState.MakeMove(move, _zobristHash);

            move = 0U;            
            move.SetMovingPiece(MoveUtility.BlackKing);
            move.SetFromMove(42);
            move.SetToMove(34);

            gameState.MakeMove(move, _zobristHash);

            move = 0U;  
            move.SetMovingPiece(MoveUtility.WhiteKing);
            move.SetFromMove(18);
            move.SetToMove(10);

            gameState.MakeMove(move, _zobristHash);

            move = 0U;  
            move.SetMovingPiece(MoveUtility.BlackKing);
            move.SetFromMove(34);
            move.SetToMove(48);

            gameState.MakeMove(move, _zobristHash);

            move = 0U;  
            move.SetMovingPiece(MoveUtility.WhiteKing);
            move.SetFromMove(10);
            move.SetToMove(18);

            gameState.MakeMove(move, _zobristHash);

            Assert.That(gameState.IsThreeMoveRepetition(), Is.True);

	    }
	}
}
