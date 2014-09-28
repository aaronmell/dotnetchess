using System;
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
                            }

                            _engine.TryMakeMove(parameters[parameters.Length - 1]);
                        }
                        else 
                        {
                           _engine.TryMakeMove(parameters[parameters.Length - 1]);
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

        static void _engine_BestMoveFound(object sender, BestMoveFoundEventArgs e)
        {
            Console.WriteLine("bestmove {0}", e.BestMove);
        }
	}
}
