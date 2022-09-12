using DataTableImport.DefaultMappers;

namespace DataTableImport.DefaultMapperRealization;

public static class PropertyMapperHelper
{
    private static readonly List<AbstractPropertyMapper> DefaultMappers 
        = new List<AbstractPropertyMapper>()
        {
            new StringIntPropertyMapper()
        };

    public static AbstractPropertyMapper GetDefaultMapper(Type from, Type to)
    {
        return DefaultMappers
            .FirstOrDefault(mapper => mapper.From == from && mapper.To == to);
    }
}