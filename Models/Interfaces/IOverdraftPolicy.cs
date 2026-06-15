namespace MiniBank.Models.Interfaces;

interface IOverdraftPolicy
{
    public decimal OverdraftLimit { get; }
}
