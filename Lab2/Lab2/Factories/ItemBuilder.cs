using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ItemBuilder<T> where T : Item
{
    private string _name;
    private int _stat;

    public ItemBuilder<T> SetName(string name)
    {
        _name = name ?? throw new ArgumentNullException(nameof(name));
        return this;
    }

    public ItemBuilder<T> SetStat(int stat)
    {
        _stat = stat;
        return this;
    }

    public T Build()
    {
        if (typeof(T) == typeof(Weapon)) return new Weapon(_name, _stat) as T;
        if (typeof(T) == typeof(Armor)) return new Armor(_name, _stat) as T;
        if (typeof(T) == typeof(Potion)) return new Potion(_name, _stat) as T;
        if (typeof(T) == typeof(QuestItem)) return new QuestItem(_name) as T;
        throw new Exception("Неизвестный тип предмета");
    }
}
