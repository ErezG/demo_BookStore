using IntakeAgent.Common;
using Microsoft.Extensions.Logging;

namespace IntakeAgent.BL.IntakeSteps.Transformations
{
    internal abstract class IntakeTransformation : IIntakeStep
    {
        public IntakeTransformation()
        {
            _typeName = this.GetType().Name;
        }

        private readonly string _typeName;

        public (bool, Book) RunStep(Book book, ILogger? logger)
        {
            book = Transform(book, logger);
            return (true, book);
        }

        public abstract Book Transform(Book book, ILogger? logger);

        protected string CreateLog(string title, string change)
        {
            return $"\"{title}\" through {_typeName} - change {change}";
        }
    }
}
