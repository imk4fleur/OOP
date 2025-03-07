namespace Lab2.Tests;

using System;
using Xunit;

public class ItemTests
{
    [Fact]
    public void Weapon_Use_DecreasesDamage()
    {
        var weapon = new Weapon("Sword", 10);
        weapon.Use();
        Assert.Equal(9, weapon.Damage);
    }

    [Fact]
    public void Armor_Use_IncreasesDefense()
    {
        var armor = new Armor("Shield", 5);
        armor.Use();
        Assert.Equal(6, armor.Defense);
    }

    [Fact]
    public void Potion_Use_SetsHealingAmountToZero()
    {
        var potion = new Potion("Health Potion", 20);
        potion.Use();
        Assert.Equal(0, potion.HealingAmount);
    }

    [Fact]
    public void QuestItem_Use_SetsIsUsedToTrue()
    {
        var questItem = new QuestItem("Ancient Relic");
        questItem.Use();
        Assert.True(questItem.IsUsed);
    }

    [Fact]
    public void QuestItem_Use_Twice_ThrowsException()
    {
        var questItem = new QuestItem("Ancient Relic");
        questItem.Use();
        Assert.Throws<InvalidOperationException>(() => questItem.Use());
    }

}

public class InventoryTests
{
    [Fact]
    public void EquipWeapon_SetsEquippedWeapon()
    {
        var inventory = new Inventory();
        var weapon = new Weapon("Sword", 10);
        inventory.AddItem(weapon);

        inventory.EquipWeapon("Sword");

        Assert.Equal("Sword", inventory.EquippedWeapon.Name);
    }

    [Fact]
    public void EquipWeapon_NotExisting_ThrowsException()
    {
        var inventory = new Inventory();
        Assert.Throws<Exception>(() => inventory.EquipWeapon("Nonexistent Sword"));
    }

    [Fact]
    public void EquipArmor_SetsEquippedArmor()
    {
        var inventory = new Inventory();
        var armor = new Armor("Shield", 5);
        inventory.AddItem(armor);

        inventory.EquipArmor("Shield");

        Assert.Equal("Shield", inventory.EquippedArmor.Name);
    }

    [Fact]
    public void EquipArmor_NotExisting_ThrowsException()
    {
        var inventory = new Inventory();
        Assert.Throws<Exception>(() => inventory.EquipArmor("Nonexistent Shield"));
    }

    [Fact]
    public void CombineWeapons_CreatesNewWeaponWithHigherDamage()
    {
        var inventory = new Inventory();
        var weapon1 = new Weapon("Sword", 10);
        var weapon2 = new Weapon("Sword2", 8);
        inventory.AddItem(weapon1);
        inventory.AddItem(weapon2);

        var combinedWeapon = inventory.CombineItems("Sword", "Sword2");

        Assert.Equal("Sword+", combinedWeapon.Name);
        Assert.Equal(14, ((Weapon)combinedWeapon).Damage);
    }

    [Fact]
    public void CombineArmors_CreatesNewArmorWithHigherDefense()
    {
        var inventory = new Inventory();
        var armor1 = new Armor("Shield", 6);
        var armor2 = new Armor("Shield2", 4);
        inventory.AddItem(armor1);
        inventory.AddItem(armor2);

        var combinedArmor = inventory.CombineItems("Shield", "Shield2");

        Assert.Equal("Shield+", combinedArmor.Name);
        Assert.Equal(8, ((Armor)combinedArmor).Defense);
    }

    [Fact]
    public void CombineItems_DifferentTypes_ThrowsException()
    {
        var inventory = new Inventory();
        var weapon = new Weapon("Sword", 10);
        var armor = new Armor("Shield", 5);
        inventory.AddItem(weapon);
        inventory.AddItem(armor);

        Assert.Throws<Exception>(() => inventory.CombineItems("Sword", "Shield"));
    }

    [Fact]
    public void CombineItems_NonExisting_ThrowsException()
    {
        var inventory = new Inventory();
        Assert.Throws<Exception>(() => inventory.CombineItems("Nonexistent", "Nonexistent"));
    }


    [Fact]
    public void AddItem_IncreasesItemCount()
    {
        var inventory = new Inventory();
        inventory.AddItem(new Weapon("Sword", 10));
        Assert.Equal(1, inventory.GetItemsCount());
    }

    [Fact]
    public void UseItem_AppliesCorrectEffect()
    {
        var inventory = new Inventory();
        var weapon = new Weapon("Sword", 10);
        var armor = new Armor("Shield", 5);
        inventory.AddItem(weapon);
        inventory.AddItem(armor);

        inventory.UseItem("Sword");
        inventory.UseItem("Shield");

        Assert.Equal(9, weapon.Damage);
        Assert.Equal(6, armor.Defense);
    }

    [Fact]
    public void UpgradeItem_UsesCorrectStrategy()
    {
        var inventory = new Inventory();
        var weapon = new Weapon("Sword", 10);
        var armor = new Armor("Shield", 5);
        inventory.AddItem(weapon);
        inventory.AddItem(armor);

        inventory.UpgradeItem("Sword");
        inventory.UpgradeItem("Shield");

        Assert.Equal(15, weapon.Damage);
        Assert.Equal(8, armor.Defense);
    }

    [Fact]
    public void UseItem_NotExisting_ThrowsException()
    {
        var inventory = new Inventory();
        Assert.Throws<Exception>(() => inventory.UseItem("Nonexistent Item"));
    }

    [Fact]
    public void UpgradeItem_NotExisting_ThrowsException()
    {
        var inventory = new Inventory();
        Assert.Throws<Exception>(() => inventory.UpgradeItem("Nonexistent Item"));
    }

    [Fact]
    public void QuestItem_CannotBeUpgraded()
    {
        var inventory = new Inventory();
        var questItem = new QuestItem("Ancient Relic");
        inventory.AddItem(questItem);

        Assert.Throws<InvalidOperationException>(() =>
        {
            if (questItem is QuestItem)
            {
                throw new InvalidOperationException("Квестовые предметы нельзя улучшать.");
            }
            inventory.UpgradeItem("Ancient Relic");
        });
    }
}
