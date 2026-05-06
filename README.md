# FinanceApp — Finansal Yönetim Uygulaması

Kullanıcıların hesap bakiyelerini yönetebildiği, masraf kaydı oluşturabildiği ve birbirlerine para transferi yapabildiği full-stack bir finansal yönetim uygulamasıdır. Case study amaçlı geliştirilmiştir.

---

## Teknoloji Stack

| Katman | Teknoloji |
|--------|-----------|
| Backend | C# / .NET 9 Web API |
| ORM | Entity Framework Core 9 (Code First) |
| Veritabanı | SQL Server Express |
| Frontend | React 19 + Vite |
| HTTP İstemcisi | Axios |
| Routing | React Router DOM |

---

## Gereksinimler

Projeyi çalıştırabilmek için aşağıdakilerin kurulu olması gerekir:

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Node.js 18+](https://nodejs.org/)
- [SQL Server Express](https://www.microsoft.com/tr-tr/sql-server/sql-server-downloads)
- [dotnet-ef CLI aracı](https://learn.microsoft.com/tr-tr/ef/core/cli/dotnet)

```bash
# dotnet-ef global aracını kur
dotnet tool install --global dotnet-ef
```

---

## Kurulum

### 1. Depoyu klonla

```bash
git clone <repo-url>
cd FinanceApp
```

### 2. Veritabanını oluştur

```bash
cd backend
dotnet ef database update
```

> `appsettings.json` içindeki bağlantı dizesi `DESKTOP-SIBQAMC\SQLEXPRESS` örneğine işaret etmektedir. Farklı bir SQL Server örneği kullanıyorsanız `ConnectionStrings.DefaultConnection` değerini güncelleyin.

---

## Çalıştırma

### Backend

```bash
cd backend
dotnet run
```

Uygulama `http://localhost:5117` adresinde başlar.

### Frontend

```bash
cd frontend
npm install
npm run dev
```

Uygulama `http://localhost:5173` adresinde başlar.

---

## API Endpoints

Base URL: `http://localhost:5117`

### Kullanıcılar (`/users`)

| Metot | Endpoint | Açıklama |
|-------|----------|----------|
| `GET` | `/users` | Tüm kullanıcıları listele |
| `GET` | `/users/{id}` | Tekil kullanıcı getir |
| `POST` | `/users` | Yeni kullanıcı oluştur |
| `PUT` | `/users/{id}` | Kullanıcı güncelle |
| `DELETE` | `/users/{id}` | Kullanıcı sil |

### Masraflar (`/transactions`)

| Metot | Endpoint | Açıklama |
|-------|----------|----------|
| `GET` | `/transactions` | Tüm masrafları listele |
| `GET` | `/transactions/{id}` | Tekil masraf getir |
| `GET` | `/transactions/user/{userId}` | Kullanıcıya göre filtrele |
| `POST` | `/transactions` | Yeni masraf kaydı oluştur |
| `PUT` | `/transactions/{id}` | Masraf güncelle |
| `DELETE` | `/transactions/{id}` | Masraf sil |

### Transferler (`/transfers`)

| Metot | Endpoint | Açıklama |
|-------|----------|----------|
| `GET` | `/transfers` | Tüm transferleri listele |
| `GET` | `/transfers/{id}` | Tekil transfer getir |
| `POST` | `/transfers` | Yeni transfer oluştur |

> Transfer oluşturulurken bakiye kontrolü yapılır. Yetersiz bakiye veya geçersiz kullanıcı durumunda transfer kaydı `Failed` statüsüyle oluşturulur, bakiyeler değişmez.

### Mock Servisler (`/mock`)

| Metot | Endpoint | Açıklama |
|-------|----------|----------|
| `GET` | `/mock/exchange-rates` | EUR, USD, TRY sabit kurlarını döner |
| `POST` | `/mock/bank-transfer` | Dış banka transferini simüle eder |

---

## Klasör Yapısı

```
FinanceApp/
├── backend/                  → .NET 9 Web API
│   ├── Controllers/          → HTTP katmanı
│   ├── Services/             → İş mantığı
│   ├── Models/               → EF Core entity'leri
│   ├── DTOs/                 → İstek/yanıt modelleri
│   ├── Data/                 → AppDbContext
│   └── Migrations/           → EF Core migration'ları
├── frontend/                 → React + Vite
│   └── src/
│       ├── pages/            → Dashboard, TransactionList, TransferForm
│       └── api.js            → Axios yapılandırması
├── docs/                     → Sistem tasarım dökümanları
└── README.md
```

---

## Sistem Tasarımı

Ayrıntılı diyagramlar için [`/docs`](./docs) klasörüne bakın:

| Döküman | İçerik |
|---------|--------|
| [`docs/api-endpoints.md`](./docs/api-endpoints.md) | Tüm REST endpoint'lerinin mindmap diyagramı |
| [`docs/database-schema.md`](./docs/database-schema.md) | Veritabanı ER diyagramı ve tablo açıklamaları |
| [`docs/transfer-flow.md`](./docs/transfer-flow.md) | Transfer akışı flowchart ve durum diyagramı |

---

## Mimari

```
HTTP İsteği
    │
    ▼
Controller        → İstek doğrulama, HTTP yanıtı
    │
    ▼
Service           → İş mantığı, bakiye kontrolü
    │
    ▼
AppDbContext      → Entity Framework Core
    │
    ▼
SQL Server Express
```

- Repository Pattern kullanılmamıştır; EF Core doğrudan servis katmanından kullanılmaktadır.
- CORS yalnızca `http://localhost:5173` için açıktır.
- Tüm `decimal` alanlar `decimal(18, 2)` hassasiyetiyle tanımlanmıştır.
