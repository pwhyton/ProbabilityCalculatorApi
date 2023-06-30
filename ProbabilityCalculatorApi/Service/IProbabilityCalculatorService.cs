using ProbabilityCalculatorApi.Model;

namespace ProbabilityCalculatorApi.Service
{
    public interface IProbabilityCalculatorService
    {
        ProbabilityCalculationResult CalculateProbability(ProbabilityCalculationModel probablityCalculationModel);
        IEnumerable<ProbabilityCalculationResult> ListCalculations();
    }
}
