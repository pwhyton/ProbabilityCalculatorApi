using ProbabilityCalculatorApi.Model;

namespace ProbabilityCalculatorApi.Calculators
{
    public class CombinedWithProbabilityCalculator : ProbabilityCalculator
    {
        public override ProbabilityCalculationType CalculatesProbabilityType => ProbabilityCalculationType.CombinedWith;

        public override decimal RunCalculation(ProbabilityCalculationModel calculationModel)
        {
            if (calculationModel.ProbabilityCalculationType != ProbabilityCalculationType.CombinedWith)
            {
                throw new InvalidOperationException($"Calculator {nameof(CombinedWithProbabilityCalculator)} is not valid for {calculationModel.ProbabilityCalculationType} calculations");
            }

            return calculationModel.EventA * calculationModel.EventB;
        }
    }
}
