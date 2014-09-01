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
    public class PawnGenerationTests
    {
        private MoveData _moveData = new MoveData();

        #region White Pawn Moves
        
        [TestCase(8U, 16U, MoveGenerationMode.All)]
        [TestCase(9U, 17U, MoveGenerationMode.All)]
        [TestCase(10U, 18U, MoveGenerationMode.All)]
        [TestCase(11U, 19U, MoveGenerationMode.All)]
        [TestCase(12U, 20U, MoveGenerationMode.All)]
        [TestCase(13U, 21U, MoveGenerationMode.All)]
        [TestCase(14U, 22U, MoveGenerationMode.All)]
        [TestCase(15U, 23U, MoveGenerationMode.All)]
        [TestCase(8U, 16U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(9U, 17U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(10U, 18U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(11U, 19U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(12U, 20U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(13U, 21U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(14U, 22U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(15U, 23U, MoveGenerationMode.QuietMovesOnly)]
        public void Generates_Valid_WhiteSinglePawn_Moves_When_Not_CapturesOnly(uint fromMove, uint toMove, MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadStateFromFen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");

            gameState.GenerateMoves(mode, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var promotedPiece = move.GetPromotedPiece();
            var movingPiece = move.GetMovingPiece();

            Assert.That(movingPiece, Is.EqualTo(MoveUtility.WhitePawn), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.Empty), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.Empty), "Promoted Piece");

        }

        [TestCase(8U, 16U)]
        [TestCase(9U, 17U)]
        [TestCase(10U, 18U)]
        [TestCase(11U, 19U)]
        [TestCase(12U, 20U)]
        [TestCase(13U, 21U)]
        [TestCase(14U, 22U)]
        [TestCase(15U, 23U)]
        public void Generates_No_WhiteSinglePawn_Moves_When_CapturesOnly(uint fromMove, uint toMove)
        {
            var gameState = GameStateUtility.LoadStateFromFen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");

            gameState.GenerateMoves(MoveGenerationMode.CaptureMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        }        

        [TestCase(8U, 24U, MoveGenerationMode.All)]
        [TestCase(9U, 25U, MoveGenerationMode.All)]
        [TestCase(10U, 26U, MoveGenerationMode.All)]
        [TestCase(11U, 27U, MoveGenerationMode.All)]
        [TestCase(12U, 28U, MoveGenerationMode.All)]
        [TestCase(13U, 29U, MoveGenerationMode.All)]
        [TestCase(14U, 30U, MoveGenerationMode.All)]
        [TestCase(15U, 31U, MoveGenerationMode.All)]
        [TestCase(8U, 24U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(9U, 25U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(10U, 26U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(11U, 27U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(12U, 28U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(13U, 29U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(14U, 30U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(15U, 31U, MoveGenerationMode.QuietMovesOnly)]
        public void Generates_Valid_WhiteDoublePawn_Moves_When_Not_CapturesOnly(uint fromMove, uint toMove, MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadStateFromFen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");

            gameState.GenerateMoves(mode, 1, _moveData);

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
        public void Generates_No_WhiteDoubleePawn_Moves_When_CapturesOnly(uint fromMove, uint toMove)
        {
            var gameState = GameStateUtility.LoadStateFromFen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");

            gameState.GenerateMoves(MoveGenerationMode.CaptureMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        } 

        [TestCase(8U, 17U, MoveGenerationMode.All)]
        [TestCase(9U, 16U, MoveGenerationMode.All)]
        [TestCase(9U, 18U, MoveGenerationMode.All)]
        [TestCase(10U, 17U, MoveGenerationMode.All)]
        [TestCase(10U, 19U, MoveGenerationMode.All)]
        [TestCase(11U, 18U, MoveGenerationMode.All)]
        [TestCase(11U, 20U, MoveGenerationMode.All)]
        [TestCase(12U, 19U, MoveGenerationMode.All)]
        [TestCase(12U, 21U, MoveGenerationMode.All)]
        [TestCase(13U, 20U, MoveGenerationMode.All)]
        [TestCase(13U, 22U, MoveGenerationMode.All)]
        [TestCase(14U, 21U, MoveGenerationMode.All)]
        [TestCase(14U, 23U, MoveGenerationMode.All)]
        [TestCase(15U, 22U, MoveGenerationMode.All)]
        [TestCase(8U, 17U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(9U, 16U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(9U, 18U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(10U, 17U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(10U, 19U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(11U, 18U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(11U, 20U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(12U, 19U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(12U, 21U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(13U, 20U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(13U, 22U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(14U, 21U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(14U, 23U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(15U, 22U, MoveGenerationMode.CaptureMovesOnly)]
        public void Generates_Valid_White_Pawn_Capture_Moves_When_Not_QuietMovesOnly(uint fromMove, uint toMove, MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/pppppppp/PPPPPPPP/8 w KQkq - 0 1");

            gameState.GenerateMoves(mode, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var promotedPiece = move.GetPromotedPiece();
            var movingPiece = move.GetMovingPiece();

            Assert.That(movingPiece, Is.EqualTo(MoveUtility.WhitePawn), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.BlackPawn), "Captured Piece");
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
        public void Generates_No_Valid_White_Pawn_Capture_Moves_When_QuietMovesOnly(uint fromMove, uint toMove)
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/pppppppp/PPPPPPPP/8 w KQkq - 0 1");

            gameState.GenerateMoves(MoveGenerationMode.QuietMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
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
        public void Generates_Valid_White_Pawn_Promotion_Moves_When_All(uint fromMove, uint toMove, uint promotedPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/PPPPPPPP/8/8/8/8/8/8 w KQkq - 0 1");

            gameState.GenerateMoves(MoveGenerationMode.All, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove && x.GetPromotedPiece() == promotedPiece);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();

            Assert.That(movingPiece, Is.EqualTo(MoveUtility.WhitePawn), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.Empty), "Captured Piece");
        }

        [TestCase(48U, 56U, MoveUtility.WhiteKnight)]
        [TestCase(49U, 57U, MoveUtility.WhiteKnight)]
        [TestCase(50U, 58U, MoveUtility.WhiteKnight)]
        [TestCase(51U, 59U, MoveUtility.WhiteKnight)]
        [TestCase(52U, 60U, MoveUtility.WhiteKnight)]
        [TestCase(53U, 61U, MoveUtility.WhiteKnight)]
        [TestCase(54U, 62U, MoveUtility.WhiteKnight)]
        [TestCase(55U, 63U, MoveUtility.WhiteKnight)]
        [TestCase(48U, 56U, MoveUtility.WhiteQueen)]
        [TestCase(49U, 57U, MoveUtility.WhiteQueen)]
        [TestCase(50U, 58U, MoveUtility.WhiteQueen)]
        [TestCase(51U, 59U, MoveUtility.WhiteQueen)]
        [TestCase(52U, 60U, MoveUtility.WhiteQueen)]
        [TestCase(53U, 61U, MoveUtility.WhiteQueen)]
        [TestCase(54U, 62U, MoveUtility.WhiteQueen)]
        [TestCase(55U, 63U, MoveUtility.WhiteQueen)]
        public void Generates_Valid_White_Pawn_Promotion_Moves_When_QuietMovesOnly(uint fromMove, uint toMove, uint promotedPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/PPPPPPPP/8/8/8/8/8/8 w KQkq - 0 1");

            gameState.GenerateMoves(MoveGenerationMode.QuietMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove && x.GetPromotedPiece() == promotedPiece);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();

            Assert.That(movingPiece, Is.EqualTo(MoveUtility.WhitePawn), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.Empty), "Captured Piece");
        }

        [TestCase(48U, 56U, MoveUtility.WhiteBishop)]
        [TestCase(49U, 57U, MoveUtility.WhiteBishop)]
        [TestCase(50U, 58U, MoveUtility.WhiteBishop)]
        [TestCase(51U, 59U, MoveUtility.WhiteBishop)]
        [TestCase(52U, 60U, MoveUtility.WhiteBishop)]
        [TestCase(53U, 61U, MoveUtility.WhiteBishop)]
        [TestCase(54U, 62U, MoveUtility.WhiteBishop)]
        [TestCase(55U, 63U, MoveUtility.WhiteBishop)]
        [TestCase(48U, 56U, MoveUtility.WhiteRook)]
        [TestCase(49U, 57U, MoveUtility.WhiteRook)]
        [TestCase(50U, 58U, MoveUtility.WhiteRook)]
        [TestCase(51U, 59U, MoveUtility.WhiteRook)]
        [TestCase(52U, 60U, MoveUtility.WhiteRook)]
        [TestCase(53U, 61U, MoveUtility.WhiteRook)]
        [TestCase(54U, 62U, MoveUtility.WhiteRook)]
        [TestCase(55U, 63U, MoveUtility.WhiteRook)]
        public void Generates_No_Rook_Or_Bishop_Valid_White_Pawn_Promotion_Moves_When_QuietMovesOnly(uint fromMove, uint toMove, uint promotedPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/PPPPPPPP/8/8/8/8/8/8 w KQkq - 0 1");

            gameState.GenerateMoves(MoveGenerationMode.QuietMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove && x.GetPromotedPiece() == promotedPiece);

            Assert.That(move, Is.EqualTo(0));            
        }

        [TestCase(48U, 57U, MoveUtility.WhiteKnight)]
        [TestCase(49U, 58U, MoveUtility.WhiteKnight)]
        [TestCase(50U, 59U, MoveUtility.WhiteKnight)]
        [TestCase(51U, 60U, MoveUtility.WhiteKnight)]
        [TestCase(52U, 61U, MoveUtility.WhiteKnight)]
        [TestCase(53U, 62U, MoveUtility.WhiteKnight)]
        [TestCase(54U, 63U, MoveUtility.WhiteKnight)]
        [TestCase(55U, 62U, MoveUtility.WhiteKnight)]
        [TestCase(54U, 61U, MoveUtility.WhiteKnight)]
        [TestCase(53U, 60U, MoveUtility.WhiteKnight)]
        [TestCase(52U, 59U, MoveUtility.WhiteKnight)]
        [TestCase(51U, 58U, MoveUtility.WhiteKnight)]
        [TestCase(50U, 57U, MoveUtility.WhiteKnight)]
        [TestCase(49U, 56U, MoveUtility.WhiteKnight)]
        [TestCase(48U, 57U, MoveUtility.WhiteQueen)]
        [TestCase(49U, 58U, MoveUtility.WhiteQueen)]
        [TestCase(50U, 59U, MoveUtility.WhiteQueen)]
        [TestCase(51U, 60U, MoveUtility.WhiteQueen)]
        [TestCase(52U, 61U, MoveUtility.WhiteQueen)]
        [TestCase(53U, 62U, MoveUtility.WhiteQueen)]
        [TestCase(54U, 63U, MoveUtility.WhiteQueen)]
        [TestCase(55U, 62U, MoveUtility.WhiteQueen)]
        [TestCase(54U, 61U, MoveUtility.WhiteQueen)]
        [TestCase(53U, 60U, MoveUtility.WhiteQueen)]
        [TestCase(52U, 59U, MoveUtility.WhiteQueen)]
        [TestCase(51U, 58U, MoveUtility.WhiteQueen)]
        [TestCase(50U, 57U, MoveUtility.WhiteQueen)]
        [TestCase(49U, 56U, MoveUtility.WhiteQueen)]
        public void Generates_Valid_White_Pawn_Promotion_Moves_When_CaptureMovesOnly(uint fromMove, uint toMove, uint promotedPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen("pppppppp/PPPPPPPP/8/8/8/8/8/8 w KQkq - 0 1");

            gameState.GenerateMoves(MoveGenerationMode.CaptureMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove && x.GetPromotedPiece() == promotedPiece);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();

            Assert.That(movingPiece, Is.EqualTo(MoveUtility.WhitePawn), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.BlackPawn), "Captured Piece");
        }

        [TestCase(48U, 57U, MoveUtility.WhiteBishop)]
        [TestCase(49U, 58U, MoveUtility.WhiteBishop)]
        [TestCase(50U, 59U, MoveUtility.WhiteBishop)]
        [TestCase(51U, 60U, MoveUtility.WhiteBishop)]
        [TestCase(52U, 61U, MoveUtility.WhiteBishop)]
        [TestCase(53U, 62U, MoveUtility.WhiteBishop)]
        [TestCase(54U, 63U, MoveUtility.WhiteBishop)]
        [TestCase(55U, 62U, MoveUtility.WhiteBishop)]
        [TestCase(54U, 61U, MoveUtility.WhiteBishop)]
        [TestCase(53U, 60U, MoveUtility.WhiteBishop)]
        [TestCase(52U, 59U, MoveUtility.WhiteBishop)]
        [TestCase(51U, 58U, MoveUtility.WhiteBishop)]
        [TestCase(50U, 57U, MoveUtility.WhiteBishop)]
        [TestCase(49U, 56U, MoveUtility.WhiteBishop)]
        [TestCase(48U, 57U, MoveUtility.WhiteRook)]
        [TestCase(49U, 58U, MoveUtility.WhiteRook)]
        [TestCase(50U, 59U, MoveUtility.WhiteRook)]
        [TestCase(51U, 60U, MoveUtility.WhiteRook)]
        [TestCase(52U, 61U, MoveUtility.WhiteRook)]
        [TestCase(53U, 62U, MoveUtility.WhiteRook)]
        [TestCase(54U, 63U, MoveUtility.WhiteRook)]
        [TestCase(55U, 62U, MoveUtility.WhiteRook)]
        [TestCase(54U, 61U, MoveUtility.WhiteRook)]
        [TestCase(53U, 60U, MoveUtility.WhiteRook)]
        [TestCase(52U, 59U, MoveUtility.WhiteRook)]
        [TestCase(51U, 58U, MoveUtility.WhiteRook)]
        [TestCase(50U, 57U, MoveUtility.WhiteRook)]
        [TestCase(49U, 56U, MoveUtility.WhiteRook)]
        public void Generates_No_Rook_Or_Bishop_White_Pawn_Promotion_Moves_When_CaptureMovesOnly(uint fromMove, uint toMove, uint promotedPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen("pppppppp/PPPPPPPP/8/8/8/8/8/8 w KQkq - 0 1");

            gameState.GenerateMoves(MoveGenerationMode.CaptureMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove && x.GetPromotedPiece() == promotedPiece);

            Assert.That(move, Is.EqualTo(0));
        }

        [TestCase(32U, 41U, MoveGenerationMode.All)]
        [TestCase(33U, 42U, MoveGenerationMode.All)]
        [TestCase(34U, 43U, MoveGenerationMode.All)]
        [TestCase(35U, 44U, MoveGenerationMode.All)]
        [TestCase(36U, 45U, MoveGenerationMode.All)]
        [TestCase(37U, 46U, MoveGenerationMode.All)]
        [TestCase(38U, 47U, MoveGenerationMode.All)]
        [TestCase(39U, 46U, MoveGenerationMode.All)]
        [TestCase(38U, 45U, MoveGenerationMode.All)]
        [TestCase(37U, 44U, MoveGenerationMode.All)]
        [TestCase(36U, 43U, MoveGenerationMode.All)]
        [TestCase(35U, 42U, MoveGenerationMode.All)]
        [TestCase(34U, 41U, MoveGenerationMode.All)]
        [TestCase(33U, 40U, MoveGenerationMode.All)]
        [TestCase(32U, 41U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(33U, 42U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(34U, 43U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(35U, 44U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(36U, 45U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(37U, 46U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(38U, 47U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(39U, 46U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(38U, 45U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(37U, 44U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(36U, 43U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(35U, 42U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(34U, 41U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(33U, 40U, MoveGenerationMode.CaptureMovesOnly)]
        public void Generates_Valid_White_Pawn_EnPassant_Moves_When_Not_QuietMovesOnly(uint fromMove, uint toMove, MoveGenerationMode mode)
        {
            var gameState = new GameState();

            if (fromMove % 2 == 0)
                gameState = GameStateUtility.LoadStateFromFen("8/8/8/PpPpPpPp/8/8/8/8 w - - 0 1");
            else
                gameState = GameStateUtility.LoadStateFromFen("8/8/8/pPpPpPpP/8/8/8/8 w - - 0 1");

            gameState.EnpassantTargetSquare = toMove;
            gameState.GenerateMoves(mode, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();
            var promotedPiece = move.GetPromotedPiece();


            Assert.That(movingPiece, Is.EqualTo(MoveUtility.WhitePawn), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.BlackPawn), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.WhitePawn), "En Passant Piece");
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
        public void Generates_No_Valid_White_Pawn_EnPassant_Moves_When_QuietMovesOnly(uint fromMove, uint toMove)
        {
            var gameState = new GameState();

            if (fromMove % 2 == 0)
                gameState = GameStateUtility.LoadStateFromFen("8/8/8/PpPpPpPp/8/8/8/8 w - - 0 1");
            else
                gameState = GameStateUtility.LoadStateFromFen("8/8/8/pPpPpPpP/8/8/8/8 w - - 0 1");

            gameState.EnpassantTargetSquare = toMove;
            gameState.GenerateMoves(MoveGenerationMode.QuietMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        }

        [TestCase(MoveGenerationMode.All)]
        [TestCase(MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(MoveGenerationMode.QuietMovesOnly)]
        public void Does_Not_Generate_Invalid_White_Pawn_Captures(MoveGenerationMode  mode)
        {
            var gameState = new GameState();

            gameState = GameStateUtility.LoadStateFromFen("8/8/RRRRRRRR/PPPPPPPP/8/8/8/8 w - - 0 1");           
                       
            gameState.GenerateMoves(mode, 1, _moveData);

            Assert.That(gameState.Moves[1].Count(), Is.EqualTo(0));

        }
        #endregion

        #region Black Pawn Moves
        [TestCase(48U, 40U, MoveGenerationMode.All)]
        [TestCase(49U, 41U, MoveGenerationMode.All)]
        [TestCase(50U, 42U, MoveGenerationMode.All)]
        [TestCase(51U, 43U, MoveGenerationMode.All)]
        [TestCase(52U, 44U, MoveGenerationMode.All)]
        [TestCase(53U, 45U, MoveGenerationMode.All)]
        [TestCase(54U, 46U, MoveGenerationMode.All)]
        [TestCase(55U, 47U, MoveGenerationMode.All)]
        [TestCase(48U, 40U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(49U, 41U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(50U, 42U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(51U, 43U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(52U, 44U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(53U, 45U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(54U, 46U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(55U, 47U, MoveGenerationMode.QuietMovesOnly)]
        public void Generates_Valid_BlackSinglePawn_Moves_When_Not_CapturesOnly(uint fromMove, uint toMove, MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadStateFromFen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1");

            gameState.GenerateMoves(mode, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var promotedPiece = move.GetPromotedPiece();
            var movingPiece = move.GetMovingPiece();

            Assert.That(movingPiece, Is.EqualTo(MoveUtility.BlackPawn), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.Empty), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.Empty), "Promoted Piece");

        }

        [TestCase(48U, 40U)]
        [TestCase(49U, 41U)]
        [TestCase(50U, 42U)]
        [TestCase(51U, 43U)]
        [TestCase(52U, 44U)]
        [TestCase(53U, 45U)]
        [TestCase(54U, 46U)]
        [TestCase(55U, 47U)]
        public void Generates_No_BlackSinglePawn_Moves_When_CapturesOnly(uint fromMove, uint toMove)
        {
            var gameState = GameStateUtility.LoadStateFromFen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1");

            gameState.GenerateMoves(MoveGenerationMode.CaptureMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        }  

        [TestCase(48U, 32U, MoveGenerationMode.All)]
        [TestCase(49U, 33U, MoveGenerationMode.All)]
        [TestCase(50U, 34U, MoveGenerationMode.All)]
        [TestCase(51U, 35U, MoveGenerationMode.All)]
        [TestCase(52U, 36U, MoveGenerationMode.All)]
        [TestCase(53U, 37U, MoveGenerationMode.All)]
        [TestCase(54U, 38U, MoveGenerationMode.All)]
        [TestCase(55U, 39U, MoveGenerationMode.All)]
        [TestCase(48U, 32U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(49U, 33U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(50U, 34U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(51U, 35U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(52U, 36U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(53U, 37U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(54U, 38U, MoveGenerationMode.QuietMovesOnly)]
        [TestCase(55U, 39U, MoveGenerationMode.QuietMovesOnly)]
        public void Generates_Valid_BlackDoublePawn_Moves_When_Not_CapturesOnly(uint fromMove, uint toMove, MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadStateFromFen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1");

            gameState.GenerateMoves(mode, 1, _moveData);

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
        public void Generates_No_BlackDoubleePawn_Moves_When_CapturesOnly(uint fromMove, uint toMove)
        {
            var gameState = GameStateUtility.LoadStateFromFen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1");

            gameState.GenerateMoves(MoveGenerationMode.CaptureMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        } 

        [TestCase(48U, 41U, MoveGenerationMode.All)]
        [TestCase(49U, 40U, MoveGenerationMode.All)]
        [TestCase(49U, 42U, MoveGenerationMode.All)]
        [TestCase(50U, 41U, MoveGenerationMode.All)]
        [TestCase(50U, 43U, MoveGenerationMode.All)]
        [TestCase(51U, 42U, MoveGenerationMode.All)]
        [TestCase(51U, 44U, MoveGenerationMode.All)]
        [TestCase(52U, 43U, MoveGenerationMode.All)]
        [TestCase(52U, 45U, MoveGenerationMode.All)]
        [TestCase(53U, 44U, MoveGenerationMode.All)]
        [TestCase(53U, 46U, MoveGenerationMode.All)]
        [TestCase(54U, 45U, MoveGenerationMode.All)]
        [TestCase(54U, 47U, MoveGenerationMode.All)]
        [TestCase(55U, 46U, MoveGenerationMode.All)]
        [TestCase(48U, 41U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(49U, 40U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(49U, 42U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(50U, 41U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(50U, 43U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(51U, 42U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(51U, 44U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(52U, 43U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(52U, 45U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(53U, 44U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(53U, 46U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(54U, 45U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(54U, 47U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(55U, 46U, MoveGenerationMode.CaptureMovesOnly)]
         public void Generates_Valid_Black_PawnCapture_Moves_When_Not_QuietMovesOnly(uint fromMove, uint toMove, MoveGenerationMode mode)
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/pppppppp/PPPPPPPP/8/8/8/8/8 b KQkq - 0 1");

            gameState.GenerateMoves(MoveGenerationMode.All, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var promotedPiece = move.GetPromotedPiece();
            var movingPiece = move.GetMovingPiece();

            Assert.That(movingPiece, Is.EqualTo(MoveUtility.BlackPawn), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.WhitePawn), "Captured Piece");
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
        public void Generates_No_Valid_Black_Pawn_Capture_Moves_When_QuietMovesOnly(uint fromMove, uint toMove)
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/pppppppp/PPPPPPPP/8 b KQkq - 0 1");

            gameState.GenerateMoves(MoveGenerationMode.QuietMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
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
        public void Generates_Valid_Black_PawnPromotion_Moves_When_All(uint fromMove, uint toMove, uint promotedPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/pppppppp/8 b KQkq - 0 1");

            gameState.GenerateMoves(MoveGenerationMode.All, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove && x.GetPromotedPiece() == promotedPiece);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();

            Assert.That(movingPiece, Is.EqualTo(MoveUtility.BlackPawn), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.Empty), "Captured Piece");

        }

        [TestCase(8U, 0U, MoveUtility.BlackKnight)]
        [TestCase(9U, 1U, MoveUtility.BlackKnight)]
        [TestCase(10U, 2U, MoveUtility.BlackKnight)]
        [TestCase(11U, 3U, MoveUtility.BlackKnight)]
        [TestCase(12U, 4U, MoveUtility.BlackKnight)]
        [TestCase(13U, 5U, MoveUtility.BlackKnight)]
        [TestCase(14U, 6U, MoveUtility.BlackKnight)]
        [TestCase(15U, 7U, MoveUtility.BlackKnight)]
        [TestCase(8U, 0U, MoveUtility.BlackQueen)]
        [TestCase(9U, 1U, MoveUtility.BlackQueen)]
        [TestCase(10U, 2U, MoveUtility.BlackQueen)]
        [TestCase(11U, 3U, MoveUtility.BlackQueen)]
        [TestCase(12U, 4U, MoveUtility.BlackQueen)]
        [TestCase(13U, 5U, MoveUtility.BlackQueen)]
        [TestCase(14U, 6U, MoveUtility.BlackQueen)]
        [TestCase(15U, 7U, MoveUtility.BlackQueen)]
        public void Generates_Valid_Black_Pawn_Promotion_Moves_When_QuietMovesOnly(uint fromMove, uint toMove, uint promotedPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/pppppppp/8 b KQkq - 0 1");

            gameState.GenerateMoves(MoveGenerationMode.All, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove && x.GetPromotedPiece() == promotedPiece);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();

            Assert.That(movingPiece, Is.EqualTo(MoveUtility.BlackPawn), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.Empty), "Captured Piece");
        }

        [TestCase(8U, 0U, MoveUtility.BlackBishop)]
        [TestCase(9U, 1U, MoveUtility.BlackBishop)]
        [TestCase(10U, 2U, MoveUtility.BlackBishop)]
        [TestCase(11U, 3U, MoveUtility.BlackBishop)]
        [TestCase(12U, 4U, MoveUtility.BlackBishop)]
        [TestCase(13U, 5U, MoveUtility.BlackBishop)]
        [TestCase(14U, 6U, MoveUtility.BlackBishop)]
        [TestCase(15U, 7U, MoveUtility.BlackBishop)]
        [TestCase(8U, 0U, MoveUtility.BlackRook)]
        [TestCase(9U, 1U, MoveUtility.BlackRook)]
        [TestCase(10U, 2U, MoveUtility.BlackRook)]
        [TestCase(11U, 3U, MoveUtility.BlackRook)]
        [TestCase(12U, 4U, MoveUtility.BlackRook)]
        [TestCase(13U, 5U, MoveUtility.BlackRook)]
        [TestCase(14U, 6U, MoveUtility.BlackRook)]
        [TestCase(15U, 7U, MoveUtility.BlackRook)]
        public void Generates_No_Valid_Rook_Or_Bishop_Black_Pawn_Promotion_Moves_When_QuietMovesOnly(uint fromMove, uint toMove, uint promotedPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/pppppppp/8 b KQkq - 0 1");

            gameState.GenerateMoves(MoveGenerationMode.QuietMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove && x.GetPromotedPiece() == promotedPiece);

            Assert.That(move, Is.EqualTo(0));
        }

        [TestCase(8U, 1U, MoveUtility.BlackQueen)]
        [TestCase(9U, 2U, MoveUtility.BlackQueen)]
        [TestCase(10U, 3U, MoveUtility.BlackQueen)]
        [TestCase(11U, 4U, MoveUtility.BlackQueen)]
        [TestCase(12U, 5U, MoveUtility.BlackQueen)]
        [TestCase(13U, 6U, MoveUtility.BlackQueen)]
        [TestCase(14U, 7U, MoveUtility.BlackQueen)]
        [TestCase(15U, 6U, MoveUtility.BlackQueen)]
        [TestCase(14U, 5U, MoveUtility.BlackQueen)]
        [TestCase(13U, 4U, MoveUtility.BlackQueen)]
        [TestCase(12U, 3U, MoveUtility.BlackQueen)]
        [TestCase(11U, 2U, MoveUtility.BlackQueen)]
        [TestCase(10U, 1U, MoveUtility.BlackQueen)]
        [TestCase(9U, 0U, MoveUtility.BlackQueen)]
        [TestCase(8U, 1U, MoveUtility.BlackKnight)]
        [TestCase(9U, 2U, MoveUtility.BlackKnight)]
        [TestCase(10U, 3U, MoveUtility.BlackKnight)]
        [TestCase(11U, 4U, MoveUtility.BlackKnight)]
        [TestCase(12U, 5U, MoveUtility.BlackKnight)]
        [TestCase(13U, 6U, MoveUtility.BlackKnight)]
        [TestCase(14U, 7U, MoveUtility.BlackKnight)]
        [TestCase(15U, 6U, MoveUtility.BlackKnight)]
        [TestCase(14U, 5U, MoveUtility.BlackKnight)]
        [TestCase(13U, 4U, MoveUtility.BlackKnight)]
        [TestCase(12U, 3U, MoveUtility.BlackKnight)]
        [TestCase(11U, 2U, MoveUtility.BlackKnight)]
        [TestCase(10U, 1U, MoveUtility.BlackKnight)]
        [TestCase(9U, 0U, MoveUtility.BlackKnight)]    
        public void Generates_Valid_Black_Pawn_Promotion_Moves_When_CaptureMovesOnly(uint fromMove, uint toMove, uint promotedPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/pppppppp/PPPPPPPP b KQkq - 0 1");

            gameState.GenerateMoves(MoveGenerationMode.CaptureMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove && x.GetPromotedPiece() == promotedPiece);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();

            Assert.That(movingPiece, Is.EqualTo(MoveUtility.BlackPawn), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.WhitePawn), "Captured Piece");
        }

        [TestCase(8U, 1U, MoveUtility.BlackBishop)]
        [TestCase(9U, 2U, MoveUtility.BlackBishop)]
        [TestCase(10U, 3U, MoveUtility.BlackBishop)]
        [TestCase(11U, 4U, MoveUtility.BlackBishop)]
        [TestCase(12U, 5U, MoveUtility.BlackBishop)]
        [TestCase(13U, 6U, MoveUtility.BlackBishop)]
        [TestCase(14U, 7U, MoveUtility.BlackBishop)]
        [TestCase(15U, 6U, MoveUtility.BlackBishop)]
        [TestCase(14U, 5U, MoveUtility.BlackBishop)]
        [TestCase(13U, 4U, MoveUtility.BlackBishop)]
        [TestCase(12U, 3U, MoveUtility.BlackBishop)]
        [TestCase(11U, 2U, MoveUtility.BlackBishop)]
        [TestCase(10U, 1U, MoveUtility.BlackBishop)]
        [TestCase(9U, 0U, MoveUtility.BlackBishop)]
        [TestCase(8U, 1U, MoveUtility.BlackRook)]
        [TestCase(9U, 2U, MoveUtility.BlackRook)]
        [TestCase(10U, 3U, MoveUtility.BlackRook)]
        [TestCase(11U, 4U, MoveUtility.BlackRook)]
        [TestCase(12U, 5U, MoveUtility.BlackRook)]
        [TestCase(13U, 6U, MoveUtility.BlackRook)]
        [TestCase(14U, 7U, MoveUtility.BlackRook)]
        [TestCase(15U, 6U, MoveUtility.BlackRook)]
        [TestCase(14U, 5U, MoveUtility.BlackRook)]
        [TestCase(13U, 4U, MoveUtility.BlackRook)]
        [TestCase(12U, 3U, MoveUtility.BlackRook)]
        [TestCase(11U, 2U, MoveUtility.BlackRook)]
        [TestCase(10U, 1U, MoveUtility.BlackRook)]
        [TestCase(9U, 0U, MoveUtility.BlackRook)]    
        public void Generates_No_Valid_Rook_Or_Bishop_Promotions_From_Black_Pawn_When_CaptureMovesOnly(uint fromMove, uint toMove, uint promotedPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/pppppppp/PPPPPPPP b KQkq - 0 1");

            gameState.GenerateMoves(MoveGenerationMode.CaptureMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove && x.GetPromotedPiece() == promotedPiece);

            Assert.That(move, Is.EqualTo(0));
        }

        [TestCase(24U, 17U, MoveGenerationMode.All)]
        [TestCase(25U, 18U, MoveGenerationMode.All)]
        [TestCase(26U, 19U, MoveGenerationMode.All)]
        [TestCase(27U, 20U, MoveGenerationMode.All)]
        [TestCase(28U, 21U, MoveGenerationMode.All)]
        [TestCase(29U, 22U, MoveGenerationMode.All)]
        [TestCase(30U, 23U, MoveGenerationMode.All)]
        [TestCase(31U, 22U, MoveGenerationMode.All)]
        [TestCase(30U, 21U, MoveGenerationMode.All)]
        [TestCase(29U, 20U, MoveGenerationMode.All)]
        [TestCase(28U, 19U, MoveGenerationMode.All)]
        [TestCase(27U, 18U, MoveGenerationMode.All)]
        [TestCase(26U, 17U, MoveGenerationMode.All)]
        [TestCase(25U, 16U, MoveGenerationMode.All)]
        [TestCase(24U, 17U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(25U, 18U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(26U, 19U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 20U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(28U, 21U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(29U, 22U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(30U, 23U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(31U, 22U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(30U, 21U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(29U, 20U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(28U, 19U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(27U, 18U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(26U, 17U, MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(25U, 16U, MoveGenerationMode.CaptureMovesOnly)]
        public void Generates_Valid_Black_Pawn_EnPassant_Moves_When_Not_QuietMovesOnly(uint fromMove, uint toMove, MoveGenerationMode mode)
        {
            var gameState = new GameState();

            if (fromMove % 2 == 0)
                gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/pPpPpPpP/8/8/8 b - - 0 1"); 
            else
                gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/PpPpPpPp/8/8/8 b - - 0 1");

            gameState.EnpassantTargetSquare = toMove;
            gameState.GenerateMoves(MoveGenerationMode.All, 1, _moveData);

            var move = gameState.Moves[1].First(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            var capturedPiece = move.GetCapturedPiece();
            var movingPiece = move.GetMovingPiece();
            var promotedPiece = move.GetPromotedPiece();

            Assert.That(movingPiece, Is.EqualTo(MoveUtility.BlackPawn), "Moving Piece");
            Assert.That(capturedPiece, Is.EqualTo(MoveUtility.WhitePawn), "Captured Piece");
            Assert.That(promotedPiece, Is.EqualTo(MoveUtility.BlackPawn), "En Passant Piece");
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
        public void Generates_No_Valid_Black_Pawn_EnPassant_Moves_When_QuietMovesOnly(uint fromMove, uint toMove)
        {
            var gameState = new GameState();

            if (fromMove % 2 == 0)
                gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/pPpPpPpP/8/8/8 b - - 0 1");
            else
                gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/PpPpPpPp/8/8/8 b - - 0 1");

            gameState.EnpassantTargetSquare = toMove;
            gameState.GenerateMoves(MoveGenerationMode.QuietMovesOnly, 1, _moveData);

            var move = gameState.Moves[1].FirstOrDefault(x => x.GetFromMove() == fromMove && x.GetToMove() == toMove);

            Assert.That(move, Is.EqualTo(0));
        }

        [TestCase(MoveGenerationMode.All)]
        [TestCase(MoveGenerationMode.CaptureMovesOnly)]
        [TestCase(MoveGenerationMode.QuietMovesOnly)]
        public void Does_Not_Generate_Invalid_Black_PawnCaptures(MoveGenerationMode mode)
        {
            var gameState = new GameState();

            gameState = GameStateUtility.LoadStateFromFen("8/8/pppppppp/rrrrrrrr/8/8/8/8 w - - 0 1");

            gameState.GenerateMoves(mode, 1, _moveData);

            Assert.That(gameState.Moves[1].Count(), Is.EqualTo(0));

        }
        #endregion
    }
}
