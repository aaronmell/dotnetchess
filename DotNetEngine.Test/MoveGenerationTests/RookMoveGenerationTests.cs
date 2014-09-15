using DotNetEngine.Engine;
using NUnit.Framework;
using System.Linq;

namespace DotNetEngine.Test.MoveGenerationTests
{
    public class RookMoveGenerationTests
    {
        private readonly MoveData _moveData = new MoveData();

        #region White Rooks
        [TestCase(27U, 24U, MoveGenerationMode.All)]
        [TestCase(27U, 25U, MoveGenerationMode.All)]
        [TestCase(27U, 26U, MoveGenerationMode.All)]      
        [TestCase(27U, 28U, MoveGenerationMode.All)]
        [TestCase(27U, 29U, MoveGenerationMode.All)]
        [TestCase(27U, 30U, MoveGenerationMode.All)]
        [TestCase(27U, 31U, MoveGenerationMode.All)]
        [TestCase(27U, 59U, MoveGenerationMode.All)]
        [TestCase(27U, 51U, MoveGenerationMode.All)]
        [TestCase(27U, 43U, MoveGenerationMode.All)]
        [TestCase(27U, 35U, MoveGenerationMode.All)]
        [TestCase(27U, 19U, MoveGenerationMode.All)]
        [TestCase(27U, 11U, MoveGenerationMode.All)]
        [TestCase(27U, 3U, MoveGenerationMode.All)]
        [TestCase(27U, 24U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 25U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 26U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 28U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 29U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 30U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 31U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 59U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 51U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 43U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 35U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 19U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 11U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 3U, MoveGenerationMode.QuietMovesOnly)]     
        public void Generates_Valid_White_Rook_Moves_When_Not_CaptureOnly(uint fromMove, uint toMove, MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/3R4/8/8/8 w - - 0 1");
            gameState.GenerateMoves(mode, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();
            var promotedPiece = move.GetPromotedPiece();


            Assert.That(movingPiece, Is.EqualTo(MoveUtility.WhiteRook), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.Empty), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.Empty), "Promoted Piece");
        }

        [TestCase(27U, 24U)]
        [TestCase(27U, 25U)]
        [TestCase(27U, 26U)]
        [TestCase(27U, 28U)]
        [TestCase(27U, 29U)]
        [TestCase(27U, 30U)]
        [TestCase(27U, 31U)]
        [TestCase(27U, 59U)]
        [TestCase(27U, 51U)]
        [TestCase(27U, 43U)]
        [TestCase(27U, 35U)]
        [TestCase(27U, 19U)]
        [TestCase(27U, 11U)]
        [TestCase(27U, 3U)]
        public void Generates_No_Valid_White_Rook_Moves_When_CaptureOnly(uint fromMove, uint toMove)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/3R4/8/8/8 w - - 0 1");
            gameState.GenerateMoves(MoveGenerationMode.CaptureMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        }

