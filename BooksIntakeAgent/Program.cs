using ExportModule.CSV;
using IntakeAgent.BL;
using IntakeAgent.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace BooksIntakeAgent
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IHost host = CreateDefaultBuilder().Build();
            var appTask = host.RunAsync();


            var intakeLogic = host.Services.GetService<IntakeLogic>();
            var filePath = _configuration.GetValue<string>("inputPath");

            try
            {
                ICollection<Book> books = null;
                try
                {
                    books = await intakeLogic.Parse(filePath);
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
                    var success = await intakeLogic.RunIntake(books);
                    var processingOutcome = success ? "completed successfully" : "failed";
                    Console.WriteLine($"new books processing {processingOutcome}");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: An unexpected technical error had occured. Please contact the application maintainers:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t" + ex);
                Console.ForegroundColor = ConsoleColor.White;
            }

            await appTask;
        }

        private static IConfiguration _configuration;

        static IHostBuilder CreateDefaultBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((hostBuilder, configBuilder) =>
                {
                    configBuilder.AddJsonFile("appsettings.json");
                })
                .ConfigureServices((hostBuilder, services) =>
                {
                    _configuration = hostBuilder.Configuration;
                    services.AddOptions().Configure<IntakeConfigs>(hostBuilder.Configuration.GetSection("IntakeLogic"));
                    services.AddSingleton<IExportModule, CSVBooksExporter>();
                    services.AddSingleton<IntakeLogic>();
                });
        }
    }
}