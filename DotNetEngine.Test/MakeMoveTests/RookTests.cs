using DotNetEngine.Engine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEngine.Test.MakeMoveTests
{
    public class RookTests
    {
        #region White Rook
        [Test]
        public void MakeMove_Sets_Rook_Bitboard_When_White_Rook_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/3R4/8 w - - 0 1");

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteRook);

            gameState.MakeMove(move);

            Assert.That(gameState.WhiteRooks, Is.EqualTo(MoveUtility.BitStates[19]), "Piece Bitboard");
        }

        [Test]
        public void MakeMove_Sets_White_Bitboard_When_White_Rook_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/3R4/8 w - - 0 1");

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteRook);

            gameState.MakeMove(move);

            Assert.That(gameState.WhitePieces, Is.EqualTo(MoveUtility.BitStates[19]), "Piece Bitboard");
        }

        [Test]
        public void MakeMove_Sets_AllPieces_Bitboard_When_White_Rook_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/3R4/8 w - - 0 1");

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteRook);

            gameState.MakeMove(move);

            Assert.That(gameState.AllPieces, Is.EqualTo(MoveUtility.BitStates[19]), "All Pieces Bitboard");
        }

        [Test]
        public void MakeMove_Sets_50_Move_Rules_When_White_Rook_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/3R4/8 w - - 0 1");
            gameState.FiftyMoveRuleCount = 10;

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteRook);

            gameState.MakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(11));
        }

        [Test]
        public void MakeMove_Sets_50_Move_Rules_When_White_Rook_Captures()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/4p3/3R4/8 w - - 0 1");
            gameState.FiftyMoveRuleCount = 10;

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteRook);
            move = move.SetCapturedPiece(MoveUtility.BlackPawn);

            gameState.MakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(0));
        }
        #endregion

        #region Black Rook
        [Test]
        public void MakeMove_Sets_Rook_Bitboard_When_Black_Rook_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/3r4/8 b - - 0 1");

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackRook);

            gameState.MakeMove(move);

            Assert.That(gameState.BlackRooks, Is.EqualTo(MoveUtility.BitStates[19]), "Piece Bitboard");
        }

        [Test]
        public void MakeMove_Sets_Black_Bitboard_When_Black_Rook_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/3r4/8 b - - 0 1");

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackRook);

            gameState.MakeMove(move);

            Assert.That(gameState.BlackPieces, Is.EqualTo(MoveUtility.BitStates[19]), "Piece Bitboard");
        }

        [Test]
        public void MakeMove_Sets_AllPieces_Bitboard_When_Black_Rook_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/3r4/8 b - - 0 1");

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackRook);

            gameState.MakeMove(move);

            Assert.That(gameState.AllPieces, Is.EqualTo(MoveUtility.BitStates[19]), "All Pieces Bitboard");
        }

        [Test]
        public void MakeMove_Sets_50_Move_Rules_When_Black_Rook_Moves()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/8/3r4/8 b - - 0 1");
            gameState.FiftyMoveRuleCount = 10;

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackRook);

            gameState.MakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(11));
        }

        [Test]
        public void MakeMove_Sets_50_Move_Rules_When_Black_Rook_Captures()
        {
            var gameState = GameStateUtility.LoadStateFromFen("8/8/8/8/8/4p3/3r4/8 b - - 0 1");
            gameState.FiftyMoveRuleCount = 10;

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackRook);
            move = move.SetCapturedPiece(MoveUtility.WhitePawn);

            gameState.MakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(0));
        }
        #endregion

        #region BoardArray
        [TestCase("8/8/8/8/8/8/3R4/8 w - - 0 1", MoveUtility.WhiteBishop)]
        [TestCase("8/8/8/8/8/8/3r4/8 b - - 0 1", MoveUtility.BlackBishop)]
        public void MakeMove_Sets_Board_Array_From_Square(string initialFen, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen(initialFen);

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(movingPiece);

            gameState.MakeMove(move);

            Assert.That(gameState.BoardArray[11U], Is.EqualTo(MoveUtility.Empty));
        }

        [TestCase("8/8/8/8/8/8/3R4/8 w - - 0 1", MoveUtility.WhiteBishop)]
        [TestCase("8/8/8/8/8/8/3r4/8 b - - 0 1", MoveUtility.BlackBishop)]
        public void MakeMove_Sets_Board_Array_To_Square(string initialFen, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadStateFromFen(initialFen);

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(movingPiece);

            gameState.MakeMove(move);

            Assert.That(gameState.BoardArray[19U], Is.EqualTo(movingPiece));
        }
        #endregion
    }
}
