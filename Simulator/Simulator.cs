using ToyRobot.Models;
using Simulator.Config;
using Simulator.Common;
using Simulator.ConsoleParser;
using ToyRobot;

namespace Simulator
{
    public class Simulator : ISimulator
    {
        public IToyRobot? ToyRobot { get; internal set; }
        private readonly ITabletop tableTop;
        private string[] commands;
        private readonly Dictionary<Command, Func<string>> Methods = new Dictionary<Command, Func<string>>();

        public string HandleCommand(Command command)
        {
            if (!Methods.ContainsKey(command))
                throw new ArgumentException(Errors.InvalidCommand);

            return Methods[command]();
        }

        public Simulator(ITabletop tableTop)
        {
            this.tableTop = tableTop;

            Methods.Add(Command.Place, new Func<string>(Place));
            Methods.Add(Command.Move, new Func<string>(Move));
            Methods.Add(Command.Left, new Func<string>(Left));
            Methods.Add(Command.Right, new Func<string>(Right));
            Methods.Add(Command.Report, new Func<string>(GetReport));
        }

        private string Left()
        {
            ToyRobot?.RotateLeft();
            return String.Empty;
        }

        private string Right()
        {
            ToyRobot?.RotateRight();
            return String.Empty;
        }

        private string Move()
        {
            if (ToyRobot != null)
            {
                var newPosition = ToyRobot.GetNextPosition();
                if (tableTop.IsValidPosition(newPosition))
                    ToyRobot.Position = newPosition;
            }
            return String.Empty;
        }

        private string Place()
        {
            var placeCommandParameter = commands.ParseParameters();

            if (!tableTop.IsValidPosition(placeCommandParameter.Position))
                return String.Empty;

            if (placeCommandParameter.Direction == null)
            {
                if (ToyRobot == null)
                    throw new ArgumentException(Errors.MissingDirection);

                // Update position only
                ToyRobot.Position = placeCommandParameter.Position;
            }
            else
                // instansiate / override with new ToyRobot
                ToyRobot = new ToyRobot.ToyRobot(placeCommandParameter.Position, (Direction)placeCommandParameter.Direction);

            return String.Empty;
        }

        ///<summary>
        /// ProcessCommand & Check command and any input params are valid
        ///</summary> 
        public string ProcessCommand(string[] commands)
        {
            if (!commands[0].ParseCommand(out Command command))
                throw new ArgumentException(Errors.InvalidCommand);

            this.commands = commands;
            return HandleCommand(command);
        }

        ///<summary>
        /// GetReport: & Check command and any input params are valid
        ///</summary> 
        public string GetReport()
        {
            if (ToyRobot == null)
                return String.Empty;

            return ToyRobot.Report();
        }
    }

}