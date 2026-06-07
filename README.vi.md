<div align="center">

# 🤖 SmartBear — AI-Powered IoT Companion Server

### Máy chủ hỗ trợ IoT đồ chơi thông minh (gấu bông) tích hợp AI dành cho giáo dục & giải trí của trẻ em

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Gemini AI](https://img.shields.io/badge/Gemini-2.5_Flash-4285F4?logo=google&logoColor=white)](https://ai.google.dev/)
[![SignalR](https://img.shields.io/badge/SignalR-WebSocket-512BD4?logo=dotnet&logoColor=white)](https://learn.microsoft.com/aspnet/signalr)
[![Redis](https://img.shields.io/badge/Redis-Cache-DC382D?logo=redis&logoColor=white)](https://redis.io/)
[![GCP](https://img.shields.io/badge/GCP-Storage_&_TTS-4285F4?logo=googlecloud&logoColor=white)](https://cloud.google.com/)
[![Docker](https://img.shields.io/badge/Docker-Ready-2496ED?logo=docker&logoColor=white)](https://www.docker.com/)

<br>

[![English Version](https://img.shields.io/badge/Language-English-blue?style=for-the-badge&logo=translate)](./README.md)

---

</div>

## 📌 Lưu ý Showcase & Trạng thái phát triển

> **🔒 Lưu ý Showcase:** 
> Đây là phiên bản **showcase công khai** của SmartBear IoT Server, được phát triển ban đầu trong một **repository riêng tư** thuộc Đồ án Tốt nghiệp kỳ Spring 2026 tại Đại học FPT TP.HCM. Do repo gốc là private nên không có lịch sử commit tại đây. Dự án hiện đang được hoàn thiện, phát triển và duy trì duy nhất bởi **Tri Nguyen Minh** (@minhtris-peternguyxn) phục vụ cho hồ sơ năng lực xin việc cá nhân.

---

## 🔗 Tài liệu & Các Repo liên quan

| Tài nguyên | Liên kết |
|:-----------|:---------|
| 🌐 **Website Demo trực tuyến** | [designabear.shop](https://designabear.shop) (Mật khẩu: `minhtri10504`) |
| 📑 **Tài liệu dự án (Báo cáo, Demo, Ảnh)** | [Google Drive Folder](https://drive.google.com/drive/folders/1HyvJ_ja7P2BdN88A7oy6Hs2fxi4oRyQE?usp=sharing) |
| 🤖 **SmartBear IoT Server** | Repo này (Bản Tiếng Việt) |
| 🧸 **E-commerce Backend** | [DesignABear-Ecommerce-Showcase](https://github.com/minhtris-peternguyxn/DesignABear-Ecommerce-Showcase) |
| 📱 **Mobile App** | [DesignABear-SmartBearMobileApp-Showcase](https://github.com/minhtris-peternguyxn/DesignABear-SmartBearMobileApp-Showcase) |
| 📄 **Tài liệu API (Swagger)** | Khả dụng tại đường dẫn `/swagger` khi chạy local |

---

## 📖 Tổng Quan

**SmartBear Server** là hệ thống backend AI điều khiển gấu bông thông minh IoT dành cho trẻ em từ 3–10 tuổi. Gấu bông lắng nghe trẻ thông qua vi điều khiển ESP32, truyền phát âm thanh (audio streaming) về server qua WebSocket (SignalR), xử lý giọng nói thành văn bản thông qua Google STT, tạo phản hồi thông minh bằng **Gemini 2.5 Flash**, và phát lại câu trả lời dạng âm thanh thông qua **ElevenLabs / Google Cloud TTS**.

Hệ thống hỗ trợ các tính năng như hội thoại tự do, kể chuyện, phát nhạc, trợ lý học toán, báo thức thông minh, kiểm soát nội dung an toàn cho phụ huynh và mô hình đăng ký gói thành viên (subscription) thông qua cổng thanh toán **PayOS**.

---

## 🏗️ Kiến Trúc Hệ Thống

```
┌──────────────┐   WebSocket/SignalR   ┌──────────────────────────┐
│  ESP32 Bear  │◄─────────────────────►│    SmartBear Server      │
│  (Firmware)  │   PCM Audio Stream    │    (.NET 8 Backend)      │
└──────────────┘                       ├──────────────────────────┤
                                       │  LLMHub (SignalR)        │
┌──────────────┐   REST API + JWT      │  AI Service (Gemini)     │
│  Mobile App  │◄─────────────────────►│  Speech / TTS Pipeline   │
│  (Flutter)   │                       │  Content Safety Filter   │
└──────────────┘                       │  Smart Alarm Engine      │
                                       │  Subscription & Payment  │
                                       ├──────────────────────────┤
                                       │  PostgreSQL · Redis      │
                                       │  GCS · ElevenLabs · PayOS│
                                       └──────────────────────────┘
                                                    │
                                       ┌────────────┴────────────┐
                                       │   Python TTS Bridge     │
                                       │   (FFmpeg + ElevenLabs) │
                                       └─────────────────────────┘
```

---

## 🛠️ Công Nghệ Sử Dụng

| Tầng | Công nghệ sử dụng |
|:-----|:------------------|
| **Runtime** | .NET 8, ASP.NET Core, C# 12 |
| **Real-time** | SignalR WebSocket Hub (truyền phát nhị phân luồng âm thanh PCM) |
| **AI / LLM** | Google Gemini 2.5 Flash, OpenAI GPT-3.5 (dự phòng) |
| **Giọng nói** | Google Cloud STT (Speech-to-Text), Google Cloud TTS, ElevenLabs TTS |
| **Database** | PostgreSQL 16 + Entity Framework Core |
| **Caching** | Redis (phiên chat, hạn ngạch sử dụng, trạng thái thiết bị) |
| **Lưu trữ** | Google Cloud Storage (quản lý truyện kể, âm nhạc, mẫu giọng) |
| **Xác thực** | JWT Bearer + Google OAuth 2.0 + Xác thực mã thiết bị |
| **Thanh toán** | PayOS (Cổng thanh toán ngân hàng trực tuyến Việt Nam) |
| **TTS Bridge** | Microservice Python (sử dụng thư viện FFmpeg để chuyển mã âm thanh) |
| **DevOps** | Docker Compose (stack 3 dịch vụ), Swagger/OpenAPI |
| **Testing** | xUnit (kiểm thử bộ lọc từ cấm, tính năng an toàn, hạn ngạch, mã giảm giá) |

---

## 📐 Các Mẫu Thiết Kế (Design Patterns)

| Pattern | Cách áp dụng |
|:--------|:-------------|
| **Strategy Pattern** | `ModeInstructionStrategy` (điều khiển chế độ/tính cách gấu bông), `VoucherStrategy` (tính toán các loại mã giảm giá) |
| **Repository Pattern** | Tạo các repository tổng quát (Generic) kết hợp chuyên biệt cho các thực thể |
| **Dependency Injection** | Sử dụng toàn bộ DI container tích hợp sẵn của ASP.NET Core với Scoped/Singleton |
| **Background Services** | Sử dụng Hosted Services: `QuotaResetWorker`, `SmartAlarmWorker`, `SessionCleanupWorker`, `ChatPersistenceWorker`, `CreditResetWorker` |
| **Command Processor** | `BearCommandProcessor` hỗ trợ AI phân tích lệnh và thực thi hành động tương ứng |
| **Prompt Engineering** | `PromptBuilder` kết hợp `PromptTemplates` cung cấp chỉ dẫn hệ thống phù hợp theo từng chế độ chơi |
| **Action Handler** | Interface `IAIActionHandler` giúp dễ dàng mở rộng các hành động của AI (phát nhạc, kể chuyện, dạy toán) |

---

## ✨ Tính Năng Nổi Bật

- 🗣️ **AI Giọng nói Real-time** — Truyền trực tiếp luồng âm thanh PCM qua SignalR → Chuyển đổi STT → Gửi Gemini xử lý → Tạo giọng nói TTS → Phát qua loa của Gấu.
- 🧒 **Hệ thống An toàn cho Trẻ em** — Bộ lọc nội dung đa tầng (kết hợp bộ lọc từ cấm thủ công và AI classifier phân loại độ an toàn).
- 📚 **Kể chuyện thông minh** — Thư viện truyện nói Tiếng Việt lưu trữ trên GCS, phân loại theo chủ đề và độ tuổi.
- 🎵 **Trình phát nhạc** — Thư viện bài hát thiếu nhi hỗ trợ tìm kiếm ngẫu nhiên hoặc theo từ khóa.
- ⏰ **Báo thức thông minh** — Đặt giờ báo thức cho gấu với lời nhắn bằng giọng nói cá nhân hoá của AI.
- 🧮 **Chế độ Dạy Toán** — Trò chơi học toán tương tác bằng giọng nói phù hợp với độ tuổi của trẻ.
- 🎭 **Cấu hình Tính cách** — Cho phép tùy chỉnh chế độ hoạt động của gấu (Thân thiện, Giáo dục, Kể chuyện, Học toán, v.v.).
- 💳 **Mô hình Subscription** & Quota — Các gói tài khoản Free/Pro/Ultra, giới hạn số lượng câu hỏi ("kẹo thông minh") trong ngày.
- 🔗 **Ghép nối thiết bị** — Quy trình đăng ký thiết bị ESP32 qua mã OTP và quản lý đa hồ sơ cho nhiều trẻ em.
- 📊 **API Dashboard Admin** — Hỗ trợ quản trị viên quản lý người dùng, theo dõi trạng thái thiết bị và phân tích doanh thu.

---

## 🚀 Cài Đặt và Chạy thử

### Yêu cầu hệ thống
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL 16+](https://www.postgresql.org/download/)
- [Redis](https://redis.io/download)
- Tài khoản GCP Service Account (cho Storage & TTS)
- API Key ElevenLabs (nếu dùng giọng nói cao cấp)
- API Key Gemini (cho AI xử lý phản hồi)

### Các bước thiết lập
```bash
# 1. Clone repository này về máy local
git clone https://github.com/minhtris-peternguyxn/DesignABear-SmartBearServer-Showcase.git
cd DesignABear-SmartBearServer-Showcase

# 2. Tạo cấu hình secrets từ file mẫu
cp SmartBearServer/appsettings.template.json SmartBearServer/appsettings.json
cp SmartBearServer/gcp-credentials.template.json SmartBearServer/your-gcp-credentials.json
# Cập nhật thông tin kết nối và API Key thực tế vào hai file trên

# 3. Restore các package NuGet và chạy Database Migrations
dotnet restore
dotnet ef database update --project SmartBearServer

# 4. Khởi chạy dự án
dotnet run --project SmartBearServer
```

### Chạy bằng Docker Compose (Full Stack)
```bash
docker-compose up -d
# Khởi động đồng thời: Redis + SmartBear Server + Python TTS Bridge
```

| Dịch vụ | Cổng | Vai trò |
|:--------|:-----|:--------|
| SmartBear Server | `7017` | API Backend chính + SignalR Hub |
| Python TTS Bridge | `8000` | Microservice xử lý chuyển mã âm thanh (FFmpeg) |
| Redis | `6379` | Quản lý cache và session trò chuyện |

---

## 📁 Cấu Trúc Thư Mục

```
DesignABear-SmartBearServer-Showcase/
├── SmartBearServer/                    # Dự án .NET 8 chính
│   ├── Controllers/                    # Gồm 16 controllers API REST
│   │   ├── AIController.cs             # Các endpoint tương tác giọng nói AI
│   │   ├── IoTController.cs            # Giao tiếp với thiết bị vi điều khiển ESP32
│   │   ├── DeviceController.cs         # Quản lý thiết bị và hồ sơ người dùng
│   │   ├── PaymentController.cs        # Xử lý webhook thanh toán subscription PayOS
│   │   ├── SafetyController.cs         # Cấu hình bộ lọc an toàn trẻ em
│   │   ├── SmartAlarmController.cs     # API CRUD báo thức thông minh
│   │   └── ...                         # Xác thực, Quản trị, Ghép cặp...
│   ├── Hubs/
│   │   └── LLMHub.cs                   # SignalR WebSocket hub (stream audio nhị phân)
│   ├── Services/
│   │   ├── Implementations/            # 28 lớp triển khai dịch vụ nghiệp vụ
│   │   ├── Interfaces/                 # 25 giao diện (interfaces) định nghĩa dịch vụ
│   │   ├── Prompts/                    # Các prompt mẫu cho AI
│   │   ├── Strategies/                 # Triển khai Strategy Pattern (các chế độ gấu, voucher)
│   │   └── BearCommandProcessor.cs     # Bộ phân tích lệnh từ AI
│   ├── Infrastructure/                 # Cấu hình máy khách Gemini/OpenAI, hằng số
│   ├── Model/                          # Định nghĩa Entity và DTOs
│   ├── Repositories/                   # Lớp truy cập cơ sở dữ liệu (Data Access Layer)
│   ├── Migrations/                     # Các tệp database migrations của EF Core
│   └── Data/                           # DbContext quản lý database
├── SmartBearServer.Tests/              # Bộ Unit Tests sử dụng xUnit
│   ├── SafetyServiceTests.cs           # Kiểm thử bộ lọc an toàn
│   ├── UsageQuotaServiceTests.cs       # Kiểm thử giới hạn "kẹo" sử dụng
│   ├── BannedWordServiceTests.cs       # Kiểm thử lọc từ cấm
│   ├── BearCommandProcessorTests.cs    # Kiểm thử thực thi lệnh AI
│   └── VoucherTests.cs                 # Kiểm thử chiến lược giảm giá
├── docker-compose.yml                  # Cấu hình container orchestrations
└── SmartBearServer.sln                 # File Solution chính
```

---

## 🧪 Chạy Kiểm Thử (Unit Tests)

```bash
dotnet test
```

---

<div align="center">

## 👥 Đội ngũ phát triển

**Đại học FPT TP.HCM — Capstone Project Spring 2026**

Được phát triển với ❤️ bởi đội ngũ DesignABear

---

⭐ Nếu bạn thấy dự án này hữu ích, hãy để lại một ngôi sao (star) ủng hộ nhé!

</div>
