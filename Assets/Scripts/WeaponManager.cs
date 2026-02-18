using UnityEngine;

/// <summary>
/// Dummy weapon manager. Replace with your actual weapon switching logic.
/// </summary>
public class WeaponManager : MonoBehaviour
{
    public void SwitchWeapon(int index)
    {
        Debug.Log($"[WeaponManager] Switched to weapon {index}");
    }
}
