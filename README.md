# Mini-ERP (C# / .NET)

## 📌 Projektbeschreibung

Dieses Projekt ist ein **konsolenbasiertes Mini-ERP-System**, das im Rahmen meiner
**Umschulung zum Fachinformatiker für Anwendungsentwicklung** entwickelt wird.

Ziel ist es, grundlegende Konzepte der professionellen Softwareentwicklung mit **C# und .NET**
praxisnah umzusetzen.

---

## 🎯 Lern- und Projektziele

* Objektorientierte Programmierung (OOP)
* Saubere Projekt- und Ordnerstruktur
* Trennung von Zuständigkeiten (Separation of Concerns)
* Arbeiten mit JSON-Dateien
* Zentrales Logging
* Schreiben und Ausführen von Unit-Tests (xUnit)

---

## ⚙️ Funktionen

Das Mini-ERP unterstützt aktuell:

* Kunden anzeigen
* Kunden hinzufügen
* Kunden suchen
* Kunden bearbeiten
* Kunden löschen
* Persistente Speicherung der Kundendaten (JSON)
* Protokollierung von Aktionen und Fehlern (Log-Datei)

---

## 🧱 Projektstruktur

```
Mini-ERP
│
├── Program.cs                → Konsolen-Menü & Benutzereingaben
│
├── Models
│   └── Kunde.cs              → Datenmodell Kunde
│
├── Services
│   ├── IKundenService.cs     → Service-Interface
│   ├── KundenService.cs      → Geschäftslogik & Datenzugriff
│   └── Logger.cs             → Logging-Service
│
├── kunden.json               → Kundendaten (automatisch erstellt)
├── app.log                   → Log-Datei
│
├── MiniERP.Tests              → Unit-Tests (xUnit)
│
├── Mini-ERP.csproj
└── Mini-ERP.sln
```

---

## 🧠 Architektur & Konzepte

### Model

* Enthält reine Datenklassen (z. B. `Kunde`)
* Keine Geschäftslogik

### Services

* Enthalten die komplette Geschäftslogik
* Verantwortlich für:

  * Kundenverwaltung
  * Dateioperationen
  * Validierung
  * Fehlerbehandlung

### Interface (`IKundenService`)

* Entkoppelt Programmlogik von der Implementierung
* Erhöht Testbarkeit und Erweiterbarkeit

### Logger (`Logger.cs`)

* Zentraler Logging-Service
* Protokolliert:

  * Erfolgreiche Aktionen
  * Fehler und Ausnahmen

### Program.cs

* Enthält ausschließlich:

  * Menüführung
  * Benutzereingaben
  * Aufrufe der Services
* Keine Geschäftslogik

---

## ▶️ Anwendung starten

Voraussetzungen:

* .NET SDK (aktuell)

Projekt starten:

```bash
dotnet run
```

Nach dem Start erscheint ein Konsolenmenü.

Beispiel:

* `1` → Kunden anzeigen
* `2` → Kunde hinzufügen
* `3` → Kunde suchen
* `4` → Kunde löschen
* `5` → Kunde bearbeiten
* `6` → Beenden

---

## 💾 Datenspeicherung

* Kundendaten werden in `kunden.json` gespeichert
* Format: JSON
* Die Datei wird automatisch erstellt, falls sie noch nicht existiert

---

## 📝 Logging

* Aktionen und Fehler werden in `app.log` protokolliert
* Beispiel:

```
2026-02-18 14:32:10 [INFO] Kunde hinzugefügt: ID 1
```

---

## ⚠️ Fehlerbehandlung

* Ungültige Benutzereingaben werden abgefangen
* Doppelte Kunden-IDs werden verhindert
* Datei- und JSON-Fehler werden behandelt und geloggt
* Das Programm stürzt bei Fehlern nicht ab

---

## 🧪 Tests

* Unit-Tests mit **xUnit**
* Testprojekt: `MiniERP.Tests`
* Getestet wird die Geschäftslogik des `KundenService`

Tests ausführen:

```bash
dotnet test
```

---

## 📄 Lizenz

Dieses Projekt dient Lern- und Ausbildungszwecken.
