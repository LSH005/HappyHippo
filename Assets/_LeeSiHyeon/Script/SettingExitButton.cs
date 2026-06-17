using UnityEngine;

namespace LeeSihyeon
{
    public class SettingExitButton : MonoBehaviour
    {
        /// <summary> <see cref="SceneTransition.TransitionToScene"/>함수를 호출하여 "01_Main" Scene으로 이동 </summary>
        public void SettingExit()
        {
            SceneTransition.Instance?.TransitionToLastScene();
        }
    }
}