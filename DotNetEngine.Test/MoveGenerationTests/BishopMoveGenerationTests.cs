using DotNetEngine.Engine.Enums;
using DotNetEngine.Engine.Helpers;
using DotNetEngine.Engine.Objects;
using NUnit.Framework;
using System.Linq;

namespace DotNetEngine.Test.MoveGenerationTests
{
    public class BishopMoveGenerationTests
    {
        private readonly MoveData _moveData = new MoveData();
        
        #region White Bishops
        [TestCase(27U, 0U, MoveGenerationMode.All)]
        [TestCase(27U, 9U, MoveGenerationMode.All)]
        [TestCase(27U, 18U, MoveGenerationMode.All)]       
        [TestCase(27U, 36U, MoveGenerationMode.All)]
        [TestCase(27U, 45U, MoveGenerationMode.All)]
        [TestCase(27U, 54U, MoveGenerationMode.All)]
        [TestCase(27U, 63U, MoveGenerationMode.All)]
        [TestCase(27U, 48U, MoveGenerationMode.All)]
        [TestCase(27U, 41U, MoveGenerationMode.All)]
        [TestCase(27U, 34U, MoveGenerationMode.All)]
        [TestCase(27U, 20U, MoveGenerationMode.All)]
        [TestCase(27U, 13U, MoveGenerationMode.All)]
        [TestCase(27U, 6U, MoveGenerationMode.All)]
        [TestCase(27U, 0U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 9U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 18U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 36U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 45U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 54U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 63U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 48U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 41U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 34U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 20U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 13U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 6U, MoveGenerationMode.QuietMovesOnly)]
        public void Generates_Valid_White_Bishop_Moves_When_Not_CaptureOnly(uint fromMove, uint toMove, MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/3B4/8/8/8 w - - 0 1");
            gameState.GenerateMoves(mode, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();
            var promotedPiece = move.GetPromotedPiece();


            Assert.That(movingPiece, Is.EqualTo(MoveUtility.WhiteBishop), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.EmptyPiece), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.EmptyPiece), "Promoted Piece");
        }

        [TestCase(27U, 0U)]
        [TestCase(27U, 9U)]
        [TestCase(27U, 18U)]
        [TestCase(27U, 36U)]
        [TestCase(27U, 45U)]
        [TestCase(27U, 54U)]
        [TestCase(27U, 63U)]
        [TestCase(27U, 48U)]
        [TestCase(27U, 41U)]
        [TestCase(27U, 34U)]
        [TestCase(27U, 20U)]
        [TestCase(27U, 13U)]
        [TestCase(27U, 6U)]
        public void Generates_No_Valid_White_Bishop_Moves_When_CaptureOnly(uint fromMove, uint toMove)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/3B4/8/8/8 w - - 0 1");
            gameState.GenerateMoves(MoveGenerationMode.CaptureMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        }
       
