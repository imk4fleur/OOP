using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Potion : Item
{
    public int HealingAmount { get; set; }

    public Potion(string name, int healingAmount)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        HealingAmount = healingAmount;
    }

    public override void Use()
    {
        HealingAmount = 0;
    }

    public override void Upgrade()
    {
        HealingAmount += 10;
    }
}