using System.Collections.Generic;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using info6350WebAPI;

public class CompanyControllerTests
{
    
    private readonly Mock<IDB<Company>> _mockDb;
    private readonly CompanyController  _controller;

    public CompanyControllerTests()
    {
        _mockDb     = new Mock<IDB<Company>>();
        _controller = new CompanyController(_mockDb.Object);

        // Set up mock behavior for Get
        _mockDb.Setup(m => m.Get(It.IsAny<long>())).Returns(new Company { Id = 1, Name = "Test Company" });

        // Set up mock behavior for GetAll
        _mockDb.Setup(m => m.GetAll()).Returns(new List<Company>
                                               {
                                                   new Company { Id = 1, Name = "Test Company 1" },
                                                   new Company { Id = 2, Name = "Test Company 2" }
                                               });

        // Set up mock behavior for Insert
        _mockDb.Setup(m => m.Insert(It.IsAny<Company>())).Callback<Company>(c => c.Id = 3);

        // Set up mock behavior for Update (no need to return anything, just verify if it's called)
        
        

        // Set up mock behavior for Delete (no need to return anything, just verify if it's called)

        // ... set up other mock methods as needed
    }

    [Fact]
    public void Add_ValidCompany_ReturnsOkResult()
    {
        // Arrange
        var company = new Company
        {
            Name = "Test Company",
            Address = "123 Test Street",
            Country = "USA",
            Zip = "12345",
            CompanyType = "Public",
            Description = "A test company",
            LogoUrl = "https://example.com/logo.png"
        };

        // Act
        var result = _controller.Add(company);

        // Assert
        Assert.IsType<CreatedResult>(result);
    }

    // ...add more test cases
}
