<div align="center">

# 🤖 SmartBear — AI-Powered IoT Companion Server

### Intelligent Teddy Bear Backend for Children's Education & Entertainment

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Gemini AI](https://img.shields.io/badge/Gemini-2.5_Flash-4285F4?logo=google&logoColor=white)](https://ai.google.dev/)
[![SignalR](https://img.shields.io/badge/SignalR-WebSocket-512BD4?logo=dotnet&logoColor=white)](https://learn.microsoft.com/aspnet/signalr)
[![Redis](https://img.shields.io/badge/Redis-Cache-DC382D?logo=redis&logoColor=white)](https://redis.io/)
[![GCP](https://img.shields.io/badge/GCP-Storage_&_TTS-4285F4?logo=googlecloud&logoColor=white)](https://cloud.google.com/)
[![Docker](https://img.shields.io/badge/Docker-Ready-2496ED?logo=docker&logoColor=white)](https://www.docker.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](./LICENSE)

[English](#-english) · [Tiếng Việt](#-tiếng-việt)

</div>

---

## 📌 About This Repository / Về Repository Này

> **🔒 Note:** This is a **public showcase** of the SmartBear IoT Server, originally developed in a **private repository** during the Spring 2026 Capstone Project at FPT University HCM. As the original repo is private, **commit history is not available here** — we kindly ask readers to understand. All secrets, API keys, and GCP credentials have been removed.
>
> **🔒 Lưu ý:** Đây là phiên bản **showcase công khai** của SmartBear IoT Server, được phát triển trong **repository riêng tư** thuộc Đồ án Tốt nghiệp kỳ Spring 2026 tại Đại học FPT TP.HCM. Do repo gốc là private nên **không có lịch sử commit** — mong quý độc giả thông cảm. Toàn bộ secrets, API keys và GCP credentials đã được loại bỏ.

---

## 🔗 Related Repositories / Các Repo Liên Quan

| Repository | Description / Mô tả | Link |
|:-----------|:---------------------|:-----|
| 🤖 **SmartBear IoT Server** | This repository / Repo hiện tại | You are here ✨ |
| 🧸 **E-commerce Backend** | Custom toy e-commerce platform / Nền tảng bán gấu bông tuỳ chỉnh | [DesignABear-Ecommerce-Showcase](https://github.com/minhtris-peternguyxn/DesignABear-Ecommerce-Showcase) |
| 📱 **Mobile App** | Flutter mobile application / Ứng dụng di động Flutter | [Coming Soon / Sẽ cập nhật](#) |

## 📄 Documentation & Demo / Tài Liệu & Demo

| Resource | Link |
|:---------|:-----|
| 📑 **Project Report / Báo Cáo** | [Google Drive — Coming Soon](#) |
| 🎬 **Video Demo** | [Google Drive — Coming Soon](#) |
| 🖼️ **Screenshots** | [Google Drive — Coming Soon](#) |
| 📊 **API Docs (Swagger)** | Available at `/swagger` when running locally |

---

# 🇬🇧 English

## Overview

**SmartBear Server** is the AI backend powering an interactive IoT teddy bear for children aged 3–10. The bear listens to children via an ESP32 microcontroller, streams audio to this server over WebSocket (SignalR), processes speech via Google STT, generates intelligent responses with **Gemini 2.5 Flash**, and speaks back through **ElevenLabs / Google Cloud TTS**.

The system supports conversation, storytelling, music playback, math tutoring, smart alarms, parental safety controls, and a subscription-based monetization model with **PayOS**.

## 🏗️ System Architecture

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

## 🛠️ Tech Stack

| Layer | Technologies |
|:------|:-------------|
| **Runtime** | .NET 8, ASP.NET Core, C# 12 |
| **Real-time** | SignalR WebSocket Hub (binary PCM audio streaming) |
| **AI / LLM** | Google Gemini 2.5 Flash, OpenAI GPT-3.5 (fallback) |
| **Speech** | Google Cloud STT, Google Cloud TTS, ElevenLabs TTS |
| **Database** | PostgreSQL 16 + Entity Framework Core |
| **Caching** | Redis (chat sessions, usage quotas, device state) |
| **Storage** | Google Cloud Storage (stories, music, voice samples) |
| **Auth** | JWT Bearer + Google OAuth 2.0 + Device Token Auth |
| **Payment** | PayOS (Vietnamese payment gateway) |
| **TTS Bridge** | Python microservice (FFmpeg audio transcoding) |
| **DevOps** | Docker Compose (3-service stack), Swagger/OpenAPI |
| **Testing** | xUnit (safety, quota, banned words, commands, vouchers) |

## 📐 Design Patterns

| Pattern | Usage |
|:--------|:------|
| **Strategy Pattern** | `ModeInstructionStrategy` (bear personality modes), `VoucherStrategy` (discount types) |
| **Repository Pattern** | Generic + specialized repositories for all entities |
| **Dependency Injection** | Full DI container, scoped/singleton services |
| **Background Services** | `QuotaResetWorker`, `SmartAlarmWorker`, `SessionCleanupWorker`, `ChatPersistenceWorker`, `CreditResetWorker` |
| **Command Processor** | `BearCommandProcessor` for structured AI action dispatch |
| **Prompt Engineering** | `PromptBuilder` + `PromptTemplates` with per-mode system instructions |
| **Action Handler** | `IAIActionHandler` interface for extensible AI actions (music, stories, math) |

## ✨ Key Features

- 🗣️ **Real-time Voice AI** — Live PCM audio streaming via SignalR → STT → Gemini → TTS → Bear speaker
- 🧒 **Child Safety System** — Multi-layer content filtering (banned words + AI safety classifier)
- 📚 **Story Playback** — GCS-hosted Vietnamese stories with category-based selection
- 🎵 **Music Player** — Song library with random/keyword search from GCS buckets
- ⏰ **Smart Alarms** — Scheduled bear wake-up with custom voice messages
- 🧮 **Math Tutor Mode** — Age-appropriate math exercises with voice interaction
- 🎭 **Personality Modes** — Configurable bear behavior (Friendly, Educational, Storyteller, etc.)
- 💳 **Subscription System** — Free/Pro/Ultra tiers with candy-based usage quotas
- 🔗 **Device Pairing** — OTP-based ESP32 device registration and multi-profile support
- 📊 **Admin Dashboard API** — User management, device monitoring, subscription analytics
- 🎙️ **Custom Voice Selection** — Multiple TTS voices with demo preview via ElevenLabs

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL 16+](https://www.postgresql.org/download/)
- [Redis](https://redis.io/download)
- [GCP Service Account](https://cloud.google.com/) (for Storage & TTS)
- [ElevenLabs API Key](https://elevenlabs.io/) (for premium TTS)
- [Gemini API Key](https://ai.google.dev/) (for AI responses)

### Setup

```bash
# 1. Clone
git clone https://github.com/minhtris-peternguyxn/DesignABear-SmartBearServer-Showcase.git
cd DesignABear-SmartBearServer-Showcase

# 2. Configure secrets
cp SmartBearServer/appsettings.template.json SmartBearServer/appsettings.json
cp SmartBearServer/gcp-credentials.template.json SmartBearServer/your-gcp-credentials.json
# Edit both files with your actual credentials

# 3. Restore & run
dotnet restore
dotnet ef database update --project SmartBearServer
dotnet run --project SmartBearServer
```

### Docker Compose (Full Stack)

```bash
docker-compose up -d
# Starts: Redis + SmartBear Server + Python TTS Bridge
```

| Service | Port | Description |
|:--------|:-----|:------------|
| SmartBear Server | `7017` | Main API + SignalR Hub |
| Python TTS Bridge | `8000` | Audio transcoding microservice |
| Redis | `6379` | Cache & session store |

## 📁 Project Structure

```
DesignABear-SmartBearServer-Showcase/
├── SmartBearServer/                    # Main .NET 8 project
│   ├── Controllers/                    # 16 REST API controllers
│   │   ├── AIController.cs             # Voice AI interaction endpoints
│   │   ├── IoTController.cs            # ESP32 device communication
│   │   ├── DeviceController.cs         # Device & profile management
│   │   ├── PaymentController.cs        # PayOS subscription payments
│   │   ├── SafetyController.cs         # Content safety management
│   │   ├── SmartAlarmController.cs     # Alarm CRUD & scheduling
│   │   └── ...                         # Auth, Admin, Pairing, etc.
│   ├── Hubs/
│   │   └── LLMHub.cs                   # SignalR WebSocket hub (audio streaming)
│   ├── Services/
│   │   ├── Implementations/            # 28 service classes
│   │   ├── Interfaces/                 # 25 service contracts
│   │   ├── Prompts/                    # AI prompt templates
│   │   ├── Strategies/                 # Strategy pattern (modes, vouchers)
│   │   └── BearCommandProcessor.cs     # AI action dispatch engine
│   ├── Infrastructure/                 # Gemini/OpenAI clients, constants
│   ├── Model/                          # Entity models & DTOs
│   ├── Repositories/                   # Data access layer
│   ├── Migrations/                     # EF Core migrations
│   └── Data/                           # DbContext
├── SmartBearServer.Tests/              # xUnit test suite
│   ├── SafetyServiceTests.cs           # Content safety tests
│   ├── UsageQuotaServiceTests.cs       # Candy/quota logic tests
│   ├── BannedWordServiceTests.cs       # Word filter tests
│   ├── BearCommandProcessorTests.cs    # Command dispatch tests
│   └── VoucherTests.cs                 # Discount strategy tests
├── docker-compose.yml                  # 3-service deployment
└── SmartBearServer.sln                 # Solution file
```

## 🧪 Running Tests

```bash
dotnet test
```

---

# 🇻🇳 Tiếng Việt

## Tổng Quan

**SmartBear Server** là backend AI điều khiển gấu bông thông minh IoT dành cho trẻ em 3–10 tuổi. Gấu lắng nghe trẻ qua vi điều khiển ESP32, stream audio về server qua WebSocket (SignalR), xử lý giọng nói qua Google STT, tạo phản hồi thông minh bằng **Gemini 2.5 Flash**, và phát lại qua **ElevenLabs / Google Cloud TTS**.

Hệ thống hỗ trợ trò chuyện, kể chuyện, phát nhạc, dạy toán, báo thức thông minh, kiểm soát an toàn cho phụ huynh, và mô hình thanh toán subscription qua **PayOS**.

## 🛠️ Công Nghệ

| Tầng | Công nghệ |
|:-----|:----------|
| **Runtime** | .NET 8, ASP.NET Core, C# 12 |
| **Real-time** | SignalR WebSocket Hub (stream audio PCM nhị phân) |
| **AI / LLM** | Google Gemini 2.5 Flash, OpenAI GPT-3.5 (dự phòng) |
| **Giọng nói** | Google Cloud STT, Google Cloud TTS, ElevenLabs TTS |
| **Database** | PostgreSQL 16 + Entity Framework Core |
| **Cache** | Redis (chat sessions, quota sử dụng, trạng thái thiết bị) |
| **Lưu trữ** | Google Cloud Storage (truyện, nhạc, mẫu giọng) |
| **Xác thực** | JWT Bearer + Google OAuth 2.0 + Device Token Auth |
| **Thanh toán** | PayOS (cổng thanh toán Việt Nam) |
| **TTS Bridge** | Python microservice (FFmpeg chuyển đổi audio) |
| **DevOps** | Docker Compose (3 services), Swagger/OpenAPI |

## ✨ Tính Năng Nổi Bật

- 🗣️ **AI Giọng nói Real-time** — Stream audio PCM qua SignalR → STT → Gemini → TTS → Loa gấu
- 🧒 **Hệ thống An toàn** — Lọc nội dung đa tầng (từ cấm + AI phân loại)
- 📚 **Kể chuyện** — Thư viện truyện tiếng Việt trên GCS theo danh mục
- 🎵 **Phát nhạc** — Tìm kiếm bài hát theo từ khóa / phát ngẫu nhiên
- ⏰ **Báo thức thông minh** — Hẹn giờ gấu nói với giọng tuỳ chỉnh
- 🧮 **Chế độ Dạy Toán** — Bài tập toán theo độ tuổi qua giọng nói
- 🎭 **Chế độ Tính cách** — Cấu hình hành vi gấu (Thân thiện, Giáo dục, Kể chuyện...)
- 💳 **Subscription** — Gói Free/Pro/Ultra với quota "kẹo thông minh"
- 🔗 **Ghép nối thiết bị** — Đăng ký ESP32 qua OTP, hỗ trợ nhiều hồ sơ trẻ
- 📊 **Admin Dashboard API** — Quản lý người dùng, thiết bị, thống kê

## 🚀 Cài Đặt

```bash
# 1. Clone
git clone https://github.com/minhtris-peternguyxn/DesignABear-SmartBearServer-Showcase.git
cd DesignABear-SmartBearServer-Showcase

# 2. Cấu hình
cp SmartBearServer/appsettings.template.json SmartBearServer/appsettings.json
cp SmartBearServer/gcp-credentials.template.json SmartBearServer/your-gcp-credentials.json
# Chỉnh sửa 2 file trên với credentials thực tế

# 3. Chạy
dotnet restore
dotnet ef database update --project SmartBearServer
dotnet run --project SmartBearServer
```

### Docker Compose

```bash
docker-compose up -d
# Khởi động: Redis + SmartBear Server + Python TTS Bridge
```

---

<div align="center">

## 👥 Team

**FPT University HCM — Capstone Project Spring 2026**

Made with ❤️ by the DesignABear Team

---

⭐ If you found this project useful, please consider giving it a star!

⭐ Nếu bạn thấy dự án này hữu ích, hãy cho chúng tôi một ngôi sao nhé!

</div>
