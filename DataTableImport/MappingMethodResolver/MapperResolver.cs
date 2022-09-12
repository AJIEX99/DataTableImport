using DataTableImport.DefaultMapperRealization;
using DataTableImport.UserMapperRealization;

namespace DataTableImport.MappingMethodResolver;

public class MapperResolver<TSource,TProperty>
{
    private IGenericPropertyMapper<TSource, TProperty> propertyMapper;

    private Func<TSource, TProperty> propertyMappingRule;

    public void UseMapper(IGenericPropertyMapper<TSource,TProperty> propertyMapper)
    {
        this.propertyMapper = propertyMapper;
    }

    public void UseMappingRule(Func<TSource, TProperty> propertyMappingRule)
    {
        this.propertyMappingRule = propertyMappingRule;
    }

    public Func<TSource, TProperty> GetMappingMethod(Type sourceType)
    {
        if (propertyMappingRule != null)
        {
            return propertyMappingRule;
        }

        if (propertyMapper != null)
        {
            return propertyMapper.Map;
        }

        var modelPropertyType = typeof(TProperty);
        
        var unpackedIfNullable = Nullable.GetUnderlyingType(modelPropertyType);

        if (sourceType != modelPropertyType && unpackedIfNullable != sourceType)
        {
            var destinationType = unpackedIfNullable ?? modelPropertyType;

            var mapper = PropertyMapperHelper.GetDefaultMapper(sourceType, destinationType);

            return o => (TProperty) mapper.Map(o);
        }

        return null;
    }
}