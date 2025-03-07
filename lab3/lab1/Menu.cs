using System;
using System.Collections.Generic;
using System.Linq;

public class Menu
{
    private List<MenuItem> items;

    public Menu()
    {
        items = new List<MenuItem>
        {
            new MenuItem("Pizza", 100),
            new MenuItem("Burger", 50),
            new MenuItem("Sushi", 80),
            new MenuItem("Pasta", 70),
            new MenuItem("Salad", 40)
        };
    }

    public List<MenuItem> GetAllItems() => items;

    public MenuItem GetItemByName(string name) =>
        items.FirstOrDefault(item => item.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
}
