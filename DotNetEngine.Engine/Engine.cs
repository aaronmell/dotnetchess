using System;
using System.Diagnostics;
using Common.Logging;

namespace DotNetEngine.Engine
{
	/// <summary>
	/// The Chess Engine
	/// </summary>
	public class Engine
	{
		private ILog Logger = LogManager.GetCurrentClassLogger();
        private static readonly MoveData _moveData = new MoveData();
        private GameState _gameState;

       	public Engine()
        {
            SetBoard("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
        }

        public void SetBoard(string initialFen)
        {
            _gameState = GameStateUtility.LoadGameStateFromFen(initialFen);
        }

        public void Perft(int depth)
        {
	        var perftData = new PerftData();
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			var count = _gameState.RunPerftRecursively(_moveData, perftData, 1, depth);
			stopwatch.Stop();

			Logger.InfoFormat("Total Nodes: {0} {1} mS Elapsed: {2}", count, Environment.NewLine, stopwatch.ElapsedMilliseconds);
			Logger.DebugFormat("Total Nodes: {0} Total Captures {1} Total Checks {2} Total EnPassants {3} Total OO Castles {4} Total OOO Castles {5} Total Promotions {6}", count, perftData.TotalCaptures, perftData.TotalChecks, perftData.TotalEnpassants, perftData.TotalOOCastles, perftData.TotalOOOCastles, perftData.TotalPromotions);
        }

        public void Divide(int depth)
        {
            _gameState.CalculateDivide(_moveData, new PerftData(), 1, depth);
        }
	}
}
