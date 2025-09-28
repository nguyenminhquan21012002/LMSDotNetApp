# LMS .NET Application - Clean Architecture Guide

## 📋 Tổng quan dự án

Dự án LMS (Learning Management System) được xây dựng theo Clean Architecture pattern với microservices architecture. Mỗi service độc lập và tuân theo nguyên tắc Clean Architecture.

## 🏗️ Cấu trúc Clean Architecture

Clean Architecture chia hệ thống thành 4 tầng chính với dependency flow từ ngoài vào trong:

```
Presentation Layer → Application Layer → Domain Layer
       ↓                    ↓              ↑
Infrastructure Layer ←←←←←←←←←←←←←←←←←←←←←←←←
```

## 📁 Cấu trúc thư mục hiện tại

```
LMSDotNetApp/
├── Course/                    # Course Management Service
├── Identity/                  # Authentication & Authorization Service  
├── Enrollment/               # Student Enrollment Service
├── Quiz/                     # Quiz & Assessment Service
├── Notification/             # Notification Service
└── LMSApp/                   # Main Gateway/Aggregator
```

### Cấu trúc mỗi Service (VD: Course Service)

```
Course/
├── Core/
│   ├── Domain/                    # 🎯 DOMAIN LAYER (Tầng trung tâm)
│   │   ├── Entities/              # Domain entities (Course, CourseModule)
│   │   ├── Enums/                 # Business enumerations (CourseType)
│   │   ├── Events/                # Domain events (CourseCreatedEvent)
│   │   └── Interfaces/            # Repository contracts (ICourseRepository)
│   └── Application/               # 🔧 APPLICATION LAYER (Use Cases)
│       ├── Commands/              # Write operations (CreateCourse, UpdateCourse)
│       ├── Queries/               # Read operations (GetAllCourses, GetCourseById)
│       ├── Handlers/              # Command/Query handlers (CQRS pattern)
│       ├── DTOs/                  # Data Transfer Objects
│       ├── Mappings/              # AutoMapper profiles
│       └── Validators/            # FluentValidation rules
├── Infrastructure/                # 🗄️ INFRASTRUCTURE LAYER (External Concerns)
│   ├── Data/
│   │   ├── DbContexts/            # Entity Framework DbContext
│   │   └── Repositories/          # Repository implementations
│   └── Messaging/                 # Message queue implementations
└── Presentation/                  # 🌐 PRESENTATION LAYER (Entry Points)
    ├── API/                       # Web API Controllers
    └── Extensions/                # Dependency Injection configuration
```

## 🎯 Giải thích từng tầng

### 1. 🎯 Domain Layer (Core/Domain)
**Mục đích**: Chứa business logic thuần túy, không phụ thuộc framework

**Thành phần**:
- **Entities**: Business objects với properties và behavior
- **Enums**: Các giá trị cố định trong domain (CourseType: Streaming, OnlineVideos)
- **Events**: Sự kiện domain để notify các tầng khác
- **Interfaces**: Contracts cho repositories (ICourseRepository)

**Nguyên tắc**:
- ❌ KHÔNG import bất kỳ framework nào
- ✅ Chỉ chứa pure business logic
- ✅ Định nghĩa interfaces cho Infrastructure implement

### 2. 🔧 Application Layer (Core/Application)
**Mục đích**: Orchestrate business logic, xử lý use cases

**Thành phần**:
- **Commands**: Write operations (Create, Update, Delete)
- **Queries**: Read operations (GetAll, GetById)
- **Handlers**: Xử lý Commands/Queries (CQRS pattern)
- **DTOs**: Objects để transfer data giữa tầng
- **Mappings**: AutoMapper để convert Entity ↔ DTO
- **Validators**: FluentValidation cho business rules

**Nguyên tắc**:
- ✅ Sử dụng MediatR cho CQRS pattern
- ✅ Validate input trước khi xử lý
- ✅ Map giữa Domain entities và DTOs

### 3. 🗄️ Infrastructure Layer
**Mục đích**: Implement interfaces từ Domain, kết nối external systems

**Thành phần**:
- **DbContexts**: Entity Framework configuration
- **Repositories**: Implement ICourseRepository từ Domain
- **Messaging**: Message queues (RabbitMQ, Azure Service Bus)

**Nguyên tắc**:
- ✅ Implement tất cả interfaces từ Domain
- ✅ Handle database operations
- ✅ Integrate với external services

### 4. 🌐 Presentation Layer
**Mục đích**: Expose application ra ngoài (API, Web, Console)

**Thành phần**:
- **Controllers**: REST API endpoints
- **Extensions**: Dependency Injection configuration

**Nguyên tắc**:
- ✅ Chỉ handle HTTP requests/responses
- ✅ Delegate business logic cho Application layer
- ✅ Configure DI container

## 🔗 Dependency Flow & Rules

### Dependency Direction
```
Presentation → Application → Domain
Infrastructure → Domain (implement interfaces)
```

