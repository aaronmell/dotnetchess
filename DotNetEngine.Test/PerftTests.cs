using DotNetEngine.Engine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEngine.Test
{
    /// <summary>
    /// These are the Perft tests that validate the move path enumeration. Caution: They take awhile to run all of them.
    /// </summary>
    public class PerftTests
    {
        private MoveData _moveData = new MoveData();

        private long numberOfCaptures = 0;
        private long numberOfEnpassants = 0;
        private long numberOfPromotions = 0;
        private long numberOfOOCastles = 0;
        private long numberOfOOOCastles = 0;
        private long numberOfChecks = 0;
        private long checkmates = 0;

        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 0, 1)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 1, 20)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 2, 400)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 3, 8902)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 4, 197281)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 5, 4865609)]
        public void RunPerft(string fen, int depth, long moveCount)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(fen);

            var count = RunPerftRecursively(gameState, 1, depth);
            Assert.That(count, Is.EqualTo(moveCount));

        }

        private int RunPerftRecursively(GameState gameState, int ply, int depth)
        {           

            if (depth == 0)
            {
                return 1;
            }

            var count = 0;

            gameState.GenerateMoves(MoveGenerationMode.All, ply, _moveData);

            foreach (var move in gameState.Moves[ply])
            {
                gameState.MakeMove(move);

                if (!gameState.IsOppositeSideKingAttacked(_moveData))
                {
                    count += RunPerftRecursively(gameState, ply + 1, depth - 1);

                    if (move.IsPieceCaptured())
                        numberOfCaptures++;
                    if (move.IsEnPassant())
                        numberOfEnpassants++;
                    if(move.IsPromotion())
                        numberOfPromotions++;
                    if(move.IsCastleOO())
                        numberOfOOCastles++;
                    if (move.IsCastleOOO())
                        numberOfOOOCastles++;
                    if (gameState.IsCurrentSideKingAttacked(_moveData))
                        numberOfChecks++;
                    if (move.GetCapturedPiece() == MoveUtility.WhiteKing || move.GetCapturedPiece() == MoveUtility.BlackKing)
                        checkmates++;
                }
                  

                gameState.UnMakeMove(move);
            }
            return count;
        }

    }
}
