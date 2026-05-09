# Aether-Forge

Voxel builder meets factory automation — Minecraft creativity + Factorio logistics.

## Quick Start

1. Clone this repository.
2. Open the project in **Unity 6** (recommended) or Unity 2022.3 LTS.
3. Open `Assets/_AetherForge/Scenes/WorldTest.unity` (or your active prototype scene).
4. Press Play.

## Architecture Overview

Aether-Forge is organized around two major gameplay domains:

- **Voxel World (`Assets/_AetherForge/World/`)**
  - Chunking, terrain generation, mining/editing, and world persistence.
- **Factory Automation (`Assets/_AetherForge/Factory/`)**
  - Belts, inserters, machines, power/logistics, and high-entity simulation.

Supporting systems are separated under:

- `Core/` for shared infrastructure (input, save/load, modding APIs)
- `Player/` for movement, inventory, crafting, survival loops
- `UI/` for HUD, research, and planning tools
- `Tests/` for EditMode and PlayMode coverage

## How to Add a New Machine / Kit

1. Create or update machine prefabs in:
   - `Assets/_AetherForge/Factory/Prefabs/`
2. Add machine and recipe data via ScriptableObjects in:
   - `Assets/_AetherForge/Factory/ScriptableObjects/`
3. Define or extend reusable kits in:
   - `Assets/_AetherForge/Factory/Kits/`
4. Wire machine behavior into factory simulation systems:
   - `Assets/_AetherForge/Factory/Systems/`
5. Validate in prototype scenes and add tests under:
   - `Assets/_AetherForge/Tests/`

## Contribution Guidelines

- Create focused branches (`feat/*`, `fix/*`, `chore/*`) for each change.
- Keep `main` stable and require passing CI checks for merges.
- Avoid committing Unity-generated folders (`Library/`, `Temp/`, etc.).
- Use Git LFS for large binary assets.

## Repository Specification

Project setup and governance details are documented in:

- [`/docs/repo-creation-spec.md`](docs/repo-creation-spec.md)
