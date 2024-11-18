
[![CodeFactor](https://www.codefactor.io/repository/github/josunlp/agiarchitecture/badge)](https://www.codefactor.io/repository/github/josunlp/agiarchitecture)

# AGI Architecture Project

## Überblick

Dieses Projekt stellt eine modulare Architektur für eine General Artificial Intelligence (AGI) in C# bereit. Es basiert auf einer klar strukturierten Schichtenarchitektur mit Datenmanagement, maschinellem Lernen, Wissensverarbeitung, Planung, Anpassung und Sicherheit.

## Projektstruktur und implementierte Features

- **Data Layer**: Speicherung und Zugriff auf Wissensdaten und Erfahrungen. Unterstützt durch DataManager und KnowledgeGraph.
- **Core Services Layer**: Beinhaltet Machine Learning, Reasoning Engine und Planning Engine.
- **Intelligence Layer**: Anpassungsmodul und Self-Monitoring zur Verhaltensoptimierung.
- **Ethics and Safety**: Modul zur Regelüberwachung und Sicherheitsprüfung.
- **Application Layer**: Web-Interface und API, inkl. Swagger-Dokumentation.

### Features

1. **Data Management**: Verwaltung von strukturierten und unstrukturierten Daten.
2. **Machine Learning**: Trainings- und Vorhersagemodelle.
3. **Reasoning**: Wissenslogik und Inferenz.
4. **Planning**: Zielverwaltung und Priorisierung.
5. **Adaptation**: Anpassungsstrategien und Überwachung.
6. **Self-Monitoring**: Protokollierung und Aktivitätsüberwachung.
7. **Ethics and Safety**: Regel- und Sicherheitsüberwachung.
8. **Web Interface und API**: API-Zugriff und Visualisierung über Swagger.

### Neue API-Endpunkte

Die folgenden Endpunkte wurden hinzugefügt, um das Training und die Inferenz von neuronalen Netzwerken zu ermöglichen:

1. **Train Model** (`POST /api/adaptation/train`)
   - **Beschreibung**: Trainiert ein neuronales Netzwerk-Modell basierend auf den bereitgestellten Trainingsdaten und speichert es.
   - **Body Parameter**:
     - `Data`: Liste von Trainingsdaten (Array von Arrays mit Features).
     - `Labels`: Liste von Labels, die den Daten entsprechen.
     - `Epochs`: Anzahl der Epochen, für die das Modell trainiert wird.
     - `LearningRate`: Lernrate für das Training.
     - `ModelName`: Name des Modells, unter dem es gespeichert werden soll.

2. **Predict** (`POST /api/adaptation/predict`)
   - **Beschreibung**: Führt eine Vorhersage auf Basis der Eingabedaten durch, unter Verwendung eines zuvor trainierten Modells.
   - **Body Parameter**:
     - `InputData`: Eingabedaten, auf denen die Vorhersage basieren soll (Array von Features).
     - `ModelName`: Name des Modells, das verwendet werden soll.

## Installation und Nutzung

### Voraussetzungen

Stelle sicher, dass .NET 9 SDK installiert ist. [Download hier](https://dotnet.microsoft.com/download/dotnet/9.0).

### Installation

1. **Repository klonen**:

   ```bash
   git clone <URL zum Repository>
   cd AGIArchitecture
   ```

2. **Abhängigkeiten installieren**:

   ```bash
   dotnet restore
   ```

3. **Solution bauen**:

   ```bash
   dotnet build
   ```

4. **Web-Interface starten**:

   ```bash
   dotnet run --project AGI.WebInterface
   ```

### Nutzung

Besuche `http://localhost:5000` im Browser, um die API-Dokumentation über Swagger zu sehen und Endpunkte zu testen.

## Beiträge

Beiträge und Ideen zur Weiterentwicklung sind willkommen! Bitte erstelle einen Branch oder reiche einen Pull-Request ein.

## Lizenz

Dieses Projekt steht unter der MIT-Lizenz.
