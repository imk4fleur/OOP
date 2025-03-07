using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class BankTests
{
    [Fact]
    public void Account_Deposit_IncreasesBalance()
    {
        var account = new Account("123", AccountType.Checking, Currency.USD, 100);
        account.Deposit(50);
        Assert.Equal(150, account.Balance);
    }

    [Fact]
    public void Account_Withdraw_DecreasesBalance()
    {
        var account = new Account("123", AccountType.Checking, Currency.USD, 100);
        account.Withdraw(40);
        Assert.Equal(60, account.Balance);
    }

    [Fact]
    public void Account_Withdraw_ThrowsException_WhenInsufficientFunds()
    {
        var account = new Account("123", AccountType.Checking, Currency.USD, 30);
        Assert.Throws<InvalidOperationException>(() => account.Withdraw(50));
    }

    [Fact]
    public void Account_ApplyInterest_OnlyForDepositAccounts()
    {
        var depositAccount = new Account("123", AccountType.Deposit, Currency.USD, 100);
        depositAccount.ApplyInterest(0.05m);
        Assert.Equal(105, depositAccount.Balance);

        var checkingAccount = new Account("456", AccountType.Checking, Currency.USD, 100);
        checkingAccount.ApplyInterest(0.05m);
        Assert.Equal(100, checkingAccount.Balance); 
    }

    [Fact]
    public void Client_CanAddAccounts()
    {
        var client = new Client("John Doe");
        var account = new Account("123", AccountType.Checking, Currency.USD, 200);
        client.AddAccount(account);

        Assert.Contains(account, client.Accounts);
    }

    [Fact]
    public void Bank_AddClient_Successfully()
    {
        var bank = new Bank();
        var client = new Client("John Doe");
        bank.AddClient(client);

        var report = bank.GenerateReport();
        Assert.Contains("John Doe", report);
    }

    [Fact]
    public void Bank_Transfer_SameCurrency_Success()
    {
        var bank = new Bank();
        var client = new Client("Alice");
        var account1 = new Account("A1", AccountType.Checking, Currency.USD, 100);
        var account2 = new Account("A2", AccountType.Checking, Currency.USD, 50);
        client.AddAccount(account1);
        client.AddAccount(account2);
        bank.AddClient(client);

        bank.Transfer("A1", "A2", 30);
        Assert.Equal(70, account1.Balance);
        Assert.Equal(50.0m + 30.0m * (1.0m - bank.BankCommission), account2.Balance);
    }

    [Fact]
    public async void Bank_Transfer_DifferentCurrencies_UsesConversion()
    {
        var bank = new Bank();
        Client client = new Client("Alice");
        bank.AddClient(client);

        var account1 = new Account("A1", AccountType.Checking, Currency.USD, 10000);
        var account2 = new Account("A2", AccountType.Checking, Currency.RUB, 0);
        client.AddAccount(account1);
        client.AddAccount(account2);

        await bank.UpdateExchangeRates();

        decimal a1Balance = 9000;
        decimal a2Balance = 1000.0m * bank.GetExchangeRate(Currency.USD) / bank.GetExchangeRate(Currency.RUB) * (1.0m - bank.BankCommission);

        bank.Transfer("A1", "A2", 1000);
        Assert.Equal(a1Balance, account1.Balance);
        Assert.Equal(a2Balance, account2.Balance); 
    }

    [Fact]
    public async Task Bank_UpdateExchangeRates_SetsNewRates()
    {
        var bank = new Bank();
        await bank.UpdateExchangeRates();

        Assert.True(bank.GetExchangeRate(Currency.USD) > 0);
        Assert.True(bank.GetExchangeRate(Currency.CNY) > 0);
    }
}
