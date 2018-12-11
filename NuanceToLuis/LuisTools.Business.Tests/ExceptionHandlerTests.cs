using LuisTools.CrossCutting.UnitTesting;
using LuisTools.Domain.Contracts;
using Moq;
using Should;
using System;
using Xunit;

namespace LuisTools.Business.Tests
{
    public class ExceptionHandlerTests : TestsFor<ExceptionHandler>
    {
        [Fact]        
        [Trait("TestCategory", "BusinessLogic")]
        public void Get_UnsafeFunctionIsNull_ReturnsDefault()
        {
            // Arrange
            Func<int> nullFunction = null;

            // Act
            var result = Instance.Get(nullFunction);

            // Assert
            result.ShouldEqual(default(int));
        }

        [Fact]
        public void Get_UnsafeFunctionDelivers_ReturnsValue()
        {
            // Arrange
            var randomString = Random.NextString(10, 20);
            Func<string> workingFunction = () => randomString;

            // Act
            var result = Instance.Get(workingFunction);

            // Assert
            result.ShouldEqual(randomString);
        }

        [Fact]
        public void Get_UnsafeFunctionThrows_ExceptionIsLogged()
        {
            // Arrange
            var exceptionMessage  = "I'm really bad";
            var badException      = new Exception(exceptionMessage);
            Func<int> badFunction = () => throw (badException);

            // Act
            Instance.Get(badFunction);

            // Assert
            GetMockFor<ILogger>().Verify(logger => logger.Logerror(exceptionMessage), Times.Once());
        }

        [Fact]
        public void Run_UnsafeActionIsNull_LogsWarning()
        {
            // Arrange
            Action nullAction = null;

            // Act
            Instance.Run(nullAction);

            // Assert
            GetMockFor<ILogger>().Verify(logger => logger.Warning(Any<string>()), Times.Once());
        }

        [Fact]
        public void Run_UnsafeFunctionIsWorking_PerformsTheFunction()
        {
            // Arrange
            bool flagged = false;
            Action workingFunction = () => flagged = true;

            // Act
            Instance.Run(workingFunction);

            // Assert
            flagged.ShouldBeTrue();
        }


        [Fact]
        public void Run_UnsafeFunctionThrows_LogsError()
        {
            // Arrange
            var exceptionMessage = "I'm so bad";
            var exception = new Exception(exceptionMessage);
            Action throwingAction = () => throw exception;

            // Act
            Instance.Run(throwingAction);

            // Assert
            GetMockFor<ILogger>().Verify(logger => logger.Logerror(exceptionMessage), Times.Once());
        }
    }
}
