Requirements and status for MiniBank Console

- Create accounts (Checking, Savings, Loan) — implemented
  Short notice: Account types can be created, owner name required, initial deposit/loan amount supported and account IDs assigned.

- List accounts — implemented
  Short notice: Console listing shows ID, owner, type and balance.

- Deposit funds — implemented
  Short notice: Deposits accept non-negative amounts and add transactions; negative input is rejected.

- Withdraw funds with overdraft rules — Partial Implemented
  Short notice: Checking supports an overdraft limit; other accounts prevent negative balance. Error reporting/user feedback is basic and could be improved.

- View account statement — implemented
  Short notice: Transaction history is recorded and printed per account.

- Apply monthly interest (month-end processing) — implemented
  Short notice: Savings and Loan implement IInterestBearing and Run month-end applies interest.

- Input validation (owner name, amounts) — implemented
  Short notice: Owner name cannot be blank; numeric amount parsing and non-negative checks exist, but validation messages and flows are minimal.

- Persist data (save/load accounts) — Not implemented
  Short notice: No persistence layer; accounts exist only in memory for the process lifetime.

- Concurrency, transactions, and durability — Not implemented
  Short notice: Single-threaded in-memory implementation without transaction or concurrency control.

- Unit tests / automated tests — Not implemented
  Short notice: No test project or automated tests present in the repository.

Notes:
- Status values used: implemented, Not implemented, Partial Implemented.
- File created by automated workspace scan; update statuses if additional non-source requirements exist.
