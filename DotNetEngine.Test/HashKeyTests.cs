using DotNetEngine.Engine.Helpers;
using DotNetEngine.Engine.Objects;
using NUnit.Framework;

namespace DotNetEngine.Test
{
    public class HashKeyTests
    {
        private static readonly ZobristHash _zobristHash = new ZobristHash();

        #region Load Fen Tests
        [TestCase("8/8/8/8/8/8/8/P7 b - - 0 1", 0, MoveUtility.WhitePawn)]
        [TestCase("7P/8/8/8/8/8/8/8 b - - 0 1", 63, MoveUtility.WhitePawn)]
        [TestCase("8/8/8/8/8/8/8/Q7 b - - 0 1", 0, MoveUtility.WhiteQueen)]
        [TestCase("7Q/8/8/8/8/8/8/8 b - - 0 1", 63, MoveUtility.WhiteQueen)]
        [TestCase("8/8/8/8/8/8/8/R7 b - - 0 1", 0, MoveUtility.WhiteRook)]
        [TestCase("7R/8/8/8/8/8/8/8 b - - 0 1", 63, MoveUtility.WhiteRook)]
        [TestCase("8/8/8/8/8/8/8/B7 b - - 0 1", 0, MoveUtility.WhiteBishop)]
        [TestCase("7B/8/8/8/8/8/8/8 b - - 0 1", 63, MoveUtility.WhiteBishop)]
        [TestCase("8/8/8/8/8/8/8/K7 b - - 0 1", 0, MoveUtility.WhiteKing)]
        [TestCase("7K/8/8/8/8/8/8/8 b - - 0 1", 63, MoveUtility.WhiteKing)]
        [TestCase("8/8/8/8/8/8/8/N7 b - - 0 1", 0, MoveUtility.WhiteKnight)]
        [TestCase("7N/8/8/8/8/8/8/8 b - - 0 1", 63, MoveUtility.WhiteKnight)]
        [TestCase("8/8/8/8/8/8/8/p7 b - - 0 1", 0, MoveUtility.BlackPawn)]
        [TestCase("7p/8/8/8/8/8/8/8 b - - 0 1", 63, MoveUtility.BlackPawn)]
        [TestCase("8/8/8/8/8/8/8/q7 b - - 0 1", 0, MoveUtility.BlackQueen)]
        [TestCase("7q/8/8/8/8/8/8/8 b - - 0 1", 63, MoveUtility.BlackQueen)]
        [TestCase("8/8/8/8/8/8/8/r7 b - - 0 1", 0, MoveUtility.BlackRook)]
        [TestCase("7r/8/8/8/8/8/8/8 b - - 0 1", 63, MoveUtility.BlackRook)]
        [TestCase("8/8/8/8/8/8/8/b7 b - - 0 1", 0, MoveUtility.BlackBishop)]
        [TestCase("7b/8/8/8/8/8/8/8 b - - 0 1", 63, MoveUtility.BlackBishop)]
        [TestCase("8/8/8/8/8/8/8/k7 b - - 0 1", 0, MoveUtility.BlackKing)]
        [TestCase("7k/8/8/8/8/8/8/8 b - - 0 1", 63, MoveUtility.BlackKing)]
        [TestCase("8/8/8/8/8/8/8/n7 b - - 0 1", 0, MoveUtility.BlackKnight)]
        [TestCase("7n/8/8/8/8/8/8/8 b - - 0 1", 63, MoveUtility.BlackKnight)]
        public void HashKey_Piece_Set_Correct_On_Load_For_Piece(string fen, int position, uint piece)
        {
            var gameState = new GameState(fen, _zobristHash);

            Assert.That(gameState.HashKey, Is.Not.EqualTo(0));

            gameState.HashKey ^= _zobristHash.PieceArray[position][piece];

            Assert.That(gameState.HashKey, Is.EqualTo(0));
        }

        [TestCase("8/8/8/8/8/8/8/P7 w - - 0 1", 0, MoveUtility.WhitePawn)]
        [TestCase("8/8/8/8/8/8/8/P7 b - - 0 1", 0, MoveUtility.WhitePawn)]
        public void HashKey_Side_Set_Correctly_On_Load_For_Piece(string fen, int position, uint piece)
        {
            var gameState = new GameState(fen, _zobristHash);

            Assert.That(gameState.HashKey, Is.Not.EqualTo(0));

            gameState.HashKey ^= _zobristHash.PieceArray[position][piece];

            if (gameState.WhiteToMove)
            {
                gameState.HashKey ^= _zobristHash.WhiteToMove;
            }

            Assert.That(gameState.HashKey, Is.EqualTo(0));
        }

