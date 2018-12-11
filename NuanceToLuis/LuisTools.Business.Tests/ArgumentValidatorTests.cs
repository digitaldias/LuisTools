using LuisTools.CrossCutting.UnitTesting;
using LuisTools.Domain.Contracts;
using Moq;
using Should;
using Xunit;

namespace LuisTools.Business.Tests
{
    public class ArgumentValidatorTests : TestsFor<ArgumentValidator>
    {
        [Fact]        
        public void IsValid_WhenCalled_LogsUsage()
        {
            // Arrange
            string[] args = new string[0];

            // Act
            var result = Instance.IsValid(args);

            // Assert
            GetMockFor<ILogger>().Verify(logger => logger.Enter(Any<object>(), Any<string>(), Any<string[]>()), Times.Once());
        }

        [Fact]
        public void IsValid_ArgsIsEmpty_ReturnsFalse()
        {
            // Arrange
            var args = new string[0];

            // Act
            var result = Instance.IsValid(args);

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void IsValid_ArgsIsEmpty_LogsWarning()
        {
            // Arrange
            var args = new string[0];

            // Act
            var result = Instance.IsValid(args);

            // Assert
            GetMockFor<ILogger>().Verify(logger => logger.Warning(Any<string>()));
        }

        [Fact]
        public void IsValid_InputPatternMissing_ReturnsFalseAndLogsWarning()
        {
            // Arrange
            var args = new string[] { "-o outputFolder" };

            // Act
            var result = Instance.IsValid(args);

            // Assert
            result.ShouldBeFalse();
            GetMockFor<ILogger>().Verify(logger => logger.Warning(Any<string>()));
        }

        [Fact]
        public void IsValid_OutputFolderMissing_ReturnsFalseAndLogsWarning()
        {
            // Arrange
            var args = new string[] { "-i", "inputFolder" };

            // Act
            var result = Instance.IsValid(args);

            // Assert
            result.ShouldBeFalse();
            GetMockFor<ILogger>().Verify(logger => logger.Warning(Any<string>()));
        }

        [Fact]
        public void IsValid_RequiredArgumentsArePresent_ReturnsTrue()
        {
            // Arrange
            var args = new string[] { "-i", "inputFolder", "-o", "outputFolder" };

            // Act
            var result = Instance.IsValid(args);

            // Assert
            result.ShouldBeTrue();
        }
    }
}
