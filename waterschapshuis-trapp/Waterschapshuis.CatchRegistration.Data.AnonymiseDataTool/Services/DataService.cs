using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Waterschapshuis.CatchRegistration.Data.AnonymiseDataTool.Services
{
    public class DataService : IDataService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IServiceScope _scope;

        public DataService(
            ILogger<DataService> logger,
            IServiceScopeFactory serviceScopeFactory
        )
        {
            _logger = logger;
            _scope = serviceScopeFactory.CreateScope();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }

        public async Task RunProcessingAsync(CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine("Please enter the path to mapping file (csv):");
                string csvPath = Console.ReadLine();

                var codes = File.ReadAllLines(csvPath)
                    .Skip(1)
                    .Select(WaterschapshuisCodes.FromCsv)
                    .ToList();

                Console.WriteLine("Please enter the path to json file you want to anonymise:");
                string jsonPath = Console.ReadLine();

                string line;

                var newFilePath = Path.Combine(Path.GetDirectoryName(jsonPath),
                    "ANONYMISED_" + Path.GetFileName(jsonPath));

                Console.WriteLine("PROCESSING STARTED. PLEASE WAIT...");

                await using (var outputFile = new StreamWriter(newFilePath))
                {
                    var file = new StreamReader(jsonPath ?? throw new InvalidOperationException());
                    while ((line = file.ReadLine()) != null)
                    {
                        codes.ForEach(code =>
                        {
                            // When replacing, we do it with "" (quotes) to avoid errors in cases like XYZ1 and XYZ11
                            line = Regex.Replace(line, "\"" + code.Code + "\"", "\"" + code.CodeV2 + "\"", RegexOptions.IgnoreCase);
                        });

                        outputFile.WriteLine(line);
                    }

                    file.Close();
                }

                Console.WriteLine("FINISHED. Anonymised file name: " + newFilePath);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error");
            }

            await Task.CompletedTask;
        }
    }
}
