namespace MiniBank.Models.Interfaces;


interface ITransactable
{
    public void Deposit(decimal amount);
    public bool Withdraw(decimal amount, out string? error);
}