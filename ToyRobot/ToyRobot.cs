using ToyRobot.Models;
using ToyRobot.Utils;

namespace ToyRobot
{
    public class ToyRobot : IToyRobot
    {
        public Direction Direction { get; internal set;}

        public Position Position { get; set; }

        public ToyRobot(Position position, Direction direction)
        {
            Position = position;
            Direction = direction;
        }
        public Position GetNextPosition()
        {
            switch (Direction)
            {
                case Direction.North:
                    return new Position(Position.X,Position.Y+1);

                case Direction.South:
                    return new Position(Position.X,Position.Y-1);

                case Direction.West:
                    return new Position(Position.X-1,Position.Y);

                case Direction.East:
                    return new Position(Position.X+1,Position.Y);
            }
            return Position;
        }

        public void RotateLeft()
        {
            Direction = Direction.Previous();
        }

        public void RotateRight()
        {
            Direction = Direction.Next();
        }

        public string Report()
        {
            return $"Output: {Position.X},{Position.Y},{Direction.ToString().ToUpper()}";
        }
    }
}
