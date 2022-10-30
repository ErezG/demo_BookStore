using IntakeAgent.BL.IntakeSteps;
using IntakeAgent.Common;
using Microsoft.Extensions.Logging;

namespace IntakeAgent.BL
{
    public class BooksProcessor
    {
        private IEnumerable<IIntakeStep> _intakeSteps;

        //logs
        //---------------------------------------
        private bool _keepLog;
        private bool _isFullLog;
        private ILogger _logger;
        //---------------------------------------

        public BooksProcessor(IEnumerable<IIntakeStep> intakeSteps, ILogger logger)
        {
            //temp - get from config
            var keepLog = true;
            var isFullLog = true;

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
                //var scope = logger?.BeginScope(item);
                
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

                //scope?.Dispose();

                if (isValid) { yield return book; }
            }
        }
    }
}