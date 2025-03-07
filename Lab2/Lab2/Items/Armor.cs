using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Armor : Item
{
    public int Defense { get; set; }

    public Armor(string name, int defense)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Defense = defense;
    }

    public override void Use()
    {
        Defense += 1;
    }

    public override void Upgrade()
    {
        Defense += 3;
    }
}