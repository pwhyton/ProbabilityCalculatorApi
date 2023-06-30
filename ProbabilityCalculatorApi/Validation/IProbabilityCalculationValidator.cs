using ProbabilityCalculatorApi.Model;

namespace ProbabilityCalculatorApi.Validation
{
    public interface IProbabilityCalculationValidator
    {
        bool IsValid(ProbabilityCalculationModel probabilityCalculationModel);
    }
}
