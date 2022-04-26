using ToyRobot.Models;

namespace Simulator.ConsoleParser
{
    public class ParseCommandParameter
    {
        public Position Position { get; set; }
        public Direction? Direction { get; set; }

        public ParseCommandParameter(Position position, Direction? direction = null)
        {
            Position = position;
            Direction = direction ?? this.Direction;
        }
    }
}