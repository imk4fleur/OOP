using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WeaponFactory : IItemFactory
{
    public Item CreateItem(string name, int stat) => new Weapon(name, stat);
}

public class ArmorFactory : IItemFactory
{
    public Item CreateItem(string name, int stat) => new Armor(name, stat);
}

public class PotionFactory : IItemFactory
{
    public Item CreateItem(string name, int stat) => new Potion(name, stat);
}

public class QuestItemFactory : IItemFactory
{
    public Item CreateItem(string name, int stat) => new QuestItem(name);
}
