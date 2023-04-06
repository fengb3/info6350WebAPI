using System.ComponentModel.DataAnnotations;
using SQLite;

namespace info6350WebAPI;

public interface IModel
{
    long Id { get; set; }
}

[Table("Product")]
public partial class Product : IModel
{
    [PrimaryKey, AutoIncrement]
    public long Id { get; set; } = 0;
    
    [Required, MinLength(1)]
    public string Name               { get; set; } = ""; // can not be empty
    
    public string ProductDescription { get; set; } = ""; // can be empty
    
    [Range(0, 5)]
    public double ProductRating      { get; set; } = 0; // should between 0 and 5
    
    [Required,Indexed]
    public long   CompanyId          { get; set; } = 0; // should be a valid company id in Company table
    
    [Range(1, int.MaxValue)]
    public int    Quantity           { get; set; } = 0; // should be a positive number
}

[Table("Company")]
public partial class Company : IModel
{
    [PrimaryKey, AutoIncrement]
    public long Id { get; set; } = 0;

    [Required, MinLength(1)]
    public string Name        { get; set; } = ""; // can not be empty
    
    [Required, MinLength(1)]
    public string Address     { get; set; } = ""; // can not be empty
    
    [Required, MinLength(1)]
    public string Country     { get; set; } = ""; // can not be empty
    
    [Required, RegularExpression(@"^\d{5}$")]
    public string Zip         { get; set; } = ""; // can not be empty, should be a 5 digit number
    
    [Required, MinLength(1)]
    public string CompanyType { get; set; } = ""; // can not be empty
    
    public string Description { get; set; } = ""; // can be empty
    
    public string LogoUrl     { get; set; } = ""; // can be empty
}

[Table("ProductPost")]
public partial class ProductPost : IModel
{
    [PrimaryKey, AutoIncrement]
    public long Id { get; set; } = 0;

    [Indexed, Required]
    public long ProductId { get; set; } = 0; // should be a valid product id in Product table

    [Indexed, Required]
    public long CompanyId { get; set; } = 0; // should be a valid company id in Company table

    [Indexed, Required]
    public long ProductTypeId { get; set; } = 0; // should be a valid product type id in ProductType table

    [NotNull, Required, MinLength(10)]
    public string PostedDate { get; set; } = ""; // can not be empty, should be a valid date formatted in "yyyy-MM-dd"

    [NotNull, Required]
    public double Price { get; set; } = 0; // should be a positive number
}

[Table("ProductType")]
public partial class ProductType : IModel
{
    [PrimaryKey, AutoIncrement]
    public long Id { get; set; } = 0;

    [Required, MinLength(1)]
    public string Name { get; set; } = ""; // can not be empty, can not be duplicated
}

[Table("Order")]
public partial class Order : IModel
{
    [PrimaryKey, AutoIncrement]
    public long Id { get; set; } = 0;

    [Required]
    public long PostId { get; set; } = 0; // should be a valid product post id in ProductPost table

    [Required, MinLength(10)]
    public string Date { get; set; } = ""; // can not be empty, should be a valid date formatted in "yyyy-MM-dd"

    [Required]
    public long ProductId { get; set; } = 0; // should be a valid product id in Product table

    [Required]
    public long ProductTypeId { get; set; } = 0; // should be a valid product type id in ProductType table
}