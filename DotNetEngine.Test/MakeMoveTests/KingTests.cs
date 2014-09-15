using DotNetEngine.Engine;
using NUnit.Framework;

namespace DotNetEngine.Test.MakeMoveTests
{
    public class KingTests
    {
        private readonly MoveData _moveData = new MoveData();

        #region White King
        [Test]
        public void MakeMove_Sets_Kings_Bitboard_When_White_King_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/8/3K4/8 w - - 0 1");

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteKing);

            gameState.MakeMove(move);

            Assert.That(gameState.WhiteKing, Is.EqualTo(MoveUtility.BitStates[19]), "Piece Bitboard");               
        }

        [Test]
        public void MakeMove_Sets_White_Bitboard_When_White_King_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/8/3K4/8 w - - 0 1");

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteKing);

            gameState.MakeMove(move);

            Assert.That(gameState.WhitePieces, Is.EqualTo(MoveUtility.BitStates[19]), "Piece Bitboard");
        }

        [Test]
        public void MakeMove_Sets_AllPieces_Bitboard_When_White_King_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/8/3K4/8 w - - 0 1");

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteKing);

            gameState.MakeMove(move);

            Assert.That(gameState.AllPieces, Is.EqualTo(MoveUtility.BitStates[19]), "All Pieces Bitboard");
        }

        [Test]
        public void MakeMove_Sets_CastleStatus_When_White_King_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/8/8/R3K2R w KQ - 0 1");
            
            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteKing);

            gameState.MakeMove(move);

            Assert.That(gameState.CurrentWhiteCastleStatus, Is.EqualTo((int)CastleStatus.CannotCastle), "All Pieces Bitboard");
        }

        [Test]
        public void MakeMove_Sets_Rook_BitBoard_When_CastleOO()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/8/8/4K2R w K - 0 1");

	        uint move = _moveData.WhiteCastleOOMove;

            gameState.MakeMove(move);

            Assert.That(gameState.WhiteRooks, Is.EqualTo(MoveUtility.BitStates[5]));
        }

        [Test]
        public void MakeMove_Sets_White_BitBoard_When_CastleOO()
        {

            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/8/8/4K2R w K - 0 1");

	        uint move = _moveData.WhiteCastleOOMove;

            gameState.MakeMove(move);

            Assert.That(gameState.WhitePieces, Is.EqualTo(MoveUtility.BitStates[5] | MoveUtility.BitStates[6]));
        }

        [Test]
        public void MakeMove_Sets_Rook_BitBoard_When_CastleOOO()
        {

            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/8/8/R3K3 w Q - 0 1");

	        uint move = _moveData.WhiteCastleOOOMove;

            gameState.MakeMove(move);

            Assert.That(gameState.WhiteRooks, Is.EqualTo(MoveUtility.BitStates[3]));
        }

        [Test]
        public void MakeMove_Sets_White_BitBoard_When_CastleOOO()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/8/8/R3K3 w Q - 0 1");

	        uint move = _moveData.WhiteCastleOOOMove;

            gameState.MakeMove(move);

            Assert.That(gameState.WhitePieces, Is.EqualTo(MoveUtility.BitStates[3] | MoveUtility.BitStates[2]));
        }

        [Test]
        public void MakeMove_Sets_50_Move_Rules_When_White_King_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/8/3K4/8 w - - 0 1");
            gameState.FiftyMoveRuleCount = 10;

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteKing);

            gameState.MakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(11));
        }

        [Test]
        public void MakeMove_Sets_50_Move_Rules_When_White_King_Captures()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/3p4/3K4/8 w - - 0 1");
            gameState.FiftyMoveRuleCount = 10;

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.WhiteKing);
            move = move.SetCapturedPiece(MoveUtility.BlackPawn);

            gameState.MakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(0));
        }

        #endregion

        #region Black King
        [Test]
        public void MakeMove_Sets_Kings_Bitboard_When_Black_King_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/8/3k4/8 w - - 0 1");

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackKing);

            gameState.MakeMove(move);

            Assert.That(gameState.BlackKing, Is.EqualTo(MoveUtility.BitStates[19]), "Piece Bitboard");
        }

        [Test]
        public void MakeMove_Sets_Black_Bitboard_When_Black_King_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/8/3k4/8 w - - 0 1");

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackKing);

            gameState.MakeMove(move);

            Assert.That(gameState.BlackPieces, Is.EqualTo(MoveUtility.BitStates[19]), "Piece Bitboard");
        }

        [Test]
        public void MakeMove_Sets_AllPieces_Bitboard_When_Black_King_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/8/3k4/8 w - - 0 1");

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackKing);

            gameState.MakeMove(move);

            Assert.That(gameState.AllPieces, Is.EqualTo(MoveUtility.BitStates[19]), "All Pieces Bitboard");
        }

        [Test]
        public void MakeMove_Sets_CastleStatus_When_Black_King_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("r3k2r/8/8/8/8/8/8/8 b kq - 0 1");
            

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackKing);

            gameState.MakeMove(move);

            Assert.That(gameState.CurrentWhiteCastleStatus, Is.EqualTo((int)CastleStatus.CannotCastle), "All Pieces Bitboard");
        }

        [Test]
        public void MakeMove_Sets_Rook_BitBoard_When_CastleOO_Black()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("4k2r/8/8/8/8/8/8/8 b k - 0 1");

	        uint move = _moveData.BlackCastleOOMove;

            gameState.MakeMove(move);

            Assert.That(gameState.BlackRooks, Is.EqualTo(MoveUtility.BitStates[61]));
        }

        [Test]
        public void MakeMove_Sets_Black_BitBoard_When_CastleOO()
        {

            var gameState = GameStateUtility.LoadGameStateFromFen("4k2r/8/8/8/8/8/8/8 b k - 0 1");

	        uint move = _moveData.BlackCastleOOMove;

            gameState.MakeMove(move);

            Assert.That(gameState.BlackPieces, Is.EqualTo(MoveUtility.BitStates[61] | MoveUtility.BitStates[62]));
        }

        [Test]
        public void MakeMove_Sets_Rook_BitBoard_When_CastleOOO_Black()
        {

            var gameState = GameStateUtility.LoadGameStateFromFen("r3k3/8/8/8/8/8/8/8 b q - 0 1");

	        uint move = _moveData.BlackCastleOOOMove;

            gameState.MakeMove(move);

            Assert.That(gameState.BlackRooks, Is.EqualTo(MoveUtility.BitStates[59]));
        }

        [Test]
        public void MakeMove_Sets_Black_BitBoard_When_CastleOOO_Black()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("r3k3/8/8/8/8/8/8/8 b q - 0 1");

	        uint move = _moveData.BlackCastleOOOMove;

            gameState.MakeMove(move);

            Assert.That(gameState.BlackPieces, Is.EqualTo(MoveUtility.BitStates[58] | MoveUtility.BitStates[59]));
        }

        [Test]
        public void MakeMove_Sets_50_Move_Rules_When_Black_King_Moves()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/8/3k4/8 w - - 0 1");
            gameState.FiftyMoveRuleCount = 10;

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackKing);

            gameState.MakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(11));
        }

        [Test]
        public void MakeMove_Sets_50_Move_Rules_When_Black_King_Captures()
        {
            var gameState = GameStateUtility.LoadGameStateFromFen("8/8/8/8/8/3P4/3k4/8 w - - 0 1");
            gameState.FiftyMoveRuleCount = 10;

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(MoveUtility.BlackKing);
            move = move.SetCapturedPiece(MoveUtility.WhitePawn);

            gameState.MakeMove(move);

            Assert.That(gameState.FiftyMoveRuleCount, Is.EqualTo(0));
        }
        #endregion

        #region BoardArray
        [TestCase("8/8/8/8/8/8/3K4/8 w - - 0 1", 3U, 11U, MoveUtility.WhiteKing)]
        [TestCase("8/8/8/8/8/8/3k4/8 b - - 0 1", 3U, 11U, MoveUtility.BlackKing)]
        public void MakeMove_Sets_Board_Array_From_Square(string initialFen, uint fromSquare, uint toSquare, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(initialFen);

            var move = 0U;
            move = move.SetFromMove(fromSquare);
            move = move.SetToMove(toSquare);
            move = move.SetMovingPiece(movingPiece);

            gameState.MakeMove(move);

            Assert.That(gameState.BoardArray[fromSquare], Is.EqualTo(MoveUtility.Empty));
        }

        [TestCase("8/8/8/8/8/8/3K4/8 w - - 0 1", 3U, 11U, MoveUtility.WhiteKing)]
        [TestCase("8/8/8/8/8/8/3k4/8 b - - 0 1", 3U, 11U, MoveUtility.BlackKing)]
        public void MakeMove_Sets_Board_Array_To_Square(string initialFen, uint fromSquare, uint toSquare, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(initialFen);

            var move = 0U;
            move = move.SetFromMove(fromSquare);
            move = move.SetToMove(toSquare);
            move = move.SetMovingPiece(movingPiece);

            gameState.MakeMove(move);

            Assert.That(gameState.BoardArray[toSquare], Is.EqualTo(movingPiece));
        }

        [TestCase("8/8/8/8/8/8/8/4K2R w K - 0 1", 5U, MoveUtility.WhiteRook)]
        [TestCase("4k2r/8/8/8/8/8/8/8 b k - 0 1", 61U, MoveUtility.BlackRook)]
        public void MakeMove_Sets_Board_Array_RookSquare_When_CastleOO(string initialFen, uint rookToSquare, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(initialFen);

            var move = movingPiece == MoveUtility.WhiteRook ? _moveData.WhiteCastleOOMove : _moveData.BlackCastleOOMove;

            gameState.MakeMove(move);

            Assert.That(gameState.BoardArray[rookToSquare], Is.EqualTo(movingPiece));
        }

        [TestCase("8/8/8/8/8/8/8/R3K3 w K - 0 1", 3U, MoveUtility.WhiteRook)]
        [TestCase("r3k3/8/8/8/8/8/8/8 b q - 0 1", 59U, MoveUtility.BlackRook)]
        public void MakeMove_Sets_Board_Array_RookSquare_When_CastleOOO(string initialFen, uint rookToSquare, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(initialFen);

            var move = movingPiece == MoveUtility.WhiteRook ? _moveData.WhiteCastleOOOMove : _moveData.BlackCastleOOOMove;

            gameState.MakeMove(move);

            Assert.That(gameState.BoardArray[rookToSquare], Is.EqualTo(movingPiece));
        }

        [TestCase("8/8/8/8/8/8/8/4K2R w K - 0 1", 7U, MoveUtility.WhiteRook)]
        [TestCase("4k2r/8/8/8/8/8/8/8 b k - 0 1", 63U, MoveUtility.BlackRook)]
        public void MakeMove_Sets_Board_Array_Rook_From_Square_When_CastleOO(string initialFen, uint rookToSquare, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(initialFen);

            var move = movingPiece == MoveUtility.WhiteRook ? _moveData.WhiteCastleOOMove : _moveData.BlackCastleOOMove;

            gameState.MakeMove(move);

            Assert.That(gameState.BoardArray[rookToSquare], Is.EqualTo(MoveUtility.Empty));
        }

        [TestCase("8/8/8/8/8/8/8/R3K3 w K - 0 1", 0U, MoveUtility.WhiteRook)]
        [TestCase("r3k3/8/8/8/8/8/8/8 b q - 0 1", 56U, MoveUtility.BlackRook)]
        public void MakeMove_Sets_Board_Array_Rook_From_Square_When_CastleOOO(string initialFen, uint rookToSquare, uint movingPiece)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(initialFen);

            var move = movingPiece == MoveUtility.WhiteRook ? _moveData.WhiteCastleOOOMove : _moveData.BlackCastleOOOMove;

            gameState.MakeMove(move);

            Assert.That(gameState.BoardArray[rookToSquare], Is.EqualTo(MoveUtility.Empty));
        }     
        #endregion
    }
}
