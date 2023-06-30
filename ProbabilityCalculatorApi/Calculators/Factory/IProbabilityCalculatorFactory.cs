using ProbabilityCalculatorApi.Model;

namespace ProbabilityCalculatorApi.Calculators
{
    public interface IProbabilityCalculatorFactory
    {
        ProbabilityCalculator GetProbabilityCalculator(ProbabilityCalculationModel probabilityCalculationModel);
    }
}
