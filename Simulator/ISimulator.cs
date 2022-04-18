using ToyRobot;
using ToyRobot.Models;

namespace Simulator
{
    public interface ISimulator
    {
        IToyRobot? ToyRobot { get; }
        string HandleCommand(Command command);
        string GetReport();
        string ProcessCommand(string[] input);

    }
}