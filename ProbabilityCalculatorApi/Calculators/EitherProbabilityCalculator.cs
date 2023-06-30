using ProbabilityCalculatorApi.Model;

namespace ProbabilityCalculatorApi.Calculators
{
    public class EitherProbabilityCalculator : ProbabilityCalculator
    {
        public override ProbabilityCalculationType CalculatesProbabilityType => ProbabilityCalculationType.Either;

        public override decimal RunCalculation(ProbabilityCalculationModel calculationModel)
        {
            if (calculationModel.ProbabilityCalculationType != ProbabilityCalculationType.Either)
            {
                throw new InvalidOperationException($"Calculator {nameof(EitherProbabilityCalculator)} is not valid for {calculationModel.ProbabilityCalculationType} calculations");
            }

            return (calculationModel.EventA + calculationModel.EventB) - (calculationModel.EventA * calculationModel.EventB);
        }
    }
}
