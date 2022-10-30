using IntakeAgent.Common;
using Microsoft.Extensions.Logging;

namespace IntakeAgent.BL.IntakeSteps.Transformations
{
    internal class PriceRoundUpper : IntakeTransformation
    {
        public override Book Transform(Book book, ILogger? logger)
        {
            float from = book.Price;
            book.Price = (int)book.Price + (book.Price % 1 > 0 ? 1 : 0);
            float to = book.Price;

            if (logger != null)
            {
                var log = CreateLog(book.Title, $"\'{nameof(book.Price)}\': {from} => {to}");
                logger.LogTrace(log);
            }

            return book;
        }
    }
}
