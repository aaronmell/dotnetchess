using NUnit.Framework;
using DotNetEngine.Engine;

namespace DotNetEngine.Test.MakeMoveTests
{
    public class PawnTests
    {
        #region White Pawns
        [Test]
        public void MakeMove_Sets_White_Pawn_Board_Correctly()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/P7/8 w - - 0 1");

            var move = 0U;
            move = move.SetFromMove(8U);
            move = move.SetToMove(16U);
            move = move.SetMovingPiece(MoveUtility.WhitePawn);

            gameState.MakeMove(move);

            Assert.That(gameState.WhitePawns, Is.EqualTo(MoveUtility.BitStates[16]), "Piece Bitboard");
        }

        [Test]
        public void MakeMove_Sets_White_Pieces_Board_Correctly()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/P7/8 w - - 0 1");

            var move = 0U;
            move = move.SetFromMove(8U);
            move = move.SetToMove(16U);
            move = move.SetMovingPiece(MoveUtility.WhitePawn);

            gameState.MakeMove(move);

            
            Assert.That(gameState.WhitePieces, Is.EqualTo(MoveUtility.BitStates[16]), "Color Bitboard");
        }

        [Test]
        public void MakeMove_Sets_All_Pieces_Board_Correctly_When_White()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/P7/8 w - - 0 1");

            var move = 0U;
            move = move.SetFromMove(8U);
            move = move.SetToMove(16U);
            move = move.SetMovingPiece(MoveUtility.WhitePawn);

            gameState.MakeMove(move);           
            Assert.That(gameState.AllPieces, Is.EqualTo(MoveUtility.BitStates[16]), "All Bitboard");
        }

        [Test]
        public void MakeMove_Sets_EnPassant_Square_Correctly_When_White()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/P7/8 w - - 0 1");

            var move = 0U;
            move = move.SetFromMove(8U);
            move = move.SetToMove(24U);
            move = move.SetMovingPiece(MoveUtility.WhitePawn);

            gameState.MakeMove(move);
            Assert.That(gameState.EnpassantTargetSquare, Is.EqualTo(16U));
        }
        
        [Test]
        public void MakeMove_Sets_EnPassant_Opposite_Pawn_Board_Correctly_When_White()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/Pp6/8/8/8/8 w - - 0 1");

            var move = 0U;
            move = move.SetFromMove(32U);
            move = move.SetToMove(41U);
            move = move.SetMovingPiece(MoveUtility.WhitePawn);
            move = move.SetCapturedPiece(MoveUtility.BlackPawn);
            move = move.SetPromotionPiece(MoveUtility.WhitePawn);

            gameState.MakeMove(move);
            Assert.That(gameState.BlackPawns, Is.EqualTo(MoveUtility.Empty));
            Assert.That(gameState.BlackPieces, Is.EqualTo(MoveUtility.Empty));
        }

        [Test]
        public void MakeMove_Sets_50_Move_Rule_After_Pawn_Moves_When_White()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/P7/8 w - - 0 1");
            gameState.FiftyMoveRuleCount = 10;

            var move = 0U;
            move = move.SetFromMove(8U);
            move = move.SetToMove(16U);
            move = move.SetMovingPiece(MoveUtility.WhitePawn);

            gameState.MakeMove(move);
            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(0));
        }
        
        [Test]
        public void MakeMove_Sets_50_Move_Rule_After_Capture_When_White()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/P7/8 w - - 0 1");
            gameState.FiftyMoveRuleCount = 10;

            var move = 0U;
            move = move.SetFromMove(8U);
            move = move.SetToMove(17U);
            move = move.SetMovingPiece(MoveUtility.WhitePawn);
            move.SetCapturedPiece(MoveUtility.BlackPawn);

            gameState.MakeMove(move);
            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(0));
        }       

        [Test]
        public void MakeMove_Sets_White_Queen_Promotion_Piece_Correctly()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/P7/8/8/8/8/8/8 w - - 0 1");

            var move = 0U;
            move = move.SetFromMove(48U);
            move = move.SetToMove(56U);
            move = move.SetMovingPiece(MoveUtility.WhitePawn);
            move = move.SetPromotionPiece(MoveUtility.WhiteQueen);

            gameState.MakeMove(move);
            Assert.That(gameState.WhitePawns, Is.EqualTo(0), "PawnBoard");
            Assert.That(gameState.WhiteQueens, Is.EqualTo(MoveUtility.BitStates[56]), "PromotionBoard");
        }

        [Test]
        public void MakeMove_Sets_White_Knight_Promotion_Piece_Correctly()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/P7/8/8/8/8/8/8 w - - 0 1");

            var move = 0U;
            move = move.SetFromMove(48U);
            move = move.SetToMove(56U);
            move = move.SetMovingPiece(MoveUtility.WhitePawn);
            move = move.SetPromotionPiece(MoveUtility.WhiteKnight);

            gameState.MakeMove(move);
            Assert.That(gameState.WhitePawns, Is.EqualTo(0), "PawnBoard");
            Assert.That(gameState.WhiteKnights, Is.EqualTo(MoveUtility.BitStates[56]), "PromotionBoard");
        }

        [Test]
        public void MakeMove_Sets_White_Bishop_Promotion_Piece_Correctly()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/P7/8/8/8/8/8/8 w - - 0 1");

            var move = 0U;
            move = move.SetFromMove(48U);
            move = move.SetToMove(56U);
            move = move.SetMovingPiece(MoveUtility.WhitePawn);
            move = move.SetPromotionPiece(MoveUtility.WhiteBishop);

            gameState.MakeMove(move);
            Assert.That(gameState.WhitePawns, Is.EqualTo(0), "PawnBoard");
            Assert.That(gameState.WhiteBishops, Is.EqualTo(MoveUtility.BitStates[56]), "PromotionBoard");
        }

        [Test]
        public void MakeMove_Sets_White_Rook_Promotion_Piece_Correctly()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/P7/8/8/8/8/8/8 w - - 0 1");

            var move = 0U;
            move = move.SetFromMove(48U);
            move = move.SetToMove(56U);
            move = move.SetMovingPiece(MoveUtility.WhitePawn);
            move = move.SetPromotionPiece(MoveUtility.WhiteRook);

            gameState.MakeMove(move);
            Assert.That(gameState.WhitePawns, Is.EqualTo(0), "PawnBoard");
            Assert.That(gameState.WhiteRooks, Is.EqualTo(MoveUtility.BitStates[56]), "PromotionBoard");
        }     
        #endregion

        #region Black Pawns
        [Test]
        public void MakeMove_Sets_Black_Pawn_Board_Correctly()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/p7/8/8/8/8/8/8 b - - 0 1");

            var move = 0U;
            move = move.SetFromMove(48U);
            move = move.SetToMove(40U);
            move = move.SetMovingPiece(MoveUtility.BlackPawn);

            gameState.MakeMove(move);

            Assert.That(gameState.BlackPawns, Is.EqualTo(MoveUtility.BitStates[40]), "Piece Bitboard");            
        }

        [Test]
        public void MakeMove_Sets_Black_Pieces_Board_Correctly()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/p7/8/8/8/8/8/8 b - - 0 1");

            var move = 0U;
            move = move.SetFromMove(48U);
            move = move.SetToMove(40U);
            move = move.SetMovingPiece(MoveUtility.BlackPawn);

            gameState.MakeMove(move);


            Assert.That(gameState.BlackPieces, Is.EqualTo(MoveUtility.BitStates[40]), "Color Bitboard");
        }

        [Test]
        public void MakeMove_Sets_All_Pieces_Board_Correctly_When_Black()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/p7/8/8/8/8/8/8 b - - 0 1");

            var move = 0U;
            move = move.SetFromMove(48U);
            move = move.SetToMove(40U);
            move = move.SetMovingPiece(MoveUtility.BlackPawn);

            gameState.MakeMove(move);
            Assert.That(gameState.AllPieces, Is.EqualTo(MoveUtility.BitStates[40]), "All Bitboard");
        }

        [Test]
        public void MakeMove_Sets_EnPassant_Square_Correctly_When_Black()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/P7/8 b - - 0 1");

            var move = 0U;
            move = move.SetFromMove(48U);
            move = move.SetToMove(32U);
            move = move.SetMovingPiece(MoveUtility.BlackPawn);

            gameState.MakeMove(move);
            Assert.That(gameState.EnpassantTargetSquare, Is.EqualTo(40U));
        }

        [Test]
        public void MakeMove_Sets_EnPassant_Opposite_Pawn_Board_Correctly_When_Black()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/Pp6/8/8/8 b - - 0 1");

            var move = 0U;
            move = move.SetFromMove(258U);
            move = move.SetToMove(16U);
            move = move.SetMovingPiece(MoveUtility.BlackPawn);
            move = move.SetCapturedPiece(MoveUtility.WhitePawn);
            move = move.SetPromotionPiece(MoveUtility.BlackPawn);

            gameState.MakeMove(move);
            Assert.That(gameState.WhitePawns, Is.EqualTo(MoveUtility.Empty));
            Assert.That(gameState.WhitePieces, Is.EqualTo(MoveUtility.Empty));
        }

        [Test]
        public void MakeMove_Sets_50_Move_Rule_After_Pawn_Moves_When_Black()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/P7/8 b - - 0 1");
            gameState.FiftyMoveRuleCount = 10;

            var move = 0U;
            move = move.SetFromMove(48U);
            move = move.SetToMove(32U);
            move = move.SetMovingPiece(MoveUtility.BlackPawn);

            gameState.MakeMove(move);
            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(0));
        }

        [Test]
        public void MakeMove_Sets_50_Move_Rule_After_Capture_When_Black()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/P7/8 b - - 0 1");
            gameState.FiftyMoveRuleCount = 10;

            var move = 0U;
            move = move.SetFromMove(48);
            move = move.SetToMove(41U);
            move = move.SetMovingPiece(MoveUtility.BlackPawn);
            move.SetCapturedPiece(MoveUtility.WhitePawn);

            gameState.MakeMove(move);
            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(0));
        }

        [Test]
        public void MakeMove_Sets_Black_Queen_Promotion_Piece_Correctly()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/p7/8 b - - 0 1");

            var move = 0U;
            move = move.SetFromMove(8U);
            move = move.SetToMove(0U);
            move = move.SetMovingPiece(MoveUtility.BlackPawn);
            move = move.SetPromotionPiece(MoveUtility.BlackQueen);

            gameState.MakeMove(move);
            Assert.That(gameState.BlackPawns, Is.EqualTo(0), "PawnBoard");
            Assert.That(gameState.BlackQueens, Is.EqualTo(MoveUtility.BitStates[0]), "PromotionBoard");
        }

        [Test]
        public void MakeMove_Sets_Black_Knight_Promotion_Piece_Correctly()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/p7/8 b - - 0 1");

            var move = 0U;
            move = move.SetFromMove(8U);
            move = move.SetToMove(0U);
            move = move.SetMovingPiece(MoveUtility.BlackPawn);
            move = move.SetPromotionPiece(MoveUtility.BlackKnight);

            gameState.MakeMove(move);
            Assert.That(gameState.BlackPawns, Is.EqualTo(0), "PawnBoard");
            Assert.That(gameState.BlackKnights, Is.EqualTo(MoveUtility.BitStates[0]), "PromotionBoard");
        }

        [Test]
        public void MakeMove_Sets_Black_Bishop_Promotion_Piece_Correctly()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/p7/8 b - - 0 1");

            var move = 0U;
            move = move.SetFromMove(8U);
            move = move.SetToMove(0U);
            move = move.SetMovingPiece(MoveUtility.BlackPawn);
            move = move.SetPromotionPiece(MoveUtility.BlackBishop);

            gameState.MakeMove(move);
            Assert.That(gameState.BlackPawns, Is.EqualTo(0), "PawnBoard");
            Assert.That(gameState.BlackBishops, Is.EqualTo(MoveUtility.BitStates[0]), "PromotionBoard");
        }

        [Test]
        public void MakeMove_Sets_Black_Rook_Promotion_Piece_Correctly()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/p7/8 b - - 0 1");

            var move = 0U;
            move = move.SetFromMove(8U);
            move = move.SetToMove(0U);
            move = move.SetMovingPiece(MoveUtility.BlackPawn);
            move = move.SetPromotionPiece(MoveUtility.BlackRook);

            gameState.MakeMove(move);
            Assert.That(gameState.BlackPawns, Is.EqualTo(0), "PawnBoard");
            Assert.That(gameState.BlackRooks, Is.EqualTo(MoveUtility.BitStates[0]), "PromotionBoard");
        }        
        #endregion

        #region Board Array
        [TestCase("8/8/8/8/8/8/P7/8 w - - 0 1", 8U, 16U, MoveUtility.WhitePawn)]
        [TestCase("8/p7/8/8/8/8/8/8 b - - 0 1", 48U, 40U, MoveUtility.BlackPawn)]
        public void MakeMove_Sets_Board_Array_From_Square(string initialFen, uint fromSquare, uint toSquare, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen(initialFen);

            var move = 0U;
            move = move.SetFromMove(fromSquare);
            move = move.SetToMove(toSquare);
            move = move.SetMovingPiece(movingPiece);

            gameState.MakeMove(move);

            Assert.That(gameState.BoardArray[fromSquare], Is.EqualTo(MoveUtility.Empty));
        }

        [TestCase("8/8/8/8/8/8/P7/8 w - - 0 1", 8U, 16U, MoveUtility.WhitePawn)]
        [TestCase("8/p7/8/8/8/8/8/8 b - - 0 1", 48U, 40U, MoveUtility.BlackPawn)]
        public void MakeMove_Sets_Board_Array_To_Square(string initialFen, uint fromSquare, uint toSquare, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen(initialFen);

            var move = 0U;
            move = move.SetFromMove(fromSquare);
            move = move.SetToMove(toSquare);
            move = move.SetMovingPiece(movingPiece);

            gameState.MakeMove(move);

            Assert.That(gameState.BoardArray[toSquare], Is.EqualTo(movingPiece));
        }

        [TestCase("8/8/8/pP6/8/8/8/8 w - - 0 1", 33U, 40U, MoveUtility.WhitePawn, MoveUtility.BlackPawn)]
        [TestCase("8/8/8/8/pP6/8/8/8 b - - 0 1", 24U, 17U, MoveUtility.BlackPawn, MoveUtility.WhitePawn)]
        public void MakeMove_Sets_Board_Array_To_Square_After_Enpassant_Capture(string initialFen, uint fromSquare, uint toSquare, uint movingPiece, uint capturedPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen(initialFen);
            
            var move = 0U;
            move = move.SetFromMove(fromSquare);
            move = move.SetToMove(toSquare);
            move = move.SetMovingPiece(movingPiece);
            move = move.SetPromotionPiece(movingPiece);
            move = move.SetCapturedPiece(capturedPiece);            

            gameState.MakeMove(move);
            Assert.That(gameState.BoardArray[toSquare], Is.EqualTo(movingPiece));
        }

        [TestCase("8/8/8/pP6/8/8/8/8 w - - 0 1", 33U, 40U, MoveUtility.WhitePawn, MoveUtility.BlackPawn)]
        [TestCase("8/8/8/8/pP6/8/8/8 b - - 0 1", 24U, 17U, MoveUtility.BlackPawn, MoveUtility.WhitePawn)]
        public void MakeMove_Sets_Board_Array_From_Square_After_Enpassant_Capture(string initialFen, uint fromSquare, uint toSquare, uint movingPiece, uint capturedPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen(initialFen);

            var move = 0U;
            move = move.SetFromMove(fromSquare);
            move = move.SetToMove(toSquare);
            move = move.SetMovingPiece(movingPiece);
            move = move.SetPromotionPiece(movingPiece);
            move = move.SetCapturedPiece(capturedPiece);

            gameState.MakeMove(move);
            Assert.That(gameState.BoardArray[fromSquare], Is.EqualTo(MoveUtility.Empty));
        }

        [TestCase("pppppppp/pppppppp/1ppppppp/pPpppppp/pppppppp/pppppppp/pppppppp/pppppppp w - - 0 1", 33U, 40U, 32U, MoveUtility.WhitePawn, MoveUtility.BlackPawn)]
        [TestCase("PPPPPPPP/PPPPPPPP/PPPPPPPP/PPPPPPPP/pPPPPPPP/1PPPPPPP/PPPPPPPP/PPPPPPPP b - - 0 1", 24U, 17U, 25U, MoveUtility.BlackPawn, MoveUtility.WhitePawn)]
        public void MakeMove_Sets_Board_Array_Caputured_Square_After_Enpassant_Capture(string initialFen, uint fromSquare, uint toSquare, uint capturedSquare, uint movingPiece, uint capturedPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen(initialFen);

            var move = 0U;
            move = move.SetFromMove(fromSquare);
            move = move.SetToMove(toSquare);
            move = move.SetMovingPiece(movingPiece);
            move = move.SetPromotionPiece(movingPiece);
            move = move.SetCapturedPiece(capturedPiece);

            gameState.MakeMove(move);
            Assert.That(gameState.BoardArray[capturedSquare], Is.EqualTo(MoveUtility.Empty));
        }

        [TestCase("8/P7/8/8/8/8/8/8 w - - 0 1", 48U, 56U, MoveUtility.WhitePawn, MoveUtility.WhiteQueen)]
        [TestCase("8/P7/8/8/8/8/8/8 w - - 0 1", 48U, 56U, MoveUtility.WhitePawn, MoveUtility.WhiteRook)]
        [TestCase("8/P7/8/8/8/8/8/8 w - - 0 1", 48U, 56U, MoveUtility.WhitePawn, MoveUtility.WhiteKnight)]
        [TestCase("8/P7/8/8/8/8/8/8 w - - 0 1", 48U, 56U, MoveUtility.WhitePawn, MoveUtility.WhiteBishop)]
        [TestCase("8/8/8/8/8/8/p7/8 b - - 0 1", 48U, 56U, MoveUtility.BlackPawn, MoveUtility.BlackQueen)]
        [TestCase("8/8/8/8/8/8/p7/8 b - - 0 1", 48U, 56U, MoveUtility.BlackPawn, MoveUtility.BlackRook)]
        [TestCase("8/8/8/8/8/8/p7/8 b - - 0 1", 48U, 56U, MoveUtility.BlackPawn, MoveUtility.BlackKnight)]
        [TestCase("8/8/8/8/8/8/p7/8 b - - 0 1", 48U, 56U, MoveUtility.BlackPawn, MoveUtility.BlackBishop)]
        public void MakeMove_Sets_Board_Array_To_Square_On_Promotion(string fen, uint moveFrom, uint moveTo, uint movedPiece, uint promotedPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen(fen);
            
            var move = 0U;
            move = move.SetFromMove(moveFrom);
            move = move.SetToMove(moveTo);
            move = move.SetMovingPiece(movedPiece);
            move = move.SetPromotionPiece(promotedPiece);

            gameState.MakeMove(move);

            Assert.That(gameState.BoardArray[moveTo], Is.EqualTo(promotedPiece));
        }

        [TestCase("8/P7/8/8/8/8/8/8 w - - 0 1", 48U, 56U, MoveUtility.WhitePawn, MoveUtility.WhiteQueen)]
        [TestCase("8/P7/8/8/8/8/8/8 w - - 0 1", 48U, 56U, MoveUtility.WhitePawn, MoveUtility.WhiteRook)]
        [TestCase("8/P7/8/8/8/8/8/8 w - - 0 1", 48U, 56U, MoveUtility.WhitePawn, MoveUtility.WhiteKnight)]
        [TestCase("8/P7/8/8/8/8/8/8 w - - 0 1", 48U, 56U, MoveUtility.WhitePawn, MoveUtility.WhiteBishop)]
        [TestCase("8/8/8/8/8/8/p7/8 b - - 0 1", 48U, 56U, MoveUtility.BlackPawn, MoveUtility.BlackQueen)]
        [TestCase("8/8/8/8/8/8/p7/8 b - - 0 1", 48U, 56U, MoveUtility.BlackPawn, MoveUtility.BlackRook)]
        [TestCase("8/8/8/8/8/8/p7/8 b - - 0 1", 48U, 56U, MoveUtility.BlackPawn, MoveUtility.BlackKnight)]
        [TestCase("8/8/8/8/8/8/p7/8 b - - 0 1", 48U, 56U, MoveUtility.BlackPawn, MoveUtility.BlackBishop)]
        public void MakeMove_Sets_Board_Array_From_Square_On_Promotion(string fen, uint moveFrom, uint moveTo, uint movedPiece, uint promotedPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen(fen);

            var move = 0U;
            move = move.SetFromMove(moveFrom);
            move = move.SetToMove(moveTo);
            move = move.SetMovingPiece(movedPiece);
            move = move.SetPromotionPiece(promotedPiece);

            gameState.MakeMove(move);

            Assert.That(gameState.BoardArray[moveFrom], Is.EqualTo(MoveUtility.Empty));
        }
        #endregion

        //Need enpassant tests that other color pawn board was updated
    }
}