        [Test]
        public void HashKey_White_Castle_OO_Set_Correctly_On_Load()
        {
            var gameState = new GameState("8/8/8/8/8/8/8/4K2R b K - 0 1", _zobristHash);

            Assert.That(gameState.HashKey, Is.Not.EqualTo(0));

            gameState.HashKey ^= _zobristHash.PieceArray[4][MoveUtility.WhiteKing];
            gameState.HashKey ^= _zobristHash.PieceArray[7][MoveUtility.WhiteRook];
            gameState.HashKey ^= _zobristHash.WhiteCanCastleOO;
            
            Assert.That(gameState.HashKey, Is.EqualTo(0));
        }

        [Test]
        public void HashKey_White_Castle_OOO_Set_Correctly_On_Load()
        {
            var gameState = new GameState("8/8/8/8/8/8/8/R3K3 b Q - 0 1", _zobristHash);

            Assert.That(gameState.HashKey, Is.Not.EqualTo(0));

            gameState.HashKey ^= _zobristHash.PieceArray[4][MoveUtility.WhiteKing];
            gameState.HashKey ^= _zobristHash.PieceArray[0][MoveUtility.WhiteRook];
            gameState.HashKey ^= _zobristHash.WhiteCanCastleOOO;

            Assert.That(gameState.HashKey, Is.EqualTo(0));
        }

        [Test]
        public void HashKey_Black_Castle_OO_Set_Correctly_On_Load()
        {
            var gameState = new GameState("4k2r/8/8/8/8/8/8/8 b k - 0 1", _zobristHash);

            Assert.That(gameState.HashKey, Is.Not.EqualTo(0));

            gameState.HashKey ^= _zobristHash.PieceArray[60][MoveUtility.BlackKing];
            gameState.HashKey ^= _zobristHash.PieceArray[63][MoveUtility.BlackRook];
            gameState.HashKey ^= _zobristHash.BlackCanCastleOO;

            Assert.That(gameState.HashKey, Is.EqualTo(0));
        }

        [Test]
        public void HashKey_Black_Castle_OOO_Set_Correctly_On_Load()
        {
            var gameState = new GameState("r3k3/8/8/8/8/8/8/8 b q - 0 1", _zobristHash);

            Assert.That(gameState.HashKey, Is.Not.EqualTo(0));

            gameState.HashKey ^= _zobristHash.PieceArray[60][MoveUtility.BlackKing];
            gameState.HashKey ^= _zobristHash.PieceArray[56][MoveUtility.BlackRook];
            gameState.HashKey ^= _zobristHash.BlackCanCastleOOO;

            Assert.That(gameState.HashKey, Is.EqualTo(0));
        }

        [TestCase("8/8/8/8/8/8/8/8 b - a2 0 1", 8)]
        [TestCase("8/8/8/8/8/8/8/8 b - h7 0 1", 55)]
        public void HashKey_Enpassant_Set_Correctly_On_Load(string fen, int position)
        {
            var gameState = new GameState(fen, _zobristHash);

            Assert.That(gameState.HashKey, Is.Not.EqualTo(0));

            gameState.HashKey ^= _zobristHash.EnPassantSquares[position];

            Assert.That(gameState.HashKey, Is.EqualTo(0));
        }
        #endregion

        #region Make Move Tests

        [TestCase("8/8/8/8/8/8/8/P7 b - - 0 1", 0U, 8U, MoveUtility.WhitePawn)]
        [TestCase("8/8/8/8/8/8/8/R7 b - - 0 1", 0U, 8U, MoveUtility.WhiteRook)]
        [TestCase("8/8/8/8/8/8/8/Q7 b - - 0 1", 0U, 8U, MoveUtility.WhiteQueen)]
        [TestCase("8/8/8/8/8/8/8/K7 b - - 0 1", 0U, 8U, MoveUtility.WhiteKing)]
        [TestCase("8/8/8/8/8/8/8/B7 b - - 0 1", 0U, 9U, MoveUtility.WhiteBishop)]
        [TestCase("8/8/8/8/8/8/8/N7 b - - 0 1", 0U, 17U, MoveUtility.WhiteKnight)]
        [TestCase("7p/8/8/8/8/8/8/8 b - - 0 1", 63U, 55U, MoveUtility.BlackPawn)]
        [TestCase("7r/8/8/8/8/8/8/8 b - - 0 1", 63U, 55U, MoveUtility.BlackRook)]
        [TestCase("7q/8/8/8/8/8/8/8 b - - 0 1", 63U, 55U, MoveUtility.BlackQueen)]
        [TestCase("7k/8/8/8/8/8/8/8 b - - 0 1", 63U, 55U, MoveUtility.BlackKing)]
        [TestCase("7b/8/8/8/8/8/8/8 b - - 0 1", 63U, 54U, MoveUtility.BlackBishop)]
        [TestCase("7n/8/8/8/8/8/8/8 b - - 0 1", 63U, 46U, MoveUtility.BlackKnight)]
        public void HashKey_Piece_Set_Correct_On_MakeMove(string fen, uint fromposition, uint toPosition,
            uint piece)
        {
            var gameState = new GameState(fen, _zobristHash);
            var move = 0U;
            move = move.SetFromMove(fromposition);
            move = move.SetToMove(toPosition);
            move = move.SetMovingPiece(piece);


            gameState.MakeMove(move, _zobristHash);
            Assert.That(gameState.HashKey, Is.Not.EqualTo(0));

            gameState.HashKey ^= _zobristHash.PieceArray[toPosition][piece];
            gameState.HashKey ^= _zobristHash.WhiteToMove;

            Assert.That(gameState.HashKey, Is.EqualTo(0));
        }

