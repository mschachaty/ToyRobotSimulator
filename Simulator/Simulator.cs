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
        public ITabletop TableTop { get; internal set; }

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
            this.TableTop = tableTop;

            Methods.Add(Command.Place, new Func<string>(Place));
            Methods.Add(Command.Move, new Func<string>(Move));
            Methods.Add(Command.Left, new Func<string>(Left));
            Methods.Add(Command.Right, new Func<string>(Right));
            Methods.Add(Command.Report, new Func<string>(GetReport));
            Methods.Add(Command.Avoid, new Func<string>(Avoid));
        }
        // The AVOID command should be discarded if it tells the robot to avoid the current coordinates or if the given coordinates fall outside of the table surface.
        // AVOID will inform the robot about an obstruction on the table in position X,Y.
        // e.g. AVOID 2,2
        private string Avoid()
        {
            if (ToyRobot == null)
                return String.Empty;

            var avoidCommandParameter = commands.ParseParameters();

            if (!TableTop.IsValidPosition(avoidCommandParameter.Position))
                return String.Empty;

            if (ToyRobot.Position.X == avoidCommandParameter.Position.X || ToyRobot.Position.Y == avoidCommandParameter.Position.Y)
                return String.Empty;

            TableTop.AvoidPositions.Add(avoidCommandParameter.Position);
            
            return String.Empty;
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
                if (TableTop.IsValidPosition(newPosition))
                    ToyRobot.Position = newPosition;
            }
            return String.Empty;
        }

        private string Place()
        {
            var placeCommandParameter = commands.ParseParameters();

            if (!TableTop.IsValidPosition(placeCommandParameter.Position))
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