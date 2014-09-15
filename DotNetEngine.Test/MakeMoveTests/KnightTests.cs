using DotNetEngine.Engine;
using NUnit.Framework;

namespace DotNetEngine.Test.MakeMoveTests
{
    public class KnightTests
    {
        #region White Knight
        [Test]
        public void MakeMove_Sets_Knights_Bitboard_When_White_Knight_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/8/3N4/8 w - - 0 1");

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(MoveUtility.WhiteKnight);

            gameState.MakeMove(move);

            Assert.That(gameState.WhiteKnights, Is.EqualTo(MoveUtility.BitStates[28]), "Piece Bitboard");
        }

        [Test]
        public void MakeMove_Sets_White_Bitboard_When_White_Knight_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/8/3N4/8 w - - 0 1");

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(MoveUtility.WhiteKnight);

            gameState.MakeMove(move);

            Assert.That(gameState.WhitePieces, Is.EqualTo(MoveUtility.BitStates[28]), "Piece Bitboard");
        }

        [Test]
        public void MakeMove_Sets_AllPieces_Bitboard_When_White_Knight_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/8/3N4/8 w - - 0 1");

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(MoveUtility.WhiteKnight);

            gameState.MakeMove(move);

            Assert.That(gameState.AllPieces, Is.EqualTo(MoveUtility.BitStates[28]), "All Pieces Bitboard");
        }

        [Test]
        public void MakeMove_Sets_50_Move_Rules_When_White_Knight_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/8/3N4/8 w - - 0 1");
            gameState.FiftyMoveRuleCount = 10;

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(MoveUtility.WhiteKnight);

            gameState.MakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(11));
        }

        [Test]
        public void MakeMove_Sets_50_Move_Rules_When_White_Knight_Captures()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/4p3/8/3N4/8 w - - 0 1");
            gameState.FiftyMoveRuleCount = 10;

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(MoveUtility.WhiteKnight);
            move = move.SetCapturedPiece(MoveUtility.BlackPawn);

            gameState.MakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(0));
        }
        #endregion

        #region Black Knight
        [Test]
        public void MakeMove_Sets_Knights_Bitboard_When_Black_Knight_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/8/3n4/8 b - - 0 1");

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(MoveUtility.BlackKnight);

            gameState.MakeMove(move);

            Assert.That(gameState.BlackKnights, Is.EqualTo(MoveUtility.BitStates[28]), "Piece Bitboard");
        }

        [Test]
        public void MakeMove_Sets_Black_Bitboard_When_Black_Knight_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/8/3n4/8 b - - 0 1");

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(MoveUtility.BlackKnight);

            gameState.MakeMove(move);

            Assert.That(gameState.BlackPieces, Is.EqualTo(MoveUtility.BitStates[28]), "Piece Bitboard");
        }

        [Test]
        public void MakeMove_Sets_AllPieces_Bitboard_When_Black_Knight_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/8/3n4/8 b - - 0 1");

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(MoveUtility.BlackKnight);

            gameState.MakeMove(move);

            Assert.That(gameState.AllPieces, Is.EqualTo(MoveUtility.BitStates[28]), "All Pieces Bitboard");
        }

        [Test]
        public void MakeMove_Sets_50_Move_Rules_When_Black_Knight_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/8/3n4/8 w - - 0 1");
            gameState.FiftyMoveRuleCount = 10;

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(MoveUtility.BlackKnight);

            gameState.MakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(11));
        }

        [Test]
        public void MakeMove_Sets_50_Move_Rules_When_Black_Knight_Captures()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/4P3/8/3n4/8 b - - 0 1");
            gameState.FiftyMoveRuleCount = 10;

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(MoveUtility.BlackKnight);
            move = move.SetCapturedPiece(MoveUtility.WhitePawn);

            gameState.MakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(0));
        }
        # endregion

        #region BoardArray
        [TestCase("8/8/8/8/8/8/3N4/8 w - - 0 1", MoveUtility.WhiteKnight)]
        [TestCase("8/8/8/8/8/8/3n4/8 w - - 0 1", MoveUtility.BlackKnight)]
        public void MakeMove_Sets_Board_Array_From_Square(string initialFen, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(initialFen);

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(movingPiece);

            gameState.MakeMove(move);

            Assert.That(gameState.BoardArray[11U], Is.EqualTo(MoveUtility.Empty));
        }

        [TestCase("8/8/8/8/8/8/3N4/8 w - - 0 1", MoveUtility.WhiteKnight)]
        [TestCase("8/8/8/8/8/8/3n4/8 w - - 0 1", MoveUtility.BlackKnight)]
        public void MakeMove_Sets_Board_Array_To_Square(string initialFen, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(initialFen);

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(28U);
            move = move.SetMovingPiece(movingPiece);

            gameState.MakeMove(move);

            Assert.That(gameState.BoardArray[28U], Is.EqualTo(movingPiece));
        }
        #endregion
    }
}
