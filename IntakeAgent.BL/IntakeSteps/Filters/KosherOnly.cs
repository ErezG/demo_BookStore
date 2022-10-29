using IntakeAgent.Common;

namespace IntakeAgent.BL.IntakeSteps.Filters
{
    internal class KosherOnly : IntakeFilter
    {
        public override bool IsValid(Book book)
        {
            var publishDay = book.PublishDate.DayOfWeek;
            return publishDay != DayOfWeek.Saturday;
        }
    }
}
