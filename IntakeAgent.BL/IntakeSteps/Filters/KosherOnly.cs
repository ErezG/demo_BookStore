using IntakeAgent.Common;

namespace IntakeAgent.BL.IntakeSteps.Filters
{
    internal class KosherOnly : IntakeFilter
    {
        public override bool IsValid(Book book)
        {
            var publishDay = book.PublishDate.DayOfWeek;
            bool isValid = publishDay != DayOfWeek.Saturday;
            var log = CreateLog(book.Title, isValid, $"published on {publishDay}");
            return isValid;
        }
    }
}
