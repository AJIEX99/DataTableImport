using System.Data;
using System.Reflection;
using DataTableImport.MappingMethodResolver;

namespace DataTableImport.PropertyBuilders;

public class SinglePropertyAssigner<TModel, TProperty> : IPropertyAssigner<TModel>
{
    public MapperResolver<object, TProperty> MapperResolver { get; }

    private readonly PropertyInfo propertyInfo;

    private readonly string columnName;

    public SinglePropertyAssigner(PropertyInfo propertyInfo, string columnName)
    {
        this.propertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
        this.columnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
        MapperResolver = new MapperResolver<object, TProperty>();
    }

    public TModel Build(TModel model, DataRow dataRow)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        if (dataRow == null)
        {
            throw new ArgumentNullException(nameof(dataRow));
        }

        var cellToMap = dataRow[columnName];

        var mappingMethod = MapperResolver
            .GetMappingMethod(cellToMap.GetType());

        propertyInfo.SetValue(model, mappingMethod != null
            ? mappingMethod.Invoke(cellToMap)
            : cellToMap);

        return model;
    }
}