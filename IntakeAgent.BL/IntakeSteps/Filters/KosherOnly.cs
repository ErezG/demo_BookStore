using IntakeAgent.Common;
using Microsoft.Extensions.Logging;

namespace IntakeAgent.BL.IntakeSteps.Filters
{
    internal class KosherOnly : IntakeFilter
    {
        public override bool IsValid(Book book, ILogger? logger)
        {
            var publishDay = book.PublishDate.DayOfWeek;
            bool isValid = publishDay != DayOfWeek.Saturday;

            if (logger != null)
            {
                var log = CreateLog(book.Title, isValid, $"published on {publishDay}");
                logger.LogDebug(log);
            }

            return isValid;
        }
    }
}
