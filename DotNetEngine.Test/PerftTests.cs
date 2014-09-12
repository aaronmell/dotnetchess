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

        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 0, 1)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 1, 20)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 2, 400)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 3, 8902)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 4, 197281)]
        public void RunPerft(string fen, int depth, long moveCount)
        {
            var gameState = GameStateUtility.LoadStateFromFen(fen);

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

                count += RunPerftRecursively(gameState, ply + 1, depth - 1);

                gameState.UnMakeMove(move);
            }
            return count;
        }

    }
}
