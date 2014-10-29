using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Common.Logging;
using DotNetEngine.Engine.Objects;
using JetBrains.Annotations;

namespace DotNetEngine.Engine
{
    /// <summary>
    /// The Runner Class, a shim between the engine and the Engine Runner.
    /// </summary>
    public class Runner
    {
        private const string EngineId = "id name DotNetEngine {0}";
        private const string AuthorId = "id author Aaron A Mell";
        private bool _newGame;

        private static readonly Version _version = System.Reflection.Assembly.GetExecutingAssembly()
            .GetName()
            .Version;

        private static Engine _engine;

        private static ILog _logger;
      
        internal Runner([NotNull] ILog logger)
        {
            _engine = new Engine();
            _engine.BestMoveFound += _engine_BestMoveFound;

            _engine.NewGame();

            _logger = logger;
        }

        public Runner()
        {
            _engine = new Engine();
            _engine.BestMoveFound += _engine_BestMoveFound;

            _engine.NewGame();
            
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void ProcessCommand(string command, string[] commandArguments)
        {
            switch (command.ToUpper())
            {
                case "SETBOARD":
                    {
                        try
                        {
                            var sb = new StringBuilder();

                            foreach (var argument in commandArguments)
                            {
                                sb.Append(argument);
                                sb.Append(" ");
                            }

                            _engine.SetBoard(sb.ToString());
                        }
                        catch (Exception)
                        {
                             _logger.Error("Unable to Load FEN. FEN is not valid");
                        }
                        break;
                    }
                case "PERFT":
                    {
                        byte depth;
                        var canParse = byte.TryParse(commandArguments.FirstOrDefault(), out depth);

                        if (!canParse)
                        {
                            _logger.Error("Depth is not a valid value");
                            break;
                        }
                        _engine.Perft(depth);
                        break;
                    }
                case "DIVIDE":
                    {
                        byte depth;
                        var canParse = byte.TryParse(commandArguments.FirstOrDefault(), out depth);

                        if (!canParse)
                        {
                            _logger.Error("Depth is not a valid value");
                            break;
                        }

                        _engine.Divide(depth);
                        break;
                    }
                case "UCI":
                {
                    _logger.InfoFormat(EngineId, _version);
                    _logger.InfoFormat(AuthorId);
                    _logger.Info("uciok");
                   
                    break;
                }
                case "ISREADY":
                {
                    _logger.Info("readyok");
                    break;
                }
                case "POSITION":
                    {

                        if (_newGame)
                        {
                            if (commandArguments[0] == "fen")
                            {
                                var fen = string.Format("{0} {1} {2} {3} {4} {5}", commandArguments[1], commandArguments[2],
                               commandArguments[3], commandArguments[4], commandArguments[5], commandArguments[6]);

                                _engine.NewGame(fen);
                            }

                            var movesIndex = Array.IndexOf(commandArguments, "moves");

                            for (int i = movesIndex + 1; i <= commandArguments.Length - 1; i++)
                            {
                                _engine.TryMakeMove(commandArguments[i]);
                            }
                        }
                        else
                        {
                            _engine.TryMakeMove(commandArguments[commandArguments.Length - 1]);
                        }

                        _newGame = false;
                        break;
                    }
                case "UCINEWGAME":
                    {
                        _engine = new Engine();
                        _engine.NewGame();
                        _engine.BestMoveFound += _engine_BestMoveFound;

                        _newGame = true;
                        break;
                    }
                case "STOP":
                    {
                        _engine.Stop();
                        break;
                    }
                case "GO":
                    {
                        SetEngineMoveParameters(commandArguments);
                        _engine.Calculate();
                        break;
                    }
                default:
                    {
                        _logger.ErrorFormat("Invalid Command Entered");
                        break;
                    }
            }
        }
      
        private static void SetEngineMoveParameters(string[] commandArguments)
        {
            _engine.InfiniteTime = false;
            _engine.CalculateToDepth = -1;
            _engine.MoveTime = -1;
            _engine.MateDepth = -1;
            _engine.MaxNodes = -1;
            _engine.WhiteTime = -1;
            _engine.BlackTime = -1;
            _engine.WhiteIncrementTime = -1;
            _engine.BlackIncrementTime = -1;
            _engine.MovesUntilNextTimeControl = -1;

            if (commandArguments.Any(x => x == "infinite"))
            {
                _engine.InfiniteTime = true;
            }

            var depthIndex = Array.IndexOf(commandArguments, "depth");

            if (depthIndex != -1)
            {
                _engine.CalculateToDepth = int.Parse(commandArguments[depthIndex + 1]);
            }

            var moveTimeIndex = Array.IndexOf(commandArguments, "moveTime");

            if (moveTimeIndex != -1)
            {
                _engine.MoveTime = int.Parse(commandArguments[moveTimeIndex + 1]);
            }

            var mateDepthIndex = Array.IndexOf(commandArguments, "mate");

            if (mateDepthIndex != -1)
            {
                _engine.MateDepth = int.Parse(commandArguments[mateDepthIndex + 1]);
            }

            var maxNodesIndex = Array.IndexOf(commandArguments, "nodes");

            if (maxNodesIndex != -1)
            {
                _engine.MaxNodes = int.Parse(commandArguments[maxNodesIndex + 1]);
            }

            var whiteTimeIndex = Array.IndexOf(commandArguments, "wtime");

            if (whiteTimeIndex != -1)
            {
                _engine.WhiteTime = int.Parse(commandArguments[whiteTimeIndex + 1]);
            }

            var blackTimeIndex = Array.IndexOf(commandArguments, "btime");

            if (blackTimeIndex != -1)
            {
                _engine.BlackTime = int.Parse(commandArguments[blackTimeIndex + 1]);
            }

            var whiteIncrementTimeIndex = Array.IndexOf(commandArguments, "winc");

            if (whiteIncrementTimeIndex != -1)
            {
                _engine.WhiteIncrementTime = int.Parse(commandArguments[whiteIncrementTimeIndex + 1]);
            }

            var blackIncrementTimeIndex = Array.IndexOf(commandArguments, "binc");

            if (blackIncrementTimeIndex != -1)
            {
                _engine.BlackIncrementTime = int.Parse(commandArguments[blackIncrementTimeIndex + 1]);
            }

            var movesToGoIndex = Array.IndexOf(commandArguments, "movestogo");

            if (movesToGoIndex != -1)
            {
                _engine.MovesUntilNextTimeControl = int.Parse(commandArguments[movesToGoIndex + 1]);
            }
        }

        static void _engine_BestMoveFound(object sender, BestMoveFoundEventArgs e)
        {
            _logger.InfoFormat("bestmove {0}", e.BestMove);
        }
    }
}
