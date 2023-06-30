namespace ProbabilityCalculatorApi.Tests.Service
{
    public class ProbabilityCalculatorServiceFacts
    {
        private ProbabilityCalculatorService _sut;
        private readonly Mock<IProbabilityCalculatorFactory> _probabilityCalculatorFactory;
        private readonly Mock<IProbabilityCalculatorRepository> _probabilityCalculatorRepository;
        private readonly Mock<IProbabilityCalculationValidator> _probabilityCalculationValidator;
        private readonly Mock<ProbabilityCalculator> _probabilityCalculator;
        private readonly ProbabilityCalculationModel _defaultModel;

        public ProbabilityCalculatorServiceFacts()
        {
            _probabilityCalculatorFactory = new Mock<IProbabilityCalculatorFactory>();
            _probabilityCalculatorRepository = new Mock<IProbabilityCalculatorRepository>();
            _probabilityCalculationValidator = new Mock<IProbabilityCalculationValidator>();
            _probabilityCalculator = new Mock<ProbabilityCalculator>();

            _probabilityCalculatorFactory.Setup(f => f.GetProbabilityCalculator(It.IsAny<ProbabilityCalculationModel>()))
                .Returns(_probabilityCalculator.Object);

            _probabilityCalculationValidator.Setup(v => v.IsValid(It.IsAny<ProbabilityCalculationModel>()))
                .Returns(true);

            _defaultModel = new ProbabilityCalculationModel
            {
                EventA = 0.25M,
                EventB = 0.75M,
                ProbabilityCalculationType = ProbabilityCalculationType.CombinedWith
            };

            _sut = new ProbabilityCalculatorService(
                _probabilityCalculatorFactory.Object,
                _probabilityCalculatorRepository.Object,
                _probabilityCalculationValidator.Object);
        }

        public class GivenARequestToCalculateAProbability : ProbabilityCalculatorServiceFacts
        {
            [Fact]
            public void WhenTheRequestModelIsNotValid_ThenIExpectAnException()
            {
                //arrange
                var model = new ProbabilityCalculationModel
                {
                    EventA = 1.1M,
                    EventB = 0,
                    ProbabilityCalculationType = ProbabilityCalculationType.CombinedWith
                };

                _probabilityCalculationValidator
                    .Setup(v => v.IsValid(It.IsAny<ProbabilityCalculationModel>()))
                    .Returns(false);

                //act
                Assert.Throws<Exception>(() => _sut.CalculateProbability(model));
            }

            [Fact]
            public void WhenACalculatorCannotBeFound_ThenIExpectAnException()
            {
                //arrange
                _probabilityCalculatorFactory
                    .Setup(f => f.GetProbabilityCalculator(It.IsAny<ProbabilityCalculationModel>()))
                    .Returns((ProbabilityCalculator)null);

                //act
                Assert.Throws<Exception>(() => _sut.CalculateProbability(_defaultModel));
            }

            [Fact]
            public void WhenTheCalculationModelIsNotValid_ThenIExpectAnException()
            {
                //arrange
                _probabilityCalculationValidator.Setup(v => v.IsValid(It.IsAny<ProbabilityCalculationModel>()))
                .Returns(false);

                //act
                Assert.Throws<Exception>(() => _sut.CalculateProbability(_defaultModel));
            }

            [Fact]
            public void WhenThereIsAValidCalculator_ThenTheCalculationShouldBeRun()
            {
                //act
                _sut.CalculateProbability(_defaultModel);

                //assert
                _probabilityCalculator
                    .Verify(pc=>pc.RunCalculation(It.IsAny<ProbabilityCalculationModel>()), Times.Once);
            }

            [Fact]
            public void WhenTheCalculationIsRun_ThenItShouldBePersistedToTheDataStore()
            {
                //arrange
                _probabilityCalculator
                    .Setup(pc => pc.RunCalculation(It.IsAny<ProbabilityCalculationModel>()))
                    .Returns(1);

                //act
                _sut.CalculateProbability(_defaultModel);

                //assert
                _probabilityCalculatorRepository
                    .Verify(r => r.AddCalculationResult(It.Is<ProbabilityCalculationResult>(rs => rs.Probability == 1)), Times.Once);


            }
        }

        public class GivenARequestToListAllCalculations : ProbabilityCalculatorServiceFacts
        {
            [Fact]
            public void WhenThereAreStoredCalculations_ThenTheyShouldBeRetrievedFromTheRepo()
            {
                //act
                _sut.ListCalculations();

                //assert
                _probabilityCalculatorRepository
                    .Verify(repo=>repo.ListProbabilityCalculations(), Times.Once);
            }
        }
    }
}
