# AetherForge

**Voxel Builder meets Factory Automation** — Minecraft creativity + Factorio logistics + modular Kits.

**Now with full mobile support!** Runs great on phones & tablets.

## 🚀 Current Progress
- [x] Foundation Core (voxel data, DOTS, Kits)
- [x] Voxel mesh + world editing
- [x] **Mobile foundation** (touch input + auto performance scaling)
- [x] **Procedural terrain generation** (Perlin heightmap + multi-layer voxels)
- [x] **Full virtual joystick UI** (left-screen drag movement + touch zones)
- [x] **First factory machine** (ConveyorBelt — placeable, moves items)
- [x] **Full automation loop** (Inserter + Assembler + recipe crafting from Kits)

## Quick Start (Desktop + Mobile)
1. Open in Unity 6
2. Add MobilePerformanceSettings + MobileInputManager to your scene
3. Play on desktop (mouse) or build to Android/iOS (touch)

## Mobile Optimizations
- 30 FPS target on mobile
- Reduced render scale & shadows
- Shared core logic (no code duplication)

**Next**: Polish, save/load, visuals, full kit placement UI, research tree

**Status**: Core playable loop complete — place terrain, use mobile controls, build automation chains with kits. Active development — PRs welcome!

---

**Milestone Achieved**: Full "place kit → watch factory run" fantasy now works. Conveyor → Inserter → Assembler chains possible. DOTS foundation ready for massive scale.