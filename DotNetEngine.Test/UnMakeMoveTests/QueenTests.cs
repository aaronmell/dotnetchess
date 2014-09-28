using DotNetEngine.Engine.Helpers;
using DotNetEngine.Engine.Objects;
using NUnit.Framework;

namespace DotNetEngine.Test.UnMakeMoveTests
{
    public class QueenTests
    {
        #region White Queen
        [Test]
        public void UnMakeMove_Sets_Queen_Bitboard_When_White_Queen_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/3Q4/8/8 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteQueen);

            gameState.UnMakeMove(move);

            Assert.That(gameState.WhiteQueens, Is.EqualTo(MoveUtility.BitStates[11]), "Piece Bitboard");
        }

        [Test]
        public void UnMakeMove_Sets_White_Bitboard_When_White_Queen_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/3Q4/8/8 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteQueen);

            gameState.UnMakeMove(move);

            Assert.That(gameState.WhitePieces, Is.EqualTo(MoveUtility.BitStates[11]), "Piece Bitboard");
        }

        [Test]
        public void UnMakeMove_Sets_AllPieces_Bitboard_When_White_Queen_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/3Q4/8/8 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteQueen);

            gameState.UnMakeMove(move);

            Assert.That(gameState.AllPieces, Is.EqualTo(MoveUtility.BitStates[11]), "All Pieces Bitboard");
        }

        [Test]
        public void UnMakeMove_Sets_50_Move_Rules_When_White_Queen_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/3Q4/8/8 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord
            {
                FiftyMoveRuleCount = 10
            });

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteQueen);

            gameState.UnMakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(10));
        }

        [Test]
        public void UnMakeMove_Sets_50_Move_Rules_When_White_Queen_Captures()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/3Q4/8/8 b - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord
            {
                FiftyMoveRuleCount = 10
            });

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteQueen);
            move = move.SetCapturedPiece(MoveUtility.BlackPawn);

            gameState.UnMakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(10));
        }
        #endregion

        #region Black Queen
        [Test]
        public void UnMakeMove_Sets_Queen_Bitboard_When_Black_Queen_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/3q4/8/8 w - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackQueen);

            gameState.UnMakeMove(move);

            Assert.That(gameState.BlackQueens, Is.EqualTo(MoveUtility.BitStates[11]), "Piece Bitboard");
        }

        [Test]
        public void UnMakeMove_Sets_Black_Bitboard_When_Black_Queen_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/3q4/8/8 w - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackQueen);

            gameState.UnMakeMove(move);

            Assert.That(gameState.BlackPieces, Is.EqualTo(MoveUtility.BitStates[11]), "Piece Bitboard");
        }

        [Test]
        public void UnMakeMove_Sets_AllPieces_Bitboard_When_Black_Queen_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/3q4/8/8 w - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackQueen);

            gameState.UnMakeMove(move);

            Assert.That(gameState.AllPieces, Is.EqualTo(MoveUtility.BitStates[11]), "All Pieces Bitboard");
        }

        [Test]
        public void UnMakeMove_Sets_50_Move_Rules_When_Black_Queen_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/3q4/8/8 w - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord
            {
                FiftyMoveRuleCount = 10
            });

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackQueen);

            gameState.UnMakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(10));
        }

        [Test]
        public void UnMakeMove_Sets_50_Move_Rules_When_Black_Queen_Captures()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/3q4/8/8 w - - 0 1");
            gameState.PreviousGameStateRecords.Push(new GameStateRecord
            {
                FiftyMoveRuleCount = 10
            });

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackQueen);
            move = move.SetCapturedPiece(MoveUtility.WhitePawn);

            gameState.UnMakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(10));
        }
        #endregion

        #region BoardArray
        [TestCase("8/8/8/8/8/3Q4/8/8 b - - 0 1", MoveUtility.WhiteQueen)]
        [TestCase("8/8/8/8/8/3q4/8/8 w - - 0 1", MoveUtility.BlackQueen)]
        public void UnMakeMove_Sets_Board_Array_From_Square(string initialFen, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(initialFen);
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(movingPiece);

            gameState.UnMakeMove(move);

            Assert.That(gameState.BoardArray[11U], Is.EqualTo(movingPiece));
        }

        [TestCase("8/8/8/8/8/3Q4/8/8 b - - 0 1", MoveUtility.WhiteQueen)]
        [TestCase("8/8/8/8/8/3q4/8/8 w - - 0 1", MoveUtility.BlackQueen)]
        public void UnMakeMove_Sets_Board_Array_To_Square(string initialFen, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(initialFen);
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(movingPiece);

            gameState.UnMakeMove(move);

            Assert.That(gameState.BoardArray[19U], Is.EqualTo(MoveUtility.EmptyPiece));
        }


        [TestCase("8/8/8/8/8/3Q4/3q4/8 w - - 0 1", MoveUtility.BlackQueen, MoveUtility.WhiteQueen)]
        [TestCase("8/8/8/8/8/3q4/3Q4/8 w - - 0 1", MoveUtility.WhiteQueen, MoveUtility.BlackQueen)]
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
