using DotNetEngine.Engine.Enums;
using DotNetEngine.Engine.Helpers;
using DotNetEngine.Engine.Objects;
using NUnit.Framework;
using System.Linq;

namespace DotNetEngine.Test.MoveGenerationTests
{
    public class KingMoveGenerationTests
    {
        private readonly MoveData _moveData = new MoveData();

        #region White King Moves
        [TestCase(27U, 26U, MoveGenerationMode.All)]
        [TestCase(27U, 28U, MoveGenerationMode.All)]
        [TestCase(27U, 34U, MoveGenerationMode.All)]
        [TestCase(27U, 35U, MoveGenerationMode.All)]
        [TestCase(27U, 36U, MoveGenerationMode.All)]
        [TestCase(27U, 18U, MoveGenerationMode.All)]
        [TestCase(27U, 19U, MoveGenerationMode.All)]
        [TestCase(27U, 20U, MoveGenerationMode.All)]
        [TestCase(27U, 26U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 28U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 34U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 35U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 36U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 18U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 19U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 20U, MoveGenerationMode.QuietMovesOnly)]
        public void Generates_Valid_White_King_Moves_When_Not_CaptureOnly(uint fromMove, uint toMove, MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/3K4/8/8/8 w - - 0 1");
            gameState.GenerateMoves(mode, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();
            var promotedPiece = move.GetPromotedPiece();


            Assert.That(movingPiece, Is.EqualTo(MoveUtility.WhiteKing), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.EmptyPiece), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.EmptyPiece), "Promoted Piece");
        }

        [TestCase(27U, 26U)]
        [TestCase(27U, 28U)]
        [TestCase(27U, 34U)]
        [TestCase(27U, 35U)]
        [TestCase(27U, 36U)]
        [TestCase(27U, 18U)]
        [TestCase(27U, 19U)]
        [TestCase(27U, 20U)]
        public void Generates_No_Valid_White_King_Moves_When_CaptureOnly(uint fromMove, uint toMove)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/3K4/8/8/8 w - - 0 1");
            gameState.GenerateMoves(MoveGenerationMode.CaptureMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        }

