
namespace ProbabilityCalculatorApi.Tests.Calculators.Factory
{
    public class ProbabilityCalculatorFactoryFacts
    {
        private ProbabilityCalculatorFactory _sut;

        public ProbabilityCalculatorFactoryFacts()
        {
            _sut= new ProbabilityCalculatorFactory();
        }

        public class GivenARequestToCreateAProbabilityCalculator : ProbabilityCalculatorFactoryFacts
        {
            [Fact]
            public void WhenTheCalculationIsOfTypeCombinedWith_ThenCombinedWithCalculatorIsReturned()
            {
                //arrange
                var calculationModel = new ProbabilityCalculationModel
                {
                    EventA = 1M,
                    EventB = 0.5M,
                    ProbabilityCalculationType = ProbabilityCalculationType.CombinedWith
                };

                //act
                var calculator = _sut.GetProbabilityCalculator(calculationModel);

                //Assert
                Assert.NotNull(calculator);
                Assert.IsAssignableFrom<CombinedWithProbabilityCalculator>(calculator);
            }

            [Fact]
            public void WhenTheCalculationIsOfTypeEither_ThenEitherCalculatorIsReturned()
            {
                //arrange
                var calculationModel = new ProbabilityCalculationModel
                {
                    EventA = 0.5M,
                    EventB = 1,
                    ProbabilityCalculationType = ProbabilityCalculationType.Either
                };

                //act
                var calculator = _sut.GetProbabilityCalculator(calculationModel);

                //Assert
                Assert.NotNull(calculator);
                Assert.IsAssignableFrom<EitherProbabilityCalculator>(calculator);
            }
        }

       
    }
}
