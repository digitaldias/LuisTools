using Moq;
using StructureMap.AutoMocking.Moq;

namespace LuisTools.CrossCutting.UnitTesting
{
    public abstract class TestsFor<TInstance> where TInstance : class
    {
        protected TInstance Instance { get; set; }

        protected MoqAutoMocker<TInstance> AutoMock { get; set; }

        public TestsFor()
        {
            AutoMock = new MoqAutoMocker<TInstance>();

            BeforeTestRun();

            Instance = AutoMock.ClassUnderTest;
        }

        protected virtual void BeforeTestRun(){
            // No code
        }

        public Mock<TContract> GetMockFor<TContract>() where TContract : class
        {
            return Mock.Get(AutoMock.Get<TContract>());
        }
    }
}
