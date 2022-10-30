using IntakeAgent.Common;

namespace IntakeAgent.BL.IntakeSteps.Transformations
{
    internal class PriceRoundUpper : IntakeTransformation
    {
        public override Book Transform(Book book)
        {
            float from = book.Price;
            book.Price = (int)book.Price + (book.Price % 1 > 0 ? 1 : 0);
            float to = book.Price;
            var log = CreateLog(book.Title, $"\'{nameof(book.Price)}\': {from} => {to}");
            return book;
        }
    }
}
