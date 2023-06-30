using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProbabilityCalculatorApi.Model;
using ProbabilityCalculatorApi.Options;

namespace ProbabilityCalculatorApi.Data
{
    public class ProbabilityCalculationDataManager : IProbabilityCalculationDataManager, IDisposable
    {
        private List<ProbabilityCalculationResult> _probabilityCalculationResults = new List<ProbabilityCalculationResult>();
        private string _dataFilePath;
        private readonly JsonDataOptions _jsonDataOptions;
        private bool _dataAdded;
        public ProbabilityCalculationDataManager(IOptions<JsonDataOptions> jsonDataOptions)
        {
            _jsonDataOptions = jsonDataOptions.Value ?? throw new ArgumentNullException(nameof(jsonDataOptions));
            _dataFilePath = Path.Combine(_jsonDataOptions.DataFileLocation, _jsonDataOptions.DataFileName);
            LoadData();
        }

        public IEnumerable<ProbabilityCalculationResult> List()
        {
            return _probabilityCalculationResults;
        }

        public void RecordResult(ProbabilityCalculationResult result)
        {
            _probabilityCalculationResults.Add(result);
            _dataAdded = true;
        }

        private void LoadData()
        {
            if (!Directory.Exists(_jsonDataOptions.DataFileLocation))
            {
                Directory.CreateDirectory(_jsonDataOptions.DataFileLocation);
            }

            if (!File.Exists(_dataFilePath))
            {
                File.Create(_dataFilePath).Close();
            }

            var textData = File.ReadAllText(_dataFilePath);
            if (string.IsNullOrEmpty(textData))
            {
                return;
            }

            JArray jsonData = JArray.Parse(textData);
            var data = jsonData.ToObject<IEnumerable<ProbabilityCalculationResult>>();
            if (data != null && data.Any())
            {
                _probabilityCalculationResults = data.ToList();
            }

        }

        public void Dispose()
        {
            if (!_dataAdded || !_probabilityCalculationResults.Any())
            {
                return;
            }

            File.WriteAllText(_dataFilePath, JsonConvert.SerializeObject(_probabilityCalculationResults));
        }
    }
}
