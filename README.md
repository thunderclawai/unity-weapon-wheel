# Unity Weapon Wheel

Radial weapon wheel UI prototype for Unity 6 (6000.0.67f1).

Built entirely with Unity primitive UI sprites — no custom art needed. Drop it into your project and connect it to your weapon system.

## Features
- 6-segment radial menu
- Hover detection (angle-based)
- Hold Tab to open, release to select
- Calls `SwitchWeapon(int index)` on close
- Keyboard + mouse only

## Usage
1. Open in Unity 6000.0.67f1+
2. Open the `WeaponWheel` scene
3. Play — hold Tab to open the wheel, hover a segment, release to select

## Integration
The `WeaponWheel` script calls `SwitchWeapon(int index)` on a referenced `WeaponManager`. Replace the dummy `WeaponManager` with your own weapon script that has the same method signature.

## License
MIT
