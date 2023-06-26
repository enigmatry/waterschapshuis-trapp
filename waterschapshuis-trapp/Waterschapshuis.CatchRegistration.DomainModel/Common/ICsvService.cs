using CsvHelper.Configuration;
using System.Collections.Generic;

namespace Waterschapshuis.CatchRegistration.DomainModel.Common
{
    public interface ICsvService
    {
        byte[] AsBytes<TRecord, TRecordMap>(IEnumerable<TRecord> csvRecords) where TRecordMap : ClassMap<TRecord>;
        List<TRecord> AsCsvRecords<TRecord, TRecordMap>(FileUpload file) where TRecordMap : ClassMap<TRecord>;
    }
}
