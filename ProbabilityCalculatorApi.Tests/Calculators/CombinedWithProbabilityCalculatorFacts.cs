namespace ProbabilityCalculatorApi.Tests.Calculators
{
    public class CombinedWithProbabilityCalculatorFacts
    {
        private CombinedWithProbabilityCalculator _sut;

        public CombinedWithProbabilityCalculatorFacts()
        {
            _sut = new CombinedWithProbabilityCalculator();
        }

        [Theory]
        [InlineData(0.5,0.5,0.25)]
        [InlineData(0, 1, 0)]
        [InlineData(1, 0, 0)]
        [InlineData(1, 1, 1)]
        [InlineData(0.1, 0.1, 0.01)]
        [InlineData(0.75, 0.25, 0.1875)]        
        public void CalculatorShouldReturnCorrectResults(decimal eventA, decimal eventB, decimal expectedResult)
        {
            Assert.Equal(expectedResult,_sut.RunCalculation(new ProbabilityCalculationModel { EventA = eventA, EventB = eventB, ProbabilityCalculationType = ProbabilityCalculationType.CombinedWith }));
        }

        [Fact]
        public void GivenACalculationModelForCalculatingProbability_WhenTheProbabiltyTypeIsNotCombinedWith_ThenIExpectAnException()
        {
            Assert.Throws<InvalidOperationException>(()=> _sut.RunCalculation(new ProbabilityCalculationModel { EventA = 1, EventB= 1, ProbabilityCalculationType= ProbabilityCalculationType.Either }));
        }
    }
}
