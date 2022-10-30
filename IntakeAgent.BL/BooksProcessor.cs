using IntakeAgent.BL.IntakeSteps;
using IntakeAgent.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IntakeAgent.BL
{
    public class BooksProcessor
    {
        private IEnumerable<IIntakeStep> _intakeSteps;

        #region logging members
        private bool _keepLog;
        private bool _isFullLog;
        private ILogger _logger; 
        #endregion

        public BooksProcessor(IOptions<BooksProcessorConfigs> configs, ILoggerFactory loggerFactory)
        {
            var keepLog = configs.Value.KeepLog;
            var isFullLog = configs.Value.IsFullLog;
            var intakeSteps = StepsInitializer.InitializeSteps(configs.Value.IntakeSteps).ToArray();
            var logger = loggerFactory.CreateLogger(nameof(BooksProcessor));

            _intakeSteps = intakeSteps;
            _logger = logger;
            _keepLog = logger != null && keepLog;
            _isFullLog = keepLog && isFullLog;
        }

        public IEnumerable<Book> Process(IEnumerable<Book> books)
        {
            var logger = _keepLog ? _logger : null;

            foreach (var item in books)
            {
                var isValid = true;
                var book = item;
                foreach (var step in _intakeSteps)
                {
                    (bool isStepValid, book) = step.RunStep(book, logger);

                    if (!isStepValid)
                    {
                        isValid = false;

                        if (!_isFullLog) { break; }
                    }
                }

                if (isValid) { yield return book; }
            }
        }
    }
}