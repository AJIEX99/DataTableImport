using DataTableImport.PropertyBuilders;

namespace DataTableImport.PropertyBuilderFactory;

public interface IPropertyAssignerFactory<TModel>
{
    public IPropertyAssigner<TModel> GetAssigner();
}