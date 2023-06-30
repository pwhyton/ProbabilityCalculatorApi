namespace ProbabilityCalculatorApi.Model
{
    public class ProbabilityCalculationResult
    {
        public decimal? Probability { get; set; }
        public DateTime? CalculationDate { get; set; }
        public ProbabilityCalculationModel? ProbabilityCalculationModel { get; set; }
    }
}
