using ProbabilityCalculatorApi.Calculators;
using ProbabilityCalculatorApi.Model;
using ProbabilityCalculatorApi.Repository;
using ProbabilityCalculatorApi.Validation;

namespace ProbabilityCalculatorApi.Service
{
    public class ProbabilityCalculatorService : IProbabilityCalculatorService
    {
        private readonly IProbabilityCalculatorFactory _probabilityCalculatorFactory;
        private readonly IProbabilityCalculatorRepository _probabilityCalculatorRepository;
        private readonly IProbabilityCalculationValidator _probabilityCalculationValidator;

        public ProbabilityCalculatorService(
            IProbabilityCalculatorFactory probabilityCalculatorFactory,
            IProbabilityCalculatorRepository probabilityCalculatorRepository,
            IProbabilityCalculationValidator probabilityCalculationValidator)
        {
            _probabilityCalculatorFactory = probabilityCalculatorFactory ?? throw new ArgumentNullException(nameof(probabilityCalculatorFactory));
            _probabilityCalculatorRepository = probabilityCalculatorRepository ?? throw new ArgumentNullException(nameof(probabilityCalculatorRepository));
            _probabilityCalculationValidator = probabilityCalculationValidator ?? throw new ArgumentNullException(nameof(probabilityCalculationValidator));
        }
        public ProbabilityCalculationResult CalculateProbability(ProbabilityCalculationModel probablityCalculationModel)
        {
            if (!_probabilityCalculationValidator.IsValid(probablityCalculationModel))
            {
                throw new Exception("Probability calculation model is not valid for this operation");
            }

            ProbabilityCalculator probabilityCalculator = _probabilityCalculatorFactory.GetProbabilityCalculator(probablityCalculationModel);
            if(probabilityCalculator == null)
            {
                throw new Exception($"Cannot resolve probability calculator for probability calculation type {probablityCalculationModel.ProbabilityCalculationType}");
            }

            var probability = probabilityCalculator.RunCalculation(probablityCalculationModel);

            var result = new ProbabilityCalculationResult
            {
                Probability = probability,
                CalculationDate = DateTime.Now,
                ProbabilityCalculationModel = probablityCalculationModel
            };

            _probabilityCalculatorRepository.AddCalculationResult(result);

            return result;
        }

        public IEnumerable<ProbabilityCalculationResult> ListCalculations()
        {
            return _probabilityCalculatorRepository.ListProbabilityCalculations();
        }
    }
}
