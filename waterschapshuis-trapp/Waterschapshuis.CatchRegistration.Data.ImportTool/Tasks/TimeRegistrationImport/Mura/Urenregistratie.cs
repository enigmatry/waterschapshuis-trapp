using System;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.TimeRegistrationImport.Mura
{
    public class Urenregistratie
    {
        public string Id { get; set; } = Guid.Empty.ToString();
        public string User { get; set; }
        public string SubArea { get; set; }
        public string HourSquare { get; set; }
        public DateTimeOffset? Date { get; set; }
        public int? Hours { get; set; }
        public int? Minutes { get; set; }
        public string TrappingType { get; set; }
    }
}
