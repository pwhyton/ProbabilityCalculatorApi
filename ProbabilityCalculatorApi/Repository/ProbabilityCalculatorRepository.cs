using ProbabilityCalculatorApi.Data;
using ProbabilityCalculatorApi.Model;

namespace ProbabilityCalculatorApi.Repository
{
    public class ProbabilityCalculatorRepository : IProbabilityCalculatorRepository
    {
        private readonly IProbabilityCalculationDataManager _probabilityCalculationDataManager;

        public ProbabilityCalculatorRepository(IProbabilityCalculationDataManager probabilityCalculationDataManager)
        {
            _probabilityCalculationDataManager = probabilityCalculationDataManager ?? throw new ArgumentNullException(nameof(probabilityCalculationDataManager));
        }
        public IEnumerable<ProbabilityCalculationResult> ListProbabilityCalculations()
        {
            return _probabilityCalculationDataManager.List();
        }

        public void AddCalculationResult(ProbabilityCalculationResult result)
        {
            _probabilityCalculationDataManager.RecordResult(result);
        }
    }
}
