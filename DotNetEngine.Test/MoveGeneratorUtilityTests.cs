using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using DotNetEngine.Engine;

namespace DotNetEngine.Test
{
    public class MoveGeneratorUtilityTests
    {
        private MoveData _moveData = new MoveData();

        #region White Pawn Moves
        [TestCase(8U, 16U)]
        [TestCase(9U, 17U)]
        [TestCase(10U, 18U)]
        [TestCase(11U, 19U)]
        [TestCase(12U, 20U)]
        [TestCase(13U, 21U)]
        [TestCase(14U, 22U)]
        [TestCase(15U, 23U)]
        public void Generates_Valid_WhiteSinglePawn_Moves(uint fromMove, uint toMove)
        {
            var gameState = FenUtility.LoadStateFromFen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");

            gameState.GenerateMoves(1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var promotedPiece = move.GetPromotedPiece();
            var movingPiece = move.GetMovingPiece();

            Assert.That(movingPiece, Is.EqualTo(MoveUtility.WhitePawn), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.Empty), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.Empty), "Promoted Piece");

        }

        [TestCase(8U, 24U)]
        [TestCase(9U, 25U)]
        [TestCase(10U, 26U)]
        [TestCase(11U, 27U)]
        [TestCase(12U, 28U)]
        [TestCase(13U, 29U)]
        [TestCase(14U, 30U)]
        [TestCase(15U, 31U)]
        public void Generates_Valid_WhiteDoublePawn_Moves(uint fromMove, uint toMove)
        {
            var gameState = FenUtility.LoadStateFromFen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");

            gameState.GenerateMoves(1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var promotedPiece = move.GetPromotedPiece();
            var movingPiece = move.GetMovingPiece();

            Assert.That(movingPiece, Is.EqualTo(MoveUtility.WhitePawn), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.Empty), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.Empty), "Promoted Piece");

        }

        [TestCase(8U, 17U)]
        [TestCase(9U, 16U)]
        [TestCase(9U, 18U)]
        [TestCase(10U, 17U)]
        [TestCase(10U, 19U)]
        [TestCase(11U, 18U)]
        [TestCase(11U, 20U)]
        [TestCase(12U, 19U)]
        [TestCase(12U, 21U)]
        [TestCase(13U, 20U)]
        [TestCase(13U, 22U)]
        [TestCase(14U, 21U)]
        [TestCase(14U, 23U)]
        [TestCase(15U, 22U)]
        public void Generates_Valid_WhitePawn_Capture_Moves(uint fromMove, uint toMove)
        {
            var gameState = FenUtility.LoadStateFromFen("8/8/8/8/8/pppppppp/PPPPPPPP/8 w KQkq - 0 1");

            gameState.GenerateMoves(1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var promotedPiece = move.GetPromotedPiece();
            var movingPiece = move.GetMovingPiece();

            Assert.That(movingPiece, Is.EqualTo(MoveUtility.WhitePawn), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.BlackPawn), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.Empty), "Promoted Piece");

        }


        [TestCase(48U, 56U, MoveUtility.WhiteBishop)]
        [TestCase(49U, 57U, MoveUtility.WhiteBishop)]
        [TestCase(50U, 58U, MoveUtility.WhiteBishop)]
        [TestCase(51U, 59U, MoveUtility.WhiteBishop)]
        [TestCase(52U, 60U, MoveUtility.WhiteBishop)]
        [TestCase(53U, 61U, MoveUtility.WhiteBishop)]
        [TestCase(54U, 62U, MoveUtility.WhiteBishop)]
        [TestCase(55U, 63U, MoveUtility.WhiteBishop)]
        [TestCase(48U, 56U, MoveUtility.WhiteKnight)]
        [TestCase(49U, 57U, MoveUtility.WhiteKnight)]
        [TestCase(50U, 58U, MoveUtility.WhiteKnight)]
        [TestCase(51U, 59U, MoveUtility.WhiteKnight)]
        [TestCase(52U, 60U, MoveUtility.WhiteKnight)]
        [TestCase(53U, 61U, MoveUtility.WhiteKnight)]
        [TestCase(54U, 62U, MoveUtility.WhiteKnight)]
        [TestCase(55U, 63U, MoveUtility.WhiteKnight)]
        [TestCase(48U, 56U, MoveUtility.WhiteRook)]
        [TestCase(49U, 57U, MoveUtility.WhiteRook)]
        [TestCase(50U, 58U, MoveUtility.WhiteRook)]
        [TestCase(51U, 59U, MoveUtility.WhiteRook)]
        [TestCase(52U, 60U, MoveUtility.WhiteRook)]
        [TestCase(53U, 61U, MoveUtility.WhiteRook)]
        [TestCase(54U, 62U, MoveUtility.WhiteRook)]
        [TestCase(55U, 63U, MoveUtility.WhiteRook)]
        [TestCase(48U, 56U, MoveUtility.WhiteQueen)]
        [TestCase(49U, 57U, MoveUtility.WhiteQueen)]
        [TestCase(50U, 58U, MoveUtility.WhiteQueen)]
        [TestCase(51U, 59U, MoveUtility.WhiteQueen)]
        [TestCase(52U, 60U, MoveUtility.WhiteQueen)]
        [TestCase(53U, 61U, MoveUtility.WhiteQueen)]
        [TestCase(54U, 62U, MoveUtility.WhiteQueen)]
        [TestCase(55U, 63U, MoveUtility.WhiteQueen)]
        public void Generates_Valid_WhitePawn_Promotion_Moves(uint fromMove, uint toMove, uint promotedPiece)
        {
            var gameState = FenUtility.LoadStateFromFen("8/PPPPPPPP/8/8/8/8/8/8 w KQkq - 0 1");

            gameState.GenerateMoves(1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove && x.GetPromotedPiece() == promotedPiece);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();

            Assert.That(movingPiece, Is.EqualTo(MoveUtility.WhitePawn), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.Empty), "Captured Piece");

        }

        [TestCase(32U, 41U)]
        [TestCase(33U, 42U)]
        [TestCase(34U, 43U)]
        [TestCase(35U, 44U)]
        [TestCase(36U, 45U)]
        [TestCase(37U, 46U)]
        [TestCase(38U, 47U)]
        [TestCase(39U, 46U)]
        [TestCase(38U, 45U)]
        [TestCase(37U, 44U)]
        [TestCase(36U, 43U)]
        [TestCase(35U, 42U)]
        [TestCase(34U, 41U)]
        [TestCase(33U, 40U)]
        public void Generates_Valid_WhitePawn_EnPassant_Moves(uint fromMove, uint toMove)
        {

            var gameState = new GameState();

            if (fromMove % 2 == 0)
                gameState = FenUtility.LoadStateFromFen("8/8/8/PpPpPpPp/8/8/8/8 w - - 0 1");
            else
                gameState = FenUtility.LoadStateFromFen("8/8/8/pPpPpPpP/8/8/8/8 w - - 0 1");

            gameState.EnpassantTargetSquare = toMove;
            gameState.GenerateMoves(1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();
            var promotedPiece = move.GetPromotedPiece();


            Assert.That(movingPiece, Is.EqualTo(MoveUtility.WhitePawn), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.BlackPawn), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.WhitePawn), "En Passant Piece");
        }

        [Test]
        public void Does_Not_Generate_Invalid_WhitePawn_Captures()
        {
            var gameState = new GameState();

            gameState = FenUtility.LoadStateFromFen("8/8/RRRRRRRR/PPPPPPPP/8/8/8/8 w - - 0 1");           
                       
            gameState.GenerateMoves(1, _moveData);

            Assert.That(gameState.Moves[1].Count(), Is.EqualTo(0));

        }
        #endregion

        #region Black Pawn Moves
        [TestCase(48U, 40U)]
        [TestCase(49U, 41U)]
        [TestCase(50U, 42U)]
        [TestCase(51U, 43U)]
        [TestCase(52U, 44U)]
        [TestCase(53U, 45U)]
        [TestCase(54U, 46U)]
        [TestCase(55U, 47U)]
        public void Generates_Valid_BlackSinglePawn_Moves(uint fromMove, uint toMove)
        {
            var gameState = FenUtility.LoadStateFromFen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1");

            gameState.GenerateMoves(1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var promotedPiece = move.GetPromotedPiece();
            var movingPiece = move.GetMovingPiece();

            Assert.That(movingPiece, Is.EqualTo(MoveUtility.BlackPawn), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.Empty), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.Empty), "Promoted Piece");

        }

        [TestCase(48U, 32U)]
        [TestCase(49U, 33U)]
        [TestCase(50U, 34U)]
        [TestCase(51U, 35U)]
        [TestCase(52U, 36U)]
        [TestCase(53U, 37U)]
        [TestCase(54U, 38U)]
        [TestCase(55U, 39U)]
        public void Generates_Valid_BlackDoublePawn_Moves(uint fromMove, uint toMove)
        {
            var gameState = FenUtility.LoadStateFromFen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1");

            gameState.GenerateMoves(1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var promotedPiece = move.GetPromotedPiece();
            var movingPiece = move.GetMovingPiece();

            Assert.That(movingPiece, Is.EqualTo(MoveUtility.BlackPawn), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.Empty), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.Empty), "Promoted Piece");

        }

        [TestCase(48U, 41U)]
        [TestCase(49U, 40U)]
        [TestCase(49U, 42U)]
        [TestCase(50U, 41U)]
        [TestCase(50U, 43U)]
        [TestCase(51U, 42U)]
        [TestCase(51U, 44U)]
        [TestCase(52U, 43U)]
        [TestCase(52U, 45U)]
        [TestCase(53U, 44U)]
        [TestCase(53U, 46U)]
        [TestCase(54U, 45U)]
        [TestCase(54U, 47U)]
        [TestCase(55U, 46U)]
        public void Generates_Valid_BlackPawn_Capture_Moves(uint fromMove, uint toMove)
        {
            var gameState = FenUtility.LoadStateFromFen("8/pppppppp/PPPPPPPP/8/8/8/8/8 b KQkq - 0 1");

            gameState.GenerateMoves(1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var promotedPiece = move.GetPromotedPiece();
            var movingPiece = move.GetMovingPiece();

            Assert.That(movingPiece, Is.EqualTo(MoveUtility.BlackPawn), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.WhitePawn), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.Empty), "Promoted Piece");
        }


        [TestCase(8U, 0U, MoveUtility.BlackBishop)]
        [TestCase(9U, 1U, MoveUtility.BlackBishop)]
        [TestCase(10U, 2U, MoveUtility.BlackBishop)]
        [TestCase(11U, 3U, MoveUtility.BlackBishop)]
        [TestCase(12U, 4U, MoveUtility.BlackBishop)]
        [TestCase(13U, 5U, MoveUtility.BlackBishop)]
        [TestCase(14U, 6U, MoveUtility.BlackBishop)]
        [TestCase(15U, 7U, MoveUtility.BlackBishop)]
        [TestCase(8U, 0U, MoveUtility.BlackKnight)]
        [TestCase(9U, 1U, MoveUtility.BlackKnight)]
        [TestCase(10U, 2U, MoveUtility.BlackKnight)]
        [TestCase(11U, 3U, MoveUtility.BlackKnight)]
        [TestCase(12U, 4U, MoveUtility.BlackKnight)]
        [TestCase(13U, 5U, MoveUtility.BlackKnight)]
        [TestCase(14U, 6U, MoveUtility.BlackKnight)]
        [TestCase(15U, 7U, MoveUtility.BlackKnight)]
        [TestCase(8U, 0U, MoveUtility.BlackRook)]
        [TestCase(9U, 1U, MoveUtility.BlackRook)]
        [TestCase(10U, 2U, MoveUtility.BlackRook)]
        [TestCase(11U, 3U, MoveUtility.BlackRook)]
        [TestCase(12U, 4U, MoveUtility.BlackRook)]
        [TestCase(13U, 5U, MoveUtility.BlackRook)]
        [TestCase(14U, 6U, MoveUtility.BlackRook)]
        [TestCase(15U, 7U, MoveUtility.BlackRook)]
        [TestCase(8U, 0U, MoveUtility.BlackQueen)]
        [TestCase(9U, 1U, MoveUtility.BlackQueen)]
        [TestCase(10U, 2U, MoveUtility.BlackQueen)]
        [TestCase(11U, 3U, MoveUtility.BlackQueen)]
        [TestCase(12U, 4U, MoveUtility.BlackQueen)]
        [TestCase(13U, 5U, MoveUtility.BlackQueen)]
        [TestCase(14U, 6U, MoveUtility.BlackQueen)]
        [TestCase(15U, 7U, MoveUtility.BlackQueen)]
        public void Generates_Valid_BlackPawn_Promotion_Moves(uint fromMove, uint toMove, uint promotedPiece)
        {
            var gameState = FenUtility.LoadStateFromFen("8/8/8/8/8/8/pppppppp/8 b KQkq - 0 1");

            gameState.GenerateMoves(1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove && x.GetPromotedPiece() == promotedPiece);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();

            Assert.That(movingPiece, Is.EqualTo(MoveUtility.BlackPawn), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.Empty), "Captured Piece");

        }

        [TestCase(24U, 17U)]
        [TestCase(25U, 18U)]
        [TestCase(26U, 19U)]
        [TestCase(27U, 20U)]
        [TestCase(28U, 21U)]
        [TestCase(29U, 22U)]
        [TestCase(30U, 23U)]
        [TestCase(31U, 22U)]
        [TestCase(30U, 21U)]
        [TestCase(29U, 20U)]
        [TestCase(28U, 19U)]
        [TestCase(27U, 18U)]
        [TestCase(26U, 17U)]
        [TestCase(25U, 16U)]
        public void Generates_Valid_BlackPawn_EnPassant_Moves(uint fromMove, uint toMove)
        {

            var gameState = new GameState();

            if (fromMove % 2 == 0)
                gameState = FenUtility.LoadStateFromFen("8/8/8/8/pPpPpPpP/8/8/8 b - - 0 1"); 
            else
                gameState = FenUtility.LoadStateFromFen("8/8/8/8/PpPpPpPp/8/8/8 b - - 0 1");

            gameState.EnpassantTargetSquare = toMove;
            gameState.GenerateMoves(1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();
            var promotedPiece = move.GetPromotedPiece();


            Assert.That(movingPiece, Is.EqualTo(MoveUtility.BlackPawn), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.WhitePawn), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.BlackPawn), "En Passant Piece");
        }

        [Test]
        public void Does_Not_Generate_Invalid_BlackPawn_Captures()
        {
            var gameState = new GameState();

            gameState = FenUtility.LoadStateFromFen("8/8/pppppppp/rrrrrrrr/8/8/8/8 w - - 0 1");

            gameState.GenerateMoves(1, _moveData);

            Assert.That(gameState.Moves[1].Count(), Is.EqualTo(0));

        }
        #endregion
    }
}
