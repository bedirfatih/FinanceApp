# Database Schema

Database: `FinanceApp` on SQL Server Express

## ER Diagram

```mermaid
erDiagram
    USERS {
        int         Id          PK  "auto increment"
        nvarchar    Name
        nvarchar    Email           "unique"
        nvarchar    PasswordHash
        decimal     Balance         "precision 18,2"
        datetime2   CreatedAt
    }

    TRANSACTIONS {
        int         Id          PK  "auto increment"
        int         UserId      FK
        decimal     Amount          "precision 18,2"
        nvarchar    Category
        nvarchar    Description
        datetime2   Date
    }

    TRANSFERS {
        int         Id          PK  "auto increment"
        int         FromUserId  FK
        int         ToUserId    FK
        decimal     Amount          "precision 18,2"
        nvarchar    Status          "Pending | Completed | Failed"
        datetime2   CreatedAt
    }

    USERS ||--o{ TRANSACTIONS : "owns"
    USERS ||--o{ TRANSFERS    : "sends"
    USERS ||--o{ TRANSFERS    : "receives"
```

## Notes

- `USERS.Email` has a unique index enforced at the database level.
- `TRANSACTIONS.UserId` uses `ON DELETE CASCADE` — deleting a user removes their transactions.
- `TRANSFERS.FromUserId` and `TRANSFERS.ToUserId` use `ON DELETE NO ACTION` to prevent cascade conflicts when both FKs point to the same table.
- All `decimal` columns are `decimal(18, 2)` — suitable for currency values.
