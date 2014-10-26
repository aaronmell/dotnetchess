using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Common.Logging;
using DotNetEngine.Engine.Enums;
using DotNetEngine.Engine.Helpers;
using DotNetEngine.Engine.Objects;

namespace DotNetEngine.Engine
{
    /// <summary>
	/// The Chess Engine, This class handles actually playing the game.
	/// </summary>
	internal class Engine
    {
        #region Private Fields
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
		private ILog _logger;
        private static readonly MoveData _moveData = new MoveData();
        private GameState _gameState;
	    private static readonly ZobristHash _zobristHash = new ZobristHash();
	    private const int CheckMateScore = 10001;
        private bool _stopRaised;
	    private int _timeToMove;
        private static readonly Stopwatch _stopwatch = new Stopwatch();
	    private int _nodeCount;
	    private int _checkTimeInterval;
        /// <summary>
        /// Item1 = Depth Item2 = Flag Item3 = Move Item4 = Score
        /// </summary>
        private Dictionary<ulong, Tuple<int, int, uint, int>> _transpositionTable;
        
        #endregion

        #region Internal Properties
        /// <summary>
        /// If set to true, continue to calculate until stop has been issued by the program
        /// </summary>
        internal bool InfiniteTime { get; set; }

        /// <summary>
        /// If set to a positive value, calculate the current position to this depth.
        /// </summary>
        internal int CalculateToDepth { get; set; }

        /// <summary>
        /// If set to a positive value, this amount of time in ms is allowed to make the move
        /// </summary>
        internal int MoveTime { get; set; }

        /// <summary>
        /// If set to a positive value, search for the mate in this number of moves. 
        /// </summary>
        internal int MateDepth { get; set; }

        /// <summary>
        /// If set to a positive value, search a maximum of this number of nodes
        /// </summary>
        internal int MaxNodes { get; set; }

        /// <summary>
        /// If set to a positive value, this is the amount of time in mswhite has remaining
        /// </summary>
        internal int WhiteTime { get; set; }

        /// <summary>
        /// If set to a positive value, this is the amount of time in ms black has remaining
        /// </summary>
        internal int BlackTime { get; set; }

        /// <summary>
        /// If set to a positive value, this is the amount of time in ms added to each white move.
        /// </summary>
        internal int WhiteIncrementTime { get; set; }

        /// <summary>
        /// If set to a positive value, this is the amount of time in ms added to each black move
        /// </summary>
        internal int BlackIncrementTime { get; set; }

        /// <summary>
        /// If set to a positive value, thi s is the number of moves remaining until the time controls change. 
        /// </summary>
        internal int MovesUntilNextTimeControl { get; set; }
        #endregion

        #region Events
        internal event EventHandler<BestMoveFoundEventArgs> BestMoveFound;
       
        #endregion
        #region Internal Methods

        internal Engine() :
            this(LogManager.GetCurrentClassLogger())
        {
            
        }

        internal Engine(ILog logger)
        {
            _logger = logger;
            _transpositionTable = new Dictionary<ulong, Tuple<int, int, uint, int>>();
        }
        
        internal void SetBoard(string initialFen)
        {
            _gameState = new GameState(initialFen, _zobristHash);
        }

        internal void Perft(int depth)
        {
	        var perftData = new PerftData();
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			var count = _gameState.RunPerftRecursively(_moveData, _zobristHash, perftData, 1, depth);
			stopwatch.Stop();

			_logger.InfoFormat("Total Nodes: {0} {1} mS Elapsed: {2}", count, Environment.NewLine, stopwatch.ElapsedMilliseconds);
			_logger.DebugFormat("Total Nodes: {0} Total Captures {1} Total Checks {2} Total EnPassants {3} Total OO Castles {4} Total OOO Castles {5} Total Promotions {6}", count, perftData.TotalCaptures, perftData.TotalChecks, perftData.TotalEnpassants, perftData.TotalOOCastles, perftData.TotalOOOCastles, perftData.TotalPromotions);
        }

        internal void Divide(int depth)
        {
            _gameState.CalculateDivide(_moveData, _zobristHash, new PerftData(), 1, depth);
        }

        internal void NewGame(string fen)
        {
            _gameState = new GameState(fen, _zobristHash);
        }

	    internal void NewGame() 
	    {
            _gameState = new GameState(_zobristHash);
	    }

