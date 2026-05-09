using UnityEngine;

namespace AetherForge.Mobile
{
    /// <summary>
    /// Automatically applies mobile-optimized settings on startup.
    /// Critical for keeping voxel + factory simulation running on phones/tablets.
    /// </summary>
    public class MobilePerformanceSettings : MonoBehaviour
    {
        [Header("Mobile Optimizations")]
        public int TargetFrameRate = 30;
        public int RenderScale = 75;           // Lower resolution on mobile
        public bool EnableVSync = false;

        private void Awake()
        {
            if (!MobileInputManager.IsMobilePlatform()) return;

            Application.targetFrameRate = TargetFrameRate;
            QualitySettings.vSyncCount = EnableVSync ? 1 : 0;

            // Reduce quality for mobile
            QualitySettings.shadowDistance = 20f;
            QualitySettings.lodBias = 0.5f;

            // TODO: Add URP render scale via ScriptableRenderer
            Debug.Log("[AetherForge] Mobile performance settings applied");
        }
    }
}