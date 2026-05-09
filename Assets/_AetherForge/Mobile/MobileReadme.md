# Mobile Development

AetherForge is designed to run well on both desktop **and mobile** from day one.

## Current Mobile Status
- [x] Touch input foundation (MobileInputManager)
- [x] Automatic performance scaling (30 FPS target, reduced quality)
- [x] Cross-platform input abstraction in PlayerInteraction
- [x] Mobile detection helper
- [x] Virtual joystick movement + touch look support
- [x] On-screen break/place button hooks via MobileActionButton

## Mobile-Specific Challenges & Solutions
- **Performance**: Voxel worlds are heavy. We use DOTS + aggressive LOD + lower render scale on mobile.
- **Controls**: Virtual joystick + touch zones (expanding in next PR).
- **Memory**: Chunk streaming + entity culling critical on phones.

## Testing on Device
1. Switch platform to Android or iOS in Unity
2. Build & Run
3. The game should auto-apply mobile settings

## UI Wiring (for Joystick + Buttons)
1. Add a Canvas + EventSystem
2. Add a joystick background (`RectTransform`) and handle (`RectTransform`)
3. Assign them to `MobileInputManager.JoystickArea` and `MobileInputManager.JoystickHandle`
4. Add two UI buttons and put `MobileActionButton` on each:
   - Action = `Break` on one button
   - Action = `Place` on the other button

**Goal**: 30+ FPS on mid-range phones (e.g. Pixel 7, iPhone 13) with reduced world size.
