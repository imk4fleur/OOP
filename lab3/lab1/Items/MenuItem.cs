using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MenuItem
{
    public string Name { get; }
    public double Price { get; }

    public MenuItem(string name, double price)
    {
        Name = name;
        Price = price;
    }
}