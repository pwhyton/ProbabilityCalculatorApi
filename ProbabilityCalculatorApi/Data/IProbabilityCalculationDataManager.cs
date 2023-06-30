using ProbabilityCalculatorApi.Model;

namespace ProbabilityCalculatorApi.Data
{
    public interface IProbabilityCalculationDataManager
    {
        IEnumerable<ProbabilityCalculationResult> List();
        void RecordResult(ProbabilityCalculationResult probabilityCalculationResult);
        
    }
}
