namespace SDStore.API.ModelBinders
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System.ComponentModel;
    using System.Reflection;
    
    /// <summary>
    /// Generic array model binder for IEnumerable. When used on a Controller Action parameter it will trigger before the Action executes.
    /// It will convert the string sent parameter to IEnumerable{T} type, and then the Action will be executed.
    /// </summary>
    public class ArrayModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (!bindingContext.ModelMetadata.IsEnumerableType)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            // Extract the value (in our case - comma separated Guid values)
            var providedValue = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName)
                .ToString();

            if (string.IsNullOrEmpty(providedValue))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            // Store the type of elements stored inside the IEnumerable parameter (in our case it is Guid)
            var genericType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];

            // Create a converter (in our case it is Guid, but the implementation isn't tied up to Guids only, it could be anything)
            var converter = TypeDescriptor.GetConverter(genericType);

            // An object array that consists of all the values we send to the API (in our case Guid values).
            var objectArray = providedValue
                .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => converter.ConvertFromString(x.Trim()))
                .ToArray();

            // Array of the generic type (in our case Guid). Then we copy all the values from the objectArray to guidArray and assign it to the bindingContext.
            var guidArray = Array.CreateInstance(genericType, objectArray.Length);
            objectArray.CopyTo(guidArray, 0);
            bindingContext.Model = guidArray;

            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            return Task.CompletedTask;
        }
    }
}