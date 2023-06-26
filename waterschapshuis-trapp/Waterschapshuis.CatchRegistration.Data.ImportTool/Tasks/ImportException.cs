using System;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks
{
    public class ImportException : ApplicationException
    {
        public ImportException(string message) : base(message)
        {
        }

        // Helpers for import exceptions.
        public static ImportException NotFound(string arg) => new ImportException($"{arg} not found.");
        public static ImportException NotFoundUser() => NotFound("User");
        public static ImportException NotFoundProvince() => NotFound("Province");
        public static ImportException NotFoundSubAreaHourSquare() => NotFound("Sub area - hour square");
        public static ImportException NotFoundTrap() => NotFound("Trap");

        public static ImportException Invalid(string arg) => new ImportException($"Invalid {arg}.");
        public static ImportException InvalidDate() => Invalid("date");
        public static ImportException InvalidTrapType() => Invalid("trap-type");
        public static ImportException InvalidCatchType() => Invalid("catch-type");
        public static ImportException InvalidTimeRegistrationCategory() => Invalid("time-registration-category");
        public static ImportException InvalidWeek() => Invalid("week");
        public static ImportException InvalidYear() => Invalid("year");
        public static ImportException InvalidUser() => Invalid("user id");
        public static ImportException InvalidTrapStatus() => Invalid("trap-status");

        public static ImportException Empty(string arg) => throw new ImportException($"No {arg} found to import.");
        public static ImportException EmptyCatches() => Empty("catches");
        public static ImportException EmptyHours() => Empty("hours");

        public static ImportException Exists(string arg) => new ImportException($"{arg} already exists.");
        public static ImportException ExistsTrap() => Exists("Trap");
    }
}
