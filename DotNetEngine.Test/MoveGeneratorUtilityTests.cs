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

            Assert.That(movingPiece, Is.EqualTo(MoveUtility.WhitePawn),"Moving Piece");
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
        public void Generates_Valid_WhitePawn_Promotion_Moves(uint fromMove, uint toMove)
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
    }
}
