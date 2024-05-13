using UnityEngine;

/// Reference: https://forum.unity.com/threads/canvashelper-resizes-a-recttransform-to-iphone-xs-safe-area.521107
/// Reference: https://forum.unity.com/threads/canvashelper-resizes-a-recttransform-to-iphone-xs-safe-area.521107/page-2#post-5852500
/// Reference: https://forum.unity.com/threads/canvashelper-resizes-a-recttransform-to-iphone-xs-safe-area.521107/page-2#post-9619478
/// for notch devices, add this script to Canvas
namespace MK.Common.Miscellaneous.SafeScreenArea
{
    [RequireComponent(typeof(Canvas))]
    public sealed class CanvasHelperCustom : MonoBehaviour
    {
        private bool screenChangeVarsInitialized = false;
        private ScreenOrientation lastOrientation = ScreenOrientation.LandscapeLeft;
        private Vector2 lastResolution = Vector2.zero;
        private Rect lastSafeArea = Rect.zero;

        private Canvas canvas;
        [SerializeField] private RectTransform safeAreaToTransform;

#if !UNITY_EDITOR
        private void Awake()
        {
            Reset();
        }

        private void OnEnable()
        {
            if (!screenChangeVarsInitialized)
            {
                lastOrientation = Screen.orientation;
                lastResolution.x = Screen.width;
                lastResolution.y = Screen.height;
                lastSafeArea = Screen.safeArea;

                screenChangeVarsInitialized = true;
            }

            ApplySafeArea();
        }

        private void Update()
        {
            if (Application.isMobilePlatform && Screen.orientation != lastOrientation)
                OrientationChanged();

            if (Screen.safeArea != lastSafeArea)
                SafeAreaChanged();

            if (Screen.width != lastResolution.x || Screen.height != lastResolution.y)
                ResolutionChanged();
        }

        private void ApplySafeArea()
        {
            if (safeAreaToTransform == null)
                return;

            var safeArea = Screen.safeArea;

            var anchorMin = safeArea.position;
            var anchorMax = safeArea.position + safeArea.size;
            anchorMin.x /= canvas.pixelRect.width;
            anchorMin.y /= canvas.pixelRect.height;
            anchorMax.x /= canvas.pixelRect.width;
            anchorMax.y /= canvas.pixelRect.height;

            safeAreaToTransform.anchorMin = anchorMin;
            safeAreaToTransform.anchorMax = anchorMax;
        }

        private void OrientationChanged()
        {
            //Debug.Log("Orientation changed from " + lastOrientation + " to " + Screen.orientation + " at " + Time.time);

            lastOrientation = Screen.orientation;
            lastResolution.x = Screen.width;
            lastResolution.y = Screen.height;
        }

        private void ResolutionChanged()
        {
            //Debug.Log("Resolution changed from " + lastResolution + " to (" + Screen.width + ", " + Screen.height + ") at " + Time.time);

            lastResolution.x = Screen.width;
            lastResolution.y = Screen.height;
        }

        private void SafeAreaChanged()
        {
            // Debug.Log("Safe Area changed from " + lastSafeArea + " to " + Screen.safeArea.size + " at " + Time.time);

            lastSafeArea = Screen.safeArea;

            ApplySafeArea();
        }

        [ContextMenu("Apply Safe Area Change")]
        private void ApplySafeAreaChange()
        {
            ApplySafeArea();
        }

#endif

        // runs only in editor automatically, when you apply this script
        private void Reset()
        {
            if (canvas == null)
                canvas = GetComponent<Canvas>();
        }
    }
}
