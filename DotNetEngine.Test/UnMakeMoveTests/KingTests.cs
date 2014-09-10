using DotNetEngine.Engine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEngine.Test.UnUnMakeMoveTests
{
    public class KingTests
    {
        private readonly MoveData _moveData = new MoveData();

        #region White King
        [Test]
        public void UnMakeMove_Sets_Kings_Bitboard_When_White_King_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/3K4/8/8 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteKing);

            gameState.UnMakeMove(move);

            Assert.That(gameState.WhiteKing, Is.EqualTo(MoveUtility.BitStates[11]), "Piece Bitboard");
        }

        [Test]
        public void UnMakeMove_Sets_White_Bitboard_When_White_King_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/3K4/8/8 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteKing);

            gameState.UnMakeMove(move);

            Assert.That(gameState.WhitePieces, Is.EqualTo(MoveUtility.BitStates[11]), "Piece Bitboard");
        }

        [Test]
        public void UnMakeMove_Sets_AllPieces_Bitboard_When_White_King_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/3K4/8/8 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteKing);

            gameState.UnMakeMove(move);

            Assert.That(gameState.AllPieces, Is.EqualTo(MoveUtility.BitStates[11]), "All Pieces Bitboard");
        }

        [Test]
        public void UnMakeMove_Sets_CastleStatus_When_White_King_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/4K3/R6R b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord
                {
                    CurrentWhiteCastleStatus = (int)CastleStatus.BothCastle
                });

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteKing);

            gameState.UnMakeMove(move);

            Assert.That(gameState.CurrentWhiteCastleStatus, Is.EqualTo((int)CastleStatus.BothCastle), "Castle Status");
        }

        [Test]
        public void UnMakeMove_Sets_Rook_BitBoard_When_CastleOO()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/8/5RK1 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            uint move = _moveData.WhiteCastleOOMove;

            gameState.UnMakeMove(move);

            Assert.That(gameState.WhiteRooks, Is.EqualTo(MoveUtility.BitStates[7]));
        }

        [Test]
        public void UnMakeMove_Sets_White_BitBoard_When_CastleOO()
        {

            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/8/5RK1 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            uint move = _moveData.WhiteCastleOOMove;

            gameState.UnMakeMove(move);

            Assert.That(gameState.WhitePieces, Is.EqualTo(MoveUtility.BitStates[4] | MoveUtility.BitStates[7]));
        }

        [Test]
        public void UnMakeMove_Sets_Rook_BitBoard_When_CastleOOO()
        {

            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/8/2KR4 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            uint move = _moveData.WhiteCastleOOOMove;

            gameState.UnMakeMove(move);

            Assert.That(gameState.WhiteRooks, Is.EqualTo(MoveUtility.BitStates[0]));
        }

        [Test]
        public void UnMakeMove_Sets_White_BitBoard_When_CastleOOO()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/8/2KR4 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            uint move = _moveData.WhiteCastleOOOMove;

            gameState.UnMakeMove(move);

            Assert.That(gameState.WhitePieces, Is.EqualTo(MoveUtility.BitStates[0] | MoveUtility.BitStates[4]));
        }

        [Test]
        public void UnMakeMove_Sets_50_Move_Rules_When_White_King_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/3K4/8/8 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord 
            {
                FiftyMoveRuleCount = 10
            });

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteKing);

            gameState.UnMakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(0));
        }

        [Test]
        public void UnMakeMove_Sets_50_Move_Rules_When_White_King_Captures()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/3K4/8/8 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord
            {
                FiftyMoveRuleCount = 10
            });

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteKing);
            move = move.SetCapturedPiece(MoveUtility.BlackPawn);

            gameState.UnMakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(0));
        }
        #endregion

        #region Black King
        [Test]
        public void UnMakeMove_Sets_Kings_Bitboard_When_Black_King_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/3k4/8/8 w - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackKing);

            gameState.UnMakeMove(move);

            Assert.That(gameState.BlackKing, Is.EqualTo(MoveUtility.BitStates[11]), "Piece Bitboard");
        }

        [Test]
        public void UnMakeMove_Sets_Black_Bitboard_When_Black_King_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/3k4/8/8 w - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackKing);

            gameState.UnMakeMove(move);

            Assert.That(gameState.BlackPieces, Is.EqualTo(MoveUtility.BitStates[11]), "Piece Bitboard");
        }

        [Test]
        public void UnMakeMove_Sets_AllPieces_Bitboard_When_Black_King_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/3k4/8/8 w - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackKing);

            gameState.UnMakeMove(move);

            Assert.That(gameState.AllPieces, Is.EqualTo(MoveUtility.BitStates[11]), "All Pieces Bitboard");
        }

        [Test]
        public void UnMakeMove_Sets_CastleStatus_When_Black_King_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("r6r/4k3/8/8/8/8/8/8 w - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord
            {
                CurrentBlackCastleStatus = (int)CastleStatus.BothCastle
            });

            var move = 0U;
            move = move.SetFromMove(60U);
            move = move.SetToMove(52U);
            move = move.SetMovingPiece(MoveUtility.BlackKing);

            gameState.UnMakeMove(move);

            Assert.That(gameState.CurrentBlackCastleStatus, Is.EqualTo((int)CastleStatus.BothCastle), "Castle Status");
        }

        [Test]
        public void UnMakeMove_Sets_Rook_BitBoard_When_CastleOO_Black()
        {
            var gameState = GameStateUtility.LoadStateFromFen("5rk1/8/8/8/8/8/8/8 w - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            uint move = _moveData.BlackCastleOOMove;

            gameState.UnMakeMove(move);

            Assert.That(gameState.BlackRooks, Is.EqualTo(MoveUtility.BitStates[63]));
        }

        [Test]
        public void UnMakeMove_Sets_Black_BitBoard_When_CastleOO()
        {
            var gameState = GameStateUtility.LoadStateFromFen("5rk1/8/8/8/8/8/8/8 w - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            uint move = _moveData.BlackCastleOOMove;

            gameState.UnMakeMove(move);

            Assert.That(gameState.BlackPieces, Is.EqualTo(MoveUtility.BitStates[60] | MoveUtility.BitStates[63]));
        }

        [Test]
        public void UnMakeMove_Sets_Rook_BitBoard_When_CastleOOO_Black()
        {

            var gameState = GameStateUtility.LoadStateFromFen("2kr4/8/8/8/8/8/8/8 w - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            uint move = _moveData.BlackCastleOOOMove;

            gameState.UnMakeMove(move);

            Assert.That(gameState.BlackRooks, Is.EqualTo(MoveUtility.BitStates[56]));
        }

        [Test]
        public void UnMakeMove_Sets_Black_BitBoard_When_CastleOOO()
        {
            var gameState = GameStateUtility.LoadStateFromFen("2kr4/8/8/8/8/8/8/8 w - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            uint move = _moveData.BlackCastleOOOMove;

            gameState.UnMakeMove(move);

            Assert.That(gameState.BlackPieces, Is.EqualTo(MoveUtility.BitStates[56] | MoveUtility.BitStates[60]));
        }

        [Test]
        public void UnMakeMove_Sets_50_Move_Rules_When_Black_King_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/3k4/8/8 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord
            {
                FiftyMoveRuleCount = 10
            });

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackKing);

            gameState.UnMakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(0));
        }

        [Test]
        public void UnMakeMove_Sets_50_Move_Rules_When_Black_King_Captures()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/3k4/8/8 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord
            {
                FiftyMoveRuleCount = 10
            });

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackKing);
            move = move.SetCapturedPiece(MoveUtility.WhitePawn);

            gameState.UnMakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(0));
        }
        #endregion

        #region Array Tests
        [TestCase("8/8/8/8/8/8/8/3K4 b - - 0 1", 3U, 11U, MoveUtility.WhiteKing)]
        [TestCase("8/8/8/8/8/8/8/3k4 w - - 0 1", 3U, 11U, MoveUtility.BlackKing)]
        public void UnMakeMove_Sets_Board_Array_From_Square(string initialFen, uint fromSquare, uint toSquare, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen(initialFen);
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(fromSquare);
            move = move.SetToMove(toSquare);
            move = move.SetMovingPiece(movingPiece);

            gameState.UnMakeMove(move);

            Assert.That(gameState.BoardArray[toSquare], Is.EqualTo(MoveUtility.Empty));
        }

        [TestCase("8/8/8/8/8/8/8/3K4 b - - 0 1", 3U, 11U, MoveUtility.WhiteKing)]
        [TestCase("8/8/8/8/8/8/8/3k4 w - - 0 1", 3U, 11U, MoveUtility.BlackKing)]
        public void UnMakeMove_Sets_Board_Array_To_Square(string initialFen, uint fromSquare, uint toSquare, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen(initialFen);
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(fromSquare);
            move = move.SetToMove(toSquare);
            move = move.SetMovingPiece(movingPiece);

            gameState.UnMakeMove(move);

            Assert.That(gameState.BoardArray[fromSquare], Is.EqualTo(movingPiece));
        }

        [TestCase("8/8/8/8/8/8/8/5RK1 b - - 0 1", 5U, MoveUtility.WhiteRook)]
        [TestCase("5rk1/8/8/8/8/8/8/8 w - - 0 1", 61U, MoveUtility.BlackRook)]
        public void UnMakeMove_Sets_Board_Array_RookSquare_When_CastleOO(string initialFen, uint rookToSquare, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen(initialFen);
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = movingPiece == MoveUtility.WhiteRook ? _moveData.WhiteCastleOOMove : _moveData.BlackCastleOOMove;

            gameState.UnMakeMove(move);

            Assert.That(gameState.BoardArray[rookToSquare], Is.EqualTo(MoveUtility.Empty));
        }

        [TestCase("8/8/8/8/8/8/8/2KR4 b - - 0 1", 3U, MoveUtility.WhiteRook)]
        [TestCase("2kr4/8/8/8/8/8/8/8 w - - 0 1", 59U, MoveUtility.BlackRook)]
        public void UnMakeMove_Sets_Board_Array_RookSquare_When_CastleOOO(string initialFen, uint rookToSquare, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen(initialFen);
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = movingPiece == MoveUtility.WhiteRook ? _moveData.WhiteCastleOOOMove : _moveData.BlackCastleOOOMove;

            gameState.UnMakeMove(move);

            Assert.That(gameState.BoardArray[rookToSquare], Is.EqualTo(MoveUtility.Empty));
        }

        [TestCase("8/8/8/8/8/8/8/5RK1 b - - 0 1", 7U, MoveUtility.WhiteRook)]
        [TestCase("5rk1/8/8/8/8/8/8/8 w - - 0 1", 63U, MoveUtility.BlackRook)]
        public void UnMakeMove_Sets_Board_Array_Rook_From_Square_When_CastleOO(string initialFen, uint rookToSquare, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen(initialFen);
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = movingPiece == MoveUtility.WhiteRook ? _moveData.WhiteCastleOOMove : _moveData.BlackCastleOOMove;

            gameState.UnMakeMove(move);

            Assert.That(gameState.BoardArray[rookToSquare], Is.EqualTo(movingPiece));
        }

        [TestCase("8/8/8/8/8/8/8/2KR4 b - - 0 1", 0U, MoveUtility.WhiteRook)]
        [TestCase("2kr4/8/8/8/8/8/8/8 w - - 0 1", 56U, MoveUtility.BlackRook)]
        public void UnMakeMove_Sets_Board_Array_Rook_From_Square_When_CastleOOO(string initialFen, uint rookToSquare, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen(initialFen);
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = movingPiece == MoveUtility.WhiteRook ? _moveData.WhiteCastleOOOMove : _moveData.BlackCastleOOOMove;

            gameState.UnMakeMove(move);

            Assert.That(gameState.BoardArray[rookToSquare], Is.EqualTo(movingPiece));
        }     
        #endregion
    }
}
