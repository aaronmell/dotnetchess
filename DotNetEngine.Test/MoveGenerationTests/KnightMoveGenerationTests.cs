using DotNetEngine.Engine.Enums;
using DotNetEngine.Engine.Helpers;
using DotNetEngine.Engine.Objects;
using NUnit.Framework;
using System.Linq;

namespace DotNetEngine.Test.MoveGenerationTests
{
    public class KnightMoveGenerationTests
    {
        private readonly MoveData _moveData = new MoveData();

        #region White Knights
        [TestCase(27U, 17U, MoveGenerationMode.All)]
        [TestCase(27U, 10U, MoveGenerationMode.All)]
        [TestCase(27U, 12U, MoveGenerationMode.All)]
        [TestCase(27U, 21U, MoveGenerationMode.All)]
        [TestCase(27U, 37U, MoveGenerationMode.All)]
        [TestCase(27U, 42U, MoveGenerationMode.All)]
        [TestCase(27U, 44U, MoveGenerationMode.All)]
        [TestCase(27U, 33U, MoveGenerationMode.All)]
        [TestCase(27U, 17U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 10U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 12U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 21U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 37U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 42U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 44U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 33U, MoveGenerationMode.QuietMovesOnly)]
        public void Generates_Valid_White_Knight_Moves_When_Not_CaptureOnly(uint fromMove, uint toMove, MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/3N4/8/8/8 w - - 0 1");
            gameState.GenerateMoves(mode, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();
            var promotedPiece = move.GetPromotedPiece();


            Assert.That(movingPiece, Is.EqualTo(MoveUtility.WhiteKnight), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.EmptyPiece), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.EmptyPiece), "Promoted Piece");
        }

        [TestCase(27U, 17U)]
        [TestCase(27U, 10U)]
        [TestCase(27U, 12U)]
        [TestCase(27U, 21U)]
        [TestCase(27U, 37U)]
        [TestCase(27U, 42U)]
        [TestCase(27U, 44U)]
        [TestCase(27U, 33U)]
        public void Generates_No_Valid_White_Knight_Moves_When_CaptureOnly(uint fromMove, uint toMove)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/3N4/8/8/8 w - - 0 1");
            gameState.GenerateMoves(MoveGenerationMode.CaptureMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        }

