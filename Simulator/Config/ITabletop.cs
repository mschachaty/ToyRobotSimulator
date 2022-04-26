using ToyRobot.Models;

namespace Simulator.Config
{
    public interface ITabletop
    {
        int Width { get; }
        int Length { get; }
        public IList<Position> AvoidPositions { get; set; }
        bool IsValidPosition(Position position);
    }
}
