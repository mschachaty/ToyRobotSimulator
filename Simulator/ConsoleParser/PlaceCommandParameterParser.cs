using Simulator.Common;
using ToyRobot.Models;

namespace Simulator.ConsoleParser
{
    public static class PlaceCommandParameterParser
    {
        private const int InputParamaterLength = 2;
        private const int MaxCommandInputCount = 3;

        public static PlaceCommandParameter ParseParameters(this string[] commands)
        {
            if (commands.Length != InputParamaterLength)
                throw new ArgumentException(Errors.InvalidPlaceCommand);

            var commandParams = commands[1].SplitStringRemoveEmptyAndTrim(',');
            var totalParams = commandParams.Length;

            // Checks that Place command is followed by valid command parameters (X,Y and (optional) F toy's face direction).
            if (totalParams < InputParamaterLength || totalParams > MaxCommandInputCount)
                throw new ArgumentException(Errors.InvalidPlaceCommand);

            if (!commandParams[0].GetNumberFromString(out int x) || !commandParams[1].GetNumberFromString(out int y))
                throw new ArgumentException(Errors.InvalidPosition);

            var position = new Position(x, y);

            if (totalParams == MaxCommandInputCount)
            {
                if (!commandParams[totalParams - 1].ParseDirection(out Direction direction))
                    throw new ArgumentException(Errors.InvalidDirection);

                return new PlaceCommandParameter(position, direction);
            }

            return new PlaceCommandParameter(position);
        }
    }
}