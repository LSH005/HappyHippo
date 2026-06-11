using UnityEngine;

namespace LeeSihyeon
{
    public class SettingExitButton : MonoBehaviour
    {
        public void SettingExit()
        {
            SceneTransition.Instance.TransitionToScene("01_Main");
        }
    }
}