using UnityEngine;

public class MainMenuButtonManager : MonoBehaviour
{
    public void GoToSettingScene()
    {
        SceneTransition.Instance.TransitionToScene("02_Setting");
    }

    public void GoToQuestScene()
    {
        // 03_Quest 씬으로 이동하는 로직
    }

    public void GoToHuntingGroundScene()
    {
        // 04_HuntingGround 씬으로 이동하는 로직
    }

    public void GoToCharacterScene()
    {
        // 05_Character 씬으로 이동하는 로직
    }

    public void GoToMenuScene()
    {
        // 06_Menu 씬으로 이동하는 로직
    }

    public void GoToTransactionScene()
    {
        // 07_Transaction 씬으로 이동하는 로직
    }
}
