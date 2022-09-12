using System.Data;
using System.Reflection;
using DataTableImport.MappingMethodResolver;

namespace DataTableImport.PropertyBuilders;

public class MultipleColumnPropertyAssigner<TModel,TProperty> : IPropertyAssigner<TModel>
{
    public MapperResolver<object[], TProperty> MapperResolver { get; }
    
    private readonly IEnumerable<string> columnNames;
    
    private readonly PropertyInfo propertyInfo;
    
    public MultipleColumnPropertyAssigner(PropertyInfo propertyInfo, IEnumerable<string> columnNames)
    {
        this.propertyInfo = propertyInfo;
        this.columnNames = columnNames;
        MapperResolver = new MapperResolver<object[], TProperty>();
    }

    public TModel Build(TModel model, DataRow dataRow)
    {
        var columnValues = columnNames
            .Select(cn => dataRow[cn])
            .ToArray();

        var mappingMethod = MapperResolver.GetMappingMethod(typeof(object[]));

        if (mappingMethod == null)
            throw new Exception("Can't parse multiple columns to type " + propertyInfo.PropertyType + " without rule");

        var valueToInsert = mappingMethod.Invoke(columnValues);

        propertyInfo.SetValue(model, valueToInsert);

        return model;
    }
}