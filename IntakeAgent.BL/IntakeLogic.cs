using ExportModule.CSV;
using IntakeAgent.BL.IntakeSteps;
using IntakeAgent.Common;

namespace IntakeAgent.BL
{
    public static class IntakeLogic
    {
        static IntakeLogic()
        {
            _intakeSteps = new IIntakeStep[]
            {
                new IntakeSteps.Transformations.PriceRoundUpper(),
                new IntakeSteps.Filters.KosherOnly()
            };
            _processor = new BooksProcessor(_intakeSteps);
            _booksExporter = new CSVBooksExporter();
        }

        private static readonly IIntakeStep[] _intakeSteps;
        private static readonly BooksProcessor _processor;
        private static readonly IExportModule _booksExporter;

        public static Task<Book[]> Parse(string filePath) => BooksParser.Parse(filePath);

        public static async Task RunIntake(IEnumerable<Book> books)
        {
            var processedBooks = _processor.Process(books).ToArray();
            var exportSucceeded = await _booksExporter.ExportBookList(processedBooks);
        }
    }
}
