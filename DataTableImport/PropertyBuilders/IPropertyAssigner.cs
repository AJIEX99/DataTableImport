using System.Data;

namespace DataTableImport.PropertyBuilders;

public interface IPropertyAssigner<TModel>
{
    TModel Build(TModel model, DataRow dataRow);
}