using IntakeAgent.BL;
using IntakeAgent.Common;
using System.Text.Json;

namespace BooksIntakeAgent
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var filePath = "";

            try
            {
                ICollection<Book> books = null;
                try
                {
                    books = await IntakeLogic.Parse(filePath);
                }
                catch (FileNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: Could not find the books file:");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\t" + filePath);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch (JsonException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: Failed to read the books file, there is a problem with the schema:");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\t" + ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch (ModelParseException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: Failed to read a book from the file:");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\t" + ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }

                if (books != null)
                {
                    await IntakeLogic.RunIntake(books);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: An unexpected technical error had occured. please contact the application maintainers:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t" + ex);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}