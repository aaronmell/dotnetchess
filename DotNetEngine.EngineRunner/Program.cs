using System;
using DotNetEngine.Engine;

namespace DotNetEngine.EngineRunner
{
	class Program
	{
	    

		private static void Main()
		{
		    var runner = new Runner();

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

			    if (command.ToUpper() == "QUIT")
			        return;

			    runner.ProcessCommand(command, commandArguments.Split(' '));
                Console.WriteLine();
			}
		}
	}
}
