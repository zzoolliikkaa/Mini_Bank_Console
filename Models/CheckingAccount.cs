using MiniBank.Models.Interfaces;
using MiniBankConsole.Services;

public class CheckingAccount : BankAccount, IOverdraftPolicy
{
    public override AccountType Type => AccountType.Checking;

    public decimal OverdraftLimit { get; set; } = 200m;

    public override bool Withdraw(decimal amount, out string? error)
    {
        if (Balance - amount < -OverdraftLimit)
        {
            error = "Credit limit reached!";
            return false;
        }
        else
        {
            Balance -= amount;
            error = "No error";
            Transactions.Add($"Withdraw: {amount}");
            return true;
        }
    }
}