using IntakeAgent.Common;
using Microsoft.Extensions.Logging;

namespace IntakeAgent.BL.IntakeSteps.Filters
{
    internal abstract class IntakeFilter : IIntakeStep
    {
        public IntakeFilter()
        {
            _typeName = this.GetType().Name;
        }

        private readonly string _typeName;

        public (bool, Book) RunStep(Book book, ILogger? logger)
        {
            bool isValid = IsValid(book, logger);
            return (isValid, book);
        }

        public abstract bool IsValid(Book book, ILogger? logger);

        protected string CreateLog(string title, bool isValid, string cause)
        {
            return $"\"{title}\" through {_typeName} - valid: {isValid}, cause: {cause}";
        }
    }
}
