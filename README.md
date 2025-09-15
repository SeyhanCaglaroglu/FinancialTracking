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
├── App.API/
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── BudgetsController.cs
│   │   ├── CategoriesController.cs
│   │   ├── CustomBaseController.cs
│   │   ├── GoalsController.cs
│   │   ├── RecurringTransactionsController.cs
│   │   ├── TransactionsController.cs
│   │   └── UsersController.cs
│   │
│   ├── ExceptionHandlers/
│   │   └── GlobalExceptionHandler.cs
│   │
│   ├── Extensions/
│   │   ├── IdentityExtensions.cs
│   │   ├── ControllerExtensions.cs
│   │   ├── ExceptionHandlerExtensions.cs
│   │   ├── ServiceExtensions.cs
│   │   └── VersioningExtensions.cs
│   │
│   ├── Filters/
│   │   └── FluentValidationFilter.cs
│   │
│   └── Program.cs
│
├── Core/
│   ├── App.Domain/
│   │   ├── Entities/
│   │   │   ├── Category.cs
│   │   │   ├── Common/
│   │   │   |    └── BaseEntity.cs
│   │   │   |    ├── IAuditEntity.cs
│   │   │   ├── Goal.cs
│   │   │   ├── Budget.cs
│   │   │   ├── Transaction.cs
│   │   │   ├── RecurringTransaction.cs
│   │   │   ├── User.cs
│   │   │   └── UserRefreshToken.cs
│   │   │
│   │   ├── Enums/
│   │   │   └── TransactionType.cs
│   │   │
│   │   ├── ValueObjects/
│   │   │   └── Money.cs
│   │   │
│   │   └── Configuration/
│   │       ├── ConnectionStringOption.cs
│   │       └── Client.cs
│   │
│   └── App.Application/
│       ├── Contracts/
│       │   ├── Caching/
│       │   │   └── IRedisService.cs
│       │   │
│       │   ├── Auth/
│       │   │   ├── IAuthenticationService.cs
│       │   │   ├── ITokenService.cs
│       │   │   └── IUserService.cs
│       │   │
│       │   └── Persistence/
│       │       ├── IGenericRepository.cs
│       │       └── IUnitOfWork.cs
│       │
│       ├── Extensions/
│       │   └── ServiceExtensions.cs
│       │
│       ├── Features/
│       │   ├── Categories/
│       │   │   ├── CategoryMappingProfile.cs
│       │   │   ├── Create/
│       │   │   │   ├── CreateCategoryRequest.cs
│       │   │   │   ├── CreateCategoryRequestValidator.cs
│       │   │   │   └── CreateCategoryResponse.cs
│       │   │   ├── CommonDto/
│       │   │   │   └── CategoryDto.cs
│       │   │   ├── ICategoryRepository.cs
│       │   │   ├── Services/
│       │   │   │   ├── CategoryService.cs
│       │   │   │   └── ICategoryService.cs
│       │   │   └── Update/
│       │   │       ├── UpdateCategoryRequest.cs
│       │   │       └── UpdateCategoryRequestValidator.cs
│       │   │
│       │   ├── Budgets/
│       │   │   ├── BudgetMappingProfile.cs
│       │   │   ├── Create/
│       │   │   │   ├── CreateBudgetRequest.cs
│       │   │   │   ├── CreateBudgetRequestValidator.cs
│       │   │   │   └── CreateBudgetResponse.cs
│       │   │   ├── CommonDto/
│       │   │   │   └── BudgetDto.cs
│       │   │   ├── IBudgetRepository.cs
│       │   │   ├── Services/
│       │   │   │   ├── BudgetService.cs
│       │   │   │   └── IBudgetService.cs
│       │   │   └── Update/
│       │   │       ├── UpdateBudgetRequest.cs
│       │   │       └── UpdateBudgetRequestValidator.cs
│       │   │
│       │   ├── Goals/
│       │   │   ├── GoalMappingProfile.cs
│       │   │   ├── Create/
│       │   │   │   ├── CreateGoalRequest.cs
│       │   │   │   ├── CreateGoalRequestValidator.cs
│       │   │   │   └── CreateGoalResponse.cs
│       │   │   ├── CommonDto/
│       │   │   │   └── GoalDto.cs
│       │   │   ├── IGoalRepository.cs
│       │   │   ├── Services/
│       │   │   │   ├── GoalService.cs
│       │   │   │   └── IGoalService.cs
│       │   │   └── Update/
│       │   │       ├── UpdateGoalRequest.cs
│       │   │       └── UpdateGoalRequestValidator.cs
│       │   │
│       │   ├── Transactions/
│       │   │   ├── TransactionMappingProfile.cs
│       │   │   ├── Create/
│       │   │   │   ├── CreateTransactionRequest.cs
│       │   │   │   ├── CreateTransactionRequestValidator.cs
│       │   │   │   └── CreateTransactionResponse.cs
│       │   │   ├── CommonDto/
│       │   │   │   └── TransactionDto.cs
│       │   │   ├── ITransactionRepository.cs
│       │   │   ├── Services/
│       │   │   │   ├── TransactionService.cs
│       │   │   │   └── ITransactionService.cs
│       │   │   └── Update/
│       │   │       ├── UpdateTransactionRequest.cs
│       │   │       └── UpdateTransactionRequestValidator.cs
│       │   │
│       │   ├── RecurringTransactions/
│       │   │   ├── RecurringTransactionMappingProfile.cs
│       │   │   ├── Create/
│       │   │   │   ├── CreateRecurringTransactionRequest.cs
│       │   │   │   ├── CreateRecurringTransactionRequestValidator.cs
│       │   │   │   └── CreateRecurringTransactionResponse.cs
│       │   │   ├── CommonDto/
│       │   │   │   └── RecurringTransactionDto.cs
│       │   │   ├── IRecurringTransactionRepository.cs
│       │   │   ├── Services/
│       │   │   │   ├── RecurringTransactionService.cs
│       │   │   │   └── IRecurringTransactionService.cs
│       │   │   └── Update/
│       │   │       ├── UpdateRecurringTransactionRequest.cs
│       │   │       └── UpdateRecurringTransactionRequestValidator.cs
│       │   │
│       │   └── Users/
│       │       ├── Client/
│       │       │   ├── ClientLoginDto.cs
│       │       │   └── ClientTokenDto.cs
│       │       ├── Dtos/
│       │       │   ├── CreateUserDto.cs
│       │       │   ├── CreateUserRoleDto.cs
│       │       │   ├── LoginDto.cs
│       │       │   ├── NoDataDto.cs
│       │       │   ├── TokenDto.cs
│       │       │   └── UserDto.cs
│       │       └── UserMappingProfile.cs
│       │       | 
│               RefreshTokens/
│               ├── IRefreshTokenRepository.cs
│               └── RefreshTokenDto.cs
│       │
│       └── ServiceResult.cs
│
└── Infrastructure/
    ├── App.Persistence/
    │   ├── Context/
    │   │   └── FTDbContext.cs
    │   ├── Extensions/
    │   │   └── PersistenceExtensions.cs
    │   ├── Features/
    │   │   ├── Categories/
    │   │   │   ├── CategoryConfiguration.cs
    │   │   │   └── CategoryRepository.cs
    │   │   ├── Budgets/
    │   │   │   ├── BudgetConfiguration.cs
    │   │   │   └── BudgetRepository.cs
    │   │   ├── Goals/
    │   │   │   ├── GoalConfiguration.cs
    │   │   │   └── GoalRepository.cs
    │   │   ├── Transactions/
    │   │   │   ├── TransactionConfiguration.cs
    │   │   │   └── TransactionRepository.cs
    │   │   ├── RecurringTransactions/
    │   │   │   ├── RecurringTransactionConfiguration.cs
    │   │   │   └── RecurringTransactionRepository.cs
    │   │   └── RefreshTokens/
    │   │       ├── UserRefreshTokenConfiguration.cs
    │   │       ├── RefreshTokenRepository.cs
    |   |
    │   ├── GenericRepository.cs
    │   ├── Interceptors/
    │   │   └── AuditDbContextInterceptor.cs
    │   ├── Migrations/
    │   ├── PersistenceAssembly.cs
    │   └── UnitOfWork.cs
    │
    ├── App.Auth/
    │   ├── SignInService.cs
    │   ├── Extensions/
    │   │   └── AuthExtensions.cs
    │   ├── Options/
    │   │   └── CustomTokenOptions.cs
    │   └── Services/
    │       ├── AuthenticationService.cs
    │       ├── TokenService.cs
    │       └── UserService.cs
    │
    └── App.Caching/
        └── RedisService.cs