        [TestCase("8/8/8/8/8/8/p7/P7 b - - 0 1", 0U, 8U, MoveUtility.WhitePawn, MoveUtility.BlackPawn)]
        [TestCase("8/8/8/8/8/8/p7/R7 b - - 0 1", 0U, 8U, MoveUtility.WhiteRook, MoveUtility.BlackPawn)]
        [TestCase("8/8/8/8/8/8/p7/Q7 b - - 0 1", 0U, 8U, MoveUtility.WhiteQueen, MoveUtility.BlackPawn)]
        [TestCase("8/8/8/8/8/8/p7/K7 b - - 0 1", 0U, 8U, MoveUtility.WhiteKing, MoveUtility.BlackPawn)]
        [TestCase("8/8/8/8/8/8/1p6/B7 b - - 0 1", 0U, 9U, MoveUtility.WhiteBishop, MoveUtility.BlackPawn)]
        [TestCase("8/8/8/8/8/1p6/8/N7 b - - 0 1", 0U, 17U, MoveUtility.WhiteKnight, MoveUtility.BlackPawn)]
        [TestCase("7p/7P/8/8/8/8/8/8 b - - 0 1", 63U, 55U, MoveUtility.BlackPawn, MoveUtility.WhitePawn)]
        [TestCase("7r/7P/8/8/8/8/8/8 b - - 0 1", 63U, 55U, MoveUtility.BlackRook, MoveUtility.WhitePawn)]
        [TestCase("7q/7P/8/8/8/8/8/8 b - - 0 1", 63U, 55U, MoveUtility.BlackQueen, MoveUtility.WhitePawn)]
        [TestCase("7k/7P/8/8/8/8/8/8 b - - 0 1", 63U, 55U, MoveUtility.BlackKing, MoveUtility.WhitePawn)]
        [TestCase("7b/6P1/8/8/8/8/8/8 b - - 0 1", 63U, 54U, MoveUtility.BlackBishop, MoveUtility.WhitePawn)]
        [TestCase("7n/8/6P1/8/8/8/8/8 b - - 0 1", 63U, 46U, MoveUtility.BlackKnight, MoveUtility.WhitePawn)]
        public void HashKey_Piece_Set_Correct_On_MakeMove_Capture(string fen, uint fromposition, uint toPosition,
            uint piece, uint capturedPiece)
        {
            var gameState = new GameState(fen, _zobristHash);
            var move = 0U;
            move = move.SetFromMove(fromposition);
            move = move.SetToMove(toPosition);
            move = move.SetMovingPiece(piece);
            move = move.SetCapturedPiece(capturedPiece);


            gameState.MakeMove(move, _zobristHash);
            Assert.That(gameState.HashKey, Is.Not.EqualTo(0));

            gameState.HashKey ^= _zobristHash.PieceArray[toPosition][piece];
            gameState.HashKey ^= _zobristHash.WhiteToMove;

            Assert.That(gameState.HashKey, Is.EqualTo(0));
        }

