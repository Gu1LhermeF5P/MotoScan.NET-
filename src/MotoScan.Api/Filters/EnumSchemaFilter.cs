namespace MotoScan.MotoScan.Api.Filters;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.ComponentModel;


public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            schema.Enum.Clear();
            schema.Type = "string";
            schema.Format = null;

            foreach (var enumValue in Enum.GetValues(context.Type))
            {
                var enumName = Enum.GetName(context.Type, enumValue);
                var fieldInfo = context.Type.GetField(enumName!);
                var descriptionAttribute = fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .FirstOrDefault() as DescriptionAttribute;

                schema.Enum.Add(new OpenApiString(descriptionAttribute?.Description ?? enumName));
            }
        }
    }
}

public class SchemaFilterContext
{
    public Type Type { get; set; }
}

public interface ISchemaFilter
{
}
