using LuisTools.CrossCutting.UnitTesting;
using LuisTools.Domain.Contracts;
using Should;
using Xunit;

namespace LuisTools.Business.Tests
{
    public class ParametersExtractorTests : TestsFor<ParametersExtractor>
    {

        [Fact]
        public void Extract_argsAreInvalid_ReturnsNull()
        {
            // Arrange
            var emptyArgs = new string[0];

            // Act
            var result = Instance.Extract(emptyArgs);

            // Assert
            result.ShouldBeNull();
        }


        [Fact]
        public void Extract_AllArgumentsGiven_ParametersFilledCorrectly()
        {
            // Arrange
            PretendValidArguments();
            var arguments = new[] {
                "-i", @"C:\temp\inputs\*.trsx",
                "-o", @"C:\temp\outputs",
                "-f", "NO_INTENT"
            };

            // Act
            var result = Instance.Extract(arguments);

            // Assert
            result.FileType.ShouldEqual("trsx");
            result.InputFilePattern.ShouldEqual(@"C:\temp\inputs\*.trsx");
            result.DestinationFolderName.ShouldEqual(@"C:\temp\outputs");
            result.IgnoredIntents[0].ShouldEqual("NO_INTENT");
            result.DisplayHelp.ShouldBeFalse();
        }


        [Fact]
        public void Extract_HelpAskedFor_SetsDisplayHelpToTrue()
        {
            // Arrange
            PretendValidArguments();
            var arguments = new[] { "-h" };

            // Act
            var result = Instance.Extract(arguments);

            // Assert
            result.DisplayHelp.ShouldBeTrue();
        }


        private void PretendValidArguments()
        {
            GetMockFor<IArgumentValidator>().Setup(v => v.IsValid(Any<string[]>())).Returns(true);
        }
    }
}
