using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Simulator.Common;
using Simulator.Config;



namespace Simulator
{
    class Program
    {
        private static ITabletop SetupTabletop(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args).Build();

            // Ask the service provider for the configuration abstraction.
            IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

            // Get values from the config given their key and their target type.
            var tableWidth = config.GetValue<int>("TableWidth");
            var tableLength = config.GetValue<int>("TableLength");

            var tabletop = new Tabletop(tableWidth, tableLength);
            return tabletop;

        }
        static void Main(string[] args)
        {
            var tabletop = SetupTabletop(args);
            // Move description to editor
            string description = string.Join(Environment.NewLine,
                $"Welcome to Toy Robot Simulation - This program allows for simulation of a toy robot moving on a square {tabletop.Width} x {tabletop.Length} tabletop.",
                "The first valid command to the robot is a PLACE command. After that, any of the below sequence commands may be issued:",
                "PLACE X,Y[,F] - where F is optional Direction after first PLACE i.e.: NORTH:SOUTH:EAST:WEST",
                "MOVE",
                "LEFT",
                "RIGHT",
                "REPORT",
                "EXIT");

            Console.WriteLine(description);


            ISimulator simulator = new Simulator(tabletop);

            var exit = false;
            do
            {
                var command = Console.ReadLine();
                if (command == null) continue;

                if (command.ToUpper().Equals("EXIT"))
                    exit = true;
                else
                {
                    try
                    {
                        var output = simulator.ProcessCommand(command.SplitStringRemoveEmptyAndTrim(' '));
                        if (!string.IsNullOrEmpty(output))
                            Console.WriteLine(output);
                    }
                    catch (ArgumentException exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                }
            } while (!exit);
        }
    }
}