        //EnPassants
        [TestCase("8/8/8/8/8/8/P7/8 b - - 0 1", 8U, 24U, MoveUtility.WhitePawn)]
        [TestCase("8/p7/8/8/8/8/8/8 b - - 0 1", 48U, 32U, MoveUtility.BlackPawn)]
        public void HashKey_Enpassant_Set_Correct_On_MakeMove_To_Enpassant(string fen, uint fromposition, uint toPosition,
            uint piece)
        {
            var gameState = new GameState(fen, _zobristHash);

            var move = 0U;
            move = move.SetFromMove(fromposition);
            move = move.SetToMove(toPosition);
            move = move.SetMovingPiece(piece);

            gameState.MakeMove(move, _zobristHash);
            Assert.That(gameState.HashKey, Is.Not.EqualTo(0));

            gameState.HashKey ^= _zobristHash.PieceArray[toPosition][piece];
            gameState.HashKey ^= _zobristHash.WhiteToMove;

            if (piece == MoveUtility.WhitePawn)
                gameState.HashKey ^= _zobristHash.EnPassantSquares[fromposition + 8];
            else
                gameState.HashKey ^= _zobristHash.EnPassantSquares[fromposition - 8];

            Assert.That(gameState.HashKey, Is.EqualTo(0));
        }

        [TestCase("8/8/8/8/P7/8/8/8 b - a3 0 1", 24U, 32U, MoveUtility.WhitePawn)]
        [TestCase("8/8/8/p7/8/8/8/8 b - a6 0 2", 32U, 24U, MoveUtility.BlackPawn)]
        public void HashKey_Enpassant_Set_Correct_On_MakeMove_From_Enpassant(string fen, uint fromposition, uint toPosition,
            uint piece)
        {
            var gameState = new GameState(fen, _zobristHash);

            var move = 0U;
            move = move.SetFromMove(fromposition);
            move = move.SetToMove(toPosition);
            move = move.SetMovingPiece(piece);

            gameState.MakeMove(move, _zobristHash);
            Assert.That(gameState.HashKey, Is.Not.EqualTo(0));

            gameState.HashKey ^= _zobristHash.PieceArray[toPosition][piece];
            gameState.HashKey ^= _zobristHash.WhiteToMove;

            Assert.That(gameState.HashKey, Is.EqualTo(0));
        }

        [TestCase("8/8/8/pP6/8/8/8/8 b - a6 0 2", 33U, 40U, MoveUtility.WhitePawn, MoveUtility.BlackPawn)]
        [TestCase("8/8/8/8/Pp6/8/8/8 b - a3 0 1", 25U, 16U, MoveUtility.BlackPawn, MoveUtility.WhitePawn)]
        public void HashKey_Enpassant_Set_Correct_On_MakeMove_Enpassant_Capture(string fen, uint fromposition, uint toPosition,
            uint piece, uint capturedPiece)
        {
            var gameState = new GameState(fen, _zobristHash);

            var move = 0U;
            move = move.SetFromMove(fromposition);
            move = move.SetToMove(toPosition);
            move = move.SetMovingPiece(piece);
            move = move.SetPromotionPiece(piece);
            move = move.SetCapturedPiece(capturedPiece);

            gameState.MakeMove(move, _zobristHash);
            Assert.That(gameState.HashKey, Is.Not.EqualTo(0));

            gameState.HashKey ^= _zobristHash.PieceArray[toPosition][piece];
            gameState.HashKey ^= _zobristHash.WhiteToMove;

            Assert.That(gameState.HashKey, Is.EqualTo(0));
        }

        
        [TestCase("8/8/8/8/8/8/8/4K3 w KQ - 0 1", 4U, 12U, MoveUtility.WhiteKing)]
        [TestCase("4k3/8/8/8/8/8/8/8 w kq - 0 1", 60U, 52U, MoveUtility.BlackKing)]
        public void HashKey_Castle_Sets_CastleStatus_When_King_Moves(string fen, uint fromMove, uint toMove, uint piece)
        {
            var gameState = new GameState(fen, _zobristHash);

            var move = 0U;
            move = move.SetFromMove(fromMove);
            move = move.SetToMove(toMove);
            move = move.SetMovingPiece(piece);

            gameState.MakeMove(move, _zobristHash);
            Assert.That(gameState.HashKey, Is.Not.EqualTo(0));

            gameState.HashKey ^= _zobristHash.PieceArray[toMove][piece];
           
            Assert.That(gameState.HashKey, Is.EqualTo(0));
        }

        [TestCase("8/8/8/8/8/8/8/7R w K - 0 1", 7U, 15U, MoveUtility.WhiteRook)]
        [TestCase("8/8/8/8/8/8/8/R7 w Q - 0 1", 0U, 8U, MoveUtility.WhiteRook)]
        [TestCase("7r/8/8/8/8/8/8/8 w k - 0 1", 63U, 55U, MoveUtility.BlackRook)]
        [TestCase("r7/8/8/8/8/8/8/8 w q - 0 1", 56U, 48U, MoveUtility.BlackRook)]
        public void HashKey_Castle_Sets_CastleStatus_When_Rook_Moves(string fen, uint fromMove, uint toMove, uint piece)
        {
            var gameState = new GameState(fen, _zobristHash);

            var move = 0U;
            move = move.SetFromMove(fromMove);
            move = move.SetToMove(toMove);
            move = move.SetMovingPiece(piece);

            gameState.MakeMove(move, _zobristHash);
            Assert.That(gameState.HashKey, Is.Not.EqualTo(0));

            gameState.HashKey ^= _zobristHash.PieceArray[toMove][piece];

            Assert.That(gameState.HashKey, Is.EqualTo(0));
        }

