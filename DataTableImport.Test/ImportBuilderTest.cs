using System.Data;
using System.Linq;
using DataTableImport.Test.Models;
using NUnit.Framework;

namespace DataTableImport.Test;

public class ImportBuilderTests
{
    private readonly DataTable source = new DataTable();
    [SetUp]
    public void Setup()
    {
        source.Columns.Add("A", typeof(string));
        source.Columns.Add("B", typeof(string));
        source.Columns.Add("C", typeof(string));
        source.Columns.Add("D", typeof(string));
        source.Columns.Add("E", typeof(string));
        source.Columns.Add("F", typeof(string));
        source.Columns.Add("G", typeof(string));
        source.Columns.Add("H", typeof(string));
        
        source.Rows.Add("Frankie", "Mcdougall", "1989", "USA", "+380", "1234567", "Bronx", "10473");
        source.Rows.Add("Waqar", "Bridges", "1990", "Canada", "+380", "7654321", "Airdrie West", "T4B");
    }
    
    [Test]
    public void MappingFromColumnToProperty()
    {
        var importBuilder = new ImportBuilder<Employee>(source);

        importBuilder.Property(e => e.FirstName).FromColumn("A");
        importBuilder.Property(e => e.LastName).FromColumn("B");

        var models = importBuilder.Build().ToList();
        
        Assert.AreEqual(models[0].FirstName,"Frankie");
        Assert.AreEqual(models[0].LastName,"Mcdougall");
        Assert.AreEqual(models[1].FirstName,"Waqar");
        Assert.AreEqual(models[1].LastName,"Bridges");
    }
}