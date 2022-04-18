using Simulator.Config;
using ToyRobot.Models;
using Xunit;

namespace Simulator.Test
{
    public class TestTableTop
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(5, 5)]
        [InlineData(2, 3)]
        public void TestIsValidPosition_Valid(int x, int y)
        {
            var tableTop = new Tabletop(6, 6);
            var position = new Position(x, y);

            Assert.True(tableTop.IsValidPosition(position));
        }
        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -10)]
        [InlineData(6, 5)]
        [InlineData(3, 6)]
        [InlineData(6, 6)]
        public void TestIsValidPosition_Invalid(int x, int y)
        {
            var tableTop = new Tabletop(6, 6);
            var position = new Position(x, y);

            Assert.False(tableTop.IsValidPosition(position));
        }
    }
}