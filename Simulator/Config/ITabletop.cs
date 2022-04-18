using ToyRobot.Models;

namespace Simulator.Config
{
    public interface ITabletop
    {
        int Width { get; }  
        int Length { get; }
        bool IsValidPosition(Position position);
    }
}
