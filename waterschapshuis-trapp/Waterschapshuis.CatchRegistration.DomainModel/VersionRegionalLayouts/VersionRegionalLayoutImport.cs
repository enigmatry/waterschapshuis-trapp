using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core;

namespace Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts
{
    public class VersionRegionalLayoutImport : Entity<Guid>
    {
        public static readonly int OutputMaxMaxLength = 30000;
        public static readonly string ErrorPrefix = "ERROR";
        public static readonly string WarningPrefix = "WARN";
        public static readonly string InfoPrefix = "INFO";
        public static readonly string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        private VersionRegionalLayoutImport() { }

        public VersionRegionalLayoutImportState State { get; private set; } = VersionRegionalLayoutImportState.Started;
        public string StartedBy { get; private set; } = String.Empty;
        public DateTimeOffset StartedAt { get; private set; }
        public DateTimeOffset? FinishedAt { get; private set; } = null;
        public string NextVersionRegionalLayoutName { get; private set; } = String.Empty;
        public string Output { get; private set; } = String.Empty;


        public static VersionRegionalLayoutImport Create(string currentUserName, string nextVersionRegionalLayoutName) =>
            new VersionRegionalLayoutImport
            {
                Id = GenerateId(),
                State = VersionRegionalLayoutImportState.Started,
                NextVersionRegionalLayoutName = nextVersionRegionalLayoutName,
                StartedBy = currentUserName,
                StartedAt = DateTimeOffset.Now,
                FinishedAt = null
            };

        public void Update(VersionRegionalLayoutImport value)
        {
            State = value.State;
            NextVersionRegionalLayoutName = value.NextVersionRegionalLayoutName;
            StartedBy = value.StartedBy;
            StartedAt = value.StartedAt;
            FinishedAt = value.FinishedAt;
            Output = value.Output;
        }

        public void AddInfo(string message) =>
            Output += $"{InfoPrefix} {DateTime.Now.ToString(DateTimeFormat)}: {message}{Environment.NewLine}";

        public void AddWarning(string message) =>
            Output += $"{WarningPrefix} {DateTime.Now.ToString(DateTimeFormat)}: {message}{Environment.NewLine}";

        public void AddError(string message) =>
            Output += $"{ErrorPrefix} {DateTime.Now.ToString(DateTimeFormat)}: {message}{Environment.NewLine}";

        public void AddMessages(List<string> messages) =>
            Output += $"{String.Join(Environment.NewLine, messages)}{Environment.NewLine}";

        public void Start(string currentUserName, string nextVersionName)
        {
            StartedAt = DateTimeOffset.Now;
            FinishedAt = null;
            StartedBy = currentUserName;
            NextVersionRegionalLayoutName = nextVersionName;
            State = VersionRegionalLayoutImportState.Started;
            Output = String.Empty;
        }

        public void Finish(VersionRegionalLayoutImportState state)
        {
            FinishedAt = DateTimeOffset.Now;
            State = state;
        }

        public List<string> GetOutputMessages() => String.IsNullOrWhiteSpace(Output) 
            ? new List<string>() : Output.Split(Environment.NewLine).ToList();
    }
}
