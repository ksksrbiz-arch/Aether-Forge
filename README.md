# AetherForge

**Voxel Builder meets Factory Automation** — Minecraft creativity + Factorio logistics + modular Kits.

**Now with full mobile support!** Runs great on phones & tablets.

## 🚀 Current Progress
- [x] Foundation Core (voxel data, DOTS, Kits)
- [x] Voxel mesh + world editing
- [x] **Mobile foundation** (touch input + auto performance scaling)
- [x] **Procedural terrain generation** (Perlin heightmap + multi-layer voxels: grass/dirt/stone/ore/bedrock, mobile-optimized)
- [x] **Full virtual joystick UI** (left-screen drag movement, right touch zones for break/place, sensitivity/deadzone)

## Quick Start (Desktop + Mobile)
1. Open in Unity 6
2. Add MobilePerformanceSettings + MobileInputManager to your scene
3. Play on desktop (mouse) or build to Android/iOS (touch)

## Mobile Optimizations
- 30 FPS target on mobile
- Reduced render scale & shadows
- Shared core logic (no code duplication)

**Next**: First factory machine (conveyor/assembler DOTS integration) + kit placement on terrain

**Status**: Active cross-platform development — PRs welcome!

---

**Latest PR**: #7 — Procedural terrain + virtual joystick (merged to main)