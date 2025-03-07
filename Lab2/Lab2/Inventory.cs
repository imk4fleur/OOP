namespace Lab2;

using System;
using System.Collections.Generic;

public class Inventory
{
    private List<Item> items = new List<Item>();
    private Dictionary<Type, IUpgradeStrategy> upgradeStrategies = new Dictionary<Type, IUpgradeStrategy>
    {
        { typeof(Weapon), new WeaponUpgrade() },
        { typeof(Armor), new ArmorUpgrade() },
        { typeof(Potion), new PotionUpgrade() }
    };

    public Weapon EquippedWeapon { get; private set; }
    public Armor EquippedArmor { get; private set; }

    public void EquipWeapon(string name)
    {
        EquippedWeapon = items.OfType<Weapon>().FirstOrDefault(w => w.Name == name) ?? throw new Exception($"Оружие {name} не найдено.");
    }

    public void EquipArmor(string name)
    {
        EquippedArmor = items.OfType<Armor>().FirstOrDefault(a => a.Name == name) ?? throw new Exception($"Броня {name} не найдена.");
    }

    public Item CombineItems(string name1, string name2)
    {
        Item item1 = items.Find(i => i.Name == name1);
        Item item2 = items.Find(i => i.Name == name2);

        if (item1 == null || item2 == null || item1.GetType() != item2.GetType())
        {
            throw new Exception("Предметы нельзя комбинировать.");
        }

        if (item1 is Weapon weapon1 && item2 is Weapon weapon2)
        {
            Weapon newWeapon = new Weapon(weapon1.Name + "+", weapon1.Damage + weapon2.Damage / 2);
            return newWeapon;
        }

        if (item1 is Armor armor1 && item2 is Armor armor2)
        {
            Armor newArmor = new Armor(armor1.Name + "+", armor1.Defense + armor2.Defense / 2);
            return newArmor;
        }

        throw new Exception("Этот тип предметов нельзя комбинировать.");
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void UseItem(string name)
    {
        Item item = items.Find(i => i.Name == name);
        if (item == null) throw new Exception($"Предмет {name} не найден.");
        item.Use();
    }

    public void UpgradeItem(string name)
    {
        Item item = items.Find(i => i.Name == name);
        if (item == null) throw new Exception($"Предмет {name} не найден.");
        if (upgradeStrategies.ContainsKey(item.GetType()))
        {
            upgradeStrategies[item.GetType()].Upgrade(item);
        }
    }

    public int GetItemsCount()
    {
        return items.Count;
    }
}

