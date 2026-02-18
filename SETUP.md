# Setup Guide

## Quick Setup (Editor Menu)

1. Open your Unity 6 project (6000.0.67f1+)
2. Copy the `Assets/Scripts/` and `Assets/Editor/` folders into your project
3. Go to **GameObject → UI → Weapon Wheel** in the menu bar
4. A complete weapon wheel UI will be created on your Canvas
5. Hit **Play** and hold **Tab** to test

## Manual Setup

If you prefer to set things up manually:

1. Copy `Assets/Scripts/WeaponWheel.cs` and `Assets/Scripts/WeaponManager.cs` into your project
2. Create a **Canvas** (GameObject → UI → Canvas) if you don't have one
3. Create an empty GameObject under the Canvas called "WeaponWheel"
   - Add a `RectTransform` centered at (0, 0), size 400×400
   - Add the `WeaponWheel` component
4. Create 6 child GameObjects, each with:
   - An `Image` component using any built-in sprite (e.g., Knob)
   - Positioned in a circle (radius ~130px, spaced 60° apart)
5. Assign all 6 Images to the `WeaponWheel.segments` array (in clockwise order starting from top-right)
6. Assign your `WeaponManager` (or the dummy one) to the `WeaponWheel.weaponManager` field

## Integration with Your Game

Replace the dummy `WeaponManager` with your own script that has:

```csharp
public void SwitchWeapon(int index)
{
    // Your weapon switching logic here
}
```

Then drag your script onto the `weaponManager` field in the WeaponWheel inspector.

## Controls

| Input | Action |
|-------|--------|
| Hold **Tab** | Open weapon wheel |
| Move mouse | Highlight segment |
| Release **Tab** | Select highlighted weapon |

## Customization

In the `WeaponWheel` inspector:
- **Normal Color** — default segment color (gray)
- **Hover Color** — highlighted segment color (yellow)
- **Open Key** — key to hold (default: Tab)