        [TestCase("8/8/8/8/8/8/P7/8 w - - 0 1", 8U, 16U, MoveUtility.WhitePawn)]
        [TestCase("8/p7/8/8/8/8/8/8 b - - 0 1", 48U, 40U, MoveUtility.BlackPawn)]
        public void HashKey_Side_Set_Correct_On_MakeMove(string fen, uint fromposition, uint toPosition,
            uint piece)
        {
            var gameState = new GameState(fen, _zobristHash);

            var move = 0U;
            move = move.SetFromMove(fromposition);
            move = move.SetToMove(toPosition);
            move = move.SetMovingPiece(piece);

            gameState.MakeMove(move, _zobristHash);
            Assert.That(gameState.HashKey, Is.Not.EqualTo(0));

            gameState.HashKey ^= _zobristHash.PieceArray[toPosition][piece];
            
            if (gameState.WhiteToMove)
                gameState.HashKey ^= _zobristHash.WhiteToMove;

            Assert.That(gameState.HashKey, Is.EqualTo(0));
        }

        //Promotions
        [TestCase("8/P7/8/8/8/8/8/8 w - - 0 1", 48U, 56U, MoveUtility.WhitePawn, MoveUtility.WhiteBishop)]
        [TestCase("8/P7/8/8/8/8/8/8 w - - 0 1", 48U, 56U, MoveUtility.WhitePawn, MoveUtility.WhiteKnight)]
        [TestCase("8/P7/8/8/8/8/8/8 w - - 0 1", 48U, 56U, MoveUtility.WhitePawn, MoveUtility.WhiteQueen)]
        [TestCase("8/P7/8/8/8/8/8/8 w - - 0 1", 48U, 56U, MoveUtility.WhitePawn, MoveUtility.WhiteRook)]
        [TestCase("8/8/8/8/8/8/p7/8 b - - 0 1", 8U, 0U, MoveUtility.BlackPawn, MoveUtility.BlackBishop)]
        [TestCase("8/8/8/8/8/8/p7/8 b - - 0 1", 8U, 0U, MoveUtility.BlackPawn, MoveUtility.BlackKnight)]
        [TestCase("8/8/8/8/8/8/p7/8 b - - 0 1", 8U, 0U, MoveUtility.BlackPawn, MoveUtility.BlackQueen)]
        [TestCase("8/8/8/8/8/8/p7/8 b - - 0 1", 8U, 0U, MoveUtility.BlackPawn, MoveUtility.BlackRook)]
        public void HashKey_Promotion_Set_Correct_On_MakeMove(string fen, uint fromposition, uint toPosition,
            uint piece, uint promotionPiece)
        {
            var gameState = new GameState(fen, _zobristHash);

            var move = 0U;
            move = move.SetFromMove(fromposition);
            move = move.SetToMove(toPosition);
            move = move.SetMovingPiece(piece);
            move = move.SetPromotionPiece(promotionPiece);

            gameState.MakeMove(move, _zobristHash);
            Assert.That(gameState.HashKey, Is.Not.EqualTo(0));

            gameState.HashKey ^= _zobristHash.PieceArray[toPosition][promotionPiece];

            if (gameState.WhiteToMove)
                gameState.HashKey ^= _zobristHash.WhiteToMove;

            Assert.That(gameState.HashKey, Is.EqualTo(0));
        }
        #endregion

        [TestCase("8/8/8/8/8/8/8/P7 b - - 0 1", 0U, 8U, MoveUtility.WhitePawn)]
        public void HashKey_Set_Correct_On_UnMake(string fen, uint fromposition, uint toPosition,
            uint piece)
        {
            var gameState = new GameState(fen, _zobristHash);
            var currentHash = gameState.HashKey;

            var move = 0U;
            move = move.SetFromMove(fromposition);
            move = move.SetToMove(toPosition);
            move = move.SetMovingPiece(piece);
            
            gameState.MakeMove(move, _zobristHash);
            Assert.That(gameState.HashKey, Is.Not.EqualTo(0));

            gameState.UnMakeMove(move);
            Assert.That(gameState.HashKey, Is.EqualTo(currentHash));
        }
    }
}
