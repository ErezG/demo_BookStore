namespace IntakeAgent.Common
{
    public interface IExportModule
    {
        Task<bool> ExportBookList(IEnumerable<Book> books);
    }
}