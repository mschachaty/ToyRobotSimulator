
namespace Simulator.Common
{
    public static class Errors
    {
        public const string InvalidCommand = "Invalid Command. Valid Commands: PLACE|MOVE|LEFT|RIGHT|REPORT|EXIT";
        public const string InvalidPosition = "Invalid X &/or Y coordinates supplied.";
        public const string MissingDirection = "Direction not supplied. Direction must be supplied on first Place command.";
        public const string InvalidPlaceCommand = "Invalid Command. Place must use format: PLACE X,Y[,F]";
        public const string InvalidMoveCommand = "Invalid Command. Place must use format: PLACE X,Y[,F]";
        public const string InvalidDirection = "Invalid direction. Direction must be: NORTH|EAST|SOUTH|WEST";
    }
} 
