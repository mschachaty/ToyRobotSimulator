using ToyRobot.Models;

namespace Simulator.ConsoleParser
{
    public class PlaceCommandParameter
    {
        public Position Position { get; set; }
        public Direction? Direction { get; set; }

        public PlaceCommandParameter(Position position, Direction? direction = null)
        {
            Position = position;
            Direction = direction ?? this.Direction;
        }
    }
}