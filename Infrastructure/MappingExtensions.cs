using Newtonsoft.Json;

namespace Infrastructure;

public static class MappingExtensions
{
    public static TResult MapTo<TResult>(this object? input)
                where TResult : class, new()
    {
        var target = new TResult();
        return input.MapTo(target);
    }

    public static TResult MapTo<TResult>(this object? input, TResult target) where TResult : class, new()
    {
        JsonConvert.PopulateObject(JsonConvert.SerializeObject(input), target);
        if (target == null)
        {
            throw new ApplicationException("Failed to map to target.");
        }

        return input == null ? new TResult() : target;
    }

    public static TResult Clone<TResult>(this TResult? input) where TResult : class, new()
    {
        return input.MapTo<TResult>();
    }
}
