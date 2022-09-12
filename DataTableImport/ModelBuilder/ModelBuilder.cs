using System.Data;
using System.Reflection;
using DataTableImport.PropertyBuilderFactory;

namespace DataTableImport.ModelBuilder;

public class ModelBuilder<TModel> where TModel : new()
{
    private readonly IList<IPropertyAssignerFactory<TModel>> propertyBuilderProviders;

    public ModelBuilder()
    {
        propertyBuilderProviders = new List<IPropertyAssignerFactory<TModel>>();
    }

    public PropertyAssignerFactory<TModel,TProperty> AddPropertyMapping<TProperty>(PropertyInfo propertyInfo)
    {
        var propertyBuilder = new PropertyAssignerFactory<TModel,TProperty>(propertyInfo);

        propertyBuilderProviders.Add(propertyBuilder);

        return propertyBuilder;
    }

    public TModel Build(DataRow dataRow)
    {
        if (dataRow == null)
            throw new ArgumentNullException(nameof(dataRow));

        var model = new TModel();

        foreach (var propertyBuilderProvider in propertyBuilderProviders)
        {
            var builder = propertyBuilderProvider.GetAssigner();

            builder.Build(model, dataRow);
        }

        return model;
    }
}