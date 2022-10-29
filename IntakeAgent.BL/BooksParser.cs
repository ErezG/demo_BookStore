using IntakeAgent.Common;
using System.Text.Json;

namespace IntakeAgent.BL
{
    public static class BooksParser
    {
        public static async Task<Book[]> Parse(string filePath)
        {
            Book[]? parsedBooks;
            using (FileStream openStream = File.OpenRead(filePath))
            {
                parsedBooks = await JsonSerializer.DeserializeAsync<Book[]>(openStream);
            }

            if (parsedBooks == null || parsedBooks.Length == 0)
            {
                throw new ModelParseException("The file is empty.");
            }

            return parsedBooks;
        }
    }
}