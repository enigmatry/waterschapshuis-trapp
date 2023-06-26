using System.Reflection;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api
{
    public static class AssemblyFinder
    {
        private const string ProjectPrefix = "Waterschapshuis.CatchRegistration";

        public static Assembly ApplicationServicesAssembly => FindAssembly("ApplicationServices");
        public static Assembly BackOfficeApiAssembly => FindAssembly("BackOffice.Api");
        public static Assembly MobileApiAssembly => FindAssembly("Mobile.Api");
        public static Assembly DomainAssembly => FindAssembly("DomainModel");
        public static Assembly InfrastructureAssembly => FindAssembly("Infrastructure");
        public static Assembly ImportToolAssembly => FindAssembly("Data.ImportTool");
        public static Assembly SchedulerAssembly => FindAssembly("Scheduler");
        public static Assembly ExternalApiAssembly => FindAssembly("External.Api");

        private static Assembly FindAssembly(string projectSuffix)
        {
            return Assembly.Load($"{ProjectPrefix}.{projectSuffix}");
        }
    }
}
