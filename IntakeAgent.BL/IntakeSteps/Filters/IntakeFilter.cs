using IntakeAgent.Common;

namespace IntakeAgent.BL.IntakeSteps.Filters
{
    internal abstract class IntakeFilter : IIntakeStep
    {
        public (bool, Book) RunStep(Book book)
        {
            bool isValid = IsValid(book);
            return (isValid, book);
        }

        public abstract bool IsValid(Book book);
    }
}
