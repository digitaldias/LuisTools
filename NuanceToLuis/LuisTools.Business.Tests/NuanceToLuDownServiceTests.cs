using LuisTools.CrossCutting.UnitTesting;
using LuisTools.Domain.Contracts;
using Moq;
using Xunit;

namespace LuisTools.Business.Tests
{
    public class NuanceToLuDownServiceTests : TestsFor<NuanceToLuDownService>
    {

        [Fact]
        public void Process_ParametersFailToValidate_DoesNotAttemptToFindAnyFiles()
        {
            // Arrange
            var args = new string[0];

            // Act
            Instance.Process(args);

            // Assert
            GetMockFor<IFileFinder>().Verify(f => f.Find(Any<string>()), Times.Never());
        }
    }
}
