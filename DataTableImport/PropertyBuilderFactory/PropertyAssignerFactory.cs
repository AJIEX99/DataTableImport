using System.Reflection;
using DataTableImport.MappingMethodResolver;
using DataTableImport.PropertyBuilders;

namespace DataTableImport.PropertyBuilderFactory;

public class PropertyAssignerFactory<TModel,TProperty> : IPropertyAssignerFactory<TModel>
{
    private readonly PropertyInfo propertyInfo;

    private IPropertyAssigner<TModel> propertyAssigner;

    public PropertyAssignerFactory(PropertyInfo propertyInfo)
    {
        this.propertyInfo = propertyInfo;
    }

    public MapperResolver<object, TProperty> FromColumn(string columnName)
    {
        var single = new SinglePropertyAssigner<TModel, TProperty>(propertyInfo, columnName);

        propertyAssigner = single;

        return single.MapperResolver;
    }

    public MapperResolver<object[], TProperty> FromColumns(IEnumerable<string> columnNames)
    {
        var multiple = new MultipleColumnPropertyAssigner<TModel, TProperty>(propertyInfo, columnNames);

        propertyAssigner = multiple;

        return multiple.MapperResolver;

    }

    public IPropertyAssigner<TModel> GetAssigner()
    {
        return propertyAssigner;
    }
}