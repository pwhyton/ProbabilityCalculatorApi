using System.ComponentModel.DataAnnotations;

namespace ProbabilityCalculatorApi.Model
{
    public enum ProbabilityCalculationType
    {
        CombinedWith,
        Either
    }
    public class ProbabilityCalculationModel
    {
        [Range(0,1)]
        public decimal EventA { get; set; }
        [Range(0, 1)]
        public decimal EventB { get; set; }
        public ProbabilityCalculationType ProbabilityCalculationType { get; set; }
    }
}
