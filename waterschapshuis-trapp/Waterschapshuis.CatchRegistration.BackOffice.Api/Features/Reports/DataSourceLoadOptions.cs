using System;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data.Helpers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Reports
{
    public class DataSourceLoadOptionsBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var reportUri = bindingContext.ValueProvider.GetValue("reportUri");

            var loadOptions = new GetDevExtremeReport.Query(reportUri.FirstValue);
            DataSourceLoadOptionsParser.Parse(loadOptions, key => bindingContext.ValueProvider.GetValue(key).FirstOrDefault());
            bindingContext.Result = ModelBindingResult.Success(loadOptions);
            return Task.CompletedTask;
        }
    }
}
