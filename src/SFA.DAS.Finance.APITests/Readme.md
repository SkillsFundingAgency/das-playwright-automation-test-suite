# Employers API

This document lists the Employers API endpoints used by the test-suite. Endpoints are grouped into INNER (internal) and OUTER (public/consumer) endpoints for clarity.

---

## INNER endpoints

| # | Category | Method | Endpoint |
|---:|---|:---:|---|
| 1 | TransferConnections | GET | /api/accounts/internal/{accountId}/transfers/connections |
| 2 | TransferConnections | GET | /api/accounts/{hashedAccountId}/transfers/connections |
| 3 | EmployerAccount | POST | /api/accounts/balances |
| 4 | EmployerAccount | GET | /api/accounts/{accountId}/transferAllowanceByAccId |
| 5 | EmployerAccount | GET | /api/accounts/{hashedAccountId}/transferAllowance |
| 6 | Statistics | GET | /GetFinanceStatistics |
| 7 | AccountTransactions | GET | /api/accounts/{hashedAccountId}/transactions |
| 8 | AccountTransactions | GET | /api/accounts/{hashedAccountId}/transactions/Yr/Month |
| 9 | AccountTransactions | GET | /api/accounts/{hashedAccountId}/transactions/query |
| 10 | AccountTransactions | GET | /api/accounts/{hashedAccountId}/transactions/GetTransactions |
| 11 | FinanceLevy | GET | /api/accounts/{hashedAccountId}/levy |
| 12 | FinanceLevy | GET | /api/accounts/{hashedAccountId}/levy/GetLevyForPeriod |
| 13 | FinanceLevy | GET | /api/accounts/{hashedAccountId}/levy/english-fraction-history |
| 14 | FinanceLevy | GET | /api/accounts/{hashedAccountId}/levy/english-fraction-current |
| 15 | User | PUT | /api/user/upsert |
| 16 | PeriodEnd | GET | /api/perios-ends |
| 17 | PeriodEnd | POST | /api/perios-ends |
| 18 | PeriodEnd | GET | /api/period-end/{periodEnd} |
| 19 | SessionKeepAlive | GET | /service/keepalive |
| 20 | ping | GET | /ping |

---

## OUTER endpoints

| # | Method | Endpoint |
|---:|:---:|---|
| 1 | GET | /Accounts/{accountId}/minimum-signed-agreement-version |
| 2 | GET | /Accounts/{accountId}/users/which-receive-notifications |
| 3 | GET | /AccountUsers/{userId}/accounts |
| 4 | GET | /Pledges?accountId={accountId} |
| 5 | GET | /Providers/{id} |
| 6 | GET | /TrainingCourses/frameworks |
| 7 | GET | /TrainingCourses/standards |
| 8 | GET | /Transfers/{accountId}/counts |
| 9 | GET | /Transfers/{accountId}/financial-breakdown |
| 10 | GET | /ping |

---

If you'd like, I can:

- Add a `Scope` column to the OUTER table (e.g., `OUTER`) to match formatting with INNER rows.
- Validate or correct endpoint typos (for example `perios-ends` might be `periods-ends`) — I won't change endpoints without your confirmation.
- Commit these changes to Git with a descriptive message.
