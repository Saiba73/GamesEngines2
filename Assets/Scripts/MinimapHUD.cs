using UnityEngine;

/// <summary>
/// Mario Kart style HUD minimap.
/// A top-down orthographic camera defines the map area. Each player's world
/// position is converted to a viewport point of that camera, which is then
/// mapped onto the UI map panel where the player's icon lives.
/// </summary>
public class MinimapHUD : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Top-down orthographic camera that renders the track into a RenderTexture.")]
    [SerializeField] private Camera minimapCamera;

    [Tooltip("The RawImage (or Image) RectTransform that shows the map.")]
    [SerializeField] private RectTransform mapPanel;

    [Tooltip("The car objects to track (the ones with the Rigidbody / driving script).")]
    [SerializeField] private Transform[] players;

    [Tooltip("One icon per player. Must be children of the map panel.")]
    [SerializeField] private RectTransform[] icons;

    [Header("Options")]
    [Tooltip("Rotate icons to match the direction each car is facing (use an arrow sprite pointing up).")]
    [SerializeField] private bool rotateIcons = true;

    [Tooltip("Keep icons inside the map if a car leaves the camera view.")]
    [SerializeField] private bool clampToMap = true;

    private void Awake()
    {
        // Icons are positioned relative to the center of the map panel.
        foreach (RectTransform icon in icons)
        {
            if (icon == null) continue;
            icon.anchorMin = icon.anchorMax = new Vector2(0.5f, 0.5f);
        }
    }

    private void LateUpdate()
    {
        if (minimapCamera == null || mapPanel == null) return;

        int count = Mathf.Min(players.Length, icons.Length);
        Rect map = mapPanel.rect;

        for (int i = 0; i < count; i++)
        {
            if (players[i] == null || icons[i] == null) continue;

            // 0..1 position of the car inside the minimap camera's view
            Vector3 vp = minimapCamera.WorldToViewportPoint(players[i].position);

            if (clampToMap)
            {
                vp.x = Mathf.Clamp01(vp.x);
                vp.y = Mathf.Clamp01(vp.y);
            }

            // Convert to UI position relative to the panel's center
            icons[i].anchoredPosition = new Vector2(
                (vp.x - 0.5f) * map.width,
                (vp.y - 0.5f) * map.height);

            if (rotateIcons)
            {
                // Car heading expressed in the minimap camera's screen axes
                Vector3 fwd = players[i].forward;
                float x = Vector3.Dot(fwd, minimapCamera.transform.right);
                float y = Vector3.Dot(fwd, minimapCamera.transform.up);
                float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg - 90f;
                icons[i].localRotation = Quaternion.Euler(0f, 0f, angle);
            }
        }
    }
}