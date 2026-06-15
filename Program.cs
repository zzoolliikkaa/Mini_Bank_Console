using MiniBank.Models.Interfaces;
using MiniBankConsole.Models;
using MiniBankConsole.Services;
using System.Transactions;

Menu MainMenu = new Menu();
Menu AccountMenu = new Menu();
List<BankAccount> AccountList = new List<BankAccount>();
int mainChoice = 0;

MainMenu.AddCaption("MiniBank Console");
MainMenu.AddOption("List accounts");
MainMenu.AddOption("Create account");
MainMenu.AddOption("Deposit");
MainMenu.AddOption("Withdraw");
MainMenu.AddOption("View statement");
MainMenu.AddOption("Run month-end");
MainMenu.AddOption("Exit");

AccountMenu.AddCaption("ADD ACCOUNT");
AccountMenu.AddOption("Checking Account");
AccountMenu.AddOption("Savings Account");
AccountMenu.AddOption("Loan Account");
AccountMenu.AddOption("Back to main menu");

while (MainMenuOptions.Exit != (MainMenuOptions)mainChoice)
{
    MainMenu.DisplayMenu();
    mainChoice = MainMenu.ReadChoice();
    MainMenuOptions MainMenuSelected = (MainMenuOptions)mainChoice;
    switch (MainMenuSelected)
    {
        case MainMenuOptions.ListAccounts:
            ListAccount();
            break;

        case MainMenuOptions.CreateAccount:
            CreateAccount();
            break;

        case MainMenuOptions.Deposit:
            Deposit();
            break;

        case MainMenuOptions.Withdraw:
            Withdraw();
            break;

        case MainMenuOptions.ViewStatement:
            ViewStatement();
            break;

        case MainMenuOptions.RunMonthEnd:
            RunMonthEnd();
            break;

        case MainMenuOptions.Exit:
            Console.WriteLine("Exiting...");
            break;

        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }
}

void RunMonthEnd()
{
    if (!EmptyAccountList())
    {
        foreach (var account in AccountList)
        {
            if (account is IInterestBearing interestBearing)
            {
                interestBearing.ApplyMonthlyInterest();
            }
        }
    }
}

void ViewStatement()
{
    if (!EmptyAccountList())
    {
        Console.Write("Enter the account number:");
        int accountId = int.TryParse(Console.ReadLine(), out int result) ? result : 0;
        var account = AccountList.FirstOrDefault(d => d.Id == accountId);
        if (account != null)
        {
            Console.WriteLine("Account statement:");
            account.PrintStatement();
        }
        else
        {
            Console.WriteLine("Account not found.");
            Console.ReadLine();
        }
    }
}

void Withdraw()
{
    if (!EmptyAccountList())
    {
        Console.Write("Enter the account number:");
        int accountId = int.TryParse(Console.ReadLine(), out int result) ? result : 0;
        var account = AccountList.FirstOrDefault(d => d.Id == accountId);
        if (account != null)
        {
            Console.Write("Enter the amount:");
            string error = String.Empty;
            if (account.Withdraw(ReadAmount(), out error) == false)
            {
                Console.WriteLine(error);
                Console.ReadLine();
            }
        }
        else
        {
            Console.WriteLine("Account not found.");
            Console.ReadLine();
        }
    }
}
void Deposit()
{
    if (!EmptyAccountList())
    {
        Console.Write("Enter the account number:");
        int accountId = int.TryParse(Console.ReadLine(), out int result) ? result : 0;
        var account = AccountList.FirstOrDefault(d => d.Id == accountId);
        if (account != null)
        {
            Console.Write("Enter the amount:");
            account.Deposit(ReadAmount());
        }
        else
        {
            Console.WriteLine("Account not found.");
            Console.ReadLine();
        }
    }
}

void CreateAccount()
{
    //Create account (choose type + owner name + initial deposit or loan amount).
    int deviceChoice = 0;
    while (AccountMenuOptions.BackToMainMenu != (AccountMenuOptions)deviceChoice)
    {
        AccountMenu.DisplayMenu();
        deviceChoice = AccountMenu.ReadChoice();
        AccountMenuOptions DeviceMenuSelected = (AccountMenuOptions)deviceChoice;
        switch (DeviceMenuSelected)
        {
            case AccountMenuOptions.CheckingAccount:
                AddNewAccount(new CheckingAccount());
                break;

            case AccountMenuOptions.SavingsAccount:
                AddNewAccount(new SavingsAccount());
                break;

            case AccountMenuOptions.LoanAccount:
                AddNewAccount(new LoanAccount());
                break;

            case AccountMenuOptions.BackToMainMenu:
                Console.WriteLine("Returning to main menu...");
                break;

            default:
                Console.WriteLine("Invalid choice. Please try again.");
                Console.ReadLine();
                break;
        }
    }

}
void AddNewAccount(BankAccount account)
{
    account.Id = AccountList.Count + 1;
    account.Owner = ReadOwnerName();
    switch (account.Type)
    {
        case AccountType.Checking:
            Console.Write("Enter the initial deposit for checking account: ");
            break;
        case AccountType.Savings:
            Console.Write("Enter the initial deposit for savings account: ");
            break;
        case AccountType.Loan:
            Console.Write("Enter the initial loan amount: ");
            break;
    }
    var amount = ReadAmount();
    if (account.Type == AccountType.Loan)
    {
        account.Balance = amount * (-1);
        account.Transactions.Add($"Loan: {account.Balance}");
    }
    else
    {
        account.Deposit(amount);
    }

    AccountList.Add(account);
}

decimal ReadAmount()
{
    decimal amount = 0m;
    while (true)
    {
        if (decimal.TryParse(Console.ReadLine(), out amount) && (amount >= 0))
            return amount;
        Console.Write("Invalid amount. Please enter a number:");
    }
}

string ReadOwnerName()
{
    string? ownerName;

    do
    {
        Console.Write("Enter the account owner's name: ");
        ownerName = Console.ReadLine();
    }
    while (string.IsNullOrWhiteSpace(ownerName));

    return ownerName;
}
void ListAccount()
{

    if (!EmptyAccountList())
    {
        Console.WriteLine("Listing accounts...");
        foreach (var account in AccountList)
        {
            Console.WriteLine($"ID: {account.Id}, Owner: {account.Owner}, Type: {account.Type}, Balance: {account.Balance}");
        }
        Console.ReadLine();
    }
}
bool EmptyAccountList()
{
    if (AccountList.Count == 0)
    {
        Console.WriteLine("No accounts found.");
        Console.ReadLine();
        return true;
    }
    else
    {
        return false;
    }
}

