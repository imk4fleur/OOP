using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IUpgradeStrategy
{
    void Upgrade(Item item);
}

// Concrete Strategies
public class WeaponUpgrade : IUpgradeStrategy
{
    public void Upgrade(Item item)
    {
        if (item is Weapon weapon)
        {
            weapon.Damage += 5;
        }
    }
}

public class ArmorUpgrade : IUpgradeStrategy
{
    public void Upgrade(Item item)
    {
        if (item is Armor armor)
        {
            armor.Defense += 3;
        }
    }
}

public class PotionUpgrade : IUpgradeStrategy
{
    public void Upgrade(Item item)
    {
        if (item is Potion potion)
        {
            potion.HealingAmount += 10;
        }
    }
}
