using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Radial weapon wheel with 6 segments. Uses angle-based hover detection.
/// Hold Tab to open, release to select the hovered weapon.
/// </summary>
public class WeaponWheel : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The 6 segment Images arranged radially. Index 0 = top-right, going clockwise.")]
    public Image[] segments = new Image[6];

    [Tooltip("The WeaponManager to call SwitchWeapon() on.")]
    public WeaponManager weaponManager;

    [Header("Colors")]
    public Color normalColor = new Color(0.3f, 0.3f, 0.3f, 0.8f);
    public Color hoverColor = new Color(1f, 0.85f, 0.2f, 0.9f);

    [Header("Input")]
    public KeyCode openKey = KeyCode.Tab;

    private RectTransform _wheelRect;
    private int _activeSegment = -1;
    private bool _isOpen;
    private CursorLockMode _previousLockMode;
    private bool _previousCursorVisible;

    private void Awake()
    {
        _wheelRect = GetComponent<RectTransform>();
        SetWheelVisible(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(openKey))
            OpenWheel();

        if (Input.GetKeyUp(openKey))
            CloseWheel();

        if (_isOpen)
            UpdateHover();
    }

    private void OpenWheel()
    {
        _isOpen = true;
        _previousLockMode = Cursor.lockState;
        _previousCursorVisible = Cursor.visible;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SetWheelVisible(true);
    }

    private void CloseWheel()
    {
        if (!_isOpen) return;

        _isOpen = false;

        if (_activeSegment >= 0 && weaponManager != null)
            weaponManager.SwitchWeapon(_activeSegment);

        _activeSegment = -1;
        Cursor.lockState = _previousLockMode;
        Cursor.visible = _previousCursorVisible;
        SetWheelVisible(false);
    }

    private void UpdateHover()
    {
        Vector2 center = _wheelRect.position;
        Vector2 mousePos = Input.mousePosition;
        Vector2 dir = mousePos - center;

        float distance = dir.magnitude;
        int newActive = -1;

        // Only detect if mouse is far enough from center (dead zone)
        if (distance > 30f)
        {
            // atan2 gives angle in radians, convert to degrees
            // Unity screen: right = 0°, up = 90°
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            // Normalize to 0-360, with 0° at top going clockwise
            // Shift so top (90°) becomes 0°, then invert for clockwise
            float adjusted = 90f - angle;
            if (adjusted < 0f) adjusted += 360f;

            // Each segment spans 60°
            newActive = Mathf.FloorToInt(adjusted / 60f) % 6;
        }

        if (newActive != _activeSegment)
        {
            // Reset previous
            if (_activeSegment >= 0 && _activeSegment < segments.Length)
                segments[_activeSegment].color = normalColor;

            _activeSegment = newActive;

            // Highlight new
            if (_activeSegment >= 0 && _activeSegment < segments.Length)
                segments[_activeSegment].color = hoverColor;
        }
    }

    private void SetWheelVisible(bool visible)
    {
        for (int i = 0; i < segments.Length; i++)
        {
            if (segments[i] != null)
                segments[i].gameObject.SetActive(visible);
        }
    }
}
