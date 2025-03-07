using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class QuestItem : Item
{
    public bool IsUsed { get; private set; }

    public QuestItem(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        IsUsed = false;
    }

    public override void Use()
    {
        if (IsUsed)
            throw new InvalidOperationException($"Квестовый предмет {Name} уже использован.");

        IsUsed = true;
    }

    public override void Upgrade()
    {
        throw new InvalidOperationException("Квестовые предметы нельзя улучшать.");
    }
}