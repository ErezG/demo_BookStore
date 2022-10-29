using IntakeAgent.Common;

namespace ExportModule.CSV
{
    public class BooksExporter : IExportModule
    {
        public Task<bool> ExportBookList(IEnumerable<Book> books)
        {
            throw new NotImplementedException();
        }
    }
}