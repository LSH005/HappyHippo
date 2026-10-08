using LeeSihyeon;
using TMPro;
using UnityEngine;

namespace JeongHyerin
{
    /// <summary>
    /// 05_Character가 guid 0d0546a940fd75f4ba2f0a3ddf0b9eee 로 이 클래스를 참조하지만 원본 파일은 없었다.
    /// 씬에 저장된 필드는 유지하고, 메인으로 돌아가는 LoadMainScene만 복구했다.
    /// OnClickIcon, PlayerTitlePanelOpen, PlayerTitlePanelClose의 원래 구현은 없다.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        public GameObject playerTitlePanel;
        public StatEntry[] playerStats;
        public GameObject darknessObject;

        [System.Serializable]
        public class StatEntry
        {
            public string statName;
            public TMP_Text statText;
            public int currentValue;
        }

        public void LoadMainScene()
        {
            if (SceneTransition.Instance == null)
            {
                Debug.LogError("SceneTransition 인스턴스가 없습니다.");
                return;
            }

            SceneTransition.Instance.TransitionToScene("01_Main");
        }

        public void OnClickIcon() { }

        public void PlayerTitlePanelOpen() { }

        public void PlayerTitlePanelClose() { }
    }
}
