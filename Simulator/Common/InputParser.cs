using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyRobot.Models;

namespace Simulator.Common
{
    public static class InputParser
    {
        public static bool GetNumberFromString(this string input, out int result) => int.TryParse(input, out result);
        public static bool ParseCommand(this string input, out Command command) => Enum.TryParse(input, true, out command);
        public static bool ParseDirection(this string input, out Direction direction) => Enum.TryParse(input, true, out direction);
        public static string[] SplitStringRemoveEmptyAndTrim(this string input, char splitChar)
        {
            return input.Split(splitChar, StringSplitOptions.RemoveEmptyEntries & StringSplitOptions.TrimEntries).Where(x => !string.IsNullOrEmpty(x)).ToArray(); 
        }
    }
}
