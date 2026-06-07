<div align="center">

# 🤖 SmartBear — AI-Powered IoT Companion Server

### Intelligent Teddy Bear Backend for Children's Education & Entertainment

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Gemini AI](https://img.shields.io/badge/Gemini-2.5_Flash-4285F4?logo=google&logoColor=white)](https://ai.google.dev/)
[![SignalR](https://img.shields.io/badge/SignalR-WebSocket-512BD4?logo=dotnet&logoColor=white)](https://learn.microsoft.com/aspnet/signalr)
[![Redis](https://img.shields.io/badge/Redis-Cache-DC382D?logo=redis&logoColor=white)](https://redis.io/)
[![GCP](https://img.shields.io/badge/GCP-Storage_&_TTS-4285F4?logo=googlecloud&logoColor=white)](https://cloud.google.com/)
[![Docker](https://img.shields.io/badge/Docker-Ready-2496ED?logo=docker&logoColor=white)](https://www.docker.com/)

<br>

[![Tiếng Việt Version](https://img.shields.io/badge/Language-Tiếng_Việt-red?style=for-the-badge&logo=translate)](./README.vi.md)

---

</div>

## 📌 Showcase Notice & Development Status

> **🔒 Showcase Notice:** 
> This is a **public showcase** version of the SmartBear IoT Server, originally developed in a **private repository** during the Spring 2026 Capstone Project at FPT University HCM. As the original repo is private, commit history is not available here. This repository is maintained as a personal job application portfolio demo. Following the graduation thesis defense, this project is being solely maintained, completed, and developed by **Tri Nguyen Minh** (@minhtris-peternguyxn).

---

## 🔗 Related Repositories & Docs

| Resource | Link |
|:---------|:-----|
| 🌐 **Live Demo Website** | [designabear.shop](https://designabear.shop) (Password: `minhtri10504`) |
| 📑 **Project Assets (Report, Demo, Images)** | [Google Drive Folder](https://drive.google.com/drive/folders/1HyvJ_ja7P2BdN88A7oy6Hs2fxi4oRyQE?usp=sharing) |
| 🤖 **SmartBear IoT Server** | This repository (English Version) |
| 🧸 **E-commerce Backend** | [DesignABear-Ecommerce-Showcase](https://github.com/minhtris-peternguyxn/DesignABear-Ecommerce-Showcase) |
| 📱 **Mobile App** | [DesignABear-SmartBearMobileApp-Showcase](https://github.com/minhtris-peternguyxn/DesignABear-SmartBearMobileApp-Showcase) |
| 📄 **API Documentation** | Available at `/swagger` when running locally |

---

## 📖 Overview

**SmartBear Server** is the AI backend powering an interactive IoT teddy bear for children aged 3–10. The bear listens to children via an ESP32 microcontroller, streams audio to this server over WebSocket (SignalR), processes speech via Google STT, generates intelligent responses with **Gemini 2.5 Flash**, and speaks back through **ElevenLabs / Google Cloud TTS**.

The system supports conversation, storytelling, music playback, math tutoring, smart alarms, parental safety controls, and a subscription-based monetization model with **PayOS**.

---

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

---

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

---

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

---

## ✨ Key Features

- 🗣️ **Real-time Voice AI** — Live PCM audio streaming via SignalR → STT → Gemini → TTS → Bear speaker.
- 🧒 **Child Safety System** — Multi-layer content filtering (banned words + AI safety classifier).
- 📚 **Story Playback** — GCS-hosted Vietnamese stories with category-based selection.
- 🎵 **Music Player** — Song library with random/keyword search from GCS buckets.
- ⏰ **Smart Alarms** — Scheduled bear wake-up with custom voice messages.
- 🧮 **Math Tutor Mode** — Age-appropriate math exercises with voice interaction.
- 🎭 **Personality Modes** — Configurable bear behavior (Friendly, Educational, Storyteller, etc.).
- 💳 **Subscription System** — Free/Pro/Ultra tiers with candy-based usage quotas.
- 🔗 **Device Pairing** — OTP-based ESP32 device registration and multi-profile support.
- 📊 **Admin Dashboard API** — User management, device monitoring, subscription analytics.
- 🎙️ **Custom Voice Selection** — Multiple TTS voices with demo preview via ElevenLabs.

---

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL 16+](https://www.postgresql.org/download/)
- [Redis](https://redis.io/download)
- GCP Service Account (for Storage & TTS)
- ElevenLabs API Key (for premium TTS)
- Gemini API Key (for AI responses)

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

---

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

---

## 🧪 Running Tests

```bash
dotnet test
```

---

<div align="center">

## 👥 Team

**FPT University HCM — Capstone Project Spring 2026**

Made with ❤️ by the DesignABear Team

---

⭐ If you found this project useful, please consider giving it a star!

</div>
