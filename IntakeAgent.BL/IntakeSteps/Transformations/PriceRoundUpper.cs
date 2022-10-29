﻿using IntakeAgent.Common;

namespace IntakeAgent.BL.IntakeSteps.Transformations
{
    internal class PriceRoundUpper : IIntakeTransformation
    {
        public (bool, Book) RunStep(Book book)
        {
            book = Transform(book);
            return (true, book);
        }

        public Book Transform(Book book)
        {
            book.Price = (int)book.Price + (book.Price % 1 > 0 ? 1 : 0);
            return book;
        }
    }
}
