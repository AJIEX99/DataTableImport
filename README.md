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

### Maping from multimpe columns to property

This way of mapping can be used to combine nformation from some columns to ine property:
```
var source = new DataTable();
        
var importBuilder = new ImportBuilder<Employee>(source);

importBuilder.Property(e => e.FirstName)
  .FromColumns(new[] {"A", "B"})
  .UseMappingRule(cells => (string) cells[0] + (cells)[1]);
  
var models = importBuilder.Build();
``` 
Or to map same properties to model inside your model:
