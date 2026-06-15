namespace MiniBankConsole.Services;

enum MainMenuOptions
{
    ListAccounts = 1,
    CreateAccount,
    Deposit,
    Withdraw,
    ViewStatement,
    RunMonthEnd,
    Exit
}

enum AccountMenuOptions
{
    CheckingAccount = 1,
    SavingsAccount,
    LoanAccount,
    BackToMainMenu
}
public enum AccountType
{
    Checking,
    Savings,
    Loan
}