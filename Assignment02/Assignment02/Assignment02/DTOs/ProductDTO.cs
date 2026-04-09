using Assignment01.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment01.DTOs;

public record BasicProductDTO(string ProductName, decimal UnitPrice);
public record DetailedProductDTO(string ProductName, string Category, decimal UnitPrice, int UnitsInStock);
public record ProductDTO(int ProductID, string ProductName, string Category, decimal UnitPrice, int UnitsInStock)
{
    /* Constructor Overloads */

    // Constructor Chaining
    public ProductDTO(Product product) : this(product.ProductID, product.ProductName, product.ProductName ,product.UnitPrice, product.UnitsInStock)
    { 
    }
}

