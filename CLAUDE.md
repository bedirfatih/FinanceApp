# Proje: Finansal Yönetim Uygulaması

## Genel Açıklama
Bu proje bir full-stack finansal yönetim uygulamasıdır.
Kullanıcılar hesap yönetimi, masraf kaydı ve para transferi yapabilir.
Bu bir case study projesidir, sade ve anlaşılır kod önceliklidir.

## Teknoloji Stack
- Backend: C# / .NET 9 Web API
- Frontend: React (Vite)
- ORM: Entity Framework Core (Code First)
- Veritabanı: SQL Server Express

## Veritabanı Bağlantısı
appsettings.json içine eklenecek connection string:
Server=DESKTOP-SIBQAMC\SQLEXPRESS;Database=FinanceApp;Trusted_Connection=True;TrustServerCertificate=True;

## Klasör Yapısı
/FinanceApp
  /backend    → .NET 9 Web API
  /frontend   → React (Vite)
  /docs       → Sistem tasarım dökümanları
  CLAUDE.md

## Veritabanı Modelleri

### Users
- Id (int, PK, auto increment)
- Name (string)
- Email (string, unique)
- PasswordHash (string)
- Balance (decimal)
- CreatedAt (DateTime)

### Transactions
- Id (int, PK, auto increment)
- UserId (int, FK → Users)
- Amount (decimal)
- Category (string)
- Description (string)
- Date (DateTime)

### Transfers
- Id (int, PK, auto increment)
- FromUserId (int, FK → Users)
- ToUserId (int, FK → Users)
- Amount (decimal)
- Status (string) → "Pending", "Completed", "Failed"
- CreatedAt (DateTime)

## Mimari Kararlar
- Katmanlı yapı kullan: Controller → Service → DbContext
- Repository Pattern kullanma, EF Core zaten yeterli
- Her endpoint RESTful olmalı (URL'de fiil değil isim kullan)
- CORS ayarları frontend için açık olmalı (localhost:5173)

## CRUD Gereksinimleri

### Users
- POST   /users          → kullanıcı oluştur
- GET    /users          → tüm kullanıcıları listele
- GET    /users/{id}     → tekil kullanıcı getir
- PUT    /users/{id}     → kullanıcı güncelle
- DELETE /users/{id}     → kullanıcı sil

### Transactions
- POST   /transactions          → masraf kaydı oluştur
- GET    /transactions          → tüm masrafları listele
- GET    /transactions/{id}     → tekil masraf getir
- GET    /transactions/user/{userId} → kullanıcıya göre filtrele
- PUT    /transactions/{id}     → masraf güncelle
- DELETE /transactions/{id}     → masraf sil

### Transfers
- POST   /transfers         → transfer oluştur
- GET    /transfers         → tüm transferleri listele
- GET    /transfers/{id}    → tekil transfer getir

## Mock Servisler
Gerçek dış servis olmadığı için bunları mock olarak yaz:

### ExchangeRateService
- GET /mock/exchange-rates
- EUR, USD, TRY kurlarını sabit değerlerle dönsün

### BankTransferService
- POST /mock/bank-transfer
- Dış bankaya transfer simüle etsin, rastgele "Success" veya "Failed" dönsün

## EF Core Komutları
Migration oluştur:    dotnet ef migrations add <MigrationAdi>
Veritabanına uygula:  dotnet ef database update
Çalıştırma dizini:    /backend klasörü içinde çalıştır

## Çalıştırma Komutları
Backend:  cd backend → dotnet run
Frontend: cd frontend → npm run dev

## Git
Her tamamlanan adım sonrası commit at.
Commit mesaj formatı:
- feat: yeni özellik
- fix: hata düzeltme
- docs: dokümantasyon
