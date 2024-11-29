using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;

// List to store products
List<Product> products = new List<Product>();

Console.WriteLine("Ange en produktkategori för att lägga till en produkt till din lista, eller skriv q för att visa listan.");

while (true)
{

    Console.Write("Ange kategori: ");
    string input = VerifyInput();

    if (input == null)
    {
        DisplayList();
        

    }
    else if (!string.IsNullOrWhiteSpace(input))
    {
        newProduct(input);
    }
    else
    {
        
        returnMessage("Red", "Fel! Ange en kategori eller q");
    }
}

void newProduct(string category)
{
    Console.Write("Ange produktnamn: ");
    string productName = VerifyInput();
    if(productName  == null)
    {
        DisplayList();
        return;
    }

    Console.Write("Ange pris för produkt: ");
    string priceInput = VerifyInput();
    if (priceInput == null)
    {
        DisplayList();
        return;
    }

    if (double.TryParse(priceInput, out double price))
    {
        Product newProduct = new Product(category, productName, price);
        products.Add(newProduct);
        returnMessage("Green", "Ny produkt tilllagd!");
    }
    else
    {
        returnMessage("Red", "Fel värde på pris");

    }
}

string VerifyInput(){
    string input = Console.ReadLine().ToLower().Trim();
    if(input == "q" || input == "quit")
    {
        return null;
    }
    return input;
};

void returnMessage(string color, string message)
{
    Console.ForegroundColor = (color == "Red") ? ConsoleColor.Red : ConsoleColor.Green;
    Console.WriteLine(message);
    Console.ResetColor();
}

void DisplayList()
{
    //Set padding based on Max Length - (Linq)

    int categoryWidth = Math.Max("Kategori".Length, products.Max(p => p.Category.Length));
    int nameWidth = Math.Max("Namn".Length, products.Max(p => p.Name.Length));

    double sumPrice = products.Sum(p => p.Price);

    //Display list
    Console.WriteLine("Produktlista (sorterad efter pris)");
    List<Product> sortedProducts = products.OrderBy(product => product.Price).ToList();

    if (sortedProducts.Count > 0)
    {
        Console.WriteLine("Kategori".PadRight(categoryWidth + 5) + "Namn".PadRight(nameWidth + 5) + "Pris");
        foreach (var product in sortedProducts)
        {
            Console.WriteLine(product.Category.PadRight(categoryWidth+5) + product.Name.PadRight(nameWidth+5) + product.Price);
        }
        Console.WriteLine("Total summa: " + sumPrice.ToString());
    }
    else
    {
        returnMessage("Red", "Listan har inga produkter.");

    }

    Console.Write("Tryck på valfri tangent för att fortsätta...");
    Console.ReadLine();
}


public class Product
{
    public Product(string category, string name, double price)
    {
        Category = category;
        Name = name;
        Price = price;
    }

    public string Category { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

}