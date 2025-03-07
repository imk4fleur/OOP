using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2;

public abstract class Item
{
    public string Name { get; protected set; }
    protected IItemState State = new NormalState();
    public abstract void Use();
    public abstract void Upgrade();
}