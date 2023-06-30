using ProbabilityCalculatorApi.Model;

namespace ProbabilityCalculatorApi.Repository
{
    public interface IProbabilityCalculatorRepository
    {
        IEnumerable<ProbabilityCalculationResult> ListProbabilityCalculations();
        void AddCalculationResult(ProbabilityCalculationResult result);
    }
}
