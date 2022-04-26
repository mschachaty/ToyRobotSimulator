using ToyRobot.Models;

namespace Simulator.Config
{
    public class Tabletop : ITabletop
    {
        public IList<Position> AvoidPositions { get; set; }
        
        public int Width { get; private set; }
        public int Length { get; private set; }

        public bool IsValidPosition(Position position)
        {
            var valid = position.X < Width && position.X >= 0 &&
                   position.Y < Length && position.Y >= 0 &&
                   (!AvoidPositions.Any(p => p.Y == position.Y && p.X == position.X));

            return valid;
        }

        public Tabletop(int width, int length)
        {
            this.Width = width;
            this.Length = length;
            AvoidPositions = new List<Position>();
        }
    }
}