using IntakeAgent.BL.IntakeSteps;
using IntakeAgent.Common;
using Microsoft.Extensions.Options;

namespace IntakeAgent.BL
{
    public class IntakeLogic
    {
        public IntakeLogic(IExportModule exportModule, IOptions<IntakeConfigs> configs)
        {
            _intakeSteps = StepsInitializer.InitializeSteps(configs.Value.IntakeSteps).ToArray();
            _processor = new BooksProcessor(_intakeSteps);
            _booksExporter = exportModule;
            _exportPath = configs.Value.ExportPath;
        }

        private readonly IIntakeStep[] _intakeSteps;
        private readonly BooksProcessor _processor;
        private readonly IExportModule _booksExporter;
        private readonly string _exportPath;

        public Task<Book[]> Parse(string filePath) => BooksParser.Parse(filePath);

        public async Task<bool> RunIntake(IEnumerable<Book> books)
        {
            var processedBooks = _processor.Process(books).ToArray();
            var exportSucceeded = await _booksExporter.ExportBookList(processedBooks, _exportPath);
            return exportSucceeded;
        }
    }
}
