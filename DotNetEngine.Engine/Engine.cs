using System;
using System.Diagnostics;
using System.Linq;
using Common.Logging;
using DotNetEngine.Engine.Enums;
using DotNetEngine.Engine.Helpers;
using DotNetEngine.Engine.Objects;

namespace DotNetEngine.Engine
{
	/// <summary>
	/// The Chess Engine, This class handles actually playing the game.
	/// </summary>
	public class Engine
	{
// ReSharper disable once FieldCanBeMadeReadOnly.Local
		private ILog _logger = LogManager.GetCurrentClassLogger();
        private static readonly MoveData _moveData = new MoveData();
        private GameState _gameState;
	    private static readonly ZobristHash _zobristHash = new ZobristHash();

        public event EventHandler<BestMoveFoundEventArgs> BestMoveFound;

	    protected virtual void OnBestMoveFound(BestMoveFoundEventArgs e)
	    {
	        var handler = BestMoveFound;

	        if (handler != null)
	        {
	            handler(this, e);
	        }
	    }

       	public void SetBoard(string initialFen)
        {
            _gameState = new GameState(initialFen, _zobristHash);
        }

        public void Perft(int depth)
        {
	        var perftData = new PerftData();
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			var count = _gameState.RunPerftRecursively(_moveData, _zobristHash, perftData, 1, depth);
			stopwatch.Stop();

			_logger.InfoFormat("Total Nodes: {0} {1} mS Elapsed: {2}", count, Environment.NewLine, stopwatch.ElapsedMilliseconds);
			_logger.DebugFormat("Total Nodes: {0} Total Captures {1} Total Checks {2} Total EnPassants {3} Total OO Castles {4} Total OOO Castles {5} Total Promotions {6}", count, perftData.TotalCaptures, perftData.TotalChecks, perftData.TotalEnpassants, perftData.TotalOOCastles, perftData.TotalOOOCastles, perftData.TotalPromotions);
        }

        public void Divide(int depth)
        {
            _gameState.CalculateDivide(_moveData, _zobristHash, new PerftData(), 1, depth);
        }

        public void NewGame(string fen)
        {
            _gameState = new GameState(fen, _zobristHash);
        }

	    public void NewGame() 
	    {
            _gameState = new GameState(_zobristHash);
	    }

        public void TryMakeMove(string moveText)
        {   
            _logger.InfoFormat("Making Other Side Move");
            _gameState.GenerateMoves(MoveGenerationMode.All, 1, _moveData);

            uint move;
            var canParse = MoveUtility.TryConvertStringToMove(moveText.ToUpper(), _gameState.WhiteToMove, out move);

            if (!canParse)
            {
                _logger.ErrorFormat("Unable to Parse Move Text");
            }

            var foundMove = _gameState.Moves[1].FirstOrDefault(
                x =>
                    x.GetFromMove() == move.GetFromMove() && x.GetToMove() == move.GetToMove() &&
                    x.GetPromotedPiece() == move.GetPromotedPiece());

            if (foundMove == uint.MinValue)
            {
                _logger.ErrorFormat("Unable to Locate Move in Move Generator");
                return;
            }

            _gameState.MakeMove(foundMove, _zobristHash);
            _gameState.TotalMoveCount++;
            _logger.InfoFormat("Other Side Move Made");
        }

        public void Calculate()
        {
            var move = NegaMaxAlpaBeta(_gameState.TotalMoveCount, 6);
            _gameState.MakeMove(move, _zobristHash);

            OnBestMoveFound(new BestMoveFoundEventArgs
            {
                BestMove = string.Format("{0}{1}", move.GetFromMove().ToRankAndFile(), move.GetToMove().ToRankAndFile()).ToLower()
            });
        }

        public void Stop()
        {
            //Do nothing for now.
        }

	    public uint NegaMaxAlpaBeta(int ply, int depth)
	    {
            _gameState.GenerateMoves(MoveGenerationMode.All, ply, _moveData);
            var bestValue = int.MinValue;
	        var bestMove = 0U;

            foreach (var move in _gameState.Moves[ply])
            {
                _gameState.MakeMove(move, _zobristHash);

                if (!_gameState.IsOppositeSideKingAttacked(_moveData))
                {
                    var value = NegaMaxAlphaBetaRecursive(ply + 1, depth - 1, int.MinValue, int.MaxValue, true) * -1;


                    if (value > bestValue)
                    {
                        bestValue = value;
                        bestMove = move;
                    }
                }
                _gameState.UnMakeMove(move);
            }
	        return bestMove;
	    }

        private int NegaMaxAlphaBetaRecursive(int ply, int depth, int alpha, int beta, bool side)
	    {
            if (depth == 0)
            {
                return _gameState.Evaluate() * (side ? 1 : -1);
            }

            var bestValue = int.MinValue;

            _gameState.GenerateMoves(MoveGenerationMode.All, ply, _moveData);

            foreach (var move in _gameState.Moves[ply])
            {
                _gameState.MakeMove(move, _zobristHash);

                if (!_gameState.IsOppositeSideKingAttacked(_moveData))
                {

                    var value = NegaMaxAlphaBetaRecursive(ply + 1, depth - 1, beta * -1, alpha * -1, !side) * -1;
                    _gameState.UnMakeMove(move);

                    if (value > bestValue)
                        bestValue = value;

                    if (value > alpha)
                        alpha = value;

                    if (alpha >= beta)
                    {
                        break;
                    }
                }
                else
                {
                    _gameState.UnMakeMove(move);
                }
            }
            return bestValue;
	    }
    }
}
