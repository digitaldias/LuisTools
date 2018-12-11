using FizzWare.NBuilder;
using Moq;
using StructureMap.AutoMocking.Moq;
using System;

namespace LuisTools.CrossCutting.UnitTesting
{
    public abstract class TestsFor<TInstance> where TInstance : class
    {
        protected TInstance Instance { get; set; }

        protected MoqAutoMocker<TInstance> AutoMock { get; set; }

        protected RandomGenerator _randomGenerator;

        public TestsFor()
        {
            _randomGenerator = new RandomGenerator((int)DateTime.Now.Ticks);
            AutoMock = new MoqAutoMocker<TInstance>();

            BeforeTestRun();

            Instance = AutoMock.ClassUnderTest;


        }

        public RandomGenerator Random => _randomGenerator;

        protected virtual void BeforeTestRun(){
            // No code
        }

        public Mock<TContract> GetMockFor<TContract>() where TContract : class
        {
            return Mock.Get(AutoMock.Get<TContract>());
        }
    }
}
