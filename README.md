# 🔗 URL Shortener Service

A scalable, containerized URL shortener service built with ASP.NET Core 8.0 and Redis. Designed for high performance and easy deployment in Docker environments.

---

## 🚀 Features

- 🔗 Shorten long URLs into compact, shareable links
- 🔁 Redirect users via short URLs
- ⏱️ TTL-based expiry of URLs
- ⚡ High-speed lookup with Redis
- 🐳 Docker-ready deployment
- ✅ Unit tested with xUnit + Moq
- 📄 Swagger API documentation

---

## 🧱 Architecture Overview

- **ASP.NET Core Web API**: Handles shortening and redirection
- **Redis**: Fast key-value store with TTL for URL storage
- **Docker Compose**: Spins up API and Redis containers
- **Optional**: Background Worker for future extensions (e.g., metrics, cleanup)

---

## 📦 Setup Instructions

### 🔧 Prerequisites


- [Docker & Docker Compose](https://docs.docker.com/compose/)

### 🐳 Local Run (Docker)

```bash
docker-compose up --build
```
Then visit:

- API: http://localhost:5000/swagger
- UI: http://localhost:5002
