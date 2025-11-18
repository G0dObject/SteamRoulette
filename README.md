# SteamRoulette

Monorepository для игры SteamRoulette - рулетка с предметами из Steam инвентаря.

## 📚 Документация

- **[BUILD.md](BUILD.md)** - Детальная инструкция по сборке и запуску
- **README.md** - Общая информация о проекте (этот файл)

## 📋 Требования

Перед началом работы убедитесь, что у вас установлены:

- **.NET SDK 9.0** или выше ([скачать](https://dotnet.microsoft.com/download))
- **Node.js 18+** и npm ([скачать](https://nodejs.org/))
- **Steam API Key** - для работы с Steam API ([получить](https://steamcommunity.com/dev/apikey))
- **Redis** (опционально, для кеширования, используется через Aspire)

## 🚀 Быстрый старт

### Вариант 1: Запуск через Aspire (рекомендуется)

Aspire автоматически запускает все сервисы и управляет зависимостями.

1. **Клонируйте репозиторий** (если еще не сделали):
   ```bash
   git clone <repository-url>
   cd SteamRoulette
   ```

2. **Установите зависимости фронтенда** (обязательно перед запуском):
   ```bash
   cd SteamRoulette.FrontEnd
   npm install
   cd ..
   ```

3. **Настройте конфигурацию**:
   
   Откройте `SteamRoulette.WebApi/appsettings.json` и настройте:
   ```json
   {
     "Steam": {
       "Key": "ВАШ_STEAM_API_KEY"
     },
     "JWT": {
       "Key": "ВАШ_СЕКРЕТНЫЙ_КЛЮЧ_ДЛЯ_JWT"
     }
   }
   ```

4. **Запустите AppHost**:
   ```bash
   cd SteamRoulette.AppHost
   dotnet run
   ```

   Aspire автоматически:
   - Запустит WebApi
   - Запустит Frontend (Vite dev server)
   - Запустит Redis (если установлен Docker)
   - Откроет дашборд Aspire в браузере

### Вариант 2: Ручной запуск компонентов

#### 1. Настройка Backend (WebApi)

1. **Перейдите в папку WebApi**:
   ```bash
   cd SteamRoulette.WebApi
   ```

2. **Настройте appsettings.json**:
   - Укажите ваш Steam API Key в `Steam:Key`
   - Настройте JWT ключ в `JWT:Key` (для production используйте безопасный ключ)
   - Проверьте CORS настройки в `Cors:AllowedOrigins`

3. **Примените миграции базы данных**:
   ```bash
   dotnet ef database update --project ../SteamRoulette.Persistence
   ```
   
   Или миграции применятся автоматически при первом запуске.

4. **Запустите WebApi**:
   ```bash
   dotnet run
   ```
   
   API будет доступен по адресу:
   - HTTP: `http://localhost:5114`
   - HTTPS: `https://localhost:7069`
   - Swagger UI: `https://localhost:7069/swagger` (в режиме Development)

#### 2. Настройка Frontend

1. **Перейдите в папку Frontend**:
   ```bash
   cd SteamRoulette.FrontEnd
   ```

2. **Установите зависимости**:
   ```bash
   npm install
   ```

3. **Создайте файл `.env`** (если нужно):
   ```env
   VITE_API_URL=http://localhost:5114
   ```

4. **Запустите dev server**:
   ```bash
   npm run dev
   ```
   
   Или на порту 5000:
   ```bash
   npm run devs
   ```
   
   Frontend будет доступен по адресу: `http://localhost:5173` (или `http://localhost:5000`)

## 🔧 Сборка проекта

### Сборка всего решения

```bash
dotnet build SteamRoulette.sln
```

### Сборка для production

**Backend:**
```bash
cd SteamRoulette.WebApi
dotnet publish -c Release -o ./publish
```

**Frontend:**
```bash
cd SteamRoulette.FrontEnd
npm run build
```

Собранные файлы будут в папке `dist/`.

## 📁 Структура проекта

```
SteamRoulette/
├── SteamRoulette.AppHost/          # Aspire AppHost для оркестрации
├── SteamRoulette.WebApi/           # Backend API (.NET 9)
│   ├── Controllers/                # API контроллеры
│   ├── Services/                   # Бизнес-логика
│   ├── DTO/                        # Data Transfer Objects
│   └── GameHub.cs                  # SignalR Hub для игры
├── SteamRoulette.Domain/           # Доменная модель
├── SteamRoulette.Infrastructure/   # Интерфейсы сервисов
├── SteamRoulette.Persistence/      # Entity Framework Core
├── SteamRoulette.ServiceDefaults/  # Общие настройки сервисов
└── SteamRoulette.FrontEnd/         # React + TypeScript + Vite
    ├── components/                # React компоненты
    ├── pages/                     # Страницы приложения
    └── src/                       # Исходный код
```

## 🔐 Настройка аутентификации Steam

1. Получите Steam API Key на [Steam Web API Key](https://steamcommunity.com/dev/apikey)
2. Укажите ключ в `appsettings.json`:
   ```json
   "Steam": {
     "Key": "ВАШ_API_KEY"
   }
   ```
3. Настройте домен для редиректа (должен совпадать с вашим доменом):
   ```json
   "Steam": {
     "Domain": "https://localhost:7069"
   }
   ```

## 🗄️ База данных

Проект использует **SQLite** для хранения данных. База данных создается автоматически при первом запуске в файле `SteamRoulette.WebApi/db.db`.

### Работа с миграциями

**Создать новую миграцию:**
```bash
cd SteamRoulette.WebApi
dotnet ef migrations add MigrationName --project ../SteamRoulette.Persistence
```

**Применить миграции:**
```bash
dotnet ef database update --project ../SteamRoulette.Persistence
```

**Откатить последнюю миграцию:**
```bash
dotnet ef migrations remove --project ../SteamRoulette.Persistence
```

## 🌐 CORS настройки

Настройки CORS находятся в `appsettings.json`:

```json
"Cors": {
  "AllowedOrigins": [
    "http://localhost:5173",
    "http://localhost:5000",
    "https://localhost:5173"
  ]
}
```

Для production добавьте ваш домен в этот список.

## 🎮 Использование SignalR

SignalR Hub доступен по адресу: `/gamehub`

**Подключение с клиента:**
```javascript
import * as signalR from "@microsoft/signalr";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7069/gamehub")
    .build();

await connection.start();
connection.on("ReceiveNumber", (multiplier) => {
    console.log("Current multiplier:", multiplier);
});
connection.on("Crush", (multiplier) => {
    console.log("Game crashed at:", multiplier);
});
```

## 🐛 Решение проблем

### Ошибка подключения к базе данных

Убедитесь, что:
- SQLite файл `db.db` существует или будет создан автоматически
- У приложения есть права на запись в папку `SteamRoulette.WebApi/`

### CORS ошибки

- Проверьте, что URL фронтенда добавлен в `Cors:AllowedOrigins`
- Убедитесь, что используется правильный протокол (http/https)

### Steam аутентификация не работает

**Ошибка: "An error occurred while retrieving the user profile from Steam"**

1. Проверьте правильность Steam API Key в `appsettings.json`
2. Убедитесь, что `Steam:Domain` совпадает с URL вашего приложения (например, `https://localhost:7069`)
3. Проверьте, что Steam API Key активен на [Steam Web API](https://steamcommunity.com/dev/apikey)
4. Убедитесь, что Steam API Key имеет правильный формат (32 символа)
5. Проверьте логи приложения для детальной информации об ошибке

### Порт уже занят

Измените порт в:
- **Backend**: `launchSettings.json` или `appsettings.json`
- **Frontend**: `package.json` (скрипт `devs`) или `vite.config.js`

### Ошибка: "'vite' is not recognized"

Если при запуске через Aspire появляется ошибка о том, что vite не найден:

```bash
cd SteamRoulette.FrontEnd
npm install
```

Затем перезапустите Aspire. **Важно:** Зависимости фронтенда должны быть установлены перед запуском через Aspire.

## 📝 TODO

- [ ] Полная реализация SignalR поддержки
- [ ] Интеграция пользовательского инвентаря
- [ ] Добавление предметов из Steam
- [ ] Система ставок

- [ ] История раундов

## 📄 Лицензия

[Укажите лицензию]

## 👥 Авторы

[Укажите авторов]
