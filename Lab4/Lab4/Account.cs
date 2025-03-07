using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum AccountType { Checking, Deposit, Investment }
public enum Currency { RUB, CNY, USD }

public class Account
{
    public string AccountNumber { get; }
    public AccountType Type { get; }
    public Currency Currency { get; }
    public decimal Balance { get; private set; }
    public List<string> TransactionHistory { get; } = new();

    public Account(string accountNumber, AccountType type, Currency currency, decimal initialBalance = 0)
    {
        AccountNumber = accountNumber;
        Type = type;
        Currency = currency;
        Balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Deposit amount must be positive.");
        Balance += amount;
        TransactionHistory.Add($"Deposited {amount} {Currency}");
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Withdrawal amount must be positive.");
        if (amount > Balance) throw new InvalidOperationException("Insufficient funds.");
        Balance -= amount;
        TransactionHistory.Add($"Withdrew {amount} {Currency}");
    }

    public void ApplyInterest(decimal interestRate)
    {
        if (Type == AccountType.Deposit)
        {
            decimal interest = Balance * interestRate;
            Balance += interest;
            TransactionHistory.Add($"Interest applied: {interest} {Currency}");
        }
    }
}