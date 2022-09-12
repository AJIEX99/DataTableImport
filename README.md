# DataTableImport
This library allows you to easily convert the data stored in a DataTable into a Models

## Examples

### Maping from one column to property

```
var source = new DataTable();
        
var importBuilder = new ImportBuilder<Employee>(source);

importBuilder.Property(e => e.FirstName).FromColumn("A");
importBuilder.Property(e => e.LastName).FromColumn("B");

var models = importBuilder.Build();
```

### Using lambda to define custom mapping rule

```
var source = new DataTable();
        
var importBuilder = new ImportBuilder<Employee>(source);

importBuilder.Property(e => e.FirstName)
  .FromColumn("A")
  .UseMappingRule(cell => ((string) cell).ToUpper());

var models = importBuilder.Build();
```

### Using mapper class to define custom mapping rule

To create a mapper class, it must inherit from class ISinglePropertyMapper<TProperty> and should implement method TProperty Map(object source).
```
public class AddressMapper : ISinglePropertyMapper<Address>
{
        public Address Map(object source)
        {
            var streetPostCode = ((string) source).Split(";");

            return new Address()
            {
                Street = streetPostCode[0],
                PostCode = streetPostCode[1]
            };
        }
}
```
To use mapper class you need to use method UseMapper and pass instance of mapper class inside.
```
var source = new DataTable();

var addressMapper = new AddressMapper();

var importBuilder = new ImportBuilder<Employee>(source);

importBuilder.Property(e => e.Address)
    .FromColumn("A")
    .UseMapper(addressMapper);

var models = importBuilder.Build();
```

### Maping from multimpe columns to property

This way of mapping can be used to combine information from some columns to one property:
```
var source = new DataTable();
        
var importBuilder = new ImportBuilder<Employee>(source);

importBuilder.Property(e => e.FirstName)
  .FromColumns(new[] {"A", "B"})
  .UseMappingRule(cells => (string) cells[0] + (cells)[1]);
  
var models = importBuilder.Build();
``` 
Or to map same properties to model inside your model:

```
var source = new DataTable();
        
var importBuilder = new ImportBuilder<Employee>(source);

importBuilder.Property(e => e.Address)
  .FromColumns(new[] {"A", "B"})
  .UseMappingRule(cells => new Address()
  {
    Street = (string) cells[0],
    PostCode = (string) cells[1],
  });

var models = importBuilder.Build();      
```
### Maping from multimpe columns to property using mapper

```
public class AddressMapperMultiple : IMultipleColumnPropertyMapper<Address>
{
        public Address Map(object[] source)
        {
            return new Address()
            {
                Street = (string) source[0],
                PostCode = (string) source[1]
            };
        }
}
```
To use mapper class you need to use method UseMapper and pass instance of mapper class inside.
```
var source = new DataTable();
        
        var importBuilder = new ImportBuilder<Employee>(source);

        var addressMapper = new AddressMapperMultiple();

        importBuilder.Property(e => e.Address)
            .FromColumns(new[] {"A", "B"})
            .UseMapper(addressMapper);

        var models = importBuilder.Build();
```

