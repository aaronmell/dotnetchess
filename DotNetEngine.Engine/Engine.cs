using System;
using System.Diagnostics;

namespace DotNetEngine.Engine
{
	/// <summary>
	/// The Chess Engine
	/// </summary>
	public class Engine
	{
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

        public string Perft(int depth)
		{
			var stopwatch = new Stopwatch();
	        stopwatch.Start();
			var count = _gameState.RunPerftRecursively(_moveData, new PerftData(), 1, depth);
			stopwatch.Stop();

			return string.Format(string.Format("Total Nodes: {0} {1} mS Elapsed: {2}", count, Environment.NewLine, stopwatch.ElapsedMilliseconds));
        }

        public string Divide(int depth)
        {
            return _gameState.CalculateDivide(_moveData, new PerftData(), 1, depth);
        }
	}
}
