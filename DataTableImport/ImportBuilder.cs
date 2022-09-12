using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using DataTableImport.ModelBuilder;
using DataTableImport.PropertyBuilderFactory;

namespace DataTableImport;

public class ImportBuilder<TModel> where TModel : new()
{
    private readonly DataTable source;

    private readonly ModelBuilder<TModel> modelBuilder;

    public ImportBuilder(DataTable source)
    {
        this.source = source;
        this.modelBuilder = new ModelBuilder<TModel>();
    }

    public PropertyAssignerFactory<TModel,TProperty> Property<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
    {
        var memberSelectorExpression = propertyExpression.Body as MemberExpression;

        if (memberSelectorExpression == null)
        {
            throw new ArgumentNullException(nameof(memberSelectorExpression));
        }
        
        var property = memberSelectorExpression.Member as PropertyInfo;
        
        if (property == null)
        {
            throw new ArgumentNullException(nameof(property));
        }
        
        return modelBuilder.AddPropertyMapping<TProperty>(property);
    }

    public IEnumerable<TModel> Build()
    {
        var models = new List<TModel>();
        
        foreach (var row in source.Rows)
        {
            models.Add(modelBuilder.Build((DataRow) row));
        }
        
        return models;
    }
}