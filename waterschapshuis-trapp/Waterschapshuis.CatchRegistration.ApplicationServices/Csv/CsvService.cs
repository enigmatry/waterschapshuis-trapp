using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Waterschapshuis.CatchRegistration.DomainModel.Common;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Csv
{
    public class CsvService : ICsvService
    {
        private readonly CsvConfiguration _csvConfiguration;
        private readonly ILogger<CsvService> _logger;

        public CsvService(ILogger<CsvService> logger)
        {
            _csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                SanitizeForInjection = true,
                TrimOptions = TrimOptions.Trim,
                ShouldQuote = (field, context) => true,
                BadDataFound = null, // TODO ???
                MissingFieldFound = (field, index, context) => { field = new string[] { String.Empty }; },
            };
            _logger = logger;
        }

        public byte[] AsBytes<TRecord, TRecordMap>(IEnumerable<TRecord> csvRecords)
            where TRecordMap : ClassMap<TRecord>
        {
            try
            {
                _csvConfiguration.RegisterClassMap<TRecordMap>();

                using var memoryStream = new MemoryStream();
                using var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
                using var csvWriter = new CsvWriter(streamWriter, _csvConfiguration);

                csvWriter.WriteRecords(csvRecords);
                streamWriter.Flush();
                memoryStream.Position = 0;

                return memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed converting array of {typeof(TRecord)} csv records to byte array: ", ex);
                throw;
            }
        }

        public List<TRecord> AsCsvRecords<TRecord, TRecordMap>(FileUpload file)
            where TRecordMap : ClassMap<TRecord>
        {
            try
            {
                if (file.DataAsBase64.Contains(","))
                {
                    file.DataAsBase64 = file.DataAsBase64.Substring(file.DataAsBase64.IndexOf(",") + 1);
                }

                var csvByteArray = Convert.FromBase64String(file.DataAsBase64);

                using var memoryStream = new MemoryStream(csvByteArray);
                using var textReader = new StreamReader(memoryStream, Encoding.UTF8);
                _csvConfiguration.RegisterClassMap<TRecordMap>();
                using var csvReader = new CsvReader(textReader, _csvConfiguration);

                return csvReader.GetRecords<TRecord>().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed converting csv as Base64 string to array of {typeof(TRecord)} csv records: ", ex);
                throw;
            }
        }
    }
}
