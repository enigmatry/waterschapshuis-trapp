using System;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Configuration;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool PassesOrganizationDateConstrain(this DateTimeOffset date, string organization)
        {
            return organization switch
            {
                OrganizationNames.Brabant => date >= new DateTime(2015, 1, 1),
                OrganizationNames.Fryslan => date >= new DateTime(2015, 1, 1),
                OrganizationNames.Limburg => date >= new DateTime(2015, 1, 1),
                OrganizationNames.Noordoostnederland => date >= new DateTime(2014, 1, 1),
                OrganizationNames.Rivierenland => date >= new DateTime(2014, 1, 1),
                OrganizationNames.Scheldestromen => date >= new DateTime(2014, 1, 1),
                OrganizationNames.WestEnMidden => date >= new DateTime(2014, 1, 1),
                OrganizationNames.Zuiderzeeland => date >= new DateTime(2015, 1, 1),
                _ => true
            };
        }
    }
}
