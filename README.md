# 🔧 Generic Upgrade System for Unity (MVC Pattern)

A flexible and scalable **Upgrade System** built using the **Model-View-Controller (MVC)** design pattern. This system is suitable for games where you want players to upgrade stats like speed, damage, cooldowns, time, or anything else defined via ScriptableObjects.

---

## 🧠 Key Features

- 🎮 Supports any number of upgradeable attributes
- 🧱 Built with MVC for clean separation of concerns
- 💾 Persistent data using `PlayerPrefs`
- 🎨 Fully customizable via the Unity Inspector
- ✨ Built-in UI feedback with particle animation (DOTween required)
- 🚫 Graceful error handling (max level / no skill points)

---

## 📁 Folder Structure

```
GenericUpgradeSystem/
├── UpgradeModel.cs
├── UpgradeController.cs
├── UpgradeView.cs
├── UpgradeDefinition.cs       <-- ScriptableObject for defining upgrades
├── UpgradeKeys.cs             <-- Constants for PlayerPrefs keys
└── Editor/                    <-- Optional custom editor tools (not included yet)
```

---

## 🧰 How It Works

### MVC Breakdown

| Component     | Purpose |
|---------------|---------|
| `UpgradeModel.cs`      | Stores levels, logic, and PlayerPrefs interaction |
| `UpgradeView.cs`       | Manages all UI elements (Text, Sliders, Buttons, Particles) |
| `UpgradeController.cs` | Handles input, triggers logic, updates UI and animations |
| `UpgradeDefinition.cs` | Describes each upgrade via ScriptableObject (name, formula, caps) |

---

## 🧪 How to Set Up in Your Project

### ✅ Prerequisites

- Unity 2021+ (but compatible with older versions)
- [DOTween](http://dotween.demigiant.com/) (for animations)

### 1. Create Upgrade Definitions

1. Right-click in your **Project panel** → `Create > Upgrade System > Upgrade Definition`
2. Fill in fields like:
   - `UpgradeKey`: unique string (e.g., `"TimeBankLevel"`)
   - `DisplayName`: shown in UI
   - `BaseValue`: starting value
   - `IncrementPerLevel`: how much each level adds
   - `MaxLevel`: maximum allowed
   - `IsPercentage`: display with `%`
   - `UnitSuffix`: `"s"`, `"%"`, `"pts"`, etc.

### 2. Design the UI

For each upgrade:
- A `Text` for current value
- A `Text` for level display
- A `Button` to upgrade
- A `Slider` for visual feedback

Also add:
- A `Text` for Skill Points
- A `ParticleSystem` for success feedback
- An error panel with `Text` for warnings
- A `Reset` button (optional)

### 3. Set Up the `UpgradeView`

1. Attach `UpgradeView.cs` to a UI GameObject
2. In the Inspector, fill the **Upgrade Items array** (one per upgrade)
3. Drag in:
   - `UpgradeDefinition`
   - Value Text
   - Level Text
   - Button
   - Slider
4. Fill in other fields (Skill Points Text, Particles, Error UI)

### 4. Initialize the Controller

In any initializer script (e.g., `GameManager`):

```csharp
public class GameManager : MonoBehaviour
{
    [SerializeField] private UpgradeView upgradeView;

    private UpgradeModel model;
    private UpgradeController controller;

    void Start()
    {
        model = new UpgradeModel();
        controller = new UpgradeController(model, upgradeView);
    }
}
```

---

## 🔁 Upgrade Flow

```
Player clicks "Upgrade" button →
  Controller calls Model.TryUpgrade() →
    Model checks skill points & level limits →
      If success:
        - Level is increased
        - Skill points reduced
        - UI animation played
      If fail:
        - Error message displayed
```

---

## 🔧 Example Upgrade

**Speed Upgrade Definition:**
| Field              | Value           |
|--------------------|-----------------|
| UpgradeKey         | `"SpeedLevel"`  |
| DisplayName        | `"Speed"`       |
| BaseValue          | `1.0`           |
| IncrementPerLevel  | `0.1`           |
| MaxLevel           | `50`            |
| IsPercentage       | `false`         |
| UnitSuffix         | `"x"`           |

With Level 5 → Value = `1.0 + 0.1 * 5 = 1.5x`

---

## 🧼 Reset All Upgrades

- Resets all upgrade levels to 0
- Refunds all used skill points back
- Useful for testing, debugging, or respec systems

---

## 🧠 Notes & Tips

- You can store definitions in a `Resources` folder and load dynamically
- Combine with Unity's **Localization System** to support multiple languages
- `SkillPoints` can be controlled from other systems (e.g., achievements, level-up)

---

## 📦 Dependencies

- `DOTween` (Animation Tweening)
  - [Install via Package Manager or Asset Store](http://dotween.demigiant.com/download.php)

---

## 📝 License

This upgrade system is open for use in any **personal** or **commercial** Unity project. Attribution appreciated but not required.

---

## 🤝 Contributing

If you'd like to contribute improvements (e.g., adding saving via `ScriptableObject`, Undo support, or UI prefab templates), feel free to fork and submit a PR.

---

## 📷 Preview (optional)

> Add screenshot of the UI in action here

---

## ✉️ Support

If you find bugs or need help adapting this system, feel free to open an issue on GitHub or reach out via Discussions.