        internal void TryMakeMove(string moveText)
        {   
            _logger.InfoFormat("Making Move");
            _gameState.GenerateMoves(MoveGenerationMode.All, 1, _moveData);

            uint move;
            var canParse = MoveUtility.TryConvertStringToMove(moveText.ToUpper(), _gameState.WhiteToMove, out move);

            if (!canParse)
            {
                _logger.ErrorFormat("Unable to Parse Move Text");
            }

            var foundMove = _gameState.Moves[1].FirstOrDefault(
                x =>
                    x.GetFromMove() == move.GetFromMove() && x.GetToMove() == move.GetToMove() && (move.GetPromotedPiece() == 0 ||
                    x.GetPromotedPiece() == move.GetPromotedPiece()));

            if (foundMove == uint.MinValue)
            {
                _logger.ErrorFormat("Unable to Locate Move in Move Generator");
                return;
            }

            _gameState.MakeMove(foundMove, _zobristHash);
            _logger.InfoFormat("Move Made");
        }

        internal void Calculate()
        {
            _stopRaised = false;
            _stopwatch.Restart();
            _nodeCount = 0;
            _checkTimeInterval = 100000;

            //To Prevent the array from throwing an out of bounds exception. Games shouldn't last enough moves for this to occur.
            if (_gameState.PreviousGameStateRecords.Length - _gameState.TotalMoveCount <= 50)
            {
                var array = _gameState.PreviousGameStateRecords;
                Array.Resize(ref array,
                   _gameState.PreviousGameStateRecords.Length + _gameState.TotalMoveCount);

                _gameState.PreviousGameStateRecords = array;
            }

            Task.Factory.StartNew(() =>
            {
                _gameState.GenerateMoves(MoveGenerationMode.All, _gameState.TotalMoveCount, _moveData);
                uint bestMove;
                var legalMoves = _gameState.CountLegalMovesAtCurrentPlyAndReturnMove(_moveData, _zobristHash, out bestMove);
                
                if (legalMoves > 1)
                {
                    var maxDepth = GetMaxDepth();
                    SetTimeToMove();

                    for (var currentDepth = 1; currentDepth <= maxDepth; currentDepth++)
                    {
                        _transpositionTable = new Dictionary<ulong, Tuple<int, int, uint, int>>();
                        var tuple = NegaMaxAlphaBetaRecursive(0, currentDepth, int.MinValue + 1, int.MaxValue - 1, true);

                        _logger.InfoFormat("info depth {0} cp {1} nps {2}, pv {3} time {4}", currentDepth, tuple.Item2, GetNodePerSecond(), tuple.Item1.ToMoveString(), _stopwatch.ElapsedMilliseconds);

                        //break out if we have a forced mate.
                        if (tuple.Item2 > CheckMateScore - currentDepth ||
                            tuple.Item2 < -(CheckMateScore - currentDepth))
                        {
                            bestMove = tuple.Item1;
                            break;
                        }

                        if (_stopRaised || IsTimeUp() || IsNodeCountExceeded())
                        {
                            if (bestMove == 0)
                                bestMove = tuple.Item1;

                            break;
                        }

                        bestMove = tuple.Item1;
                    }
                }

                _gameState.MakeMove(bestMove, _zobristHash);
                OnBestMoveFound(new BestMoveFoundEventArgs
                {
                    BestMove = bestMove.ToMoveString()
                });

                _stopwatch.Stop();

            }).ContinueWith(task =>
            {
                Debug.Assert(task.Exception != null);

                _logger.ErrorFormat("Exception Occured while caclulating. Exception: {0}", task.Exception.InnerException);

                _stopwatch.Stop();
            }, TaskContinuationOptions.OnlyOnFaulted);

        }

        private long GetNodePerSecond()
        {
            if (_stopwatch.ElapsedMilliseconds == 0)
                return _nodeCount;

            return _nodeCount/(_stopwatch.ElapsedMilliseconds) * 100;
        }

        private bool IsNodeCountExceeded()
        {
            if (MaxNodes > 0)
                return _nodeCount > MaxNodes;

            return false;
        }
        #endregion

