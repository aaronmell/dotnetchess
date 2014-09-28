using DotNetEngine.Engine.Helpers;
using DotNetEngine.Engine.Objects;
using NUnit.Framework;

namespace DotNetEngine.Test.UnMakeMoveTests
{
    public class UnCapturePiecesTests
    {
        [TestCase("8/8/8/8/8/4P3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhitePawn, MoveUtility.BlackPawn)]
        [TestCase("8/8/8/8/8/4B3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhiteBishop, MoveUtility.BlackPawn)]
        [TestCase("8/8/8/8/8/4Q3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhiteQueen, MoveUtility.BlackPawn)]
        [TestCase("8/8/8/8/8/4K3/8/8 b - - 0 11", 11U, 20U, MoveUtility.WhiteKing, MoveUtility.BlackPawn)]
        [TestCase("8/8/8/8/4N3/8/8/8 b - - 0 1", 11U, 28U, MoveUtility.WhiteKnight, MoveUtility.BlackPawn)]
        [TestCase("8/8/8/8/8/3R4/8/8 b - - 0 1", 11U, 19U, MoveUtility.WhiteRook, MoveUtility.BlackPawn)]
        [TestCase("8/8/8/8/8/4p3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackPawn, MoveUtility.WhitePawn)]
        [TestCase("8/8/8/8/8/4b3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackBishop, MoveUtility.WhitePawn)]
        [TestCase("8/8/8/8/8/4q3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackQueen, MoveUtility.WhitePawn)]
        [TestCase("8/8/8/8/8/4k3/8/8 w - - 0 11", 11U, 20U, MoveUtility.BlackKing, MoveUtility.WhitePawn)]
        [TestCase("8/8/8/8/4n3/8/8/8 w - - 0 1", 11U, 28U, MoveUtility.BlackKnight, MoveUtility.WhitePawn)]
        [TestCase("8/8/8/8/8/3r4/8/8 w - - 0 1", 11U, 19U, MoveUtility.BlackRook, MoveUtility.WhitePawn)]
        public void Pawn_Bitboard_Correct_After_Capture(string initialFen, uint fromMove, uint toMove, uint movingPiece, uint capturedPiece)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(initialFen);
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(fromMove);
            move = move.SetToMove(toMove);
            move = move.SetMovingPiece(movingPiece);
            move = move.SetCapturedPiece(capturedPiece);

            gameState.UnMakeMove(move);

            Assert.That(movingPiece < 8 ? gameState.BlackPawns : gameState.WhitePawns,
                        Is.EqualTo(MoveUtility.BitStates[toMove]));
        }

        [TestCase("8/8/8/8/8/4P3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhitePawn, MoveUtility.BlackRook)]
        [TestCase("8/8/8/8/8/4B3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhiteBishop, MoveUtility.BlackRook)]
        [TestCase("8/8/8/8/8/4Q3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhiteQueen, MoveUtility.BlackRook)]
        [TestCase("8/8/8/8/8/4K3/8/8 b - - 0 11", 11U, 20U, MoveUtility.WhiteKing, MoveUtility.BlackRook)]
        [TestCase("8/8/8/8/4N3/8/8/8 b - - 0 1", 11U, 28U, MoveUtility.WhiteKnight, MoveUtility.BlackRook)]
        [TestCase("8/8/8/8/8/3R4/8/8 b - - 0 1", 11U, 19U, MoveUtility.WhiteRook, MoveUtility.BlackRook)]
        [TestCase("8/8/8/8/8/4p3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackPawn, MoveUtility.WhiteRook)]
        [TestCase("8/8/8/8/8/4b3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackBishop, MoveUtility.WhiteRook)]
        [TestCase("8/8/8/8/8/4q3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackQueen, MoveUtility.WhiteRook)]
        [TestCase("8/8/8/8/8/4k3/8/8 w - - 0 11", 11U, 20U, MoveUtility.BlackKing, MoveUtility.WhiteRook)]
        [TestCase("8/8/8/8/4n3/8/8/8 w - - 0 1", 11U, 28U, MoveUtility.BlackKnight, MoveUtility.WhiteRook)]
        [TestCase("8/8/8/8/8/3r4/8/8 w - - 0 1", 11U, 19U, MoveUtility.BlackRook, MoveUtility.WhiteRook)]
        public void Rook_Bitboard_Correct_After_Capture(string initialFen, uint fromMove, uint toMove, uint movingPiece, uint capturedPiece)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(initialFen);
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(fromMove);
            move = move.SetToMove(toMove);
            move = move.SetMovingPiece(movingPiece);
            move = move.SetCapturedPiece(capturedPiece);

            gameState.UnMakeMove(move);

            Assert.That(movingPiece < 8 ? gameState.BlackRooks : gameState.WhiteRooks,
                        Is.EqualTo(MoveUtility.BitStates[toMove]));
        }

        [TestCase("8/8/8/8/8/4P3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhitePawn, MoveUtility.BlackBishop)]
        [TestCase("8/8/8/8/8/4B3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhiteBishop, MoveUtility.BlackBishop)]
        [TestCase("8/8/8/8/8/4Q3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhiteQueen, MoveUtility.BlackBishop)]
        [TestCase("8/8/8/8/8/4K3/8/8 b - - 0 11", 11U, 20U, MoveUtility.WhiteKing, MoveUtility.BlackBishop)]
        [TestCase("8/8/8/8/4N3/8/8/8 b - - 0 1", 11U, 28U, MoveUtility.WhiteKnight, MoveUtility.BlackBishop)]
        [TestCase("8/8/8/8/8/3R4/8/8 b - - 0 1", 11U, 19U, MoveUtility.WhiteRook, MoveUtility.BlackBishop)]
        [TestCase("8/8/8/8/8/4p3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackPawn, MoveUtility.WhiteBishop)]
        [TestCase("8/8/8/8/8/4b3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackBishop, MoveUtility.WhiteBishop)]
        [TestCase("8/8/8/8/8/4q3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackQueen, MoveUtility.WhiteBishop)]
        [TestCase("8/8/8/8/8/4k3/8/8 w - - 0 11", 11U, 20U, MoveUtility.BlackKing, MoveUtility.WhiteBishop)]
        [TestCase("8/8/8/8/4n3/8/8/8 w - - 0 1", 11U, 28U, MoveUtility.BlackKnight, MoveUtility.WhiteBishop)]
        [TestCase("8/8/8/8/8/3r4/8/8 w - - 0 1", 11U, 19U, MoveUtility.BlackRook, MoveUtility.WhiteBishop)]
        public void Bishop_Bitboard_Correct_After_Capture(string initialFen, uint fromMove, uint toMove, uint movingPiece, uint capturedPiece)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(initialFen);
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(fromMove);
            move = move.SetToMove(toMove);
            move = move.SetMovingPiece(movingPiece);
            move = move.SetCapturedPiece(capturedPiece);

            gameState.UnMakeMove(move);

            Assert.That(movingPiece < 8 ? gameState.BlackBishops : gameState.WhiteBishops,
                        Is.EqualTo(MoveUtility.BitStates[toMove]));
        }

        [TestCase("8/8/8/8/8/4P3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhitePawn, MoveUtility.BlackKnight)]
        [TestCase("8/8/8/8/8/4B3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhiteBishop, MoveUtility.BlackKnight)]
        [TestCase("8/8/8/8/8/4Q3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhiteQueen, MoveUtility.BlackKnight)]
        [TestCase("8/8/8/8/8/4K3/8/8 b - - 0 11", 11U, 20U, MoveUtility.WhiteKing, MoveUtility.BlackKnight)]
        [TestCase("8/8/8/8/4N3/8/8/8 b - - 0 1", 11U, 28U, MoveUtility.WhiteKnight, MoveUtility.BlackKnight)]
        [TestCase("8/8/8/8/8/3R4/8/8 b - - 0 1", 11U, 19U, MoveUtility.WhiteRook, MoveUtility.BlackKnight)]
        [TestCase("8/8/8/8/8/4p3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackPawn, MoveUtility.WhiteKnight)]
        [TestCase("8/8/8/8/8/4b3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackBishop, MoveUtility.WhiteKnight)]
        [TestCase("8/8/8/8/8/4q3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackQueen, MoveUtility.WhiteKnight)]
        [TestCase("8/8/8/8/8/4k3/8/8 w - - 0 11", 11U, 20U, MoveUtility.BlackKing, MoveUtility.WhiteKnight)]
        [TestCase("8/8/8/8/4n3/8/8/8 w - - 0 1", 11U, 28U, MoveUtility.BlackKnight, MoveUtility.WhiteKnight)]
        [TestCase("8/8/8/8/8/3r4/8/8 w - - 0 1", 11U, 19U, MoveUtility.BlackRook, MoveUtility.WhiteKnight)]
        public void Knights_Bitboard_Correct_After_Capture(string initialFen, uint fromMove, uint toMove, uint movingPiece, uint capturedPiece)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(initialFen);
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(fromMove);
            move = move.SetToMove(toMove);
            move = move.SetMovingPiece(movingPiece);
            move = move.SetCapturedPiece(capturedPiece);

            gameState.UnMakeMove(move);

            Assert.That(movingPiece < 8 ? gameState.BlackKnights : gameState.WhiteKnights,
                        Is.EqualTo(MoveUtility.BitStates[toMove]));
        }

        [TestCase("8/8/8/8/8/4P3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhitePawn, MoveUtility.BlackQueen)]
        [TestCase("8/8/8/8/8/4B3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhiteBishop, MoveUtility.BlackQueen)]
        [TestCase("8/8/8/8/8/4Q3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhiteQueen, MoveUtility.BlackQueen)]
        [TestCase("8/8/8/8/8/4K3/8/8 b - - 0 11", 11U, 20U, MoveUtility.WhiteKing, MoveUtility.BlackQueen)]
        [TestCase("8/8/8/8/4N3/8/8/8 b - - 0 1", 11U, 28U, MoveUtility.WhiteKnight, MoveUtility.BlackQueen)]
        [TestCase("8/8/8/8/8/3R4/8/8 b - - 0 1", 11U, 19U, MoveUtility.WhiteRook, MoveUtility.BlackQueen)]
        [TestCase("8/8/8/8/8/4p3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackPawn, MoveUtility.WhiteQueen)]
        [TestCase("8/8/8/8/8/4b3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackBishop, MoveUtility.WhiteQueen)]
        [TestCase("8/8/8/8/8/4q3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackQueen, MoveUtility.WhiteQueen)]
        [TestCase("8/8/8/8/8/4k3/8/8 w - - 0 11", 11U, 20U, MoveUtility.BlackKing, MoveUtility.WhiteQueen)]
        [TestCase("8/8/8/8/4n3/8/8/8 w - - 0 1", 11U, 28U, MoveUtility.BlackKnight, MoveUtility.WhiteQueen)]
        [TestCase("8/8/8/8/8/3r4/8/8 w - - 0 1", 11U, 19U, MoveUtility.BlackRook, MoveUtility.WhiteQueen)]
        public void Queens_Bitboard_Correct_After_Capture(string initialFen, uint fromMove, uint toMove, uint movingPiece, uint capturedPiece)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(initialFen);
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(fromMove);
            move = move.SetToMove(toMove);
            move = move.SetMovingPiece(movingPiece);
            move = move.SetCapturedPiece(capturedPiece);

            gameState.UnMakeMove(move);

            Assert.That(movingPiece < 8 ? gameState.BlackQueens : gameState.WhiteQueens,
                        Is.EqualTo(MoveUtility.BitStates[toMove]));
        }

        [TestCase("8/8/8/8/8/4P3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhitePawn, MoveUtility.BlackKing)]
        [TestCase("8/8/8/8/8/4B3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhiteBishop, MoveUtility.BlackKing)]
        [TestCase("8/8/8/8/8/4Q3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhiteQueen, MoveUtility.BlackKing)]
        [TestCase("8/8/8/8/8/4K3/8/8 b - - 0 11", 11U, 20U, MoveUtility.WhiteKing, MoveUtility.BlackKing)]
        [TestCase("8/8/8/8/4N3/8/8/8 b - - 0 1", 11U, 28U, MoveUtility.WhiteKnight, MoveUtility.BlackKing)]
        [TestCase("8/8/8/8/8/3R4/8/8 b - - 0 1", 11U, 19U, MoveUtility.WhiteRook, MoveUtility.BlackKing)]
        [TestCase("8/8/8/8/8/4p3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackPawn, MoveUtility.WhiteKing)]
        [TestCase("8/8/8/8/8/4b3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackBishop, MoveUtility.WhiteKing)]
        [TestCase("8/8/8/8/8/4q3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackQueen, MoveUtility.WhiteKing)]
        [TestCase("8/8/8/8/8/4k3/8/8 w - - 0 11", 11U, 20U, MoveUtility.BlackKing, MoveUtility.WhiteKing)]
        [TestCase("8/8/8/8/4n3/8/8/8 w - - 0 1", 11U, 28U, MoveUtility.BlackKnight, MoveUtility.WhiteKing)]
        [TestCase("8/8/8/8/8/3r4/8/8 w - - 0 1", 11U, 19U, MoveUtility.BlackRook, MoveUtility.WhiteKing)]
        public void King_Bitboard_Correct_After_Capture(string initialFen, uint fromMove, uint toMove, uint movingPiece, uint capturedPiece)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(initialFen);
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(fromMove);
            move = move.SetToMove(toMove);
            move = move.SetMovingPiece(movingPiece);
            move = move.SetCapturedPiece(capturedPiece);

            gameState.UnMakeMove(move);

            Assert.That(movingPiece < 8 ? gameState.BlackKing : gameState.WhiteKing,
                        Is.EqualTo(MoveUtility.BitStates[toMove]));
        }

        [TestCase("8/8/8/8/8/4P3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhitePawn, MoveUtility.BlackPawn)]
        [TestCase("8/8/8/8/8/4B3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhiteBishop, MoveUtility.BlackPawn)]
        [TestCase("8/8/8/8/8/4Q3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhiteQueen, MoveUtility.BlackPawn)]
        [TestCase("8/8/8/8/8/4K3/8/8 b - - 0 11", 11U, 20U, MoveUtility.WhiteKing, MoveUtility.BlackPawn)]
        [TestCase("8/8/8/8/4N3/8/8/8 b - - 0 1", 11U, 28U, MoveUtility.WhiteKnight, MoveUtility.BlackPawn)]
        [TestCase("8/8/8/8/8/3R4/8/8 b - - 0 1", 11U, 19U, MoveUtility.WhiteRook, MoveUtility.BlackPawn)]
        [TestCase("8/8/8/8/8/4p3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackPawn, MoveUtility.WhitePawn)]
        [TestCase("8/8/8/8/8/4b3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackBishop, MoveUtility.WhitePawn)]
        [TestCase("8/8/8/8/8/4q3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackQueen, MoveUtility.WhitePawn)]
        [TestCase("8/8/8/8/8/4k3/8/8 w - - 0 11", 11U, 20U, MoveUtility.BlackKing, MoveUtility.WhitePawn)]
        [TestCase("8/8/8/8/4n3/8/8/8 w - - 0 1", 11U, 28U, MoveUtility.BlackKnight, MoveUtility.WhitePawn)]
        [TestCase("8/8/8/8/8/3r4/8/8 w - - 0 1", 11U, 19U, MoveUtility.BlackRook, MoveUtility.WhitePawn)]
        [TestCase("8/8/8/8/8/4P3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhitePawn, MoveUtility.BlackRook)]
        [TestCase("8/8/8/8/8/4B3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhiteBishop, MoveUtility.BlackRook)]
        [TestCase("8/8/8/8/8/4Q3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhiteQueen, MoveUtility.BlackRook)]
        [TestCase("8/8/8/8/8/4K3/8/8 b - - 0 11", 11U, 20U, MoveUtility.WhiteKing, MoveUtility.BlackRook)]
        [TestCase("8/8/8/8/4N3/8/8/8 b - - 0 1", 11U, 28U, MoveUtility.WhiteKnight, MoveUtility.BlackRook)]
        [TestCase("8/8/8/8/8/3R4/8/8 b - - 0 1", 11U, 19U, MoveUtility.WhiteRook, MoveUtility.BlackRook)]
        [TestCase("8/8/8/8/8/4p3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackPawn, MoveUtility.WhiteRook)]
        [TestCase("8/8/8/8/8/4b3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackBishop, MoveUtility.WhiteRook)]
        [TestCase("8/8/8/8/8/4q3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackQueen, MoveUtility.WhiteRook)]
        [TestCase("8/8/8/8/8/4k3/8/8 w - - 0 11", 11U, 20U, MoveUtility.BlackKing, MoveUtility.WhiteRook)]
        [TestCase("8/8/8/8/4n3/8/8/8 w - - 0 1", 11U, 28U, MoveUtility.BlackKnight, MoveUtility.WhiteRook)]
        [TestCase("8/8/8/8/8/3r4/8/8 w - - 0 1", 11U, 19U, MoveUtility.BlackRook, MoveUtility.WhiteRook)]
        [TestCase("8/8/8/8/8/4P3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhitePawn, MoveUtility.BlackKnight)]
        [TestCase("8/8/8/8/8/4B3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhiteBishop, MoveUtility.BlackKnight)]
        [TestCase("8/8/8/8/8/4Q3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhiteQueen, MoveUtility.BlackKnight)]
        [TestCase("8/8/8/8/8/4K3/8/8 b - - 0 11", 11U, 20U, MoveUtility.WhiteKing, MoveUtility.BlackKnight)]
        [TestCase("8/8/8/8/4N3/8/8/8 b - - 0 1", 11U, 28U, MoveUtility.WhiteKnight, MoveUtility.BlackKnight)]
        [TestCase("8/8/8/8/8/3R4/8/8 b - - 0 1", 11U, 19U, MoveUtility.WhiteRook, MoveUtility.BlackKnight)]
        [TestCase("8/8/8/8/8/4p3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackPawn, MoveUtility.WhiteKnight)]
        [TestCase("8/8/8/8/8/4b3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackBishop, MoveUtility.WhiteKnight)]
        [TestCase("8/8/8/8/8/4q3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackQueen, MoveUtility.WhiteKnight)]
        [TestCase("8/8/8/8/8/4k3/8/8 w - - 0 11", 11U, 20U, MoveUtility.BlackKing, MoveUtility.WhiteKnight)]
        [TestCase("8/8/8/8/4n3/8/8/8 w - - 0 1", 11U, 28U, MoveUtility.BlackKnight, MoveUtility.WhiteKnight)]
        [TestCase("8/8/8/8/8/3r4/8/8 w - - 0 1", 11U, 19U, MoveUtility.BlackRook, MoveUtility.WhiteKnight)]
        [TestCase("8/8/8/8/8/4P3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhitePawn, MoveUtility.BlackKing)]
        [TestCase("8/8/8/8/8/4B3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhiteBishop, MoveUtility.BlackKing)]
        [TestCase("8/8/8/8/8/4Q3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhiteQueen, MoveUtility.BlackKing)]
        [TestCase("8/8/8/8/8/4K3/8/8 b - - 0 11", 11U, 20U, MoveUtility.WhiteKing, MoveUtility.BlackKing)]
        [TestCase("8/8/8/8/4N3/8/8/8 b - - 0 1", 11U, 28U, MoveUtility.WhiteKnight, MoveUtility.BlackKing)]
        [TestCase("8/8/8/8/8/3R4/8/8 b - - 0 1", 11U, 19U, MoveUtility.WhiteRook, MoveUtility.BlackKing)]
        [TestCase("8/8/8/8/8/4p3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackPawn, MoveUtility.WhiteKing)]
        [TestCase("8/8/8/8/8/4b3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackBishop, MoveUtility.WhiteKing)]
        [TestCase("8/8/8/8/8/4q3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackQueen, MoveUtility.WhiteKing)]
        [TestCase("8/8/8/8/8/4k3/8/8 w - - 0 11", 11U, 20U, MoveUtility.BlackKing, MoveUtility.WhiteKing)]
        [TestCase("8/8/8/8/4n3/8/8/8 w - - 0 1", 11U, 28U, MoveUtility.BlackKnight, MoveUtility.WhiteKing)]
        [TestCase("8/8/8/8/8/3r4/8/8 w - - 0 1", 11U, 19U, MoveUtility.BlackRook, MoveUtility.WhiteKing)]
        [TestCase("8/8/8/8/8/4P3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhitePawn, MoveUtility.BlackKing)]
        [TestCase("8/8/8/8/8/4B3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhiteBishop, MoveUtility.BlackKing)]
        [TestCase("8/8/8/8/8/4Q3/8/8 b - - 0 1", 11U, 20U, MoveUtility.WhiteQueen, MoveUtility.BlackKing)]
        [TestCase("8/8/8/8/8/4K3/8/8 b - - 0 11", 11U, 20U, MoveUtility.WhiteKing, MoveUtility.BlackKing)]
        [TestCase("8/8/8/8/4N3/8/8/8 b - - 0 1", 11U, 28U, MoveUtility.WhiteKnight, MoveUtility.BlackKing)]
        [TestCase("8/8/8/8/8/3R4/8/8 b - - 0 1", 11U, 19U, MoveUtility.WhiteRook, MoveUtility.BlackKing)]
        [TestCase("8/8/8/8/8/4p3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackPawn, MoveUtility.WhiteKing)]
        [TestCase("8/8/8/8/8/4b3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackBishop, MoveUtility.WhiteKing)]
        [TestCase("8/8/8/8/8/4q3/8/8 w - - 0 1", 11U, 20U, MoveUtility.BlackQueen, MoveUtility.WhiteKing)]
        [TestCase("8/8/8/8/8/4k3/8/8 w - - 0 11", 11U, 20U, MoveUtility.BlackKing, MoveUtility.WhiteKing)]
        [TestCase("8/8/8/8/4n3/8/8/8 w - - 0 1", 11U, 28U, MoveUtility.BlackKnight, MoveUtility.WhiteKing)]
        [TestCase("8/8/8/8/8/3r4/8/8 w - - 0 1", 11U, 19U, MoveUtility.BlackRook, MoveUtility.WhiteKing)]
        public void Color_Bitboard_Correct_After_Capture(string initialFen, uint fromMove, uint toMove, uint movingPiece, uint capturedPiece)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(initialFen);
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            var move = 0U;
            move = move.SetFromMove(fromMove);
            move = move.SetToMove(toMove);
            move = move.SetMovingPiece(movingPiece);
            move = move.SetCapturedPiece(capturedPiece);

            gameState.MakeMove(move);

            Assert.That(movingPiece < 8 ? gameState.BlackPieces : gameState.WhitePieces,
                        Is.EqualTo(MoveUtility.BitStates[toMove]));
        }       
    }
}
