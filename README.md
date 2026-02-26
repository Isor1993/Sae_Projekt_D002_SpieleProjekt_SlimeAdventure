# 📘 README

### SAE Institute Stuttgart

**Module:** 4FSC0PD002 -- Game Programming (K2 / S2 / S3)\
**Student:** Eric Rosenberg\
**Project:** Isor's Slime Adventure (Unity)

------------------------------------------------------------------------

## 1. Base Module

This submission was created by **Eric Rosenberg** for the module\
**4FSC0PD002 -- Game Programming (K2 / S2 / S3)** at **SAE Institute
Stuttgart**.

The project **"Isor's Slime Adventure"** was developed using the **Unity
Game Engine** as a **2D gameplay prototype**.\
The focus lies on a **modular, physics-based character controller**, an
**element & skill system**, and a clearly structured gameplay
architecture.

All gameplay-relevant values are configurable via ScriptableObjects and
Inspector settings without modifying core logic.

------------------------------------------------------------------------

## 2. Missing Submission

*(not applicable -- all required components included)*

------------------------------------------------------------------------

## 3. Multiple Submissions in One Folder

*(not applicable -- single project)*

------------------------------------------------------------------------

## 4. Group Work

*(not applicable -- individual project by Eric Rosenberg)*

------------------------------------------------------------------------

## 5. Feature Description

### 🎮 Player Control -- Core Features (2D)

-   horizontal 2D movement\
-   physics-based movement using **Rigidbody2D**\
-   walking and sprinting\
-   ground-based jumping\
-   ground-check & wall-check system\
-   mouse & controller support\
-   fully configurable via Unity Inspector

------------------------------------------------------------------------

### 🧠 Movement & Jump System

**MoveBehaviour** - configurable ground movement speed\
- sprint mechanic\
- direction switching\
- Rigidbody2D-based implementation

**JumpBehaviour** - ground-based jump\
- configurable jump force\
- state evaluation via GroundCheck

------------------------------------------------------------------------

### 🧱 Ground Check & Physics

**GroundCheck** - collision-based ground detection\
- LayerMask support\
- configurable offset

**WallCheck** - wall contact detection\
- base system for future extensions

------------------------------------------------------------------------

### 🔥 Element & Skill System

**PlayerSkillSystem** - manages active skills\
- automatic skill switching when changing element

**NormalShoot** - projectile with single damage instance

**FireShoot** - projectile with tick damage\
- unlocked after collecting 5 fire fragments

**SkillData (ScriptableObject)** - stores damage values\
- configurable parameters

------------------------------------------------------------------------

### 👾 Enemy & AI System

**Goblin** - HP management\
- patrol route\
- detection system\
- state switching (Patrol / Attack)

**Boss Goblin** - increased HP\
- unlocks portal on death

------------------------------------------------------------------------

### 🎒 Inventory & Collect System

**Inventory** - coin storage\
- element fragment storage\
- UI connection

**ICollectable (Interface)** - unified collection contract\
- extendable for new item types

------------------------------------------------------------------------

### 🌤️ Parallax & Environment System

**SkyController** - multi-layer parallax background\
- camera-based movement

**Cloud System** - autonomous movement\
- recycling for endless loop

**JumpFlower** - bounce mechanic

**Trap** - damage-dealing environmental object

------------------------------------------------------------------------

### 🎛️ Configuration

All gameplay-relevant values are configurable via:

-   ScriptableObjects (EnemyData, SkillData)\
-   Inspector fields\
-   adjustable parameters

Core logic remains untouched.

------------------------------------------------------------------------

## 🚀 Future Improvements

-   additional elements\
-   animations for player & goblins\
-   extended audio integration\
-   additional levels
