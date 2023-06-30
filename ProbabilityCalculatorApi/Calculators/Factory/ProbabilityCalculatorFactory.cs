using ProbabilityCalculatorApi.Model;
using System.Reflection;

namespace ProbabilityCalculatorApi.Calculators
{
    public class ProbabilityCalculatorFactory : IProbabilityCalculatorFactory
    {
        public ProbabilityCalculator GetProbabilityCalculator(ProbabilityCalculationModel probabilityCalculationModel)
        {
            var calculatorTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(ProbabilityCalculator)));
            if (!calculatorTypes.Any())
            {
                throw new Exception("There are no instances of the ProbabilityCalculator base class");
            }

            foreach (var ct in calculatorTypes)
            {
                var calculator = Activator.CreateInstance(ct) as ProbabilityCalculator;
                if (calculator != null && calculator.CalculatesProbabilityType == probabilityCalculationModel.ProbabilityCalculationType)
                {
                    return calculator;
                }
            }

            throw new Exception($"There is no registered probability calculator that calculates probability types {probabilityCalculationModel.ProbabilityCalculationType}");

        }
    }
}
