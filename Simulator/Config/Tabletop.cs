using ToyRobot.Models;

namespace Simulator.Config
{
    public class Tabletop : ITabletop
    {
        public int Width { get; private set; }
        public int Length { get; private set; }

        public bool IsValidPosition(Position position)
        {
            return position.X < Width && position.X >= 0 &&
                   position.Y < Length && position.Y >= 0;
        }

        public Tabletop(int width, int length)
        {
            this.Width = width;
            this.Length = length;
        }
    }
}