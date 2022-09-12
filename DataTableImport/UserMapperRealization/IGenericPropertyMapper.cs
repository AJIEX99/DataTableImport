namespace DataTableImport.UserMapperRealization;

public interface IGenericPropertyMapper<TSource,TResult>
{
    public TResult Map(TSource source);
}