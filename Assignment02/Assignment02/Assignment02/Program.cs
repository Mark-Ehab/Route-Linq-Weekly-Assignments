using Assignment01.DataSources;
using Assignment01.DTOs;
using System.Reflection.Emit;

namespace Assignment02;

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
        // Q01: Get top 3 most expensive products.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question01 :");

        /* Get top 3 most expensive products */
        var top3ExpensiveProducts =
            products
            .OrderByDescending(p => p.UnitPrice)
            .Take(3);
        top3ExpensiveProducts.ToList().ForEach(product => Console.WriteLine(product));

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question02
        //===========================================================================================
        // Q02: show page 2 of products, with page size = 5.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question02 :");

        /* Define pageSize and pageNumber variables */
        var pageNumber = 2;
        var pageSize = 5;

        /* show page 2 of products, with page size = 5 */
        var page2OfProducts = 
            products
            .OrderBy(p => p.ProductID)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
        page2OfProducts.ToList().ForEach(product => Console.WriteLine(product));

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question03
        //===========================================================================================
        // Q03: Take products from the list as long as Their UnitPrice is less than $25 (list is
        //      ordered by price).
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question03 :");

        /* Take products from the list as long as Their UnitPrice is less than $25 */
        var productsLessThan25 =
            products
            .OrderBy(p => p.UnitPrice)
            .TakeWhile(p => p.UnitPrice < 25m);
        productsLessThan25.ToList().ForEach(product => Console.WriteLine(product));

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question04
        //===========================================================================================
        // Q04: Check if ALL products in the "Seafood" category are in stock.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question04 :");

        /* Check if ALL products in the "Seafood" category are in stock */
        var areAllSeafoodProductsInStock =
            products
            .Where(p => p.Category.Equals("Seafood"))
            .All(p => p.UnitsInStock > 0);
        Console.WriteLine(areAllSeafoodProductsInStock ? "All Seafood Products are in stock" :
            "Not all Seafood Products are in stock");

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question05
        //===========================================================================================
        // Q05: Check if the ID list contains 9
        //      int[] ids = { 3, 9, 13, 18 };
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question05 :");

        /* Define ids */
        int[] ids = { 3, 9, 13, 18 };

        /* Convert it to HashSet for a better performance */
        var hashedIds = ids.ToHashSet();

        /* Check if the ID list contains 9 */
        Console.WriteLine(
            ids
            .Contains(9) ? "ID list contains 9" : "ID list doesn't contain 9"
            );

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question06 
        //===========================================================================================
        // Q06: Group all products by Category and print each group with its product count.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question06 :");

        /* Group all products by Category and print each group with its product count */
        var productCategoriesWithCount =
            products
            .GroupBy(p => p.Category)
            .Select(g => new
            {
                Category = g.Key,
                ProductsCountPerCategory = g.Count()
            });
        productCategoriesWithCount.ToList().ForEach(group => Console.WriteLine(group));

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question07
        //===========================================================================================
        // Q07: Group products by Category and project only product names per group.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question07 :");

        /* Group products by Category and project only product names per group */
        var productsNamesPerGroup =
            products
            .GroupBy(p => p.Category)
            .Select(gp => new
            {
                Category = gp.Key,
                CategoryProductNames = gp.Select(p => p.ProductName)
            })
            .ToList();
        foreach(var productGroup in productsNamesPerGroup)
        {
            Console.WriteLine($"Category: {productGroup.Category}");

            Console.WriteLine("Products:");

            Console.WriteLine(string.Join('\n', productGroup.CategoryProductNames));

            DrawSeparatorLine();
        }

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question08
        //===========================================================================================
        // Q08: Find all categories that have MORE THAN 3 products.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question08 :");

        /* Find all categories that have MORE THAN 3 products */
        var categoriesWithMoreThan3Products =
            products
            .GroupBy(p => p.Category)
            .Where(pg => pg.Count() > 3)
            .Select(pg => pg.Key);
        categoriesWithMoreThan3Products.ToList().ForEach(category => Console.WriteLine(category));

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question09
        //===========================================================================================
        // Q09: Using QUERY SYNTAX, group customers by Country, and for each
        //      group select { Country, Count, TotalOrderValue }.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question09 :");

        /* 
           QUERY SYNTAX :group customers by Country, 
           and for each group select { Country, Count, TotalOrderValue } 
         */
        var customersGroupedByCountry =
           from c in customers
           group c by c.Country into cg
           select new 
           { 
                 Country = cg.Key,
                 Count = cg.Count(),
                 TotalOrderValue = 
                 cg
                 .SelectMany(c => c.Orders)
                 .Sum(o => o.Total)                         
           };
        customersGroupedByCountry.ToList().ForEach (customerGroup => Console.WriteLine(customerGroup));

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question10
        //===========================================================================================
        // Q10: Calculate the total number of units in stock across all products.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question10 :");

        /* Calculate the total number of units in stock across all products */
        Console.WriteLine($"Total number of units in stock across all products = " +
            $"{products.Sum(p => p.UnitsInStock)}");

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question11
        //===========================================================================================
        // Q11: Find the CHEAPEST and MOST EXPENSIVE product prices.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question11 :");

        /* Get the CHEAPEST product price */
        // 1
        var cheapestProduct_1 =
            products
            .Min(p => p.UnitPrice);
        Console.WriteLine($"Cheapest Product Price : {cheapestProduct_1:C}");

        // 2
        //var cheapestProduct_2 =
        //    products
        //    .OrderBy(p => p.UnitPrice)
        //    .FirstOrDefault();
        //Console.WriteLine($"Cheapest Product Price : {cheapestProduct_2?.UnitPrice ?? default:C}");

        /* Get the CHEAPEST product price */
        // 1
        var mostExpensiveProduct_1 =
            products
            .Max(p => p.UnitPrice);
        Console.WriteLine($"Most Expensive Product Price : {mostExpensiveProduct_1:C}");

        // 2
        //var mostExpensiveProduct_2 =
        //    products
        //    .OrderByDescending(p => p.UnitPrice)
        //    .FirstOrDefault();
        //Console.WriteLine($"Most Expensive Product Price : {mostExpensiveProduct_2?.UnitPrice ?? default:C}");

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question12
        //===========================================================================================
        // Q12: Get a distinct list of all product categories.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question12 :");

        /* Get a distinct list of all product categories */
        // 1
        var productCategories_1 =
            products
            .Select(p => p.Category)
            .Distinct();
        productCategories_1.ToList().ForEach(category => Console.WriteLine(category));
        // 2
        //var productCategories_2 =
        //    products
        //    .Select(p => p.Category)
        //    .ToHashSet();
        //foreach (var category in productCategories_2)
        //{
        //    Console.WriteLine(category);
        //}

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question13
        //===========================================================================================
        // Q13: find product IDs that are in setA but NOT in setB
        //      int[] setA = { 1, 3, 5, 7, 9, 11, 13 };
        //      int[] setB = { 3, 6, 9, 12, 15, 13 };
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question13 :");

        /* Define two sets A and B */
        int[] setA = { 1, 3, 5, 7, 9, 11, 13 };
        int[] setB = { 3, 6, 9, 12, 15, 13 };

        /* Get product IDs that are in setA but NOT in setB */
        var result = 
            setA
            .Except(setB)
            .ToList();
        Console.WriteLine($"""
            Product IDs that are in setA but NOT in setB:
            {string.Join(',',result)}
            """);

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question14 
        //===========================================================================================
        // Q14: Find countries that appear in list1 but NOT in list2 (case-insensitive).
        //      string[] list1 = { "Germany", "France", "UK", "Spain" };
        //      string[] list2 = { "france", "SPAIN", "Italy" };
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question14 :");

        /* Define two country lists list1 and list2 */
        string[] list1 = { "Germany", "France", "UK", "Spain" };
        string[] list2 = { "france", "SPAIN", "Italy" };

        /* Get product IDs that are in setA but NOT in setB */
        var countries =
            list1
            .Except(list2,StringComparer.OrdinalIgnoreCase)
            .ToList();
        Console.WriteLine($"""
            Countries that appear in list1 but NOT in list2 are:
            {string.Join(',', countries)}
            """);

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question15
        //===========================================================================================
        // Q15: Build a Dictionary<int, Product> keyed by ProductID. Then retrieve and print the
        //      product with ID = 18.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question15 :");

        /* Build a Dictionary<int, Product> keyed by ProductID */
        var productIdKeyedDictionary =
            products
            .ToDictionary(p => p.ProductID);

        /* Retrieve and print the product with ID = 18 */
        Console.WriteLine($"Product whose Id is 18 = \n{productIdKeyedDictionary[18]}");

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question16
        //===========================================================================================
        // Q16: Get the first product whose price is greater than $50.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question16 :");

        /* Get the first product whose price is greater than $50 */
        var firstProductAbove50 =
            products
            .FirstOrDefault(p => p.UnitPrice > 50m);
        Console.WriteLine($"{(firstProductAbove50 is not null ? $"The first product with a price > $500 is: \n{firstProductAbove50}" : "No product has a unit price above $500")} ");


        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question17
        //===========================================================================================
        // Q17: Try to get the first product with a price > $500. it returns null instead of throwing.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question17 :");

        /* Get the first product with a price > $500 */
        var firstProductAbove500 =
            products
            .FirstOrDefault(p => p.UnitPrice > 500m);
        Console.WriteLine($"{(firstProductAbove500 is not null ? $"The first product with a price > $500 is: \n{firstProductAbove500}" : "No product has a unit price above $500")} ");

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question18
        //===========================================================================================
        // Q18: Generate a multiplication table row for 7.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question18 :");

        /* Generate a multiplication table row for 7 */
        var multiplicationTableFor7 =
            Enumerable
            .Range(0, 13)
            .Select(num => $"7 x {num} = {7 * num}")
            .ToList();
        Console.WriteLine("Multiplication table for 7:");
        multiplicationTableFor7.ForEach(row => Console.WriteLine(row));

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question19
        //===========================================================================================
        // Q19: Generate even numbers between 1 and 30.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question19 :");

        /* Generate even numbers between 1 and 30. */
        var evenNumsBetween1And30 = Enumerable.Range(1, 30).Where(n => n % 2 == 0).ToList();
        Console.WriteLine($"""
            Even numbers between 1 and 30:
            {string.Join(',', evenNumsBetween1And30)}
            """);

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question20
        //===========================================================================================
        // Q20: Concatenate the first 3 product names with the first 3 customer company names into a
        //      single sequence.
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question20 :");

        /* 
         * Concatenate the first 3 product names with the first 3 customer 
         * company names into a single sequence 
         */
        var concatenationResultSequence =
            products
            .Zip(customers, (p, c) => new{p.ProductName , c.CompanyName})
            .Take(3)
            .ToList();
        concatenationResultSequence.ForEach(result => Console.WriteLine(result));

        /* Draw a Section Separator Line */
        DrawSectionSeparatorLine();

        #endregion

        #region Question21
        //===========================================================================================
        // Q21: Pair each product with a customer (by position) and produce a string "ProductName
        //      sold to CompanyName".
        //===========================================================================================

        /* Print Section Title */
        PrintSectionTitle("Answer of Question21 :");

        /* 
         * Pair each product with a customer (by position) and produce a string 
         * "ProductName sold to CompanyName"
         */
        var paringResult =
            products
            .Zip(customers, (p, c) => $"{p.ProductName} sold to {c.CompanyName}\n")
            .ToList();
        paringResult.ForEach(result => Console.WriteLine(result));

        #endregion
    }

    public static void PrintAssignmentIntro()
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                     Linq - ASSIGNMENT WITH ANSWERS                 ║");
        Console.WriteLine("║                            21 Questions                            ║");
        Console.WriteLine("║                           Assiginment (2)                          ║");
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
