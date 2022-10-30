using IntakeAgent.Common;

namespace IntakeAgent.BL.IntakeSteps.Filters
{
    internal abstract class IntakeFilter : IIntakeStep
    {
        public IntakeFilter()
        {
            _typeName = this.GetType().Name;
        }

        private readonly string _typeName;

        public (bool, Book) RunStep(Book book)
        {
            bool isValid = IsValid(book);
            return (isValid, book);
        }

        public abstract bool IsValid(Book book);

        protected string CreateLog(string title, bool isValid, string cause)
        {
            return $"\"{title}\" through {_typeName} - valid: {isValid}, cause: {cause}";
        }
    }
}
