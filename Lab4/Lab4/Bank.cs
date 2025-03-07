using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class Bank
{
    private const decimal bankCommission = 0.05m;
    public decimal BankCommission => bankCommission;
    private decimal banckMoney = 0;
    
    private readonly Dictionary<string, Client> _clients = new();
    private readonly Dictionary<Currency, decimal> _exchangeRates = new()
        {
            { Currency.RUB, 1m },
            { Currency.USD, 0m },
            { Currency.CNY, 0m }
        };

    public async Task UpdateExchangeRates()
    {
        try
        {
            using var client = new HttpClient();
            string url = "https://www.cbr-xml-daily.ru/daily_json.js";
            string json = await client.GetStringAsync(url);
            var data = JObject.Parse(json);

            _exchangeRates[Currency.USD] = data["Valute"]?["USD"]?["Value"]?.Value<decimal>() ?? 0m;
            _exchangeRates[Currency.CNY] = data["Valute"]?["CNY"]?["Value"]?.Value<decimal>() ?? 0m;
        }
        catch (Exception)
        {
            throw new InvalidOperationException("Failed to update exchange rates.");
        }
    }

    public Decimal GetExchangeRate(Currency currency)
    {
        return _exchangeRates[currency];
    }

    public void AddClient(Client client)
    {
        if (!_clients.ContainsKey(client.Name))
        {
            _clients[client.Name] = client;
        }
    }

    public void Transfer(string fromAccount, string toAccount, decimal amount)
    {
        var sender = FindAccount(fromAccount);
        var receiver = FindAccount(toAccount);

        if (sender == null || receiver == null)
            throw new InvalidOperationException("One or both accounts not found.");

        sender.Withdraw(amount);
        
        if (sender.Currency != receiver.Currency)
        {
            amount = ConvertCurrency(amount, sender.Currency, receiver.Currency);
        }

        banckMoney = amount * bankCommission;
        amount *= (1.0m - bankCommission);
        receiver.Deposit(amount);
    }

    private Account FindAccount(string accountNumber)
    {
        return _clients.Values.SelectMany(client => client.Accounts)
                              .FirstOrDefault(account => account.AccountNumber == accountNumber);
    }

    private decimal ConvertCurrency(decimal amount, Currency from, Currency to)
    {
        if (!_exchangeRates.ContainsKey(from) || !_exchangeRates.ContainsKey(to))
            throw new InvalidOperationException("Exchange rate not available.");

        decimal amountInRubles = amount * _exchangeRates[from];
        return amountInRubles / _exchangeRates[to];
    }

    public string GenerateReport()
    {
        var report = "Bank Report:\n";
        foreach (var client in _clients.Values)
        {
            report += $"Client: {client.Name}\n";
            foreach (var account in client.Accounts)
            {
                report += $"  Account {account.AccountNumber}: {account.Balance} {account.Currency}\n";
                report += "  Transactions:\n" + string.Join("\n", account.TransactionHistory) + "\n";
            }
        }
        return report;
    }
}


