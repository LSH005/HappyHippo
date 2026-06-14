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

        /// <summary> 수직 동기화 토글 시 프레임 제한 설정 변경 </summary>
        /// <param name="toggle">켜지면 <see langword="true"/>, 아니면 <see langword="false"/>.</param>
        public void vSyncToggleSwitch(bool toggle) => SetFrameLimit(toggle ? FrameLimitLevel.VSync : lastLevel);

        /// <summary> 프레임 제한 레벨 설정 및 모든 버튼 갱신 </summary>
        /// <param name="limit">적용할 <see cref="FrameLimitLevel"/>.</param>
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