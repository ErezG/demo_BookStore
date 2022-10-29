using IntakeAgent.Common;

namespace IntakeAgent.BL.IntakeSteps.Transformations
{
    internal abstract class IntakeTransformation : IIntakeStep
    {
        public (bool, Book) RunStep(Book book)
        {
            book = Transform(book);
            return (true, book);
        }

        public abstract Book Transform(Book book);
    }
}
