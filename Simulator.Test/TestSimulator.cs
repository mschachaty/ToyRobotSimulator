using Simulator.Common;
using Simulator.Config;
using System;
using ToyRobot.Models;
using Xunit;

namespace Simulator.Test
{
    public class TestSimulator
    {
        [Theory]
        [InlineData("PLACE 6,6,WEST")]
        [InlineData("PLACE 5,7,east")]
        [InlineData("PLACE 7,4,south")]
        [InlineData("PLACE -1,-1,North")]
        [InlineData("PLACE 5,,North", Errors.InvalidPosition)]
        [InlineData("PLACE ,1,North", Errors.InvalidPosition)]
        [InlineData("PLACE North", Errors.InvalidPlaceCommand)]
        [InlineData("PLACE 5,5", Errors.MissingDirection)]
        [InlineData("PLACE 0,0,blah", Errors.InvalidDirection)]
        public void TestProcessCommand_InvalidPlacement(string input, string? expectedError = null)
        {
            ITabletop tableTop = new Tabletop(6, 6);

            try
            {
                var simulator = new Simulator(tableTop);
                simulator.ProcessCommand(input.Split(' '));

                Assert.Null(simulator.ToyRobot);
            }
            catch (ArgumentException exception)
            {
                Assert.Equal(expectedError, exception.Message);
            }
        }
        [Fact]
        public void TestProcessCommand_Avoid_InvalidCommand()
        {
            ITabletop tableTop = new Tabletop(8,8);

            var simulator = new Simulator(tableTop);
            simulator.ProcessCommand("PLACE 2,2,West".Split(' '));
            simulator.ProcessCommand("AVOID 2,2".Split(' '));

            Assert.Equal(0, simulator.TableTop.AvoidPositions.Count);
        }
        [Fact]
        public void TestProcessCommand_Avoid_InvalidCommand_NoPlace()
        {
            ITabletop tableTop = new Tabletop(8, 8);

            var simulator = new Simulator(tableTop);
            simulator.ProcessCommand("AVOID 2,2".Split(' '));

            Assert.Equal(0, simulator.TableTop?.AvoidPositions.Count);
        }
        [Fact]
        public void TestProcessCommand_Avoid_Valid()
        {
            ITabletop tableTop = new Tabletop(8, 8);

            var simulator = new Simulator(tableTop);
            simulator.ProcessCommand("PLACE 2,2,West".Split(' '));
            simulator.ProcessCommand("AVOID 3,3".Split(' '));

            Assert.Equal(1, simulator.TableTop?.AvoidPositions.Count);

        }
        [Fact]
        public void TestProcessCommand_Avoid_MultipleValid()
        {
            ITabletop tableTop = new Tabletop(8, 8);

            var simulator = new Simulator(tableTop);
            simulator.ProcessCommand("PLACE 2,2,West".Split(' '));
            simulator.ProcessCommand("AVOID 3,3".Split(' '));
            simulator.ProcessCommand("AVOID 4,3".Split(' '));

            Assert.Equal(2, simulator.TableTop?.AvoidPositions.Count);

        }
        [Theory]
        [InlineData(6,6)]
        [InlineData(8,8)]
        public void TestProcessCommand_MoveToyRobot(int width, int length)
        {
            ITabletop tableTop = new Tabletop(width, length);

            var simulator = new Simulator(tableTop);
            simulator.ProcessCommand("PLACE 2,2,West".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));

