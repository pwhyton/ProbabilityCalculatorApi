using ProbabilityCalculatorApi.Model;

namespace ProbabilityCalculatorApi.Validation
{
    public class ProbabilityCalculationValidator : IProbabilityCalculationValidator
    {
        public bool IsValid(ProbabilityCalculationModel probabilityCalculationModel)
        {
            return probabilityCalculationModel.EventA >= 0
                    && probabilityCalculationModel.EventA <= 1
                    && probabilityCalculationModel.EventB >= 0
                    && probabilityCalculationModel.EventB <= 1;
        }
    }
}
