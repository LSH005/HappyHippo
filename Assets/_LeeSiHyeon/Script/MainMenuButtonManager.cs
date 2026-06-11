using UnityEngine;

public class MainMenuButtonManager : MonoBehaviour
{
    public void GoToMainScene() => SceneTransition.Instance.TransitionToScene("01_Main");

    public void GoToSettingScene() => SceneTransition.Instance.TransitionToScene("02_Setting");

    public void GoToQuestScene() => SceneTransition.Instance.TransitionToScene("03_Quest");

    public void GoToHuntingGroundScene() => SceneTransition.Instance.TransitionToScene("04_HuntingGround");

    public void GoToCharacterScene() => SceneTransition.Instance.TransitionToScene("05_Character");

    public void GoToMenuScene() => SceneTransition.Instance.TransitionToScene("06_Menu");

    public void GoToTransactionScene() => SceneTransition.Instance.TransitionToScene("07_Transaction");

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
