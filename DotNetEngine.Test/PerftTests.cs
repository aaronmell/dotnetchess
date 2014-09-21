using DotNetEngine.Engine;
using NUnit.Framework;

namespace DotNetEngine.Test
{
    /// <summary>
    /// These are the Perft tests that validate the move path enumeration. Caution: They take awhile to run all of them.
    /// </summary>
    public class PerftTests
    {
        private readonly MoveData _moveData = new MoveData();
        
      

        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 0, 1)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 1, 20)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 2, 400)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 3, 8902)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 4, 197281)]
        //[TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 5, 4865609)]
        //[TestCase("rnbqkbnr/ppppp2p/8/5PpQ/8/8/PPPP1PPP/RNB1KBNR b KQkq - 0 3", 1, 1)]
        public void RunPerft(string fen, int depth, long moveCount)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(fen);
            var perftData = new PerftData();
            var count = gameState.RunPerftRecursively(_moveData, perftData, 1, depth);            
            Assert.That(count, Is.EqualTo(moveCount));
        }

        //[TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 5)]
        //[TestCase("rnbqkbnr/pppppppp/8/8/8/2N5/PPPPPPPP/R1BQKBNR b KQkq - 1 1", 4)]
        //[TestCase("rnbqkbnr/p1pppppp/8/1p6/8/N7/PPPPPPPP/R1BQKBNR w KQkq b6 0 2", 4)] //B7B5 Divide
        //[TestCase("rnbqkbnr/p1pppppp/8/1p6/2N5/8/PPPPPPPP/R1BQKBNR b KQkq - 1 2", 5)] //A3C4 Divide
        //[TestCase("rnbqkbnr/p1pppppp/8/8/2p5/8/PPPPPPPP/R1BQKBNR w KQkq - 0 3", 5)]
        public void RunDivide(string fen, int depth)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(fen);
            var perftData = new PerftData();
            gameState.CalculateDivide(_moveData, perftData, 1, depth);         
        }
    }
}