            Assert.Equal(2, simulator.ToyRobot?.Position.X);
            Assert.Equal(2, simulator.ToyRobot?.Position.Y);
            Assert.Equal("Output: 1,2,WEST", simulator.GetReport());
        }
        [Theory]
        [InlineData("MOVE")]
        [InlineData("RIGHT")]
        [InlineData("LEFT")]
        [InlineData("REPORT")]
        public void TestProcessCommand_Command_WithoutPlace(string command)
        {
            ITabletop tableTop = new Tabletop(6, 6);

            var simulator = new Simulator(tableTop);
            simulator.ProcessCommand(command.Split(' '));

            Assert.Null(simulator.ToyRobot);
        }
        [Fact]
        public void TestProcessCommand_ReportCommand()
        {
            ITabletop tableTop = new Tabletop(6, 6);

            var simulator = new Simulator(tableTop);
            simulator.ProcessCommand("PLACE 1,2,EAST".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));
            simulator.ProcessCommand("LEFT".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));
            var output = simulator.ProcessCommand("REPORT".Split(' '));

            Assert.Equal("Output: 3,3,NORTH", output);
        }
        [Theory]
        [InlineData("MOVES")]
        [InlineData("UNKNOWN")]
        [InlineData("REPORTS")]
        public void TestProcessCommand_InvalidCommand(string command)
        {
            ITabletop tableTop = new Tabletop(6, 6);

            try
            {
                var simulator = new Simulator(tableTop);
                simulator.ProcessCommand("PLACE 4,4,East".Split(' '));
                simulator.ProcessCommand(command.Split(' '));

            }
            catch (ArgumentException exception)
            {
                Assert.Equal(Errors.InvalidCommand, exception.Message);
            }
        }
        [Theory]
        [InlineData("place 1,1,West", 1, 1, Direction.West)]
        [InlineData("Place 0,0,south", 0, 0, Direction.South)]
        [InlineData("PLACE 5,5,EAST", 5, 5, Direction.East)]
        public void TestProcessCommand_PlaceToyRobot_ValidDirectionAndPosition(string input, int x, int y, Direction direction)
        {
            ITabletop tableTop = new Tabletop(6, 6);

            var simulator = new Simulator(tableTop);
            simulator.ProcessCommand(input.Split(' '));

            Assert.Equal(x, simulator.ToyRobot?.Position.X);
            Assert.Equal(y, simulator.ToyRobot?.Position.Y);
            Assert.Equal(direction, simulator.ToyRobot?.Direction);
        }
        [Fact]
        public void TestProcessCommand_RightAndMove()
        {
            ITabletop tableTop = new Tabletop(6, 6);

            var simulator = new Simulator(tableTop);
            simulator.ProcessCommand("Place 1,4,east".Split(' '));
            simulator.ProcessCommand("Right".Split(' '));
            simulator.ProcessCommand("Move".Split(' '));
            Assert.Equal(1, simulator.ToyRobot?.Position.X);
            Assert.Equal(3, simulator.ToyRobot?.Position.Y);
            Assert.Equal(Direction.South, simulator.ToyRobot?.Direction);
        }
        [Fact]
        public void TestProcessCommand_MoveAndLeft()
        {
            ITabletop tableTop = new Tabletop(6, 6);

            var simulator = new Simulator(tableTop);
            simulator.ProcessCommand("place 1,5,south".Split(' '));
            simulator.ProcessCommand("move".Split(' '));
            simulator.ProcessCommand("left".Split(' '));

           Assert.Equal(Direction.East, simulator.ToyRobot?.Direction);
        }
        [Fact]
        public void TestProcessCommand_PlaceToyRobot_WithoutDirection()
        {
            ITabletop tableTop = new Tabletop(6, 6);

            var simulator = new Simulator(tableTop);
            simulator.ProcessCommand("PLACE 4,4,East".Split(' '));
            simulator.ProcessCommand("PLACE 0,0".Split(' '));
            Assert.Equal(0, simulator.ToyRobot?.Position.X);
            Assert.Equal(0, simulator.ToyRobot?.Position.Y);
            Assert.Equal(Direction.East, simulator.ToyRobot?.Direction);
        }
        [Fact]
        public void TestProcessCommand_TryDestroyToyRobot()
        {
            ITabletop tableTop = new Tabletop(6, 6);

            var simulator = new Simulator(tableTop);

            simulator.ProcessCommand("PLACE 0,0,north".Split(' '));
            simulator.ProcessCommand("move".Split(' '));
            simulator.ProcessCommand("left".Split(' '));

            // if the robot goes out of the board it ignores the command
            simulator.ProcessCommand("move".Split(' '));

            Assert.Equal("Output: 0,1,WEST", simulator.ProcessCommand("Report".Split(' ')));

        }
        [Theory]
        [InlineData("MOVE")]
        [InlineData("RIGHT")]
        [InlineData("LEFT")]
        [InlineData("REPORT")]
        public void TestHandleCommand_WithoutPlace(string command)
        {
            ITabletop tableTop = new Tabletop(6, 6);

            var simulator = new Simulator(tableTop);
            simulator.ProcessCommand(command.Split(' '));

            Assert.Null(simulator.ToyRobot);
        }
        [Theory]
        [InlineData("MOVE")]
        [InlineData("RIGHT")]
        [InlineData("LEFT")]
        [InlineData("REPORT")]
        public void TestHandleCommand_WithPlace(string command)
        {
            ITabletop tableTop = new Tabletop(6, 6);

            var simulator = new Simulator(tableTop);
            simulator.ProcessCommand("PLACE 0,0,North".Split(' '));
            simulator.ProcessCommand(command.Split(' '));

            Assert.NotNull(simulator.ToyRobot);
        }
        [Fact]
        public void Test_GetReport_WithPlace()
        {
            ITabletop tableTop = new Tabletop(6, 6);

            var simulator = new Simulator(tableTop);
            simulator.ProcessCommand("PLACE 0,0,South".Split(' '));
            simulator.ProcessCommand("Report".Split(' '));

            Assert.Equal("Output: 0,0,SOUTH", simulator.GetReport());
        }
        [Fact]
        public void Test_GetReport_WithoutPlace()
        {
            ITabletop tableTop = new Tabletop(6, 6);

            var simulator = new Simulator(tableTop);
            simulator.ProcessCommand("Report".Split(' '));

            Assert.Equal(String.Empty, simulator.GetReport());
        }
    }
}