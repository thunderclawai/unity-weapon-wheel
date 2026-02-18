using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

/// <summary>
/// Editor utility to create a Weapon Wheel UI hierarchy via the menu.
/// GameObject > UI > Weapon Wheel
/// </summary>
public class WeaponWheelSetup
{
    [MenuItem("GameObject/UI/Weapon Wheel", false, 10)]
    public static void CreateWeaponWheel()
    {
        // Find or create Canvas
        Canvas canvas = Object.FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasGO = new GameObject("Canvas");
            canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
        }

        // Create wheel root
        GameObject wheelRoot = new GameObject("WeaponWheel");
        wheelRoot.transform.SetParent(canvas.transform, false);
        RectTransform wheelRect = wheelRoot.AddComponent<RectTransform>();
        wheelRect.anchoredPosition = Vector2.zero;
        wheelRect.sizeDelta = new Vector2(400, 400);

        // Add WeaponWheel script
        WeaponWheel wheel = wheelRoot.AddComponent<WeaponWheel>();

        // Add WeaponManager to the same object (or find existing)
        WeaponManager manager = Object.FindFirstObjectByType<WeaponManager>();
        if (manager == null)
        {
            manager = wheelRoot.AddComponent<WeaponManager>();
        }
        wheel.weaponManager = manager;

        // Create 6 segments
        Image[] segments = new Image[6];
        float radius = 130f;
        float segmentSize = 80f;
        string[] labels = { "Slot 0", "Slot 1", "Slot 2", "Slot 3", "Slot 4", "Slot 5" };

        for (int i = 0; i < 6; i++)
        {
            // Angle: 0° = top, going clockwise, 60° per segment
            // Place segment at the center of its arc
            float angleDeg = i * 60f + 30f; // offset by 30° to center in arc
            float angleRad = (90f - angleDeg) * Mathf.Deg2Rad; // convert to Unity's coordinate system

            float x = Mathf.Cos(angleRad) * radius;
            float y = Mathf.Sin(angleRad) * radius;

            GameObject segGO = new GameObject(labels[i]);
            segGO.transform.SetParent(wheelRoot.transform, false);

            RectTransform segRect = segGO.AddComponent<RectTransform>();
            segRect.anchoredPosition = new Vector2(x, y);
            segRect.sizeDelta = new Vector2(segmentSize, segmentSize);

            Image img = segGO.AddComponent<Image>();
            img.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd");
            img.color = new Color(0.3f, 0.3f, 0.3f, 0.8f);
            segments[i] = img;

            // Add label
            GameObject labelGO = new GameObject("Label");
            labelGO.transform.SetParent(segGO.transform, false);
            RectTransform labelRect = labelGO.AddComponent<RectTransform>();
            labelRect.anchoredPosition = Vector2.zero;
            labelRect.sizeDelta = new Vector2(segmentSize, 20f);
            Text text = labelGO.AddComponent<Text>();
            text.text = $"W{i}";
            text.alignment = TextAnchor.MiddleCenter;
            text.fontSize = 14;
            text.color = Color.white;
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        }

        wheel.segments = segments;

        // Select the new object
        Selection.activeGameObject = wheelRoot;
        Undo.RegisterCreatedObjectUndo(wheelRoot, "Create Weapon Wheel");

        Debug.Log("[WeaponWheelSetup] Weapon Wheel created! Hold Tab to open in Play mode.");
    }
}
