using Simulator.Common;
using Simulator.ConsoleParser;
using System;
using Xunit;

namespace Simulator.Test
{
    public class TestPlaceCommandParamaterParser
    {
        [Theory]
        [InlineData("PLACE 0,0,NORTH")]
        [InlineData("PLACE 2,2,West ")]
        [InlineData("PLACE 0,0")]
        [InlineData("PLACE 5,5")]
        [InlineData("PLACE    3,5   ")]
        public void ParseParameters_Success(string input)
        {
            var placeCommandParameter = input.SplitStringRemoveEmptyAndTrim(' ').ParseParameters();
            Assert.NotNull(placeCommandParameter);
        }
        [Theory]
        [InlineData("PLACE 0,,NORTH", Errors.InvalidPosition)]
        [InlineData("PLACE ,5,NORTH", Errors.InvalidPosition)]
        [InlineData("PLACE", Errors.InvalidPlaceCommand)]
        [InlineData("PLACE 1", Errors.InvalidPlaceCommand)]
        [InlineData("PLACE 4,0,", Errors.InvalidDirection)]
        [InlineData("PLACE a,2,SOUTH", Errors.InvalidPosition)]
        [InlineData("PLACE 0,2,BLAH", Errors.InvalidDirection)]
        public void ParseParameters_Fail(string input, string expectedError)
        {
            try
            {
                var placeCommandParameter = input.Split(' ', StringSplitOptions.RemoveEmptyEntries & StringSplitOptions.TrimEntries).ParseParameters();
            }
            catch (ArgumentException ex)
            {
                Assert.Equal(expectedError, ex.Message);
            }

        }
    }
}