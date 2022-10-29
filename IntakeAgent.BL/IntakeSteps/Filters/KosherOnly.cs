using IntakeAgent.Common;

namespace IntakeAgent.BL.IntakeSteps.Filters
{
    internal class KosherOnly : IIntakeFilter
    {
        public (bool, Book) RunStep(Book book)
        {
            bool isValid = IsValid(book);
            return (isValid, book);
        }

        public bool IsValid(Book book)
        {
            var publishDay = book.PublishDate.DayOfWeek;
            return publishDay != DayOfWeek.Saturday;
        }
    }
}