        [TestCase(27U, 9U, MoveGenerationMode.All)]        
        [TestCase(27U, 45U, MoveGenerationMode.All)]        
        [TestCase(27U, 41U, MoveGenerationMode.All)]
        [TestCase(27U, 13U, MoveGenerationMode.All)]
        [TestCase(27U, 45U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 41U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 13U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 9U, MoveGenerationMode.CaptureMovesOnly)]
        public void Generates_Valid_White_Bishop_Captures_When_Not_QuietMovesOnly(uint fromMove, uint toMove, MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/1p3p2/8/3B4/8/1p3p2/8 w - - 0 1");
            gameState.GenerateMoves(mode, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();
            var promotedPiece = move.GetPromotedPiece();

            Assert.That(movingPiece, Is.EqualTo(MoveUtility.WhiteBishop), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.BlackPawn), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.EmptyPiece), "Promoted Piece");
        }

        [TestCase(27U, 9U)]
        [TestCase(27U, 45U)]
        [TestCase(27U, 41U)]
        [TestCase(27U, 13U)]
        public void Generates_No_Valid_White_Bishop_Captures_When_QuietMovesOnly(uint fromMove, uint toMove)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/1p3p2/8/3B4/8/1p3p2/8 w - - 0 1");
            gameState.GenerateMoves(MoveGenerationMode.QuietMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        }

        [TestCase(MoveGenerationMode.All)]
        [TestCase(MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(MoveGenerationMode.QuietMovesOnly)]
        public void Does_Not_Generate_Invalid_White_Bishop_Captures_Against_Own_Pieces(MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/2P1P3/3B4/2P1P3/8/8 w - - 0 1");

            gameState.GenerateMoves(mode, 1, _moveData);
            var testMoves = gameState.Moves[1].Where(x => x.GetMovingPiece() == MoveUtility.WhiteBishop);

            Assert.That(testMoves, Is.Empty);

        }
        #endregion

        #region Black Bishops
        [TestCase(27U, 0U, MoveGenerationMode.All)]
        [TestCase(27U, 9U, MoveGenerationMode.All)]
        [TestCase(27U, 18U, MoveGenerationMode.All)]
        [TestCase(27U, 36U, MoveGenerationMode.All)]
        [TestCase(27U, 45U, MoveGenerationMode.All)]
        [TestCase(27U, 54U, MoveGenerationMode.All)]
        [TestCase(27U, 63U, MoveGenerationMode.All)]
        [TestCase(27U, 48U, MoveGenerationMode.All)]
        [TestCase(27U, 41U, MoveGenerationMode.All)]
        [TestCase(27U, 34U, MoveGenerationMode.All)]
        [TestCase(27U, 20U, MoveGenerationMode.All)]
        [TestCase(27U, 13U, MoveGenerationMode.All)]
        [TestCase(27U, 6U, MoveGenerationMode.All)]
        [TestCase(27U, 0U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 9U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 18U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 36U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 45U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 54U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 63U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 48U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 41U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 34U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 20U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 13U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 6U, MoveGenerationMode.QuietMovesOnly)]
        public void Generates_Valid_Black_Bishop_Moves_When_Not_CaptureOnly(uint fromMove, uint toMove, MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/3b4/8/8/8 b - - 0 1");
            gameState.GenerateMoves(mode, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();
            var promotedPiece = move.GetPromotedPiece();


            Assert.That(movingPiece, Is.EqualTo(MoveUtility.BlackBishop), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.EmptyPiece), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.EmptyPiece), "Promoted Piece");
        }

        [TestCase(27U, 0U)]
        [TestCase(27U, 9U)]
        [TestCase(27U, 18U)]
        [TestCase(27U, 36U)]
        [TestCase(27U, 45U)]
        [TestCase(27U, 54U)]
        [TestCase(27U, 63U)]
        [TestCase(27U, 48U)]
        [TestCase(27U, 41U)]
        [TestCase(27U, 34U)]
        [TestCase(27U, 20U)]
        [TestCase(27U, 13U)]
        [TestCase(27U, 6U)]
        public void Generates_No_Valid_Black_Bishop_Moves_When_CaptureOnly(uint fromMove, uint toMove)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/3b4/8/8/8 b - - 0 1");
            gameState.GenerateMoves(MoveGenerationMode.CaptureMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        }

        [TestCase(27U, 9U, MoveGenerationMode.All)]
        [TestCase(27U, 45U, MoveGenerationMode.All)]
        [TestCase(27U, 41U, MoveGenerationMode.All)]
        [TestCase(27U, 13U, MoveGenerationMode.All)]
        [TestCase(27U, 45U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 41U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 13U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 9U, MoveGenerationMode.CaptureMovesOnly)]
        public void Generates_Valid_Black_Bishop_Captures_When_Not_QuietMovesOnly(uint fromMove, uint toMove, MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/1P3P2/8/3b4/8/1P3P2/8 b - - 0 1");
            gameState.GenerateMoves(mode, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();
            var promotedPiece = move.GetPromotedPiece();

            Assert.That(movingPiece, Is.EqualTo(MoveUtility.BlackBishop), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.WhitePawn), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.EmptyPiece), "Promoted Piece");
        }

        [TestCase(27U, 9U)]
        [TestCase(27U, 45U)]
        [TestCase(27U, 41U)]
        [TestCase(27U, 13U)]
        public void Generates_No_Valid_Black_Bishop_Captures_When_QuietMovesOnly(uint fromMove, uint toMove)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/1P3P2/8/3b4/8/1P3P2/8 b - - 0 1");
            gameState.GenerateMoves(MoveGenerationMode.QuietMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        }

        [TestCase(MoveGenerationMode.All)]
        [TestCase(MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(MoveGenerationMode.QuietMovesOnly)]
        public void Does_Not_Generate_Invalid_Black_Bishop_Captures_Against_Own_Pieces(MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/2p1p3/3b4/2p1p3/8/8 b - - 0 1");

            gameState.GenerateMoves(mode, 1, _moveData);
            var testMoves = gameState.Moves[1].Where(x => x.GetMovingPiece() == MoveUtility.BlackBishop);

            Assert.That(testMoves, Is.Empty);

        }        
        #endregion
    }
}