        [TestCase(27U, 26U, MoveGenerationMode.All)]
        [TestCase(27U, 28U, MoveGenerationMode.All)]
        [TestCase(27U, 34U, MoveGenerationMode.All)]
        [TestCase(27U, 35U, MoveGenerationMode.All)]
        [TestCase(27U, 36U, MoveGenerationMode.All)]
        [TestCase(27U, 18U, MoveGenerationMode.All)]
        [TestCase(27U, 19U, MoveGenerationMode.All)]
        [TestCase(27U, 20U, MoveGenerationMode.All)]
        [TestCase(27U, 26U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 28U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 34U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 35U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 36U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 18U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 19U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 20U, MoveGenerationMode.CaptureMovesOnly)]
        public void Generates_Valid_White_King_Captures_When_Not_QuietMovesOnly(uint fromMove, uint toMove, MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/2ppp3/2pKp3/2ppp3/8/8 w - - 0 1");
            gameState.GenerateMoves(mode, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();
            var promotedPiece = move.GetPromotedPiece();


            Assert.That(movingPiece, Is.EqualTo(MoveUtility.WhiteKing), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.BlackPawn), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.EmptyPiece), "Promoted Piece");
        }

        [TestCase(27U, 26U)]
        [TestCase(27U, 28U)]
        [TestCase(27U, 34U)]
        [TestCase(27U, 35U)]
        [TestCase(27U, 36U)]
        [TestCase(27U, 18U)]
        [TestCase(27U, 19U)]
        [TestCase(27U, 20U)]
        public void Generates_No_Valid_White_King_Captures_When_QuietMovesOnly(uint fromMove, uint toMove)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/2ppp3/2pKp3/2ppp3/8/8 w - - 0 1");
            gameState.GenerateMoves(MoveGenerationMode.QuietMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        }

        [TestCase(MoveGenerationMode.All)]
        [TestCase(MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(MoveGenerationMode.QuietMovesOnly)]
        public void Does_Not_Generate_Invalid_White_King_Captures_Against_Own_Pieces(MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/2PPP3/2PKP3/2PPP3/8/8 w - - 0 1");

            gameState.GenerateMoves(mode, 1, _moveData);
            var testMoves = gameState.Moves[1].Where(x => x.GetMovingPiece() == MoveUtility.WhiteRook);

            Assert.That(testMoves, Is.Empty);

        }

		[TestCase(2U, "8/8/8/8/8/8/8/R3K3 w Q - 0 1", MoveGenerationMode.All)]
		[TestCase(6U, "8/8/8/8/8/8/8/4K2R w K - 0 1", MoveGenerationMode.All)]
		[TestCase(2U, "8/8/8/8/8/8/8/R3K3 w Q - 0 1", MoveGenerationMode.QuietMovesOnly)]
		[TestCase(6U, "8/8/8/8/8/8/8/4K2R w K - 0 1", MoveGenerationMode.QuietMovesOnly)]
        public void Generates_Valid_White_King_Castling_When_Not_Capture_Only(uint toMove, string fen, MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(fen);
            gameState.GenerateMoves(mode, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == 4U && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();
            var promotedPiece = move.GetPromotedPiece();


            Assert.That(movingPiece, Is.EqualTo(MoveUtility.WhiteKing), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.EmptyPiece), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.WhiteKing), "Promoted Piece");
        }

        [TestCase(2U)]
        [TestCase(6U)]       
        public void Generates_No_Valid_White_King_Castling_When_Capture_Only(uint toMove)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/8/8/R3K2R w KQ - 0 1");
            gameState.GenerateMoves(MoveGenerationMode.CaptureMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == 4U && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        }

        [TestCase(2U, "8/8/8/8/8/8/8/R1P1KP1R w KQ - 0 1")]
        [TestCase(6U, "8/8/8/8/8/8/8/R1P1KP1R w KQ - 0 1")]
        [TestCase(2U, "8/8/8/8/8/8/8/RP2K1PR w KQ - 0 1")]
        [TestCase(6U, "8/8/8/8/8/8/8/RP2K1PR w KQ - 0 1")]
        [TestCase(2U, "8/8/8/8/8/8/8/R2PKP1R w KQ - 0 1")]
        [TestCase(6U, "8/8/8/8/8/8/8/R2PKP1R w KQ - 0 1")]
        public void Generates_No_Valid_White_King_Castling_When_Castling_Blocked(uint toMove, string fen)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(fen);
            gameState.GenerateMoves(MoveGenerationMode.All, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == 4U && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        }

		[TestCase(2U, "r3K2R/8/8/8/8/8/1r6/8 w kq - 0 1")]
		[TestCase(2U, "r3K2R/8/8/8/8/8/2r5/8 w kq - 0 1")]
		[TestCase(2U, "r3K2R/8/8/8/8/8/3r4/8 w kq - 0 1")]
		[TestCase(2U, "r3K2R/8/8/8/8/8/4r3/8 w kq - 0 1")]
		[TestCase(6U, "r3K2R/8/8/8/8/8/4r3/8 w kq - 0 1")]
		[TestCase(6U, "r3K2R/8/8/8/8/8/5r2/8 w kq - 0 1")]
		[TestCase(6U, "r3K2R/8/8/8/8/8/6r1/8 w kq - 0 1")]
		public void Generates_No_Valid_White_King_Castling_When_Castling_Attacked(uint toMove, string fen)
		{
			var gameState = GameStateUtility.LoadGameStateFromFen(fen);
			gameState.GenerateMoves(MoveGenerationMode.All, 1, _moveData);

			var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == 4U && x.GetToMove() == toMove);

			Assert.That(move, Is.EqualTo(0));
		}
        #endregion

        #region Black King
        [TestCase(27U, 26U, MoveGenerationMode.All)]
        [TestCase(27U, 28U, MoveGenerationMode.All)]
        [TestCase(27U, 34U, MoveGenerationMode.All)]
        [TestCase(27U, 35U, MoveGenerationMode.All)]
        [TestCase(27U, 36U, MoveGenerationMode.All)]
        [TestCase(27U, 18U, MoveGenerationMode.All)]
        [TestCase(27U, 19U, MoveGenerationMode.All)]
        [TestCase(27U, 20U, MoveGenerationMode.All)]
        [TestCase(27U, 26U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 28U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 34U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 35U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 36U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 18U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 19U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(27U, 20U, MoveGenerationMode.QuietMovesOnly)]
        public void Generates_Valid_Black_King_Moves_When_Not_CaptureOnly(uint fromMove, uint toMove, MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/3k4/8/8/8 b - - 0 1");
            gameState.GenerateMoves(mode, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();
            var promotedPiece = move.GetPromotedPiece();


            Assert.That(movingPiece, Is.EqualTo(MoveUtility.BlackKing), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.EmptyPiece), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.EmptyPiece), "Promoted Piece");
        }

        [TestCase(27U, 26U)]
        [TestCase(27U, 28U)]
        [TestCase(27U, 34U)]
        [TestCase(27U, 35U)]
        [TestCase(27U, 36U)]
        [TestCase(27U, 18U)]
        [TestCase(27U, 19U)]
        [TestCase(27U, 20U)]
        public void Generates_No_Valid_Black_King_Moves_When_CaptureOnly(uint fromMove, uint toMove)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/3k4/8/8/8 b - - 0 1");
            gameState.GenerateMoves(MoveGenerationMode.CaptureMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        }

        [TestCase(27U, 26U, MoveGenerationMode.All)]
        [TestCase(27U, 28U, MoveGenerationMode.All)]
        [TestCase(27U, 34U, MoveGenerationMode.All)]
        [TestCase(27U, 35U, MoveGenerationMode.All)]
        [TestCase(27U, 36U, MoveGenerationMode.All)]
        [TestCase(27U, 18U, MoveGenerationMode.All)]
        [TestCase(27U, 19U, MoveGenerationMode.All)]
        [TestCase(27U, 20U, MoveGenerationMode.All)]
        [TestCase(27U, 26U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 28U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 34U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 35U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 36U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 18U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 19U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 20U, MoveGenerationMode.CaptureMovesOnly)]
        public void Generates_Valid_Black_King_Captures_When_Not_QuietMovesOnly(uint fromMove, uint toMove, MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/2PPP3/2PkP3/2PPP3/8/8 b - - 0 1");
            gameState.GenerateMoves(mode, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();
            var promotedPiece = move.GetPromotedPiece();


            Assert.That(movingPiece, Is.EqualTo(MoveUtility.BlackKing), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.WhitePawn), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.EmptyPiece), "Promoted Piece");
        }

        [TestCase(27U, 26U)]
        [TestCase(27U, 28U)]
        [TestCase(27U, 34U)]
        [TestCase(27U, 35U)]
        [TestCase(27U, 36U)]
        [TestCase(27U, 18U)]
        [TestCase(27U, 19U)]
        [TestCase(27U, 20U)]
        public void Generates_No_Valid_Black_King_Captures_When_QuietMovesOnly(uint fromMove, uint toMove)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/2PPP3/2PkP3/2PPP3/8/8 b - - 0 1");
            gameState.GenerateMoves(MoveGenerationMode.QuietMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        }

        [TestCase(MoveGenerationMode.All)]
        [TestCase(MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(MoveGenerationMode.QuietMovesOnly)]
        public void Does_Not_Generate_Invalid_Black_King_Captures_Against_Own_Pieces(MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/2ppp3/2pkp3/2ppp3/8/8 b - - 0 1");

            gameState.GenerateMoves(mode, 1, _moveData);
            var testMoves = gameState.Moves[1].Where(x => x.GetMovingPiece() == MoveUtility.WhiteRook);

            Assert.That(testMoves, Is.Empty);

        }

		[TestCase(58U, "r3k3/8/8/8/8/8/8/8 b q - 0 1", MoveGenerationMode.All)]
		[TestCase(62U, "4k2r/8/8/8/8/8/8/8 b k - 0 1", MoveGenerationMode.All)]
		[TestCase(58U, "r3k3/8/8/8/8/8/8/8 b q - 0 1", MoveGenerationMode.QuietMovesOnly)]
		[TestCase(62U, "4k2r/8/8/8/8/8/8/8 b k - 0 1", MoveGenerationMode.QuietMovesOnly)]
		public void Generates_Valid_Black_King_Castling_When_Not_Capture_Only(uint toMove, string fen, MoveGenerationMode mode)
		{
			var gameState = GameStateUtility.LoadGameStateFromFen(fen);
			gameState.GenerateMoves(mode, 1, _moveData);

			var move = gameState.Moves[1].First(x => x.GetFromMove() == 60U && x.GetToMove() == toMove);

			var capturedPiece = move.GetCapturedPiece();
			var movingPiece = move.GetMovingPiece();
			var promotedPiece = move.GetPromotedPiece();

			Assert.That(movingPiece, Is.EqualTo(MoveUtility.BlackKing), "Moving Piece");
			Assert.That(capturedPiece, Is.EqualTo(MoveUtility.EmptyPiece), "Captured Piece");
			Assert.That(promotedPiece, Is.EqualTo(MoveUtility.BlackKing), "Promoted Piece");
		}

        [TestCase(58U)]
        [TestCase(62U)]
        public void Generates_No_Valid_Black_King_Castling_When_Capture_Only(uint toMove)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("r3k2r/8/8/8/8/8/8/8 b kq - 0 1");
            gameState.GenerateMoves(MoveGenerationMode.CaptureMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == 4U && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        }

        [TestCase(58U, "rp2k1pr/8/8/8/8/8/8/8 b kq - 0 1")]
        [TestCase(62U, "rp2k1pr/8/8/8/8/8/8/8 b kq - 0 1")]
        [TestCase(58U, "r1p1kp1r/8/8/8/8/8/8/8 b kq - 0 1")]
        [TestCase(62U, "r1p1kp1r/8/8/8/8/8/8/8 b kq - 0 1")]
        [TestCase(58U, "r2pkp1r/8/8/8/8/8/8/8 b kq - 0 1")]
        [TestCase(62U, "r2pkp1r/8/8/8/8/8/8/8 b kq - 0 1")]
        public void Generates_No_Valid_Black_King_Castling_When_Castling_Blocked(uint toMove, string fen)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(fen);
            gameState.GenerateMoves(MoveGenerationMode.All, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == 4U && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        }

		[TestCase(58U, "r3k2r/8/8/8/8/8/1R6/8 b kq - 0 1")]
		[TestCase(58U, "r3k2r/8/8/8/8/8/2R5/8 b kq - 0 1")]
		[TestCase(58U, "r3k2r/8/8/8/8/8/3R4/8 b kq - 0 1")]
		[TestCase(58U, "r3k2r/8/8/8/8/8/4R3/8 b kq - 0 1")]
		[TestCase(62U, "r3k2r/8/8/8/8/8/4R3/8 b kq - 0 1")]
		[TestCase(62U, "r3k2r/8/8/8/8/8/5R2/8 b kq - 0 1")]
		[TestCase(62U, "r3k2r/8/8/8/8/8/6R1/8 b kq - 0 1")]
		public void Generates_No_Valid_Black_King_Castling_When_Castling_Attacked(uint toMove, string fen)
		{
			var gameState = GameStateUtility.LoadGameStateFromFen(fen);
			gameState.GenerateMoves(MoveGenerationMode.All, 1, _moveData);

			var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == 4U && x.GetToMove() == toMove);

			Assert.That(move, Is.EqualTo(0));
		}
        #endregion
    }
       
}
