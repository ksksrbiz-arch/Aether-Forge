# AetherForge

**Voxel Builder meets Factory Automation** — Minecraft creativity + Factorio logistics + modular Kits.

**Now with full mobile support!** Runs great on phones & tablets.

## 🚀 Current Progress
- [x] Foundation Core (voxel data, DOTS, Kits)
- [x] Voxel mesh + world editing
- [x] **Mobile foundation** (touch input + auto performance scaling)
- [x] DOTS inserter/machine/conveyor transfer loop + ScriptableObject recipes
- [x] Starter Automation Kit placement flow
- [x] Virtual joystick + mobile action buttons/hooks

## Quick Start (Desktop + Mobile)
1. Open in Unity 6
2. Add `MobilePerformanceSettings`, `MobileInputManager`, and `PlayerInteraction` to your scene
3. Add a Canvas with joystick + break/place buttons and wire to `MobileInputManager` / `MobileActionButton`
4. Use `StarterAutomationKitPlacer` with a `StarterAutomationKitDefinition` asset to place a prebuilt mini-factory
5. Play on desktop (mouse/keyboard) or build to Android/iOS (touch)

## Mobile Optimizations
- 30 FPS target on mobile
- Reduced render scale & shadows
- Shared core logic (no code duplication)

**Next**: Expand machine set, build first full factory demo scene, and add balancing

**Status**: Active cross-platform development — PRs welcome!
