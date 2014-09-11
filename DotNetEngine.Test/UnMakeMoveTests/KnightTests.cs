using DotNetEngine.Engine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEngine.Test.UnMakeMoveTests
{
    public class KnightTests
    {
        #region White Knight
        [Test]
        public void UnMakeMove_Sets_Knights_Bitboard_When_White_Knight_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/4N3/8/8/8 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(MoveUtility.WhiteKnight);

            gameState.UnMakeMove(move);

            Assert.That(gameState.WhiteKnights, Is.EqualTo(MoveUtility.BitStates[11]), "Piece Bitboard");
        }

        [Test]
        public void UnMakeMove_Sets_White_Bitboard_When_White_Knight_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/4N3/8/8/8 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(MoveUtility.WhiteKnight);

            gameState.UnMakeMove(move);

            Assert.That(gameState.WhitePieces, Is.EqualTo(MoveUtility.BitStates[11]), "Piece Bitboard");
        }

        [Test]
        public void UnMakeMove_Sets_AllPieces_Bitboard_When_White_Knight_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/4N3/8/8/8 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(MoveUtility.WhiteKnight);

            gameState.UnMakeMove(move);

            Assert.That(gameState.AllPieces, Is.EqualTo(MoveUtility.BitStates[11]), "All Pieces Bitboard");
        }

        [Test]
        public void UnMakeMove_Sets_50_Move_Rules_When_White_Knight_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/4N3/8/8/8 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord
                {
                    FiftyMoveRuleCount = 10
                });
            

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(MoveUtility.WhiteKnight);

            gameState.UnMakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(10));
        }

        [Test]
        public void UnMakeMove_Sets_50_Move_Rules_When_White_Knight_Captures()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/4N3/8/8/8 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord
            {
                FiftyMoveRuleCount = 10
            });

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(MoveUtility.WhiteKnight);
            move = move.SetCapturedPiece(MoveUtility.BlackPawn);

            gameState.UnMakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(10));
        }
        #endregion

        #region Black Knight
        [Test]
        public void UnMakeMove_Sets_Knights_Bitboard_When_Black_Knight_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/4n3/8/8/8 w - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(MoveUtility.BlackKnight);

            gameState.UnMakeMove(move);

            Assert.That(gameState.BlackKnights, Is.EqualTo(MoveUtility.BitStates[11]), "Piece Bitboard");
        }

        [Test]
        public void UnMakeMove_Sets_Black_Bitboard_When_Black_Knight_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/4n3/8/8/8 w - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(MoveUtility.BlackKnight);

            gameState.UnMakeMove(move);

            Assert.That(gameState.BlackPieces, Is.EqualTo(MoveUtility.BitStates[11]), "Piece Bitboard");
        }

        [Test]
        public void UnMakeMove_Sets_AllPieces_Bitboard_When_Black_Knight_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/4n3/8/8/8 w - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(MoveUtility.BlackKnight);

            gameState.UnMakeMove(move);

            Assert.That(gameState.AllPieces, Is.EqualTo(MoveUtility.BitStates[11]), "All Pieces Bitboard");
        }

        [Test]
        public void UnMakeMove_Sets_50_Move_Rules_When_Black_Knight_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/4n3/8/8/8 w - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord
            {
                FiftyMoveRuleCount = 10
            });


            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(MoveUtility.BlackKnight);

            gameState.UnMakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(10));
        }

        [Test]
        public void UnMakeMove_Sets_50_Move_Rules_When_Black_Knight_Captures()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/4n3/8/8/8 w - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord
            {
                FiftyMoveRuleCount = 10
            });

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(MoveUtility.BlackKnight);
            move = move.SetCapturedPiece(MoveUtility.WhitePawn);

            gameState.UnMakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(10));
        }
        #endregion

        #region BoardArray
        [TestCase("8/8/8/8/4N3/8/8/8 b - - 0 1", MoveUtility.WhiteKnight)]
        [TestCase("8/8/8/8/4n3/8/8/8 w - - 0 1", MoveUtility.BlackKnight)]
        public void MakeMove_Sets_Board_Array_From_Square(string initialFen, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen(initialFen);
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(movingPiece);

            gameState.UnMakeMove(move);

            Assert.That(gameState.BoardArray[11U], Is.EqualTo(movingPiece));
        }

        [TestCase("8/8/8/8/4N3/8/8/8 b - - 0 1", MoveUtility.WhiteKnight)]
        [TestCase("8/8/8/8/4n3/8/8/8 w - - 0 1", MoveUtility.BlackKnight)]
        public void MakeMove_Sets_Board_Array_To_Square(string initialFen, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen(initialFen);
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(movingPiece);

            gameState.UnMakeMove(move);

            Assert.That(gameState.BoardArray[28U], Is.EqualTo(MoveUtility.Empty));
        }
        #endregion
    }
}
