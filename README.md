# RedGari 🧙‍♂️🎲  
**A Discord-native TTRPG Campaign Manager — built with .NET 8, Clean Architecture, and CI/CD best practices.**
![Build](https://github.com/RoJosGaRis/RedGari/actions/workflows/ci.yml/badge.svg)
![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet)
![License](https://img.shields.io/github/license/RoJosGaRis/RedGari)
![Coverage](https://codecov.io/gh/RoJosGaRis/RedGari/branch/main/graph/badge.svg)
---
## ✨ Overview

RedGari is a fully modular Discord bot and backend service that streamlines **TTRPG campaign management**. It supports character creation, dice parsing, and inventory tracking — and is designed using modern software engineering principles:
- 🎲 **Dice engine** with arithmetic support (`1d20 * (2 + 3) - 1`)
- 💬 **Command modules** via Discord.NET’s `CommandService`
- 🧱 **Clean Architecture** (Bot → Application → Infrastructure)
- 🦢 **Testable design** with unit + integration test separation
- 🔐 **Secret-safe configuration** using user-secrets + GitHub Secrets
- 🚀 **CI/CD pipeline**: build, test, and validate on every PR

---

## 📁 Project Structure

```
RedGari/
│
├── RedGari.Bot/           # DiscordSocketClient + Command Modules
├── RedGari.Application/   # Domain logic: DiceService, Interfaces
├── RedGari.Infrastructure/ # (Planned) Data access, API integrations
├── RedGari.Tests/         # Unit & integration tests
└── .github/workflows/     # CI pipeline YAML
```

## 🚀 Getting Started

### 🔧 Local (Development)

```bash
git clone https://github.com/<your-handle>/RedGari
cd RedGari

# Set up your Discord token securely
cd RedGari.Bot
dotnet user-secrets init
dotnet user-secrets set \"Discord:Token\" \"<your_token>\"

# Run the bot
dotnet run --project RedGari.Bot
```

> The bot will come online and respond to `!r 3d6+1`

---

## ✅ Features

- `!r 2d6+3` — full dice expression parser with arithmetic and order of operations
- Extensible Command modules (`!character`, `!campaign`, etc.)
- CommandService injection with DI
- Strongly-typed configuration via `IOptions<T>`
- GitHub Actions pipeline:
  - Restore, build, and test on push/PR
  - Secrets injected via GitHub’s encrypted Secrets
  - Tagged tests for unit/integration separation

---

## 📷 Screenshots & Demo (TODO)

```
![Rolling dice](docs/screenshots/roll-demo.gif)
```

---

## 📄 License

MIT License © 2025 [Roberto García](https://github.com/RoJosGaRis)

---

## 🤝 Contributing

PRs welcome! Open an issue or fork and start a feature branch

## 🙌 Thanks

This project was built with love for tabletop RPGs, automation, and robust backend design. If you’re hiring, reach out — I love clean systems, C#, and building tools that help people tell stories better 🎭.
