# Rosso Games

<table>
  <tr>
    <td><img src="https://github.com/rossogames/Rossoforge-Persistence/blob/main/logo.png?raw=true" alt="Rossoforge" width="64"/></td>
    <td><h2>Rossoforge - Persistence</h2></td>
  </tr>
</table>

**Rossoforge - Persistence** is a lightweight, strongly typed, and decoupled data management package for Unity. It provides an abstract base architecture that centralizes file I/O operations, serialization, deserialization, and optional encoding into a single file per service. 

By leveraging this architecture, you can create multiple specialized persistence services (e.g., `PlayerProgressService`, `GameSettingsService`, `InventoryPersistenceService`), each managing its own separate file independently.


**Dependencies:**
* com.unity.nuget.newtonsoft-json
* [Rossoforge-Core](https://github.com/rossogames/Rossoforge-Core.git)
* [Rossoforge-Utils](https://github.com/rossogames/Rossoforge-Utils.git)
---

## 🌟 Key Features

* **Abstract Base Service Architecture:** Easily create multiple independent persistence services by extending `PersistenceService<T>`. Each service centralizes and manages its own individual file in `Application.persistentDataPath`.
* **Generic Type Safety (`<T>`):** Works with any data model that implements `IPersistentData` and provides a parameterless constructor.
* **Modular Configuration via `ScriptableObject`:** Configure file names and optional security keys directly in the Unity Inspector using `PersistenceDataService`.
* **Optional Base64 Encoding:** Built-in integration with `Base64Encoder` to encode and safeguard save files whenever an `EncoderKey` is specified.
* **In-Memory Management:** Keep data readily available in memory for fast runtime reads and modifications, persisting changes to disk only when needed.
* **Version Control Ready:** Data models implement `IPersistentData` with a built-in `Version` property, facilitating future data migration strategies.

---

## 🚀 Usage Guide

### 1. Define Your Data Model
Create the class that represents the structure of the data you want to save. It must implement `IPersistentData`:

```csharp
using System;
using Rossoforge.Persistence.Service;

[Serializable]
public class GameSettingsData : IPersistentData
{
    public int Version { get; set; } = 1;
    public float MasterVolume = 1.0f;
    public float MusicVolume = 0.8f;
    public bool IsFullscreen = true;
}
```
#
This package is part of the **Rossoforge** suite, designed to streamline and enhance Unity development workflows.

Developed by Agustin Rosso
https://www.linkedin.com/in/rossoagustin/
