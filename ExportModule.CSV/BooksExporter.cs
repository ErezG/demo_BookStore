using IntakeAgent.Common;

namespace ExportModule.CSV
{
    public class CSVBooksExporter : IExportModule
    {
        public Task<bool> ExportBookList(IEnumerable<Book> books)
        {
            throw new NotImplementedException();
        }
    }
}