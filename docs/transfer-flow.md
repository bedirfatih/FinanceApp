# Transfer Flow

End-to-end flow of a transfer request from the frontend to the database.

## Flowchart

```mermaid
flowchart TD
    A([User fills out Transfer Form]) --> B[Selects FromUser, ToUser, Amount]
    B --> C[Clicks 'Send Transfer']
    C --> D[POST /transfers\nfrontend → axios]

    D --> E[TransfersController.Create]
    E --> F[TransfersService.CreateAsync]

    F --> G[(Load FromUser from DB)]
    F --> H[(Load ToUser from DB)]

    G & H --> I{Both users exist?}

    I -- No --> J[Status = Failed\nReason: user not found]

    I -- Yes --> K{FromUser.Balance\n>= Amount?}

    K -- No --> L[Status = Failed\nReason: insufficient balance]

    K -- Yes --> M[FromUser.Balance -= Amount]
    M --> N[ToUser.Balance += Amount]
    N --> O[Status = Completed]

    J --> P[(Save Transfer record to DB)]
    L --> P
    O --> P

    P --> Q[Return Transfer object\nHTTP 201 Created]

    Q --> R{Status?}

    R -- Completed --> S[UI shows\nCompleted in green]
    R -- Failed --> T[UI shows\nFailed in red]
```

## State Transitions

```mermaid
stateDiagram-v2
    [*] --> Pending : Transfer created
    Pending --> Completed : Users valid & balance sufficient
    Pending --> Failed : User not found OR insufficient balance
    Completed --> [*]
    Failed --> [*]
```

## Summary

| Condition | Outcome |
|-----------|---------|
| `FromUser` or `ToUser` not found | `Failed` — no balance changes |
| `FromUser.Balance < Amount` | `Failed` — no balance changes |
| All checks pass | `Completed` — balances updated atomically |
