using IntakeAgent.BL.IntakeSteps;
using IntakeAgent.Common;

namespace IntakeAgent.BL
{
    public class BooksProcessor
    {
        private IEnumerable<IIntakeStep> _intakeSteps;
        
        //logs
        //---------------------------------------
        //private bool _keepLog;
        //private bool _isFullLog;
        //private object _logger;
        //---------------------------------------

        public BooksProcessor(IEnumerable<IIntakeStep> intakeSteps)
        {
            _intakeSteps = intakeSteps;
            //_keepLog = keepLog;
            //_isFullLog = keepLog && isFullLog;
            //_logger = logger;
        }

        public IEnumerable<Book> Process(IEnumerable<Book> books)
        {
            foreach (var item in books)
            {
                var isValid = true;
                var book = item;
                foreach (var step in _intakeSteps)
                {
                    (bool isStepValid, book) = step.RunStep(book); // pass logger? to log ["<book name>" change <prop>: <from> > <to>]["Aladin" change price: 3.6 > 4]

                    //if (_keepLog)
                    //{
                    //    //todo: log
                    //    // get step name, append outputs according to interfaces, and write to log
                    //}

                    if (!isStepValid)
                    {
                        isValid = false;

                        /*if (!_isFullLog)*/ { break; }
                    }
                }

                if (isValid) { yield return book; }
            }
        }
    }
}