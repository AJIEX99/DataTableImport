namespace DataTableImport.DefaultMapperRealization;

public abstract class GenericPropertyMapper<TSource, TResult> : AbstractPropertyMapper
{
    public override Type From => typeof(TSource);
    public override Type To => typeof(TResult);
}