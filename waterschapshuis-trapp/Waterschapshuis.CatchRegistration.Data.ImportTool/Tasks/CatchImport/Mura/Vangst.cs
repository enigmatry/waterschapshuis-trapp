using System;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.CatchImport.Mura
{
    public class Vangst
    {
        public string Id { get; set; } = Guid.Empty.ToString();
        public int CatchType { get; set; }
        public string TrapId { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Number { get; set; }
    }
}