### ✅ ĐƯỢC PHÉP
- Presentation → Application
- Application → Domain  
- Infrastructure → Domain (implement interfaces)
- Infrastructure → Application (DI registration)

### ❌ KHÔNG ĐƯỢC PHÉP
- Domain → Application
- Domain → Infrastructure
- Domain → Presentation
- Application → Infrastructure (trực tiếp)

## 🚀 Hướng dẫn Development

### 1. Tạo Feature mới

**Bước 1: Domain Layer**
```csharp
// 1. Tạo Entity
public class Course { /* properties */ }

// 2. Tạo Interface
public interface ICourseRepository { /* methods */ }

// 3. Tạo Event (nếu cần)
public class CourseCreatedEvent { /* properties */ }
```

**Bước 2: Application Layer**
```csharp
// 1. Tạo DTO
public class CourseDTO { /* properties */ }

// 2. Tạo Command/Query
public class CreateCourseCommand : IRequest<CourseDTO> { }

// 3. Tạo Handler
public class CreateCourseHandler : IRequestHandler<CreateCourseCommand, CourseDTO> { }

// 4. Tạo Validator
public class CreateCourseValidator : AbstractValidator<CreateCourseDTO> { }
```

**Bước 3: Infrastructure Layer**
```csharp
// 1. Implement Repository
public class CourseRepository : ICourseRepository { }

// 2. Configure DbContext
modelBuilder.Entity<Course>().HasKey(x => x.Id);
```

**Bước 4: Presentation Layer**
```csharp
// 1. Tạo Controller
[ApiController]
public class CoursesController : ControllerBase { }

// 2. Register services
services.AddScoped<ICourseRepository, CourseRepository>();
```

### 2. Testing Strategy

```
Unit Tests:
├── Domain.Tests/          # Test business logic
├── Application.Tests/     # Test handlers, validators
└── Infrastructure.Tests/  # Test repositories

Integration Tests:
└── API.Tests/            # Test controllers, end-to-end
```

## 🛠️ Technologies Stack

- **.NET 8**: Framework chính
- **Entity Framework Core**: ORM
- **MediatR**: CQRS pattern
- **AutoMapper**: Object mapping
- **FluentValidation**: Input validation
- **Swagger**: API documentation
- **SQL Server**: Database
- **RabbitMQ**: Message queue

## 📦 Package References

Mỗi service cần các packages sau:

```xml
<PackageReference Include="MediatR" Version="12.4.0" />
<PackageReference Include="AutoMapper" Version="13.0.1" />
<PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="12.0.1" />
<PackageReference Include="FluentValidation" Version="11.9.2" />
<PackageReference Include="FluentValidation.AspNetCore" Version="11.3.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.20" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.20" />
```

## 🚦 Getting Started

### 1. Setup Database
```bash
cd Course
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 2. Run Service
```bash
dotnet run
# Navigate to https://localhost:5002/swagger
```

### 3. Test API
```bash
# Health check
GET https://localhost:5002/ping

# Course endpoints
GET https://localhost:5002/api/courses
POST https://localhost:5002/api/courses
PUT https://localhost:5002/api/courses/{id}
DELETE https://localhost:5002/api/courses/{id}
```

## 📋 Development Guidelines

### ✅ DO's
- Tuân theo dependency rules
- Sử dụng CQRS cho mọi operations
- Validate input ở Application layer
- Implement proper error handling
- Write unit tests cho business logic
- Use meaningful naming conventions

### ❌ DON'Ts
- Không vi phạm dependency direction
- Không để business logic ở Controllers
- Không hardcode connection strings
- Không skip validation
- Không expose Entities trực tiếp qua API

## 🏆 Benefits của Clean Architecture

### 🎯 Separation of Concerns
- Mỗi tầng có trách nhiệm rõ ràng
- Dễ maintain và debug

### 🔄 Dependency Inversion
- Domain không phụ thuộc Infrastructure
- Dễ thay đổi database, framework

### 🧪 Testability
- Test business logic độc lập
- Mock dependencies dễ dàng

### 📈 Scalability
- Dễ thêm features mới
- Cấu trúc nhất quán across services

## 🤝 Team Collaboration

### Code Review Checklist
- [ ] Tuân theo Clean Architecture layers
- [ ] Có unit tests cho business logic
- [ ] DTOs được validate properly
- [ ] Repository pattern được implement đúng
- [ ] Error handling được xử lý
- [ ] API documentation (Swagger) được update

### Naming Conventions
- **Entities**: `Course`, `CourseModule`
- **DTOs**: `CourseDTO`, `CreateCourseDTO`
- **Commands**: `CreateCourseCommand`, `UpdateCourseCommand`
- **Queries**: `GetAllCoursesQuery`, `GetCourseByIdQuery`
- **Handlers**: `CreateCourseHandler`, `GetAllCoursesHandler`
- **Repositories**: `ICourseRepository`, `CourseRepository`

---

**Happy Coding! 🚀**

*Tài liệu này sẽ được cập nhật khi có thay đổi trong architecture hoặc conventions.*