using Assignment01.DataSources;
using Assignment01.DTOs;
using System.Xml.Linq;

namespace Assignment01;

internal class Program
{
    static void Main(string[] args)
    {
        /* Print Assignment Intro */
        PrintAssignmentIntro();

        /* Get list of customers from source */
        var customers = Source.CustomerList;

        /* Get list of products from source */
        var products = Source.ProductList;

        #region Question01
        //===========================================================================================
        // Q01: Get all products from the "Seafood" category. Print each product's name and price.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question01 :");

        /* Method Syntax */
        Console.WriteLine("Method Syntax: ");
        var seafoodProducts_MS0 = 
             products
            .Where(p => string.Equals(p.Category, "Seafood"))
            .Select(p => new BasicProductDTO(p.ProductName, p.UnitPrice));
        seafoodProducts_MS0.ToList().ForEach(product => Console.WriteLine(product));

        /* Draw a Separator Line */
        DrawSeparatorLine();

        /* Query Syntax */
        Console.WriteLine("Query Syntax: ");
        var seafoodProucts_QS0 =
            from p in products
            where string.Equals(p.Category, "Seafood")
            select new BasicProductDTO(p.ProductName, p.UnitPrice);
        seafoodProucts_QS0.ToList().ForEach(product => Console.WriteLine(product));

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question02
        //===========================================================================================
        // Q02: Get a list of only the product names from ProductList. Print each name.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question02 :");

        /* Method Syntax */
        Console.WriteLine("Method Syntax: ");
        var productsNames_MS0 =
             products
            .Select(p => p.ProductName);
        Console.WriteLine(string.Join(',', productsNames_MS0));

        /* Draw a Separator Line */
        DrawSeparatorLine();

        /* Query Syntax */
        Console.WriteLine("Query Syntax: ");
        var productsNames_QS0 =
            from p in products
            select p.ProductName;
        Console.WriteLine(string.Join(',', productsNames_QS0));

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question03
        //===========================================================================================
        // Q03: Sort all products by UnitPrice (ascending). Print each product's name and price.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question03 :");

        /* Method Syntax */
        Console.WriteLine("Method Syntax: ");
        var sortedProductsByUnitPrice_MS0 =
            products
            .OrderBy(p => p.UnitPrice)
            .Select(p => new BasicProductDTO(p.ProductName, p.UnitPrice));
        sortedProductsByUnitPrice_MS0.ToList().ForEach(product => Console.WriteLine(product));

        /* Draw a Separator Line */
        DrawSeparatorLine();

        /* Query Syntax */
        Console.WriteLine("Query Syntax: ");
        var sortedProductsByUnitPrice_QS0 =
            from p in products
            orderby p.UnitPrice
            select new BasicProductDTO(p.ProductName, p.UnitPrice);
        sortedProductsByUnitPrice_QS0.ToList().ForEach(product => Console.WriteLine(product));


        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question04
        //===========================================================================================
        // Q04: Get all products where UnitPrice is between 10 and 30
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question04 :");

        /* Method Syntax */
        Console.WriteLine("Method Syntax: ");
        var productsBetween10And30_MS0 =
            products
            .Where(p => p.UnitPrice is >= 10m and <= 30)
            .OrderBy(p => p.UnitPrice);
        productsBetween10And30_MS0.ToList().ForEach (product => Console.WriteLine(product));

        /* Draw a Separator Line */
        DrawSeparatorLine();

        /* Query Syntax */
        Console.WriteLine("Query Syntax: ");
        var productsBetween10And30_QS0 =
            from p in products
            where p.UnitPrice is >= 10 and <= 30
            orderby p.UnitPrice
            select p;
        productsBetween10And30_QS0.ToList().ForEach(product => Console.WriteLine(product));

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question05
        //===========================================================================================
        // Q05: Get all products that are in stock (UnitsInStock > 0) and belong to the "Condiments"
        //      category.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question05 :");

        /* Method Syntax */
        Console.WriteLine("Method Syntax: ");
        var condimentInStockProducts_MS0 =
            products
            .Where(p => p.UnitsInStock > 0 && string.Equals(p.Category, "Condiments"))
            .Select(p => new DetailedProductDTO(p.ProductName, p.Category, p.UnitPrice, p.UnitsInStock));
        condimentInStockProducts_MS0.ToList().ForEach(product => Console.WriteLine(product));

        /* Draw a Separator Line */
        DrawSeparatorLine();

        /* Query Syntax */
        Console.WriteLine("Query Syntax: ");
        var condimentInStockProducts_QS0 =
            from p in products
            where p.UnitsInStock > 0 && string.Equals(p.Category, "Condiments")
            select new DetailedProductDTO(p.ProductName, p.Category, p.UnitPrice, p.UnitsInStock);
        condimentInStockProducts_QS0.ToList().ForEach(product => Console.WriteLine(product));

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question06 
        //===========================================================================================
        // Q06: Create a new anonymous type with three properties:
        //      ● Name → the product name
        //      ● Price → the unit price
        //      ● StockStatus → a string: "Available" if UnitsInStock > 0, otherwise "Out of Stock"
        //      ● Print the result.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question06 :");

        /* Method Syntax */
        Console.WriteLine("Method Syntax: ");
        var productDetails_MS0 =
            products
            .Select(p => new { Name = p.ProductName, Price = p.UnitPrice, StockStaus = p.UnitsInStock > 0 ? "Available" : "Out of Stock" });
        productDetails_MS0.ToList().ForEach(product => Console.WriteLine(product));

        /* Draw a Separator Line */
        DrawSeparatorLine();

        /* Query Syntax */
        Console.WriteLine("Query Syntax: ");
        var productDetails_QS0 =
            from p in products
            select new { Name = p.ProductName, Price = p.UnitPrice, StockStaus = p.UnitsInStock > 0 ? "Available" : "Out of Stock" };
        productDetails_QS0.ToList().ForEach(product => Console.WriteLine(product));

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question07
        //===========================================================================================
        // Q07: Print each product's name along with its position (1-based) in the list. Expected
        //      format: 1. Chai, 2. Chang, etc.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question07 :");

        /* Method Syntax */
        Console.WriteLine("Method Syntax: ");
        var productNameWithPosition =
            products.Select((p, i) => $"{i + 1}. {p.ProductName}");
        productNameWithPosition.ToList().ForEach(product => Console.WriteLine(product));

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question08
        //===========================================================================================
        // Q08: Sort ProductList by Category ascending, then within each category, sort by UnitPrice
        //      descending.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question08 :");

        /* Method Syntax */
        Console.WriteLine("Method Syntax: ");
        var sortedProductsByCategoryAndUnitPrice_MS0 =
            products
            .OrderBy(p => p.Category)
            .ThenByDescending(p => p.UnitPrice)
            .Select(p => new DetailedProductDTO(p.ProductName, p.Category, p.UnitPrice, p.UnitsInStock));
        sortedProductsByCategoryAndUnitPrice_MS0.ToList().ForEach(product=> Console.WriteLine(product));

        /* Draw a Separator Line */
        DrawSeparatorLine();

        /* Query Syntax */
        Console.WriteLine("Query Syntax: ");
        var sortedProductsByCategoryAndUnitPrice_QS0 =
            from p in products
            orderby p.Category ascending, p.UnitPrice descending
            select new DetailedProductDTO(p.ProductName, p.Category, p.UnitPrice, p.UnitsInStock);
        sortedProductsByCategoryAndUnitPrice_QS0.ToList().ForEach(product => Console.WriteLine(product));

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question09
        //===========================================================================================
        // Q09: Get all products from the "Beverages" category, sorted by UnitsInStock descending.
        //      Print name and stock.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question09 :");

        /* Method Syntax */
        Console.WriteLine("Method Syntax: ");
        var beverageProducts_MS0 =
            products
            .Where(p => string.Equals(p.Category, "Beverages"))
            .OrderByDescending(p => p.UnitsInStock)
            .Select(p => new { p.ProductName, p.UnitsInStock });
        beverageProducts_MS0.ToList().ForEach(product=> Console.WriteLine(product));

        /* Draw a Separator Line */
        DrawSeparatorLine();

        /* Query Syntax */
        Console.WriteLine("Query Syntax: ");
        var beverageProducts_QS0 =
            from p in products
            where string.Equals(p.Category, "Beverages")
            orderby p.UnitsInStock descending
            select new { p.ProductName, p.UnitsInStock };
        beverageProducts_QS0.ToList().ForEach(product => Console.WriteLine(product));

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question10
        //===========================================================================================
        // Q10: Using QUERY SYNTAX with a compound from clause, list all orders placed in 1997 or
        //      later showing CustomerID and OrderDate.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question10 :");

        /* Query Syntax */
        Console.WriteLine("Query Syntax: ");

        var orders =
            from c in customers
            from o in c.Orders
            where o.OrderDate >= new DateTime(1997,1,1)
            select new { c.CustomerID, o.OrderDate };
        orders.ToList().ForEach(order => Console.WriteLine(order));

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question11
        //===========================================================================================
        // Q11: Show position number alongside ProductName
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question11 :");

        /* Method Syntax */
        Console.WriteLine("Method Syntax: ");
        var productNameWithPosition1 =
            products.Select((p, i) => $"{i} {p.ProductName}");
        productNameWithPosition1.ToList().ForEach(product => Console.WriteLine(product));

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question12
        //===========================================================================================
        // Q12: Sort first by-word length and then by a case-insensitive sort of the words in an
        //      array.
        //
        //      String[] Arr = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question12 :");

        /* Define the array to be sorted */
        String[] Arr = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

        /* Method Syntax */
        Console.WriteLine("Method Syntax: ");
        var sortedArray_MS0 =
            Arr.OrderBy(w => w.Length)
            .ThenBy(w => w,StringComparer.OrdinalIgnoreCase);
        sortedArray_MS0.ToList().ForEach(w => Console.WriteLine(w));

        /* Draw a Separator Line */
        DrawSeparatorLine();

        /* Query Syntax */
        Console.WriteLine("Query Syntax: ");
        var sortedArray_QS0 =
            from w in Arr
            orderby w.Length ascending, w.ToLower()
            select w;
        sortedArray_QS0.ToList().ForEach(w => Console.WriteLine(w));

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question13
        //===========================================================================================
        // Q13: Create a list of all digits in the array whose second letter is 'i' that is reversed
        //      from the order in the original array.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question13 :");

        /* Define a digits array of string */
        string[] digits = {"one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};

        /* Method Syntax */
        Console.WriteLine("Method Syntax: ");
        var result_MS0 =
            digits
            .Where(d => d.ToArray()[1] == 'i')
            .Reverse()
            .Select(d => d);
        Console.WriteLine(string.Join(',',result_MS0));

        /* Draw a Separator Line */
        DrawSeparatorLine();

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion
    }

    public static void PrintAssignmentIntro()
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                     Linq - ASSIGNMENT WITH ANSWERS                 ║");
        Console.WriteLine("║                            13 Questions                            ║");
        Console.WriteLine("║                           Assiginment (1)                          ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝\n");
        Console.ResetColor();
    }

    public static void PrintSectionTitle(string title)
    {
        string formattedTitle = $"# {title} #";
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine($"{new string('=', formattedTitle.Length)}\n" +
                 $"{formattedTitle}\n" +
                 $"{new string('=', formattedTitle.Length)}");
        Console.ResetColor();
    }
    public static void DrawSeparatorLine()
    {
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine(new string('=', count: 68));
        Console.ResetColor();
    }
    public static void DrawSectionSeparatorLine()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(new string('#', count: 70));
        Console.ResetColor();
    }
}
