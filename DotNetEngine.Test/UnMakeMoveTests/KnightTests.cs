using DotNetEngine.Engine.Helpers;
using DotNetEngine.Engine.Objects;
using NUnit.Framework;

namespace DotNetEngine.Test.UnMakeMoveTests
{
    public class KnightTests
    {
        private static readonly ZobristHash _zobristHash = new ZobristHash();

        #region White Knight
        [Test]
        public void UnMakeMove_Sets_Knights_Bitboard_When_White_Knight_Moves()
        {
            var gameState = new GameState("8/8/8/8/4N3/8/8/8 b - - 0 1", _zobristHash);
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
            var gameState = new GameState("8/8/8/8/4N3/8/8/8 b - - 0 1", _zobristHash);
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
            var gameState = new GameState("8/8/8/8/4N3/8/8/8 b - - 0 1", _zobristHash);
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
            var gameState = new GameState("8/8/8/8/4N3/8/8/8 b - - 0 1", _zobristHash);
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
            var gameState = new GameState("8/8/8/8/4N3/8/8/8 b - - 0 1", _zobristHash);
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
            var gameState = new GameState("8/8/8/8/4n3/8/8/8 w - - 0 1", _zobristHash);
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
            var gameState = new GameState("8/8/8/8/4n3/8/8/8 w - - 0 1", _zobristHash);
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
            var gameState = new GameState("8/8/8/8/4n3/8/8/8 w - - 0 1", _zobristHash);
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
            var gameState = new GameState("8/8/8/8/4n3/8/8/8 w - - 0 1", _zobristHash);
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
            var gameState = new GameState("8/8/8/8/4n3/8/8/8 w - - 0 1", _zobristHash);
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
            var gameState = new GameState(initialFen, _zobristHash);
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
            var gameState = new GameState(initialFen, _zobristHash);
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(movingPiece);

            gameState.UnMakeMove(move);

            Assert.That(gameState.BoardArray[28U], Is.EqualTo(MoveUtility.EmptyPiece));
        }

        [TestCase("8/8/8/8/8/3N4/3q4/8 w - - 0 1", MoveUtility.BlackQueen, MoveUtility.WhiteKnight)]
        [TestCase("8/8/8/8/8/3n4/3Q4/8 w - - 0 1", MoveUtility.WhiteQueen, MoveUtility.BlackKnight)]
        public void UnMakeMove_Sets_Board_Array_To_Square_When_Capture(string initialFen, uint movingPiece, uint capturedPiece)
        {
            var gameState = new GameState(initialFen, _zobristHash);
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
