using System;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.UserImport
{
    public class Gebruiker
    {
        public long Id { get; set; }
        public string Name { get; } = String.Empty;
        public string Email { get; } = String.Empty;
    }
}
