using System;

namespace DotNetEngine.EngineRunner
{
	class Program
	{
        private static Engine.Engine _engine;

		private static void Main(string[] args)
		{
            _engine= new Engine.Engine();            

			while (true)
			{
				var  rawCommand = Console.ReadLine();
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

                        var output = _engine.Perft(depth);
                        Console.WriteLine(output);
                        
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

                        var output = _engine.Divide(depth);
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
