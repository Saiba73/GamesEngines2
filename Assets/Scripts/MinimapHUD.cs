using UnityEngine;

public class MinimapHUD : MonoBehaviour
{
    [System.Serializable]
    public class Minimap
    {
        [Tooltip("Top-down orthographic camera that renders the track into a RenderTexture.")]
        public Camera minimapCamera;

        [Tooltip("The RawImage (or Image) RectTransform that shows the map.")]
        public RectTransform mapPanel;

        [Tooltip("One icon per player. Must be children of the map panel, in the same order as 'players'.")]
        public RectTransform[] icons;

        [Tooltip("Rotate icons to match the direction each car is facing (use an arrow sprite pointing up).")]
        public bool rotateIcons = true;

        [Tooltip("Keep icons inside the map if a car leaves the camera view.")]
        public bool clampToMap = true;
    }

    [Header("Shared References")]
    [Tooltip("The car objects to track (the ones with the Rigidbody / driving script). Same order for every minimap.")]
    [SerializeField] private Transform[] players;

    [Header("Minimaps")]
    [Tooltip("One entry per minimap. Each has its own camera, UI panel, and icon set.")]
    [SerializeField] private Minimap[] minimaps;

    private void Awake()
    {
        if (minimaps == null) return;

        foreach (Minimap map in minimaps)
        {
            if (map?.icons == null) continue;

            foreach (RectTransform icon in map.icons)
            {
                if (icon == null) continue;
                icon.anchorMin = icon.anchorMax = new Vector2(0.5f, 0.5f);
            }
        }
    }

    private void LateUpdate()
    {
        if (players == null || minimaps == null) return;

        foreach (Minimap map in minimaps)
        {
            if (map == null || map.minimapCamera == null || map.mapPanel == null) continue;
            if (map.icons == null) continue;

            int count = Mathf.Min(players.Length, map.icons.Length);
            Rect mapRect = map.mapPanel.rect;

            for (int i = 0; i < count; i++)
            {
                if (players[i] == null || map.icons[i] == null) continue;

                Vector3 vp = map.minimapCamera.WorldToViewportPoint(players[i].position);

                if (map.clampToMap)
                {
                    vp.x = Mathf.Clamp01(vp.x);
                    vp.y = Mathf.Clamp01(vp.y);
                }

                map.icons[i].anchoredPosition = new Vector2(
                    (vp.x - 0.5f) * mapRect.width,
                    (vp.y - 0.5f) * mapRect.height);

                if (map.rotateIcons)
                {
                    Vector3 fwd = players[i].forward;
                    float x = Vector3.Dot(fwd, map.minimapCamera.transform.right);
                    float y = Vector3.Dot(fwd, map.minimapCamera.transform.up);
                    float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg - 90f;
                    map.icons[i].localRotation = Quaternion.Euler(0f, 0f, angle);
                }
            }
        }
    }
}