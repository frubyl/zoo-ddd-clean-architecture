# 🦁 Zoo Management System — DDD & Clean Architecture

A RESTful web application for managing animals, enclosures, and feeding schedules in a zoo. Built using **Domain-Driven Design (DDD)** principles and organized by the **Clean Architecture** pattern. Implemented as part of a university software engineering assignment.

---

## ✅ Features

- Add/delete animals and enclosures
- Move animals between enclosures (with validation)
- View and manage feeding schedules
- Periodic feeding via background service
- Zoo statistics (animal count, free enclosures, completed feedings)
- In-memory storage, easily replaceable with DB
- Event-driven domain model
- Layered design with strict dependency direction

---

## 🧱 Project Structure (Clean Architecture)

- `Domain` — core entities (Animal, Enclosure, FeedingSchedule), value objects, domain events, repository interfaces, service interfaces, and factories for Animal, Enclosure, and FeedingSchedule creation
- `Application` — services (AnimalTransferService, FeedingOrganizationService, ZooStatisticsService), event handlers
- `Infrastructure` — in-memory repositories implementing domain interfaces
- `Presentation` — Web API controllers, request/response DTOs

---

## 🧩 Domain Highlights

- **Entities**:
  - `Animal`: includes species, gender, status, preferred food, methods to feed/move/treat
  - `Enclosure`: logic for adding/removing animals, cleaning
  - `FeedingSchedule`: track feeding times and status

- **Events**:
  - `AnimalMovedEvent`
  - `FeedingTimeEvent`

- **Value Objects**:
  - `Species`, `Gender`, `Size`, `Food` (immutable types)

- **Factories**:
  - AnimalFactory, EnclosureFactory, and FeedingScheduleFactory are used to encapsulate object creation logic and enforce domain rules during instantiation

---

## ⚙️ Application Logic

- `AnimalTransferService`: validates move, updates repository, publishes event
- `FeedingOrganizationService`: background worker that triggers feedings on time
- `ZooStatisticsService`: aggregates zoo-level insights
- `EventHandlers`: respond to domain events and update state accordingly

---

## 🌐 API (Presentation Layer)

- `AnimalController` — add, delete, transfer animals
- `EnclosureController` — add/delete enclosures
- `FeedingScheduleController` — view/add feedings
- `ZooStatisticsController` — view zoo metrics

> Swagger (OpenAPI) is used to test endpoints and data exchange

---

## 🧪 Testing & Storage

- All repositories use **in-memory storage**
- The application is structured to easily plug in persistent storage later
- Event-based and interface-driven logic allows for unit testing of services and handlers

---

## 📌 How to Run (Suggested)

1. Build the solution in your .NET development environment
2. Run the Web API project
3. Open Swagger UI to interact with the API



