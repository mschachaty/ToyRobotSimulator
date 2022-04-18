using ToyRobot.Models;
using Xunit;

namespace ToyRobot.Test
{
    public class TestToyRobot
    {
        [Theory]
        [InlineData(0, 0, Direction.East, 1, 0)]
        [InlineData(2, 0, Direction.West, 1, 0)]
        [InlineData(3, 3, Direction.North, 3, 4)]
        [InlineData(1, 5, Direction.South, 1, 4)]
        public void TestGetNextPosition(int currentX, int currentY, Direction direction, int expectedX, int expectedY)
        {
            var robot = new ToyRobot(new Position(currentX, currentY), direction);
            var nextPosition = robot.GetNextPosition();

            Assert.Equal(expectedX, nextPosition.X);
            Assert.Equal(expectedY, nextPosition.Y);
        }

        [Fact]
        public void TestRotate_LeftAndRight()
        {
            var robot = new ToyRobot(new Position(0, 0), Direction.North);
            robot.RotateLeft();
            robot.RotateRight();
            Assert.Equal(Direction.North, robot.Direction);
        }
        [Fact]
        public void TestRotateRight_DoubleRightRotation()
        {
            var robot = new ToyRobot(new Position(0, 0), Direction.East);
            robot.RotateRight();
            robot.RotateRight();
            Assert.Equal(Direction.West, robot.Direction);
        }
        [Fact]
        public void TestRotateLeft_DoubleLeftRotation()
        {
            var robot = new ToyRobot(new Position(0, 0), Direction.East);
            robot.RotateLeft();
            robot.RotateLeft();
            Assert.Equal(Direction.West, robot.Direction);
        }
        [Fact]
        public void TestRotateLeft_SingleLeftRotation()
        {
            var robot = new ToyRobot(new Position(0, 0), Direction.East);
            robot.RotateLeft();
            Assert.Equal(Direction.North, robot.Direction);
        }
        [Fact]
        public void TestRotateLeft_SingleRightRotation()
        {
            var robot = new ToyRobot(new Position(0, 0), Direction.East);
            robot.RotateRight();
            Assert.Equal(Direction.South, robot.Direction);
        }
        [Fact]
        public void Test_Report()
        {
            var robot = new ToyRobot(new Position(0, 0), Direction.North);
            Assert.Equal("Output: 0,0,NORTH", robot.Report());

        }
    }
}