using UnityEngine;

namespace LeeSihyeon
{
    public class MainMenuButtonManager : MonoBehaviour
    {
        /// <summary> <see cref="SceneTransition.TransitionToScene"/>으로 01_Main Scene으로 이동 </summary>
        public void GoToMainScene() => SceneTransition.Instance.TransitionToScene("01_Main");

        /// <summary> <see cref="SceneTransition.TransitionToScene"/>으로 02_Setting Scene으로 이동 </summary>
        public void GoToSettingScene() => SceneTransition.Instance.TransitionToScene("02_Setting");

        /// <summary> <see cref="SceneTransition.TransitionToScene"/>으로 03_Quest Scene으로 이동 </summary>
        public void GoToQuestScene() => SceneTransition.Instance.TransitionToScene("03_Quest");

        /// <summary> <see cref="SceneTransition.TransitionToScene"/>으로 04_HuntingGround Scene으로 이동 </summary>
        public void GoToHuntingGroundScene() => SceneTransition.Instance.TransitionToScene("04_HuntingGround");

        /// <summary> <see cref="SceneTransition.TransitionToScene"/>으로 05_Character Scene으로 이동 </summary>
        public void GoToCharacterScene() => SceneTransition.Instance.TransitionToScene("05_Character");

        /// <summary> <see cref="SceneTransition.TransitionToScene"/>으로 06_Menu Scene으로 이동 </summary>
        public void GoToMenuScene() => SceneTransition.Instance.TransitionToScene("06_Menu");

        /// <summary> <see cref="SceneTransition.TransitionToScene"/>으로 07_Transaction Scene으로 이동 </summary>
        public void GoToTransactionScene() => SceneTransition.Instance.TransitionToScene("07_Transaction");

        /// <summary> 게임 종료 또는 에디터 플레이 중지 </summary>
        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
        }
    }
}