using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Weapon : Item
{
    public int Damage { get; set; }

    public Weapon(string name, int damage)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Damage = damage;
    }

    public override void Use()
    {
        Damage -= 1;
    }

    public override void Upgrade()
    {
        Damage += 5;
    }
}