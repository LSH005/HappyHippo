using UnityEngine;

namespace LeeSihyeon
{
    public enum FrameLimitLevel { fps15, fps30, fps60, Unlimited, VSync }

    public class FrameLimitButtonManager : MonoBehaviour
    {
        public static FrameLimitButtonManager Instance { get; private set; }
        public FrameLimitButton[] allButtons;
        public ToggleSwitch vSyncToggle;

        FrameLimitLevel lastLevel;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            if (vSyncToggle != null) vSyncToggle.AddToggleListener(vSyncToggleSwitch);
        }

        private void Start() => SetFrameLimit(FrameLimitLevel.fps60);

        public void vSyncToggleSwitch(bool toggle) => SetFrameLimit(toggle ? FrameLimitLevel.VSync : lastLevel);

        public void SetFrameLimit(FrameLimitLevel limit)
        {
            foreach (var button in allButtons)
            {
                button.SetSelectedWithFrameLimit(limit);
            }

            if (vSyncToggle != null)
            {
                vSyncToggle.SetEnableWithNoCallback(limit == FrameLimitLevel.VSync);
            }

            if (limit != FrameLimitLevel.VSync) lastLevel = limit;
        }

        private void OnDestroy()
        {
            if (vSyncToggle != null) vSyncToggle.RemoveToggleListener(vSyncToggleSwitch);
        }
    }
}