using DataTableImport.DefaultMapperRealization;

namespace DataTableImport.DefaultMappers;

public class StringIntPropertyMapper : GenericPropertyMapper<string, int>
{
    public override object Map(object source)
    {
        if (source.GetType() != typeof(string))
        {
            throw new ArgumentException("Used not compatible mapper");
        }

        var sourceString = source as string;

        return int.TryParse(sourceString, out int parsed)
            ? parsed
            : default(int);
    }
}