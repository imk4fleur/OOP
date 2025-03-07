using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class NormalState : IItemState
{
    public void Use(Item item)
    {
        item.Use();
    }
}
