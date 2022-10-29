using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using IntakeAgent.Common;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ExportModule.CSV
{
    public class CSVBooksExporter : IExportModule
    {
        public async Task<bool> ExportBookList(IEnumerable<Book> books, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<BookMap>();
                await csv.WriteRecordsAsync(books);
            }

            return true;
        }
    }

    public class DateConverter : DefaultTypeConverter
    {
        public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            return DateTime.Parse(text);
        }

        public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            return ((DateTime)value).ToString("yyyy-MM-dd");
        }
    }

    public class LiteralConverter : DefaultTypeConverter
    {
        public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            throw new NotImplementedException();
        }

        public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            return Regex.Replace(Regex.Replace((string)value, "\r", @"\r"), "\n", @"\n");
        }
    }

    public sealed class BookMap : ClassMap<Book>
    {
        public BookMap()
        {
            Map(m => m.ID).Name("@id");
            Map(m => m.Author).Name("author");
            Map(m => m.Title).Name("title");
            Map(m => m.Genre).Name("genre");
            Map(m => m.Price).Name("price");
            Map(m => m.PublishDate).Name("publish_date").TypeConverter<DateConverter>();
            Map(m => m.Description).Name("description").TypeConverter<LiteralConverter>();
        }
    }
}