using DotNetEngine.Engine.Helpers;
using DotNetEngine.Engine.Objects;
using NUnit.Framework;

namespace DotNetEngine.Test.MakeMoveTests
{
    public class MiscTests
    {
        private static readonly ZobristHash _zobristHash = new ZobristHash();

        #region MakeMove
        [TestCase("8/8/8/8/8/8/3P4/8 w - - 0 1", MoveUtility.WhitePawn, false)]
        [TestCase("8/8/8/8/8/8/3p4/8 b - - 0 1", MoveUtility.BlackPawn, true)]
        public void SideToMoveCorrect_After_Make_Move(string initialFen, uint movingPiece, bool whiteToMove)
        {
            var gameState = new GameState(initialFen, _zobristHash);

            var move = 0U;
            move = move.SetFromMove(11U);
            move = move.SetToMove(19U);
            move = move.SetMovingPiece(movingPiece);

            gameState.MakeMove(move, _zobristHash);

            Assert.That(gameState.WhiteToMove, Is.EqualTo(whiteToMove));
        }

        [Test]
        public void GameStateRecord_Updates_White_Castle_Status_Correctly_When_Make_Move()
        {
            var gameState = new GameState("8/8/8/8/8/8/8/R3K2R w KQ - 0 1", _zobristHash);
            var whiteCastleStatus = gameState.CurrentWhiteCastleStatus;

            var move = 0U;
            move = move.SetFromMove(4U);
            move = move.SetToMove(12U);
            move = move.SetMovingPiece(MoveUtility.WhiteKing);           

            gameState.MakeMove(move, _zobristHash);

            var gameStateRecord = gameState.PreviousGameStateRecords.Pop();

            Assert.That(gameStateRecord.CurrentWhiteCastleStatus, Is.EqualTo(whiteCastleStatus));
        }

        [Test]
        public void GameStateRecord_Updates_Black_Castle_Status_Correctly_When_Make_Move()
        {
            var gameState = new GameState("r3k2r/8/8/8/8/8/8/8 b kq - 0 1", _zobristHash);
            var blackCastleStatus = gameState.CurrentBlackCastleStatus;

            var move = 0U;
            move = move.SetFromMove(60U);
            move = move.SetToMove(52U);
            move = move.SetMovingPiece(MoveUtility.BlackKing);

            gameState.MakeMove(move, _zobristHash);

            var gameStateRecord = gameState.PreviousGameStateRecords.Pop();

            Assert.That(gameStateRecord.CurrentBlackCastleStatus, Is.EqualTo(blackCastleStatus));
        }

        [Test]
        public void GameStateRecord_Updates_EnPassant_Square_Correctly_When_Make_Move()
        {
            var gameState = new GameState("8/8/8/8/8/8/P7/8 b - - 0 1", _zobristHash);            

            var move = 0U;
            move = move.SetFromMove(48U);
            move = move.SetToMove(32U);
            move = move.SetMovingPiece(MoveUtility.BlackPawn);

            gameState.MakeMove(move, _zobristHash);

            var enPassantSquare = gameState.EnpassantTargetSquare;
            move = 0U;
            move = move.SetFromMove(33U);
            move = move.SetToMove(41U);
            move = move.SetMovingPiece(MoveUtility.WhitePawn);

            gameState.MakeMove(move, _zobristHash);

            var gameStateRecord = gameState.PreviousGameStateRecords.Pop();

            Assert.That(gameStateRecord.EnpassantTargetSquare, Is.EqualTo(enPassantSquare));
        }

        [Test]
        public void GameStateRecord_Updates_FiftyMoveRule_Correctly_When_Make_Move()
        {
            var gameState = new GameState("r3k2r/8/8/8/8/8/8/8 b kq - 0 24", _zobristHash);
            var fiftyMoveRule = gameState.FiftyMoveRuleCount;

            var move = 0U;
            move = move.SetFromMove(60U);
            move = move.SetToMove(52U);
            move = move.SetMovingPiece(MoveUtility.BlackKing);

            gameState.MakeMove(move, _zobristHash);

            var gameStateRecord = gameState.PreviousGameStateRecords.Pop();

            Assert.That(gameStateRecord.FiftyMoveRuleCount, Is.EqualTo(fiftyMoveRule));
        }
        #endregion

        #region UnMake
        [TestCase("8/8/8/8/8/8/3P4/8 w - - 0 1", MoveUtility.WhitePawn, false)]
        [TestCase("8/8/8/8/8/8/3p4/8 b - - 0 1", MoveUtility.BlackPawn, true)]
        public void SideToMoveCorrect_After_UnMake_Move(string initialFen, uint movingPiece, bool whiteToMove)
        {
            var gameState = new GameState(initialFen, _zobristHash);            
            gameState.PreviousGameStateRecords.Push(new GameStateRecord());

            gameState.UnMakeMove(0U);

            Assert.That(gameState.WhiteToMove, Is.EqualTo(whiteToMove));
        }
        #endregion
    }
}
