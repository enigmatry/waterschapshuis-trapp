using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

[assembly:InternalsVisibleTo("Waterschapshuis.CatchRegistration.External.Api.Tests")]
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
[assembly: ApiController]
