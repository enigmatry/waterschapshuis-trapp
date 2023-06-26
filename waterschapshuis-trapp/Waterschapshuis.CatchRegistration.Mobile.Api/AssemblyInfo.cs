using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

[assembly:InternalsVisibleTo("Waterschapshuis.CatchRegistration.Api.Tests")]
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
[assembly: ApiController]
