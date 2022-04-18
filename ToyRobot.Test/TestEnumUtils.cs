using ToyRobot.Models;
using ToyRobot.Utils;
using Xunit;

namespace ToyRobot.Test
{
    public class TestEnumUtils
    {
        [Fact]
        public void TestEnumExtension_Next()
        {
            var direction = Direction.East;
            direction = direction.Next();
            Assert.Equal(Direction.South, direction);
        }

        [Fact]
        public void TestEnumExtension_DoubleNext()
        {
            var direction = Direction.South;
            direction = direction.Next();
            direction = direction.Next();
            Assert.Equal(Direction.North, direction);
        }

        [Fact]
        public void TestEnumExtension_Previous()
        {
            var direction = Direction.North;
            direction = direction.Previous();
            Assert.Equal(Direction.West, direction);
        }

        [Fact]
        public void TestEnumExtension_DoublePrevious()
        {
            var direction = Direction.East;
            direction = direction.Previous();
            direction = direction.Previous();
            Assert.Equal(Direction.West, direction);
        }

        [Fact]
        public void TestEnumExtension_FullRotationNext()
        {
            var direction = Direction.North;
            direction = direction.Next();
            direction = direction.Next();
            direction = direction.Next();
            direction = direction.Next();
            Assert.Equal(Direction.North, direction);
        }

        [Fact]
        public void TestEnumExtension_FullRotationPrevious()
        {
            var direction = Direction.East;
            direction = direction.Previous();
            direction = direction.Previous();
            direction = direction.Previous();
            direction = direction.Previous();
            Assert.Equal(Direction.East, direction);
        }
    }
}