using UnityEngine;

namespace LeeHyunWoo
{
    public class BackToMainMenu : MonoBehaviour
    {
        public void BackToMainMenuScene() => SceneTransition.Instance.TransitionToScene("01_Main");

    }
}