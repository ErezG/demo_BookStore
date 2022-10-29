using IntakeAgent.Common;

namespace IntakeAgent.BL.IntakeSteps.Transformations
{
    internal class PriceRoundUpper : IntakeTransformation
    {
        public override Book Transform(Book book)
        {
            book.Price = (int)book.Price + (book.Price % 1 > 0 ? 1 : 0);
            return book;
        }
    }
}
