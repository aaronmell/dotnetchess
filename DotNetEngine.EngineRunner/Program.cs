using DotNetEngine.Engine;
using System;

namespace DotNetEngine.EngineRunner
{
	class Program
	{
        private static MoveData _moveData;
        private static GameState _gameState;

		private static void Main(string[] args)
		{
            _moveData = new MoveData();
            var boardLoaded = false;

			while (true)
			{
				var  rawCommand = Console.ReadLine();
                var commandArguments = rawCommand.Substring(rawCommand.IndexOf(' ') + 1);
                var command = string.Empty;

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
                            _gameState = GameStateUtility.LoadGameStateFromFen(commandArguments);
                            boardLoaded = true;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Unable to Load FEN. FEN is not valid");
                        }                       
                        break;
                    }
                    case "PERFT":
                    {
                        if (!boardLoaded)
                        {
                            Console.WriteLine("Board Not Loaded");
                            break;
                        }

                        byte depth = 0;
                        var canParse = byte.TryParse(commandArguments, out depth);

                        if (!canParse)
                        {
                            Console.WriteLine("Depth is not a valid value");
                            break;
                        }

                        var count = _gameState.RunPerftRecursively(_moveData, new PerftData(), 1, depth);
                        Console.WriteLine("Total Nodes {0}", count);
                        break;
                    }
                    case "DIVIDE":
                    {
                        if (!boardLoaded)
                        {
                            Console.WriteLine("Board Not Loaded");
                            break;
                        }

                        byte depth = 0;
                        var canParse = byte.TryParse(commandArguments, out depth);

                        if (!canParse)
                        {
                            Console.WriteLine("Depth is not a valid value");
                            break;
                        }

                        var output = _gameState.CalculateDivide(_moveData, new PerftData(), 1, depth);
                        Console.WriteLine(output);
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
	}
}