        #region Protected/Private Methods
        protected virtual void OnBestMoveFound(BestMoveFoundEventArgs e)
        {
            var handler = BestMoveFound;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        private Tuple<uint, int> NegaMaxAlphaBetaRecursive(int ply, int depth, int alpha, int beta, bool side)
        {
            if (_transpositionTable.ContainsKey(_gameState.HashKey))
            {
                var value = _transpositionTable[_gameState.HashKey];

                if (value.Item1 >= depth)
                {
                    switch (value.Item2)
                    {
                        case 0:
                        {
                             if (value.Item4 > 9900||
                                value.Item4 < -9900)
                             {
                                 return new Tuple<uint, int>(value.Item3, value.Item4 + (depth * (side ? 1 : -1)));
                             }

                            return new Tuple<uint, int>(value.Item3, value.Item4);
                        }
                        case 1:
                        {
                            alpha = Math.Max(alpha, value.Item4);
                            break;
                        }
                        case 2:
                        {
                            beta = Math.Min(beta, value.Item4);
                            break;
                        }
                    }
                    if (alpha >= beta)
                         return new Tuple<uint, int>(value.Item3, value.Item4);
                }
            }

            if (depth <= 0)
            {
                _nodeCount++;
                return new Tuple<uint, int>(0U, _gameState.Evaluate() * (side ? 1 : -1));
            }

            if (_gameState.IsThreeMoveRepetition())
                return new Tuple<uint, int>(0U, 0);

            var bestValue = int.MinValue + 1;
            var bestMove = 0U;
            var alphaOriginal = alpha;
            var movesFound = 0;
            _gameState.GenerateMoves(MoveGenerationMode.All, ply, _moveData);

            foreach (var move in _gameState.Moves[ply])
            {
                _gameState.MakeMove(move, _zobristHash);
                
                if (!_gameState.IsOppositeSideKingAttacked(_moveData))
                {
                    movesFound++;

                    var value = -(NegaMaxAlphaBetaRecursive(ply + 1, depth - 1, -beta, -alpha, !side).Item2);

                    _gameState.UnMakeMove(move);

                    if (--_checkTimeInterval < 0 && IsTimeUp())
                        return new Tuple<uint, int>(0U, 0); ;

                    if (value > bestValue)
                    {
                        bestValue = value;
                        bestMove = move;
                    }

                    alpha = Math.Max(alpha, value);

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

            
            if (_gameState.FiftyMoveRuleCount >= 100)
            {
                bestValue = 0;
                bestMove = 0;
            }    
            else if (movesFound != 0)
            {
                
            }
            else if (_gameState.IsCurrentSideKingAttacked(_moveData))
            {
                return new Tuple<uint, int>(0U, (side ? 1 : -1)*(CheckMateScore - ply));
            }
            else
            {
                bestValue = 0;
                bestMove = 0;
            }

            var flag = 0;
            if (bestValue <= alphaOriginal)
            {
                flag = 2;
            }
            else if (bestValue >= beta)
            {
                flag = 1;
            }

            var tuple = new Tuple<int, int, uint, int>(depth, flag, bestMove, bestValue);

            if (_transpositionTable.ContainsKey(_gameState.HashKey))
                _transpositionTable[_gameState.HashKey] = tuple;
            else _transpositionTable.Add(_gameState.HashKey, tuple);

            return new Tuple<uint, int>(bestMove, bestValue); ;
        }

        private int GetMaxDepth()
        {
            if (CalculateToDepth > 0)
                return CalculateToDepth;

            if (MateDepth > 0)
                return MateDepth;

            return InfiniteTime ? int.MaxValue : 16;
        }

        private void SetTimeToMove()
        {
            if (InfiniteTime || MaxNodes > 0 || MateDepth > 0 || CalculateToDepth > 0)
            {
                _timeToMove = int.MaxValue;
                return;
            }

            if (MoveTime > 0)
            {
                _timeToMove = MoveTime;
                return;
            }

            var remainingMoves = 80 - _gameState.TotalMoveCount;

            if (remainingMoves < 20)
                remainingMoves = 20;

            var remainingTime = _gameState.WhiteToMove ? WhiteTime + WhiteIncrementTime : BlackTime + BlackIncrementTime;

            _timeToMove = remainingTime / remainingMoves;
        }

        private bool IsTimeUp()
        {
            if (_stopwatch.ElapsedMilliseconds > _timeToMove)
            {
                return true;
            }
            
            _checkTimeInterval = 100000;
            return false;
        }

        internal void Stop()
        {
            _logger.InfoFormat("Attempting to Stop");
            _stopRaised = true;
        }
        #endregion
    }
}
