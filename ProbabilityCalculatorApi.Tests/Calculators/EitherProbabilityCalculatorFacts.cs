namespace ProbabilityCalculatorApi.Tests.Calculators
{
    public class EitherProbabilityCalculatorFacts
    {
        private EitherProbabilityCalculator _sut;

        public EitherProbabilityCalculatorFacts()
        {
            _sut = new EitherProbabilityCalculator();
        }

        [Theory]
        [InlineData(0.5, 0.5, 0.75)]
        [InlineData(0, 1, 1)]
        [InlineData(1, 0, 1)]
        [InlineData(1, 1, 1)]
        [InlineData(0.1, 0.1, 0.19)]
        [InlineData(0.75, 0.25, 0.8125)]
        public void CalculatorShouldReturnCorrectResults(decimal eventA, decimal eventB, decimal expectedResult)
        {
            Assert.Equal(expectedResult, _sut.RunCalculation(new ProbabilityCalculationModel { EventA = eventA, EventB = eventB, ProbabilityCalculationType = ProbabilityCalculationType.Either }));
        }

        [Fact]
        public void GivenACalculationModelForCalculatingProbability_WhenTheProbabiltyTypeIsNotEither_ThenIExpectAnException()
        {
            Assert.Throws<InvalidOperationException>(() => _sut.RunCalculation(new ProbabilityCalculationModel { EventA = 1, EventB = 1, ProbabilityCalculationType = ProbabilityCalculationType.CombinedWith }));
        }
    }
}