        [TestCase(27U, 11U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 43U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 25U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 29U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 11U, MoveGenerationMode.All)]
        [TestCase(27U, 43U, MoveGenerationMode.All)]
        [TestCase(27U, 25U, MoveGenerationMode.All)]
        [TestCase(27U, 29U, MoveGenerationMode.All)]
        public void Generates_Valid_White_Rook_Captures_When_Not_QuietMovesOnly(uint fromMove, uint toMove, MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/3p4/8/1p1R1p2/8/3p4/8 w - - 0 1");
            gameState.GenerateMoves(mode, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();
            var promotedPiece = move.GetPromotedPiece();


            Assert.That(movingPiece, Is.EqualTo(MoveUtility.WhiteRook), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.BlackPawn), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.Empty), "Promoted Piece");
        }

        [TestCase(27U, 11U)]
        [TestCase(27U, 43U)]
        [TestCase(27U, 25U)]
        [TestCase(27U, 29U)]
        public void Generates_No_Valid_White_Rook_Captures_When_QuietMovesOnly(uint fromMove, uint toMove)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/3p4/8/1p1R1p2/8/3p4/8 w - - 0 1");
            gameState.GenerateMoves(MoveGenerationMode.QuietMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        }
        
        [TestCase(MoveGenerationMode.All)]
        [TestCase(MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(MoveGenerationMode.QuietMovesOnly)]
        public void Does_Not_Generate_Invalid_White_Rook_Captures_Against_Own_Pieces(MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/3P4/2PRP3/3P4/8/8 w - - 0 1");

            gameState.GenerateMoves(mode, 1, _moveData);
            var testMoves = gameState.Moves[1].Where(x => x.GetMovingPiece() == MoveUtility.WhiteRook);

            Assert.That(testMoves, Is.Empty);

        }
        #endregion

        #region Black Rooks
        [TestCase(27U, 24U, MoveGenerationMode.All)]
        [TestCase(27U, 25U, MoveGenerationMode.All)]
        [TestCase(27U, 26U, MoveGenerationMode.All)]
        [TestCase(27U, 28U, MoveGenerationMode.All)]
        [TestCase(27U, 29U, MoveGenerationMode.All)]
        [TestCase(27U, 30U, MoveGenerationMode.All)]
        [TestCase(27U, 31U, MoveGenerationMode.All)]
        [TestCase(27U, 59U, MoveGenerationMode.All)]
        [TestCase(27U, 51U, MoveGenerationMode.All)]
        [TestCase(27U, 43U, MoveGenerationMode.All)]
        [TestCase(27U, 35U, MoveGenerationMode.All)]
        [TestCase(27U, 19U, MoveGenerationMode.All)]
        [TestCase(27U, 11U, MoveGenerationMode.All)]
        [TestCase(27U, 3U, MoveGenerationMode.All)]
        [TestCase(27U, 24U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 25U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 26U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 28U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 29U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 30U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 31U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 59U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 51U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 43U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 35U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 19U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 11U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 3U, MoveGenerationMode.QuietMovesOnly)]
        public void Generates_Valid_Black_Rook_Moves_When_Not_CaptureOnly(uint fromMove, uint toMove, MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/3r4/8/8/8 b - - 0 1");
            gameState.GenerateMoves(mode, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();
            var promotedPiece = move.GetPromotedPiece();


            Assert.That(movingPiece, Is.EqualTo(MoveUtility.BlackRook), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.Empty), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.Empty), "Promoted Piece");
        }

        [TestCase(27U, 24U)]
        [TestCase(27U, 25U)]
        [TestCase(27U, 26U)]
        [TestCase(27U, 28U)]
        [TestCase(27U, 29U)]
        [TestCase(27U, 30U)]
        [TestCase(27U, 31U)]
        [TestCase(27U, 59U)]
        [TestCase(27U, 51U)]
        [TestCase(27U, 43U)]
        [TestCase(27U, 35U)]
        [TestCase(27U, 19U)]
        [TestCase(27U, 11U)]
        [TestCase(27U, 3U)]
        public void Generates_No_Valid_Black_Rook_Moves_When_CaptureOnly(uint fromMove, uint toMove)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/3r4/8/8/8 b - - 0 1");
            gameState.GenerateMoves(MoveGenerationMode.CaptureMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        }

        [TestCase(27U, 11U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 43U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 25U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 29U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 11U, MoveGenerationMode.All)]
        [TestCase(27U, 43U, MoveGenerationMode.All)]
        [TestCase(27U, 25U, MoveGenerationMode.All)]
        [TestCase(27U, 29U, MoveGenerationMode.All)]
        public void Generates_Valid_Black_Rook_Captures_When_Not_QuietMovesOnly(uint fromMove, uint toMove, MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/3P4/8/1P1r1P2/8/3P4/8 b - - 0 1");
            gameState.GenerateMoves(mode, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();
            var promotedPiece = move.GetPromotedPiece();


            Assert.That(movingPiece, Is.EqualTo(MoveUtility.BlackRook), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.WhitePawn), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.Empty), "Promoted Piece");
        }

        [TestCase(27U, 11U)]
        [TestCase(27U, 43U)]
        [TestCase(27U, 25U)]
        [TestCase(27U, 29U)]
        public void Generates_No_Valid_Black_Rook_Captures_When_QuietMovesOnly(uint fromMove, uint toMove)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/3P4/8/1P1r1P2/8/3P4/8 b - - 0 1");
            gameState.GenerateMoves(MoveGenerationMode.QuietMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        }

        [TestCase(MoveGenerationMode.All)]
        [TestCase(MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(MoveGenerationMode.QuietMovesOnly)]
        public void Does_Not_Generate_Invalid_Black_Rook_Captures_Against_Own_Pieces(MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/3p4/2prp3/3p4/8/8 b - - 0 1");

            gameState.GenerateMoves(mode, 1, _moveData);
            var testMoves = gameState.Moves[1].Where(x => x.GetMovingPiece() == MoveUtility.BlackRook);

            Assert.That(testMoves, Is.Empty);

        }
        #endregion
    }
}
