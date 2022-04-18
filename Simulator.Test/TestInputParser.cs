using Simulator.Common;
using ToyRobot.Models;
using Xunit;

namespace Simulator.Test
{
    public class TestInputParser
    {
        [Theory]
        [InlineData("-5")]
        [InlineData("0")]
        [InlineData("100000000")]
        public void TestGetNumberFromString_Valid(string n)
        {
            Assert.True(n.GetNumberFromString(out int y));
            Assert.Equal(n, y.ToString());
        }

        [Theory]
        [InlineData("-a")]
        [InlineData("$")]
        [InlineData(" ")]
        [InlineData("")]
        public void TestGetNumberFromString_Invalid(string n)
        {
            Assert.False(n.GetNumberFromString(out int y));
            Assert.NotEqual(n, y.ToString());
        }
        [Theory]
        [InlineData("MovE", Command.Move)]
        [InlineData("PLACE", Command.Place)]
        [InlineData("Report", Command.Report)]
        [InlineData("right", Command.Right)]
        [InlineData("LEFT ", Command.Left)]
        public void TestParseCommand_Success(string input, Command expectedCommand)
        {
            Assert.True(input.ParseCommand(out Command command));
            Assert.Equal(expectedCommand, command);
        }
        [Fact]
        public void TestParseCommand_Fail()
        {
            Assert.False("blahblah".ParseCommand(out Command command));
        }
        [Theory]
        [InlineData("North", Direction.North)]
        [InlineData("SOUTH", Direction.South)]
        [InlineData("west", Direction.West)]
        [InlineData("EasT", Direction.East)]
        public void TestParseDirection_Success(string input, Direction expectedDirection)
        {
            Assert.True(input.ParseDirection(out Direction direction));
            Assert.Equal(expectedDirection, direction);
        }
        [Fact]
        public void TestParseDirection_Fail()
        {
            Assert.False("northeast".ParseDirection(out Direction direction));
        }
    }
}