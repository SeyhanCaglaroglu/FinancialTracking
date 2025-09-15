# 📊 FinancialTracking  

## 🚀 Proje Hakkında  
**FinancialTracking**, kullanıcıların finansal durumlarını takip edebilmeleri için geliştirilmiş bir projedir.  
Clean Architecture yaklaşımıyla tasarlanan bu sistem, modüler yapısı sayesinde kolay genişletilebilir, bakımı yapılabilir ve test edilebilir bir mimariye sahiptir.  

Uygulama içerisinde;  
- 💰 **Gelir ve gider işlemleri**  
- 🔁 **Sürekli (tekrarlayan) gelir/gider işlemleri**  
- 🏷️ **Kategorilendirme sistemi**  
- 🎯 **Hedef oluşturma**  
- 📉 **Bütçe takibi**  

gibi finansal yönetimi kolaylaştıracak özellikler bulunmaktadır.  

---  

### 📂 Core Katmanı  

#### 🔹 Domain  
- Projenin **kalbini** oluşturur.  
- İçerisinde:  
  - **Entity** sınıfları  
  - **ValueObject** yapıları  
bulunmaktadır.  

#### 🔹 Application  
- İş mantığının merkezi katmanıdır.  
- İçerisinde:  
  - **Infrastructure katmanı (Persistence, Auth, Caching)** için **Contract** tanımları  
  - **Entity’ler için DTO** sınıfları  
  - **Repository interface**’leri 
  - **Entity`lerin Service class(bussines code)**’ları 
  - **FluentValidation** kuralları  
  - **AutoMapper** profilleri  
bulunmaktadır.  

---

### 📂 Infrastructure Katmanı  

#### 🔹 Persistence  
- Veritabanı işlemlerinin yürütüldüğü katmandır.  
- İçerisinde:  
  - **DbContext**  
  - **Repository class**’ları (Entity bazlı)  
  - **Veritabanı configuration ayarları**  
  - **Interceptors**  
  - **Migrations**  
  - **UnitOfWork** implementasyonu  
bulunmaktadır.  

#### 🔹 Auth  
- Kimlik doğrulama ve yetkilendirme işlemlerinin yönetildiği katmandır.  

#### 🔹 Caching  
- Önbellekleme (Redis veya benzeri) çözümlerinin uygulandığı katmandır.  

---

### 📂 API Katmanı  

- Projenin **dış dünyaya açılan yüzüdür**.  
- İçerisinde:  
  - **API Controller’ları** (işlevsel endpoint’ler)  
  - **Extensions** (servislerin projeye eklenmesi için uzantılar)  
  - **Global Exception Handling** yapılandırmaları  
bulunmaktadır.  

---

## 📂 Dosya Yapısı

```text
FinancialTracking/
│
├── FinancialTracking.API/
│   ├── Controllers/...
│   ├── ExceptionHandlers/...
│   ├── Extensions/...
│   ├── Filters/...
│   └── Program.cs
│
├── Core/
│   ├── FinancialTracking.Domain/
│   │   ├── Entities/...
│   │   ├── Enums/...
│   │   ├── ValueObjects/...
│   │   └── Configuration/...
│   │
│   └── FinancialTracking.Application/
│       ├── Contracts/...
│       │   ├── Caching/...
│       │   ├── Auth/...
│       │   └── Persistence/...
│       ├── Extensions/...
│       ├── Features/...
│       │   ├── Categories/...
│       │   ├── Budgets/...
│       │   ├── Goals/...
│       │   ├── Transactions/...
│       │   ├── RecurringTransactions/...
│       │   ├── Users/...
│       │   │   ├── Client/...
│       │   │   └── Dtos/...
│       │   └── RefreshTokens/...
│       └── ServiceResult.cs
│
└── Infrastructure/
    ├── FinancialTracking.Persistence/
    │   ├── Context/...
    │   ├── Extensions/...
    │   ├── Features/...
    │   │   ├── Categories/...
    │   │   ├── Budgets/...
    │   │   ├── Goals/...
    │   │   ├── Transactions/...
    │   │   ├── RecurringTransactions/...
    │   │   └── RefreshTokens/...
    │   ├── Interceptors/...
    │   ├── Migrations/...
    │   ├── GenericRepository.cs
    │   ├── PersistenceAssembly.cs
    │   └── UnitOfWork.cs
    │
    ├── FinancialTracking.Auth/
    │   ├── Extensions/...
    │   ├── Options/...
    │   └── Services/...
    │       ├── SignInService.cs
    │       ├── AuthenticationService.cs
    │       ├── TokenService.cs
    │       └── UserService.cs
    │
    └── FinancialTracking.Caching/...
```



