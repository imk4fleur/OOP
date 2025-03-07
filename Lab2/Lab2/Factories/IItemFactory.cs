using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IItemFactory
{
    Item CreateItem(string name, int stat);
}
