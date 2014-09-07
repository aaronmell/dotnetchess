using DotNetEngine.Engine;
using NUnit.Framework;

namespace DotNetEngine.Test.MakeMoveTests
{
    public class BishopTests
    {
        #region White Bishop
        [Test]
        public void MakeMove_Sets_Bishop_Bitboard_When_White_Bishop_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/3B4/8 w - - 0 1");

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(20U);
            move = move.SetMovingPiece(MoveUtility.WhiteBishop);

            gameState.MakeMove(move);

            Assert.That(gameState.WhiteBishops, Is.EqualTo(MoveUtility.BitStates[20]), "Piece Bitboard");
        }

        [Test]
        public void MakeMove_Sets_White_Bitboard_When_White_Bishop_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/3B4/8 w - - 0 1");

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(20U);
            move = move.SetMovingPiece(MoveUtility.WhiteBishop);

            gameState.MakeMove(move);

            Assert.That(gameState.WhitePieces, Is.EqualTo(MoveUtility.BitStates[20]), "Piece Bitboard");
        }

        [Test]
        public void MakeMove_Sets_AllPieces_Bitboard_When_White_Bishop_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/3B4/8 w - - 0 1");

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(20U);
            move = move.SetMovingPiece(MoveUtility.WhiteBishop);

            gameState.MakeMove(move);

            Assert.That(gameState.AllPieces, Is.EqualTo(MoveUtility.BitStates[20]), "All Pieces Bitboard");
        }

        [Test]
        public void MakeMove_Sets_50_Move_Rules_When_White_Bishop_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/3B4/8 w - - 0 1");
            gameState.FiftyMoveRuleCount = 10;

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(20U);
            move = move.SetMovingPiece(MoveUtility.WhiteBishop);

            gameState.MakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(11));
        }

        [Test]
        public void MakeMove_Sets_50_Move_Rules_When_White_Bishop_Captures()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/4p3/3B4/8 w - - 0 1");
            gameState.FiftyMoveRuleCount = 10;

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(20U);
            move = move.SetMovingPiece(MoveUtility.WhiteBishop);
            move = move.SetCapturedPiece(MoveUtility.BlackPawn);

            gameState.MakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(0));
        }
        #endregion

        #region Black Bishops
        [Test]
        public void MakeMove_Sets_Bishop_Bitboard_When_Black_Bishop_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/3b4/8 b - - 0 1");

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(20U);
            move = move.SetMovingPiece(MoveUtility.BlackBishop);

            gameState.MakeMove(move);

            Assert.That(gameState.BlackBishops, Is.EqualTo(MoveUtility.BitStates[20]), "Piece Bitboard");
        }

        [Test]
        public void MakeMove_Sets_Black_Bitboard_When_Black_Bishop_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/3b4/8 b - - 0 1");

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(20U);
            move = move.SetMovingPiece(MoveUtility.BlackBishop);

            gameState.MakeMove(move);

            Assert.That(gameState.BlackPieces, Is.EqualTo(MoveUtility.BitStates[20]), "Piece Bitboard");
        }

        [Test]
        public void MakeMove_Sets_AllPieces_Bitboard_When_Black_Bishop_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/3b4/8 b - - 0 1");

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(20U);
            move = move.SetMovingPiece(MoveUtility.BlackBishop);

            gameState.MakeMove(move);

            Assert.That(gameState.AllPieces, Is.EqualTo(MoveUtility.BitStates[20]), "All Pieces Bitboard");
        }

        [Test]
        public void MakeMove_Sets_50_Move_Rules_When_Black_Bishop_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/3b4/8 b - - 0 1");
            gameState.FiftyMoveRuleCount = 10;

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(20U);
            move = move.SetMovingPiece(MoveUtility.BlackBishop);

            gameState.MakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(11));
        }

        [Test]
        public void MakeMove_Sets_50_Move_Rules_When_Black_Bishop_Captures()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/4P3/3b4/8 b - - 0 1");
            gameState.FiftyMoveRuleCount = 10;

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(20U);
            move = move.SetMovingPiece(MoveUtility.BlackBishop);
            move = move.SetCapturedPiece(MoveUtility.WhitePawn);

            gameState.MakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(0));
        }
        #endregion

        #region BoardArray
        [TestCase("8/8/8/8/8/8/3B4/8 w - - 0 1", MoveUtility.WhiteBishop)]
        [TestCase("8/8/8/8/8/8/3b4/8 w - - 0 1", MoveUtility.BlackBishop)]
        public void MakeMove_Sets_Board_Array_From_Square(string initialFen, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen(initialFen);

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(20U);
            move = move.SetMovingPiece(movingPiece);

            gameState.MakeMove(move);

            Assert.That(gameState.BoardArray[11U], Is.EqualTo(MoveUtility.Empty));
        }

        [TestCase("8/8/8/8/8/8/3B4/8 w - - 0 1", MoveUtility.WhiteBishop)]
        [TestCase("8/8/8/8/8/8/3b4/8 w - - 0 1", MoveUtility.BlackBishop)]
        public void MakeMove_Sets_Board_Array_To_Square(string initialFen, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen(initialFen);

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(20U);
            move = move.SetMovingPiece(movingPiece);

            gameState.MakeMove(move);

            Assert.That(gameState.BoardArray[20U], Is.EqualTo(movingPiece));
        }
        #endregion
    }
}