        [TestCase(27U, 17U, MoveGenerationMode.All)]
        [TestCase(27U, 10U, MoveGenerationMode.All)]
        [TestCase(27U, 12U, MoveGenerationMode.All)]
        [TestCase(27U, 21U, MoveGenerationMode.All)]
        [TestCase(27U, 37U, MoveGenerationMode.All)]
        [TestCase(27U, 42U, MoveGenerationMode.All)]
        [TestCase(27U, 44U, MoveGenerationMode.All)]
        [TestCase(27U, 33U, MoveGenerationMode.All)]
        [TestCase(27U, 17U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 10U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 12U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 21U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 37U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 42U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 44U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 33U, MoveGenerationMode.CaptureMovesOnly)]
        public void Generates_Valid_White_Knight_Captures_When_Not_QuietMovesOnly(uint fromMove, uint toMove, MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/2p1p3/1p3p2/3N4/1p3p2/2p1p3/8 w - - 0 1");
            gameState.GenerateMoves(mode, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();
            var promotedPiece = move.GetPromotedPiece();


            Assert.That(movingPiece, Is.EqualTo(MoveUtility.WhiteKnight), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.BlackPawn), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.EmptyPiece), "Promoted Piece");
        }

        [TestCase(27U, 17U)]
        [TestCase(27U, 10U)]
        [TestCase(27U, 12U)]
        [TestCase(27U, 21U)]
        [TestCase(27U, 37U)]
        [TestCase(27U, 42U)]
        [TestCase(27U, 44U)]
        [TestCase(27U, 33U)]
        public void Generates_No_Valid_White_Knight_Captures_When_QuietMovesOnly(uint fromMove, uint toMove)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/2p1p3/1p3p2/3N4/1p3p2/2p1p3/8 w - - 0 1");
            gameState.GenerateMoves(MoveGenerationMode.QuietMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        }

        [TestCase(MoveGenerationMode.All)]
        [TestCase(MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(MoveGenerationMode.QuietMovesOnly)]
        public void Does_Not_Generate_Invalid_White_Knight_Captures_Against_Own_Pieces(MoveGenerationMode mode)
        {            
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/2K1K3/1K3K2/3N4/1K3K2/2K1K3/8 w - - 0 1");

            gameState.GenerateMoves(mode, 1, _moveData);
            var testMoves = gameState.Moves[1].Where(x => x.GetMovingPiece() == MoveUtility.WhiteKnight);

            Assert.That(testMoves, Is.Empty);

        }
        #endregion    
   
        #region
        [TestCase(27U, 17U, MoveGenerationMode.All)]
        [TestCase(27U, 10U, MoveGenerationMode.All)]
        [TestCase(27U, 12U, MoveGenerationMode.All)]
        [TestCase(27U, 21U, MoveGenerationMode.All)]
        [TestCase(27U, 37U, MoveGenerationMode.All)]
        [TestCase(27U, 42U, MoveGenerationMode.All)]
        [TestCase(27U, 44U, MoveGenerationMode.All)]
        [TestCase(27U, 33U, MoveGenerationMode.All)]
        [TestCase(27U, 17U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 10U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 12U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 21U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 37U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 42U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 44U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 33U, MoveGenerationMode.QuietMovesOnly)]
        public void Generates_Valid_Black_Knight_Moves_When_Not_CaptureOnly(uint fromMove, uint toMove, MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/3n4/8/8/8 b - - 0 1");
            gameState.GenerateMoves(mode, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();
            var promotedPiece = move.GetPromotedPiece();


            Assert.That(movingPiece, Is.EqualTo(MoveUtility.BlackKnight), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.EmptyPiece), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.EmptyPiece), "Promoted Piece");
        }

        [TestCase(27U, 17U)]
        [TestCase(27U, 10U)]
        [TestCase(27U, 12U)]
        [TestCase(27U, 21U)]
        [TestCase(27U, 37U)]
        [TestCase(27U, 42U)]
        [TestCase(27U, 44U)]
        [TestCase(27U, 33U)]
        public void Generates_No_Valid_Black_Knight_Moves_When_CaptureOnly(uint fromMove, uint toMove)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/3n4/8/8/8 b - - 0 1");
            gameState.GenerateMoves(MoveGenerationMode.CaptureMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        }

        [TestCase(27U, 17U, MoveGenerationMode.All)]
        [TestCase(27U, 10U, MoveGenerationMode.All)]
        [TestCase(27U, 12U, MoveGenerationMode.All)]
        [TestCase(27U, 21U, MoveGenerationMode.All)]
        [TestCase(27U, 37U, MoveGenerationMode.All)]
        [TestCase(27U, 42U, MoveGenerationMode.All)]
        [TestCase(27U, 44U, MoveGenerationMode.All)]
        [TestCase(27U, 33U, MoveGenerationMode.All)]
        [TestCase(27U, 17U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 10U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 12U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 21U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 37U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 42U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 44U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 33U, MoveGenerationMode.CaptureMovesOnly)]
        public void Generates_Valid_Black_Knight_Captures_When_Not_QuietMovesOnly(uint fromMove, uint toMove, MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/2P1P3/1P3P2/3n4/1P3P2/2P1P3/8 b - - 0 1");
            gameState.GenerateMoves(mode, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();
            var promotedPiece = move.GetPromotedPiece();


            Assert.That(movingPiece, Is.EqualTo(MoveUtility.BlackKnight), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.WhitePawn), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.EmptyPiece), "Promoted Piece");
        }

        [TestCase(27U, 17U)]
        [TestCase(27U, 10U)]
        [TestCase(27U, 12U)]
        [TestCase(27U, 21U)]
        [TestCase(27U, 37U)]
        [TestCase(27U, 42U)]
        [TestCase(27U, 44U)]
        [TestCase(27U, 33U)]
        public void Generates_No_Valid_Black_Knight_Captures_When_QuietMovesOnly(uint fromMove, uint toMove)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/2P1P3/1P3P2/3n4/1P3P2/2P1P3/8 b - - 0 1");
            gameState.GenerateMoves(MoveGenerationMode.QuietMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        }

        [TestCase(MoveGenerationMode.All)]
        [TestCase(MoveGenerationMode.CaptureMovesOnly)]
        public void Does_Not_Generate_Invalid_Black_Knight_Captures_Against_Own_Pieces(MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/2k1k3/1k3k2/3n4/1k3k2/2k1k3/8 b - - 0 1");

            gameState.GenerateMoves(mode, 1, _moveData);
            var testMoves = gameState.Moves[1].Where(x => x.GetMovingPiece() == MoveUtility.BlackKnight);

            Assert.That(testMoves, Is.Empty);

        }
        #endregion
    }
}
