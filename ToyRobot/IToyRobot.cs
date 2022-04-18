using ToyRobot.Models;

namespace ToyRobot
{
    public interface IToyRobot
    {
        public Direction Direction { get; }
        public Position Position { get; set; }
        Position GetNextPosition();
        string Report();
        void RotateLeft();
        void RotateRight();
    }
}
