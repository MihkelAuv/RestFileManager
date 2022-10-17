using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System;
using System.Text.Json;

namespace RestFileManager.Models
{
    public class MetadataModelProvider : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext is null)
                throw new ArgumentNullException(nameof(bindingContext));

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None) return Task.CompletedTask;
            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;
            var result = JsonSerializer.Deserialize(value, bindingContext.ModelType);
            if (result is null) return Task.CompletedTask;
            bindingContext.Result = ModelBindingResult.Success(result);
            
            return Task.CompletedTask;
        }
    }
}
