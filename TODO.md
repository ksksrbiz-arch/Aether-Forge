# AetherForge TODO & Roadmap

**Last Updated**: May 2026

## Core Vision
Voxel builder + factory automation (Minecraft creativity + Factorio logistics) with modular Kits. Full desktop + mobile support.

## Completed Features
- [x] Procedural terrain (Perlin + multi-layer voxels)
- [x] Voxel editing (break/place)
- [x] Full mobile support (virtual joystick + performance scaling)
- [x] Factory automation loop (ConveyorBelt + Inserter + Assembler)
- [x] KitDefinition system (recipes & unlocks)
- [x] Save/Load system (world + machines)
- [x] Research Tree (ResearchNode + ResearchManager + ResearchUI with unlock buttons)

## High-Priority Enhancements (Next Sprint)

### 1. Research Tree + Progression [COMPLETE]
- [x] ResearchNode ScriptableObject
- [x] ResearchManager (points, unlocks, rewards from crafting)
- [x] ResearchUI (visual tree with buttons and prerequisites)
- [ ] Integrate with KitDefinition for unlockable kits

### 2. Kit Blueprints & Sharing
- Save factory layouts as reusable Blueprints
- One-click placement of entire factory sections
- Simple share code system for blueprints

### 3. Fluids & Pipes System
- New fluid resources (Oil, Water, Acid)
- Pipe networks connecting machines
- New machines: Pump, Refinery, Chemical Plant

### 4. Mobile Polish
- Gesture-based building (pinch zoom, two-finger rotate)
- Haptic feedback on placement and crafting
- Offline progression (machines run while app closed)

### 5. Visual & Audio Upgrade
- Greedy meshing + smooth lighting for voxels
- Machine animations and particle effects
- Satisfying sound design (clunks, hums, dings)

## Medium-Priority Ideas
- Multiplayer co-op (shared worlds)
- Dynamic world events (meteor showers, weather)
- Procedural ruins with scavengeable tech
- Power & energy system (generators, power poles)
- Challenge modes and story campaign

## Technical Improvements
- Full DOTS conversion for factory simulation (massive scale)
- Better chunk loading / streaming
- Mod support foundation
- WebGL optimization for GitHub Pages demo

## Long-Term Vision
- Mobile-first release (Android/iOS)
- Steam + mobile store launch
- Community blueprint sharing platform

**Current Focus**: Integrate Research Tree with KitDefinition + start Kit Blueprints