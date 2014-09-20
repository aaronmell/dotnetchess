using DotNetEngine.Engine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEngine.Test.UnUnMakeMoveTests
{
    public class BishopTests
    {

        #region White Bishop
        [Test]
        public void UnMakeMove_Sets_Bishop_Bitboard_When_White_Bishop_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/4B3/8/8 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(20U);
            move = move.SetMovingPiece(MoveUtility.WhiteBishop);

            gameState.UnMakeMove(move);

            Assert.That(gameState.WhiteBishops, Is.EqualTo(MoveUtility.BitStates[11]), "Piece Bitboard");
        }

        [Test]
        public void UnMakeMove_Sets_White_Bitboard_When_White_Bishop_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/4B3/8/8 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(20U);
            move = move.SetMovingPiece(MoveUtility.WhiteBishop);

            gameState.UnMakeMove(move);

            Assert.That(gameState.WhitePieces, Is.EqualTo(MoveUtility.BitStates[11]), "Piece Bitboard");
        }

        [Test]
        public void UnMakeMove_Sets_AllPieces_Bitboard_When_White_Bishop_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/4B3/8/8 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(20U);
            move = move.SetMovingPiece(MoveUtility.WhiteBishop);

            gameState.UnMakeMove(move);

            Assert.That(gameState.AllPieces, Is.EqualTo(MoveUtility.BitStates[11]), "All Pieces Bitboard");
        }

        [Test]
        public void UnMakeMove_Sets_50_Move_Rules_When_White_Bishop_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/4B3/8/8 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord
            {
                FiftyMoveRuleCount = 10
            });

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(20U);
            move = move.SetMovingPiece(MoveUtility.WhiteBishop);

            gameState.UnMakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(10));
        }

        [Test]
        public void UnMakeMove_Sets_50_Move_Rules_When_White_Bishop_Captures()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/4B3/8/8 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord
            {
                FiftyMoveRuleCount = 10
            });

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(20U);
            move = move.SetMovingPiece(MoveUtility.WhiteBishop);
            move = move.SetCapturedPiece(MoveUtility.BlackPawn);

            gameState.UnMakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(10));
        }
        #endregion

        #region Black Bishops
        [Test]
        public void UnMakeMove_Sets_Bishop_Bitboard_When_Black_Bishop_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/4b3/8/8 w - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(20U);
            move = move.SetMovingPiece(MoveUtility.BlackBishop);

            gameState.UnMakeMove(move);

            Assert.That(gameState.BlackBishops, Is.EqualTo(MoveUtility.BitStates[11]), "Piece Bitboard");
        }

        [Test]
        public void UnMakeMove_Sets_Black_Bitboard_When_Black_Bishop_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/4b3/8/8 w - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(20U);
            move = move.SetMovingPiece(MoveUtility.BlackBishop);

            gameState.UnMakeMove(move);

            Assert.That(gameState.BlackPieces, Is.EqualTo(MoveUtility.BitStates[11]), "Piece Bitboard");
        }

        [Test]
        public void UnMakeMove_Sets_AllPieces_Bitboard_When_Black_Bishop_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/4b3/8/8 w - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(20U);
            move = move.SetMovingPiece(MoveUtility.BlackBishop);

            gameState.UnMakeMove(move);

            Assert.That(gameState.AllPieces, Is.EqualTo(MoveUtility.BitStates[11]), "All Pieces Bitboard");
        }

        [Test]
        public void UnMakeMove_Sets_50_Move_Rules_When_Black_Bishop_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/4b3/8/8 w - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord
            {
                FiftyMoveRuleCount = 10
            });

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(20U);
            move = move.SetMovingPiece(MoveUtility.BlackBishop);

            gameState.UnMakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(10));
        }

        [Test]
        public void UnMakeMove_Sets_50_Move_Rules_When_Black_Bishop_Captures()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/4b3/8/8 w - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord
            {
                FiftyMoveRuleCount = 10
            });

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(20U);
            move = move.SetMovingPiece(MoveUtility.BlackBishop);
            move = move.SetCapturedPiece(MoveUtility.WhitePawn);

            gameState.UnMakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(10));
        }
        #endregion

        #region BoardArray
        [TestCase("8/8/8/8/8/4B3/8/8 b - - 0 1", MoveUtility.WhiteBishop)]
        [TestCase("8/8/8/8/8/4b3/8/8 w - - 0 1", MoveUtility.BlackBishop)]
        public void UnMakeMove_Sets_Board_Array_From_Square(string initialFen, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(initialFen);
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(20U);
            move = move.SetMovingPiece(movingPiece);

            gameState.UnMakeMove(move);

            Assert.That(gameState.BoardArray[11U], Is.EqualTo(movingPiece));
        }

        [TestCase("8/8/8/8/8/4B3/8/8 b - - 0 1", MoveUtility.WhiteBishop)]
        [TestCase("8/8/8/8/8/4b3/8/8 w - - 0 1", MoveUtility.BlackBishop)]
        public void UnMakeMove_Sets_Board_Array_To_Square(string initialFen, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(initialFen);
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(20U);
            move = move.SetMovingPiece(movingPiece);

            gameState.UnMakeMove(move);

            Assert.That(gameState.BoardArray[20U], Is.EqualTo(MoveUtility.Empty));
        }

        [TestCase("8/8/8/8/8/3B4/3q4/8 w - - 0 1", MoveUtility.BlackQueen, MoveUtility.WhiteBishop)]
        [TestCase("8/8/8/8/8/3b4/3Q4/8 w - - 0 1", MoveUtility.WhiteQueen, MoveUtility.BlackBishop)]
        public void UnMakeMove_Sets_Board_Array_To_Square_When_Capture(string initialFen, uint movingPiece, uint capturedPiece)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(initialFen);
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(movingPiece);
            move = move.SetCapturedPiece(capturedPiece);

            gameState.UnMakeMove(move);

            Assert.That(gameState.BoardArray[19U], Is.EqualTo(capturedPiece));
        }
        #endregion
    }
}
