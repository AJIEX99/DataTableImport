namespace DataTableImport.DefaultMapperRealization;

public abstract class AbstractPropertyMapper
{
    public abstract Type From { get; }

    public abstract Type To { get; }
    
    public abstract object Map(object source);
}