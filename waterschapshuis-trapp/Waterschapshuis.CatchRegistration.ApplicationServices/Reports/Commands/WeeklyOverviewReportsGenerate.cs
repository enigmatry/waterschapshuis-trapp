using MediatR;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Reports.Commands
{
    public static class WeeklyOverviewReportsGenerate
    {
        public class Command : IRequest<Result>
        {
            public string BackOfficeAppUrl { get; set; } = null!;
        }

        public class Result
        {
            public int EmailsSent { get; }

            private Result(in int emailsSent)
            {
                EmailsSent = emailsSent;
            }

            public static Result CreateResult(int emailsSent)
            {
                return new Result(emailsSent);
            }
        }
    }
}
