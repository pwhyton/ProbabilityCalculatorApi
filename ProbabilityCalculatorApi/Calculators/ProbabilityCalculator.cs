using ProbabilityCalculatorApi.Model;

namespace ProbabilityCalculatorApi.Calculators
{
    public abstract class ProbabilityCalculator
    {
        public abstract decimal RunCalculation(ProbabilityCalculationModel calculationModel);
        public abstract ProbabilityCalculationType CalculatesProbabilityType { get; }
    }
}
