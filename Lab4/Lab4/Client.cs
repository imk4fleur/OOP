using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Client
{
    public string Name { get; }
    public List<Account> Accounts { get; } = new();

    public Client(string name)
    {
        Name = name;
    }

    public void AddAccount(Account account)
    {
        Accounts.Add(account);
    }
}