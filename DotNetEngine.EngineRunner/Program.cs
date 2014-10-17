using System;
using System.Linq;
using DotNetEngine.Engine.Objects;

namespace DotNetEngine.EngineRunner
{
	class Program
	{
	    private const string EngineId = "id name DotNetEngine {0}";
        private const string AuthorId = "id author Aaron A Mell";

        private static Engine.Engine _engine;

		private static void Main()
		{
			_engine= new Engine.Engine();
            _engine.BestMoveFound += _engine_BestMoveFound;

            _engine.NewGame();
            var version = System.Reflection.Assembly.GetExecutingAssembly()
                                           .GetName()
                                           .Version
                                           .ToString();
		    bool newGame = false;
			while (true)
			{

				var  rawCommand = Console.ReadLine();

			    if (string.IsNullOrEmpty(rawCommand))
			        continue;

                var commandArguments = rawCommand.Substring(rawCommand.IndexOf(' ') + 1);
                string command;

                if (commandArguments.Length >= rawCommand.Length)
                {
                    command = commandArguments;
                    commandArguments = string.Empty;
                }
                else
                {
                    command = rawCommand.Substring(0, rawCommand.IndexOf(' '));
                }                

                switch (command.ToUpper())
                {
                    case "SETBOARD":
                    {
                        try
                        {
                            _engine.SetBoard(commandArguments);                            
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Unable to Load FEN. FEN is not valid");
                        }                       
                        break;
                    }
                    case "PERFT":
	                {
		               
						
                        byte depth;
                        var canParse = byte.TryParse(commandArguments, out depth);

                        if (!canParse)
                        {
                            Console.WriteLine("Depth is not a valid value");
                            break;
                        }
                        _engine.Perft(depth);
                        break;
                    }
                    case "DIVIDE":
                    {                       
                        byte depth;
                        var canParse = byte.TryParse(commandArguments, out depth);

                        if (!canParse)
                        {
                            Console.WriteLine("Depth is not a valid value");
                            break;
                        }

                        _engine.Divide(depth);
                        break;
                    }
                    case "UCI":
                    {
                        Console.WriteLine(EngineId, version);
                        Console.WriteLine(AuthorId);
                        Console.WriteLine("uciok");
                        break;
                    }
                    case "QUIT":
                    {
                        return;
                    }
                    case "ISREADY":
                    {
                        Console.WriteLine("readyok");
                        break;
                    }
                    case "POSITION":
                    {
                        var parameters = commandArguments.Split(' ');
                        
                        if (parameters[0] == "fen")
                        {
                            if (newGame)
                            {
                                 var fen = string.Format("{0} {1} {2} {3} {4} {5}", parameters[1], parameters[2],
                                parameters[3], parameters[4], parameters[5], parameters[6]);

                                _engine.NewGame(fen);

                                var movesIndex = Array.IndexOf(parameters, "moves");

                                for (int i = movesIndex + 1; i <= parameters.Length - 1; i++)
                                {
                                    _engine.TryMakeMove(parameters[i]);
                                }
                            }
                            else
                            {
                                _engine.TryMakeMove(parameters[parameters.Length - 1]);
                            }
                        }

                        newGame = false;
                        break;
                    }
                    case "UCINEWGAME":
                    {
                        _engine = new Engine.Engine();
                        _engine.NewGame();
                        _engine.BestMoveFound += _engine_BestMoveFound;
                        
                        newGame = true;
                        break;
                    }
                    case "STOP":
                    {
                        _engine.Stop();
                        break;
                    }
                    case "GO":
                    {
                        SetEngineMoveParameters(_engine, commandArguments);
                        _engine.Calculate();
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("Invalid Command Entered");
                        break;
                    }
                }
                Console.WriteLine();
			}
		}

        private static void SetEngineMoveParameters(Engine.Engine engine, string commandArguments)
        {
            engine.InfiniteTime = false;
            engine.CalculateToDepth = -1;
            engine.MoveTime = -1;
            engine.MateDepth = -1;
            engine.MaxNodes = -1;
            engine.WhiteTime = -1;
            engine.BlackTime = -1;
            engine.WhiteIncrementTime = -1;
            engine.BlackIncrementTime = -1;
            engine.MovesUntilNextTimeControl = -1;
           
            var parameters = commandArguments.Split(' ');
            
            if (parameters.Any(x => x == "infinite"))
            {
                engine.InfiniteTime = true;
            }

            var depthIndex = Array.IndexOf(parameters, "depth");

            if (depthIndex != -1)
            {
                engine.CalculateToDepth = int.Parse(parameters[depthIndex + 1]);
            }

            var moveTimeIndex = Array.IndexOf(parameters, "moveTime");

            if (moveTimeIndex != -1)
            {
                engine.MoveTime = int.Parse(parameters[moveTimeIndex + 1]);
            }

            var mateDepthIndex = Array.IndexOf(parameters, "mate");

            if (mateDepthIndex != -1)
            {
                engine.MateDepth = int.Parse(parameters[mateDepthIndex + 1]);
            }

            var maxNodesIndex = Array.IndexOf(parameters, "nodes");

            if (maxNodesIndex != -1)
            {
                engine.MaxNodes = int.Parse(parameters[maxNodesIndex + 1]);
            }

            var whiteTimeIndex = Array.IndexOf(parameters, "wtime");

            if (whiteTimeIndex != -1)
            {
                engine.WhiteTime = int.Parse(parameters[whiteTimeIndex + 1]);
            }

            var blackTimeIndex = Array.IndexOf(parameters, "btime");

            if (blackTimeIndex != -1)
            {
                engine.BlackTime = int.Parse(parameters[blackTimeIndex + 1]);
            }

            var whiteIncrementTimeIndex = Array.IndexOf(parameters, "winc");

            if (whiteIncrementTimeIndex != -1)
            {
                engine.WhiteIncrementTime = int.Parse(parameters[whiteIncrementTimeIndex + 1]);
            }

            var blackIncrementTimeIndex = Array.IndexOf(parameters, "binc");

            if (blackIncrementTimeIndex != -1)
            {
                engine.BlackIncrementTime = int.Parse(parameters[blackIncrementTimeIndex + 1]);
            }

            var movesToGoIndex = Array.IndexOf(parameters, "movestogo");

            if (movesToGoIndex != -1)
            {
                engine.MovesUntilNextTimeControl = int.Parse(parameters[movesToGoIndex + 1]);
            }

        }

        static void _engine_BestMoveFound(object sender, BestMoveFoundEventArgs e)
        {
            Console.WriteLine("bestmove {0}", e.BestMove);
        }
	}
}
