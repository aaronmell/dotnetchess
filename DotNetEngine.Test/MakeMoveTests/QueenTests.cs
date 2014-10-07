using DotNetEngine.Engine.Helpers;
using DotNetEngine.Engine.Objects;
using NUnit.Framework;

namespace DotNetEngine.Test.MakeMoveTests
{
    public class QueenTests
    {
        private static readonly ZobristHash _zobristHash = new ZobristHash();

        #region White Queen
        [Test]
        public void MakeMove_Sets_Queen_Bitboard_When_White_Queen_Moves()
        {
            var gameState = new GameState("8/8/8/8/8/8/3Q4/8 w - - 0 1", _zobristHash);

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteQueen);

            gameState.MakeMove(move, _zobristHash);

            Assert.That(gameState.WhiteQueens, Is.EqualTo(MoveUtility.BitStates[19]), "Piece Bitboard");
        }

        [Test]
        public void MakeMove_Sets_White_Bitboard_When_White_Queen_Moves()
        {
            var gameState = new GameState("8/8/8/8/8/8/3Q4/8 w - - 0 1", _zobristHash);

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteQueen);

            gameState.MakeMove(move, _zobristHash);

            Assert.That(gameState.WhitePieces, Is.EqualTo(MoveUtility.BitStates[19]), "Piece Bitboard");
        }

        [Test]
        public void MakeMove_Sets_AllPieces_Bitboard_When_White_Queen_Moves()
        {
            var gameState = new GameState("8/8/8/8/8/8/3Q4/8 w - - 0 1", _zobristHash);

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteQueen);

            gameState.MakeMove(move, _zobristHash);

            Assert.That(gameState.AllPieces, Is.EqualTo(MoveUtility.BitStates[19]), "All Pieces Bitboard");
        }

        [Test]
        public void MakeMove_Sets_50_Move_Rules_When_White_Queen_Moves()
        {
            var gameState = new GameState("8/8/8/8/8/8/3Q4/8 w - - 0 1", _zobristHash) {FiftyMoveRuleCount = 10};

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteQueen);

            gameState.MakeMove(move, _zobristHash);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(11));
        }

        [Test]
        public void MakeMove_Sets_50_Move_Rules_When_White_Queen_Captures()
        {
            var gameState = new GameState("8/8/8/8/8/4p3/3Q4/8 w - - 0 1", _zobristHash) {FiftyMoveRuleCount = 10};

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteQueen);
            move = move.SetCapturedPiece(MoveUtility.BlackPawn);

            gameState.MakeMove(move, _zobristHash);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(0));
        }
        #endregion

        #region Black Queen
        [Test]
        public void MakeMove_Sets_Queen_Bitboard_When_Black_Queen_Moves()
        {
            var gameState = new GameState("8/8/8/8/8/8/3q4/8 b - - 0 1", _zobristHash);

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackQueen);

            gameState.MakeMove(move, _zobristHash);

            Assert.That(gameState.BlackQueens, Is.EqualTo(MoveUtility.BitStates[19]), "Piece Bitboard");
        }

        [Test]
        public void MakeMove_Sets_Black_Bitboard_When_Black_Queen_Moves()
        {
            var gameState = new GameState("8/8/8/8/8/8/3q4/8 b - - 0 1", _zobristHash);

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackQueen);

            gameState.MakeMove(move, _zobristHash);

            Assert.That(gameState.BlackPieces, Is.EqualTo(MoveUtility.BitStates[19]), "Piece Bitboard");
        }

        [Test]
        public void MakeMove_Sets_AllPieces_Bitboard_When_Black_Queen_Moves()
        {
            var gameState = new GameState("8/8/8/8/8/8/3q4/8 b - - 0 1", _zobristHash);

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackQueen);

            gameState.MakeMove(move, _zobristHash);

            Assert.That(gameState.AllPieces, Is.EqualTo(MoveUtility.BitStates[19]), "All Pieces Bitboard");
        }

        [Test]
        public void MakeMove_Sets_50_Move_Rules_When_Black_Queen_Moves()
        {
            var gameState = new GameState("8/8/8/8/8/8/3q4/8 b - - 0 1", _zobristHash) {FiftyMoveRuleCount = 10};

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackQueen);

            gameState.MakeMove(move, _zobristHash);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(11));
        }

        [Test]
        public void MakeMove_Sets_50_Move_Rules_When_Black_Queen_Captures()
        {
            var gameState = new GameState("8/8/8/8/8/4P3/3q4/8 b - - 0 1", _zobristHash) {FiftyMoveRuleCount = 10};

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackQueen);
            move = move.SetCapturedPiece(MoveUtility.WhitePawn);

            gameState.MakeMove(move, _zobristHash);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(0));
        }
        #endregion

        #region BoardArray
        [TestCase("8/8/8/8/8/8/3Q4/8 w - - 0 1", MoveUtility.WhiteQueen)]
        [TestCase("8/8/8/8/8/8/3q4/8 b - - 0 1", MoveUtility.BlackQueen)]
        public void MakeMove_Sets_Board_Array_From_Square(string initialFen, uint movingPiece)
        {
            var gameState = new GameState(initialFen, _zobristHash);

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(movingPiece);

            gameState.MakeMove(move, _zobristHash);

            Assert.That(gameState.BoardArray[11U], Is.EqualTo(MoveUtility.EmptyPiece));
        }

        [TestCase("8/8/8/8/8/8/3Q4/8 w - - 0 1", MoveUtility.WhiteQueen)]
        [TestCase("8/8/8/8/8/8/3q4/8 b - - 0 1", MoveUtility.BlackQueen)]
        public void MakeMove_Sets_Board_Array_To_Square(string initialFen, uint movingPiece)
        {
            var gameState = new GameState(initialFen, _zobristHash);

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(movingPiece);

            gameState.MakeMove(move, _zobristHash);

            Assert.That(gameState.BoardArray[19U], Is.EqualTo(movingPiece));
        }
        #endregion
    }
}
